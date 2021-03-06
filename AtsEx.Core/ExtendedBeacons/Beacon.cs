using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.Plugins.Scripting;
using Automatic9045.AtsEx.PluginHost.ClassWrappers;
using Automatic9045.AtsEx.PluginHost.ExtendedBeacons;

namespace Automatic9045.AtsEx.ExtendedBeacons
{
    internal class Beacon : ExtendedBeaconBase<PassedEventArgs>
    {
        private double OldLocation = 0d;

        public Beacon(BveHacker bveHacker, string name, RepeatedStructure definedStructure, ObservingTargetTrack observingTargetTrack, ObservingTargetTrain observingTargetTrain, IPluginScript<ExtendedBeaconGlobalsBase<PassedEventArgs>> script)
            : base(bveHacker, name, definedStructure, observingTargetTrack, observingTargetTrain, script)
        {
        }

        internal void Tick(double currentLocation)
        {
            if (OldLocation < Location && Location <= currentLocation)
            {
                NotifyPassed(Direction.Forward);
            }
            else if (Location < OldLocation && currentLocation <= Location)
            {
                NotifyPassed(Direction.Backward);
            }

            OldLocation = currentLocation;


            void NotifyPassed(Direction direction)
            {
                PassedEventArgs eventArgs = new PassedEventArgs(direction);
                PassedGlobals globals = new PassedGlobals(BveHacker, this, eventArgs);
                Script.Run(globals);
                base.NotifyPassed(globals.GetEventArgsWithScriptVariables());
            }
        }
    }
}
