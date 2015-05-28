using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace JGerdesJWiemers.Game.Engine.Input
{
    class InputConfig
    {
        public AxisConfig Vertical { get; private set; }
        public AxisConfig Horizontal { get; private set; }
        public AxisConfig RotationVertical { get; private set; }
        public AxisConfig RotationHorizontal { get; private set; }
        public uint Shoot { get; private set; }
        public uint WeaponSwitch { get; private set; }
        public uint Return { get; private set; }

        public InputConfig(InputConfig.JsonFormat data)
        {
            Vertical = new AxisConfig(data.Vertical);
            Horizontal = new AxisConfig(data.Horizontal);
            RotationVertical = new AxisConfig(data.RotationVertical);
            RotationHorizontal = new AxisConfig(data.RotationHorizontal);
            Shoot = (uint)data.Shoot;
            WeaponSwitch = (uint)data.WeaponSwitch;
            Return = (uint)data.Return;

        }

        public class JsonFormat
        {
            public AxisConfig.JsonFormat Vertical { get; set; }
            public AxisConfig.JsonFormat Horizontal { get; set; }
            public AxisConfig.JsonFormat RotationVertical { get; set; }
            public AxisConfig.JsonFormat RotationHorizontal { get; set; }
            public int Shoot { get; set; }
            public int WeaponSwitch { get; set; }
            public int Return { get; set; }
        }
    }

    class AxisConfig
    {
        public static Dictionary<String, Joystick.Axis> MAPPING = new Dictionary<String, Joystick.Axis>()
        {
            {"X", Joystick.Axis.X},
            {"Y", Joystick.Axis.Y},
            {"Z", Joystick.Axis.Z},
            {"R", Joystick.Axis.R},
            {"U", Joystick.Axis.U},
            {"V", Joystick.Axis.V},
            {"PovX", Joystick.Axis.PovX},
            {"PovY", Joystick.Axis.PovY}

        };

        public Joystick.Axis Axis { get; private set; }
        public int Deadzone { get; private set; }

        public AxisConfig(AxisConfig.JsonFormat data)
        {
            Axis = MAPPING[data.Axis];
            Deadzone = data.Deadzone;
        }

        public class JsonFormat
        {
            public String Axis { get; set; }
            public int Deadzone { get; set; }
        }
    }
}
