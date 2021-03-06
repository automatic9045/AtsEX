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
    /// 車両パラメーターを表します。
    /// </summary>
    public class VehicleDynamics : ClassWrapperBase
    {
        [InitializeClassWrapper]
        private static void Initialize()
        {
            ClassMemberSet members = BveTypeSet.Instance.GetClassInfoOf<VehicleDynamics>();

            CurveResistanceFactorGetMethod = members.GetSourcePropertyGetterOf(nameof(CurveResistanceFactor));
            CurveResistanceFactorSetMethod = members.GetSourcePropertySetterOf(nameof(CurveResistanceFactor));

            RunningResistanceFactorAGetMethod = members.GetSourcePropertyGetterOf(nameof(RunningResistanceFactorA));
            RunningResistanceFactorASetMethod = members.GetSourcePropertySetterOf(nameof(RunningResistanceFactorA));

            RunningResistanceFactorBGetMethod = members.GetSourcePropertyGetterOf(nameof(RunningResistanceFactorB));
            RunningResistanceFactorBSetMethod = members.GetSourcePropertySetterOf(nameof(RunningResistanceFactorB));

            RunningResistanceFactorCGetMethod = members.GetSourcePropertyGetterOf(nameof(RunningResistanceFactorC));
            RunningResistanceFactorCSetMethod = members.GetSourcePropertySetterOf(nameof(RunningResistanceFactorC));
        }

        protected VehicleDynamics(object src) : base(src)
        {
        }

        /// <summary>
        /// オリジナル オブジェクトからラッパーのインスタンスを生成します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        /// <returns>オリジナル オブジェクトをラップした <see cref="VehicleParameterSet"/> クラスのインスタンス。</returns>
        [CreateClassWrapperFromSource]
        public static VehicleDynamics FromSource(object src) => src is null ? null : new VehicleDynamics(src);

        protected static MethodInfo CurveResistanceFactorGetMethod;
        protected static MethodInfo CurveResistanceFactorSetMethod;
        /// <summary>
        /// 曲線抵抗の係数を取得・設定します。
        /// </summary>
        public double CurveResistanceFactor
        {
            get => CurveResistanceFactorGetMethod.Invoke(Src, null);
            set => CurveResistanceFactorSetMethod.Invoke(Src, new object[] { value });
        }

        protected static MethodInfo RunningResistanceFactorAGetMethod;
        protected static MethodInfo RunningResistanceFactorASetMethod;
        /// <summary>
        /// 速度の単位を [m/s] としたときの走行抵抗の係数 a を取得・設定します。
        /// </summary>
        /// <remarks>
        /// 車両パラメーターファイルで定義する係数は速度の単位を [km/h] としたときのもののため、
        /// ここで取得・設定する値はその 3.6 ^ 2 = 12.96 倍となります。
        /// </remarks>
        public double RunningResistanceFactorA
        {
            get => RunningResistanceFactorAGetMethod.Invoke(Src, null);
            set => RunningResistanceFactorASetMethod.Invoke(Src, new object[] { value });
        }

        protected static MethodInfo RunningResistanceFactorBGetMethod;
        protected static MethodInfo RunningResistanceFactorBSetMethod;
        /// <summary>
        /// 速度の単位を [m/s] としたときの走行抵抗の係数 b を取得・設定します。
        /// </summary>
        /// <remarks>
        /// 車両パラメーターファイルで定義する係数は速度の単位を [km/h] としたときのもののため、
        /// ここで取得・設定する値はその 3.6 倍となります。
        /// </remarks>
        public double RunningResistanceFactorB
        {
            get => RunningResistanceFactorBGetMethod.Invoke(Src, null);
            set => RunningResistanceFactorBSetMethod.Invoke(Src, new object[] { value });
        }

        protected static MethodInfo RunningResistanceFactorCGetMethod;
        protected static MethodInfo RunningResistanceFactorCSetMethod;
        /// <summary>
        /// 走行抵抗の係数 c を取得・設定します。
        /// </summary>
        public double RunningResistanceFactorC
        {
            get => RunningResistanceFactorCGetMethod.Invoke(Src, null);
            set => RunningResistanceFactorCSetMethod.Invoke(Src, new object[] { value });
        }
    }
}
