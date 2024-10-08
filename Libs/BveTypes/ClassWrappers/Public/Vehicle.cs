﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX.DirectSound;

using FastMember;
using TypeWrapping;

namespace BveTypes.ClassWrappers
{
    /// <summary>
    /// 自列車を表します。
    /// </summary>
    public class Vehicle : ClassWrapperBase
    {
        [InitializeClassWrapper]
        private static void Initialize(BveTypeSet bveTypes)
        {
            ClassMemberSet members = bveTypes.GetClassInfoOf<Vehicle>();

            Constructor = members.GetSourceConstructor();

            InstrumentsGetMethod = members.GetSourcePropertyGetterOf(nameof(Instruments));
            InstrumentsSetMethod = members.GetSourcePropertySetterOf(nameof(Instruments));

            VibrationManagerGetMethod = members.GetSourcePropertyGetterOf(nameof(VibrationManager));
            VibrationManagerSetMethod = members.GetSourcePropertySetterOf(nameof(VibrationManager));

            PanelGetMethod = members.GetSourcePropertyGetterOf(nameof(Panel));

            ConductorGetMethod = members.GetSourcePropertyGetterOf(nameof(Conductor));

            DoorsGetMethod = members.GetSourcePropertyGetterOf(nameof(Doors));
            DoorsSetMethod = members.GetSourcePropertySetterOf(nameof(Doors));

            DynamicsGetMethod = members.GetSourcePropertyGetterOf(nameof(Dynamics));
            DynamicsSetMethod = members.GetSourcePropertySetterOf(nameof(Dynamics));

            PassengerGetMethod = members.GetSourcePropertyGetterOf(nameof(Passenger));
            PassengerSetMethod = members.GetSourcePropertySetterOf(nameof(Passenger));

            CameraLocationField = members.GetSourceFieldOf(nameof(CameraLocation));

            InitializeMethod = members.GetSourceMethodOf(nameof(Initialize));
        }

        /// <summary>
        /// オリジナル オブジェクトから <see cref="Vehicle"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        protected Vehicle(object src) : base(src)
        {
        }

        /// <summary>
        /// オリジナル オブジェクトからラッパーのインスタンスを生成します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        /// <returns>オリジナル オブジェクトをラップした <see cref="Vehicle"/> クラスのインスタンス。</returns>
        [CreateClassWrapperFromSource]
        public static Vehicle FromSource(object src) => src is null ? null : new Vehicle(src);

        private static FastConstructor Constructor;
        /// <summary>
        /// <see cref="Vehicle"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="assistantDrawer">補助表示を描画するための <see cref="AssistantDrawer"/>。</param>
        /// <param name="directSound">DirectSound デバイス。</param>
        /// <param name="keyProvider">キー入力を管理するための <see cref="KeyProvider"/>。</param>
        /// <param name="timeManager">時間に関する処理を行う <see cref="TimeManager"/>。</param>
        /// <param name="locationManager">自列車の位置情報に関する処理を行う <see cref="UserVehicleLocationManager"/>。</param>
        /// <param name="cameraLocation">カメラの位置に関する情報を提供する <see cref="CameraLocation"/>。</param>
        /// <param name="route">使用するマップ。</param>
        /// <param name="sectionManager">閉そくを制御するための <see cref="SectionManager" />。</param>
        public Vehicle(AssistantDrawer assistantDrawer, DirectSound directSound, KeyProvider keyProvider, TimeManager timeManager, UserVehicleLocationManager locationManager, CameraLocation cameraLocation, Route route, SectionManager sectionManager)
            : this(Constructor.Invoke(new object[] { assistantDrawer.Src, directSound, keyProvider.Src, timeManager.Src, locationManager.Src, cameraLocation.Src, route.Src, sectionManager.Src }))
        {
        }

        private static FastMethod InstrumentsGetMethod;
        private static FastMethod InstrumentsSetMethod;
        /// <summary>
        /// 自列車を構成する機器を表す <see cref="VehicleInstrumentSet"/> を取得・設定します。
        /// </summary>
        public VehicleInstrumentSet Instruments
        {
            get => VehicleInstrumentSet.FromSource(InstrumentsGetMethod.Invoke(Src, null));
            set => InstrumentsSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static FastMethod VibrationManagerGetMethod;
        private static FastMethod VibrationManagerSetMethod;
        /// <summary>
        /// 自列車の揺れを制御する <see cref="VehicleVibrationManager"/> を取得・設定します。
        /// </summary>
        public VehicleVibrationManager VibrationManager
        {
            get => VehicleVibrationManager.FromSource(VibrationManagerGetMethod.Invoke(Src, null));
            set => VibrationManagerSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static FastMethod PanelGetMethod;
        /// <summary>
        /// 自列車の運転台パネルを取得・設定します。
        /// </summary>
        public VehiclePanel Panel => VehiclePanel.FromSource(PanelGetMethod.Invoke(Src, null));

        private static FastMethod ConductorGetMethod;
        /// <summary>
        /// 車掌を表す <see cref="Conductor"/> を取得します。
        /// </summary>
        public Conductor Conductor => ClassWrappers.Conductor.FromSource(ConductorGetMethod.Invoke(Src, null));

        private static FastMethod DoorsGetMethod;
        private static FastMethod DoorsSetMethod;
        /// <summary>
        /// 自列車のドアのセットを取得・設定します。
        /// </summary>
        public DoorSet Doors
        {
            get => DoorSet.FromSource(DoorsGetMethod.Invoke(Src, null));
            set => DoorsSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static FastMethod DynamicsGetMethod;
        private static FastMethod DynamicsSetMethod;
        /// <summary>
        /// 曲線抵抗の係数を取得・設定します。
        /// </summary>
        public VehicleDynamics Dynamics
        {
            get => VehicleDynamics.FromSource(DynamicsGetMethod.Invoke(Src, null));
            set => DynamicsSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static FastMethod PassengerGetMethod;
        private static FastMethod PassengerSetMethod;
        /// <summary>
        /// 自列車の乗客を取得・設定します。
        /// </summary>
        public Passenger Passenger
        {
            get => ClassWrappers.Passenger.FromSource(PassengerGetMethod.Invoke(Src, null));
            set => PassengerSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static FastField CameraLocationField;
        /// <summary>
        /// カメラの位置に関する情報を取得・設定します。
        /// </summary>
        public CameraLocation CameraLocation
        {
            get => ClassWrappers.CameraLocation.FromSource(CameraLocationField.GetValue(Src));
            set => CameraLocationField.SetValue(Src, value.Src);
        }

        private static FastMethod InitializeMethod;
        /// <summary>
        /// 自列車を初期化します。
        /// </summary>
        /// <param name="brakePosition">ブレーキハンドルの位置。</param>
        public void Initialize(BrakePosition brakePosition) => InitializeMethod.Invoke(Src, new object[] { brakePosition });
    }
}
