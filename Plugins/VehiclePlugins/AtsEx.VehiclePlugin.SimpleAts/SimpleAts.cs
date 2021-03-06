using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.PluginHost;
using Automatic9045.AtsEx.PluginHost.ClassWrappers;
using Automatic9045.AtsEx.PluginHost.Handles;
using Automatic9045.AtsEx.PluginHost.Plugins;

namespace Automatic9045.VehiclePlugins.SimpleAts
{
    public class SimpleAts : AssemblyPluginBase
    {
        public SimpleAts(PluginBuilder builder) : base(builder, PluginType.VehiclePlugin)
        {
        }

        public override HandleCommandSet Tick(TimeSpan elapsed)
        {
            UserVehicleLocationManager locationManager = BveHacker.Scenario.LocationManager;
            AtsEx.PluginHost.Handles.HandleSet handleSet = App.Handles;

            double speedMps = locationManager.SpeedMeterPerSecond;

            if (speedMps > 100d.KmphToMps()) // 100km/h以上出ていたら常用最大ブレーキ
            {
                int atsPowerNotch = 0;
                int atsBrakeNotch = handleSet.Brake.MaxServiceBrakeNotch;

                NotchCommandBase powerCommand = handleSet.Power.GetCommandToSetNotchTo(atsPowerNotch);
                NotchCommandBase brakeCommand = handleSet.Brake.GetCommandToSetNotchTo(Math.Max(atsBrakeNotch, handleSet.Brake.Notch));
                ReverserPositionCommandBase reverserCommand = ReverserPositionCommandBase.Continue;
                ConstantSpeedCommand? constantSpeedCommand = ConstantSpeedCommand.Disable;

                return new HandleCommandSet(powerCommand, brakeCommand, reverserCommand, constantSpeedCommand);
            }
            else
            {
                return HandleCommandSet.DoNothing;
            }
        }
    }
}
