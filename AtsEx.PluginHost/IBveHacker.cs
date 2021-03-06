using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mackoy.Bvets;

using Automatic9045.AtsEx.PluginHost.ClassWrappers;
using Automatic9045.AtsEx.PluginHost.ExtendedBeacons;
using Automatic9045.AtsEx.PluginHost.Handles;

namespace Automatic9045.AtsEx.PluginHost
{
    public delegate void ScenarioCreatedEventHandler(ScenarioCreatedEventArgs e);

    public interface IBveHacker
    {
        /// <summary>
        /// 制御対象の BVE を実行している <see cref="System.Diagnostics.Process"/> を取得します。
        /// </summary>
        Process Process { get; }


        /// <summary>
        /// BVE のメインフォームのハンドルを取得します。
        /// </summary>
        IntPtr MainFormHandle { get; }

        /// <summary>
        /// BVE のメインフォームの <see cref="Form"/> インスタンスを取得します。
        /// </summary>
        Form MainFormSource { get; }

        /// <summary>
        /// BVE のメインフォームを取得します。
        /// </summary>
        MainForm MainForm { get; }


        /// <summary>
        /// BVE の「シナリオの選択」フォームの <see cref="Form"/> インスタンスを取得します。
        /// </summary>
        Form ScenarioSelectionFormSource { get; }

        /// <summary>
        /// BVE の「シナリオの選択」フォームを取得します。
        /// </summary>
        ScenarioSelectionForm ScenarioSelectionForm { get; }

        /// <summary>
        /// BVE の「シナリオを読み込んでいます...」フォームの <see cref="Form"/> インスタンスを取得します。
        /// </summary>
        Form LoadingProgressFormSource { get; }

        /// <summary>
        /// BVE の「シナリオを読み込んでいます...」フォームを取得します。
        /// </summary>
        LoadingProgressForm LoadingProgressForm { get; }

        /// <summary>
        /// BVE の「時刻と位置」フォームの <see cref="Form"/> インスタンスを取得します。
        /// </summary>
        Form TimePosFormSource { get; }

        /// <summary>
        /// BVE の「時刻と位置」フォームを取得します。
        /// </summary>
        TimePosForm TimePosForm { get; }

        /// <summary>
        /// BVE の「車両物理量」フォームの <see cref="Form"/> インスタンスを取得します。
        /// </summary>
        Form ChartFormSource { get; }

        /// <summary>
        /// BVE の「車両物理量」フォームを取得します。
        /// </summary>
        ChartForm ChartForm { get; }


        /// <summary>
        /// BVE の設定が格納された <see cref="Mackoy.Bvets.Preferences"/> を取得します。
        /// </summary>
        Preferences Preferences { get; }

        /// <summary>
        /// キー入力を管理する <see cref="ClassWrappers.KeyProvider"/> を取得します。
        /// </summary>
        KeyProvider KeyProvider { get; }


        /// <summary>
        /// 全てのハンドルのセットを取得します。
        /// </summary>
        /// <remarks>
        /// <see cref="IBveHacker"/> が利用できない場合は <see cref="IApp.Handles"/> プロパティを使用してください。
        /// ただし、<see cref="IApp.Handles"/> プロパティに設定されている値は力行ハンドルの抑速ノッチ、ブレーキハンドルの抑速ブレーキノッチを無視したものになります。
        /// </remarks>
        /// <seealso cref="IApp.Handles"/>
        Handles.HandleSet Handles { get; }

        /// <summary>
        /// 拡張地上子の一覧を取得します。
        /// </summary>
        IExtendedBeaconSet ExtendedBeacons { get; }


        /// <summary>
        /// <see cref="ScenarioCreated"/> が発生する直前に通知します。特に理由がなければ <see cref="ScenarioCreated"/> を使用してください。
        /// </summary>
        event ScenarioCreatedEventHandler PreviewScenarioCreated;

        /// <summary>
        /// <see cref="ClassWrappers.Scenario"/> のインスタンスが生成されたときに通知します。
        /// </summary>
        event ScenarioCreatedEventHandler ScenarioCreated;

        /// <summary>
        /// 現在読込中または実行中のシナリオの情報を取得・設定します。
        /// </summary>
        ScenarioInfo ScenarioInfo { get; set; }

        /// <summary>
        /// 現在実行中のシナリオを取得します。シナリオの読込中は <see cref="InvalidOperationException"/> をスローします。
        /// シナリオの読込中に <see cref="ClassWrappers.Scenario"/> を取得するには <see cref="ScenarioCreated"/> イベントを購読してください。
        /// </summary>
        Scenario Scenario { get; }

        /// <summary>
        /// <see cref="Scenario"/> が取得可能かどうかを取得します。
        /// </summary>
        bool IsScenarioCreated { get; }
    }
}
