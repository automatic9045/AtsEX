using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic9045.AtsEx.Plugins.Scripting
{
    internal interface IPluginScript<TGlobals> where TGlobals : Globals
    {
        IPluginScript<TGlobals> GetWithCheckErrors();
        IScriptResult Run(TGlobals globals);
    }

    internal interface IPluginScript<TResult, TGlobals> : IPluginScript<TGlobals> where TGlobals : Globals
    {
        new IPluginScript<TResult, TGlobals> GetWithCheckErrors();
        new IScriptResult<TResult> Run(TGlobals globals);
    }
}
