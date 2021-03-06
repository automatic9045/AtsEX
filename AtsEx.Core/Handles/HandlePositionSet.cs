using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.PluginHost.Handles;

namespace Automatic9045.AtsEx.Handles
{
    public class HandlePositionSet
    {
        public int Power { get; }
        public int Brake { get; }
        public ReverserPosition ReverserPosition { get; }
        public ConstantSpeedCommand ConstantSpeed { get; }

        public HandlePositionSet(int power, int brake, ReverserPosition reverserPosition, ConstantSpeedCommand constantSpeedCommand)
        {
            Power = power;
            Brake = brake;
            ReverserPosition = reverserPosition;
            ConstantSpeed = constantSpeedCommand;
        }
    }
}
