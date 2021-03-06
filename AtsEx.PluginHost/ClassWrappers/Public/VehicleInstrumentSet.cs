using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.PluginHost.BveTypes;

namespace Automatic9045.AtsEx.PluginHost.ClassWrappers
{
    /// <summary>
    /// 自列車を構成する機器のセットを表します。
    /// </summary>
    public class VehicleInstrumentSet : ClassWrapperBase
    {
        [InitializeClassWrapper]
        private static void Initialize()
        {
            ClassMemberSet members = BveTypeSet.Instance.GetClassInfoOf<VehicleInstrumentSet>();

            CabGetMethod = members.GetSourcePropertyGetterOf(nameof(Cab));
            CabSetMethod = members.GetSourcePropertySetterOf(nameof(Cab));
        }

        protected VehicleInstrumentSet(object src) : base(src)
        {
        }

        /// <summary>
        /// オリジナル オブジェクトからラッパーのインスタンスを生成します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        /// <returns>オリジナル オブジェクトをラップした <see cref="VehicleInstrumentSet"/> クラスのインスタンス。</returns>
        [CreateClassWrapperFromSource]
        public static VehicleInstrumentSet FromSource(object src) => src is null ? null : new VehicleInstrumentSet(src);

        private static MethodInfo CabGetMethod;
        private static MethodInfo CabSetMethod;
        /// <summary>
        /// 運転台のハンドルを表す <see cref="CabBase"/> を取得します。
        /// </summary>
        public CabBase Cab
        {
            get => CabBase.FromSource(CabGetMethod.Invoke(Src, null));
            internal set => CabSetMethod.Invoke(Src, new object[] { value.Src });
        }
    }
}
