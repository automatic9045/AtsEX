using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.Plugins;
using Automatic9045.AtsEx.Plugins.Scripting.CSharp;
using Automatic9045.AtsEx.PluginHost;
using Automatic9045.AtsEx.PluginHost.ClassWrappers;
using Automatic9045.AtsEx.PluginHost.Plugins;

namespace Automatic9045.AtsEx
{
    internal class Map
    {
        protected const string NoMapPluginHeader = "[[nompi]]";
        protected const string MapPluginUsingHeader = "<mpiusing>";

        public List<PluginBase> LoadedPlugins { get; } = new List<PluginBase>();
        public List<LoadError> MapPluginUsingErrors { get; } = new List<LoadError>();

        protected Map(List<PluginBase> loadedPlugins, List<LoadError> mapPluginUsingErrors)
        {
            LoadedPlugins = loadedPlugins;
            MapPluginUsingErrors = mapPluginUsingErrors;
        }

        public static Map Load(string filePath, PluginLoader pluginLoader)
        {
            List<PluginBase> loadedPlugins = new List<PluginBase>();
            List<LoadError> mapPluginUsingErrors = new List<LoadError>();

            string fileName = Path.GetFileName(filePath);

            using (StreamReader sr = new StreamReader(filePath))
            {
                for (int i = 0; !sr.EndOfStream; i++)
                {
                    List<TextWithCharIndex> statements = GetStatementsFromLine(sr.ReadLine());
                    statements.ForEach(s =>
                    {
                        if (s.Text.StartsWith("include'") && s.Text.EndsWith("'") && s.Text.Length - s.Text.Replace("'", "").Length == 2)
                        {
                            string includePath = s.Text.Split('\'')[1];
                            if (includePath.StartsWith(MapPluginUsingHeader))
                            {
                                string mapPluginUsingRelativePath = includePath.Substring(MapPluginUsingHeader.Length);
                                string mapPluginUsingAbsolutePath = Path.Combine(Path.GetDirectoryName(filePath), mapPluginUsingRelativePath);

                                try
                                {
                                    PluginUsing mapPluginUsing = PluginUsing.Load(PluginType.MapPlugin, mapPluginUsingAbsolutePath);
                                    IEnumerable<PluginBase> loadedMapPlugins = pluginLoader.LoadFromPluginUsing(mapPluginUsing);
                                    loadedPlugins.AddRange(loadedMapPlugins);
                                }
                                catch (CompilationException ex)
                                {
                                    ex.ThrowAsLoadError();
                                }

                                mapPluginUsingErrors.Add(new LoadError(null, fileName, i + 1, s.CharIndex + 1));
                            }
                            else if (includePath.StartsWith(NoMapPluginHeader))
                            {
                                mapPluginUsingErrors.Add(new LoadError(null, fileName, i + 1, s.CharIndex + 1));
                            }
                            else
                            {
                                string includeRelativePath = includePath;
                                string includeAbsolutePath = Path.Combine(Path.GetDirectoryName(filePath), includeRelativePath);
                                
                                Map includedMap = Load(includeAbsolutePath, pluginLoader);

                                loadedPlugins.AddRange(includedMap.LoadedPlugins);
                                mapPluginUsingErrors.AddRange(includedMap.MapPluginUsingErrors);
                            }
                        }
                    });
                }
            }

            return new Map(loadedPlugins, mapPluginUsingErrors);
        }

        protected static List<TextWithCharIndex> GetStatementsFromLine(string line)
        {
            string trimmedLine = line.ToLower();

            List<TextWithCharIndex> statements = new List<TextWithCharIndex>();

            {
                bool isInString = false;
                int lastStatementEndIndex = -1;
                int notTrimmedLastStatementEndIndex = trimmedLine.Length - trimmedLine.TrimStart().Length - 1;

                int i = 0;
                int n = 0;
                while (i < trimmedLine.Length)
                {
                    switch (trimmedLine[i])
                    {
                        case '/':
                            if (!isInString && i + 1 < trimmedLine.Length && trimmedLine[i + 1] == '/') return statements;
                            break;

                        case '#':
                            return statements;

                        case '\'':
                            isInString = !isInString;
                            break;

                        case ' ':
                        case '\t':
                            if (!isInString)
                            {
                                trimmedLine = trimmedLine.Remove(i, 1);
                                i--;
                            }
                            break;

                        case ';':
                            if (!isInString)
                            {
                                trimmedLine = trimmedLine.Remove(i, 1);
                                i--;
                                if (i != lastStatementEndIndex)
                                {
                                    string statement = trimmedLine.Substring(lastStatementEndIndex + 1, i - lastStatementEndIndex);
                                    string notTrimmedStatement = line.Substring(notTrimmedLastStatementEndIndex + 1, n - notTrimmedLastStatementEndIndex);
                                    int headSpaceCount = notTrimmedStatement.Length - notTrimmedStatement.TrimStart().Length;
                                    statements.Add(new TextWithCharIndex(notTrimmedLastStatementEndIndex + headSpaceCount + 1, statement));
                                }

                                lastStatementEndIndex = i;
                                notTrimmedLastStatementEndIndex = n;
                            }
                            break;
                    }

                    i++;
                    n++;
                }
            }

            return statements;
        }

        protected class TextWithCharIndex
        {
            public int CharIndex { get; }
            public string Text { get; }

            public TextWithCharIndex(int charIndex, string text)
            {
                CharIndex = charIndex;
                Text = text;
            }
        }
    }
}
