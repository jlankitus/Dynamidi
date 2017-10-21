using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;
using System.Threading;
using Midi;

///////////////////////////////////////////////////////////////////
/// NOTE: This project requires references to the DynamoServices
/// and ProtoGeometry DLLs. These can be found in the latest
/// ZeroTouch and DynamoServices Nuget packages.
///////////////////////////////////////////////////////////////////

namespace Dynamidi
{
    public class Dynamidi
    {
        // private static Dictionary<Pitch, bool> pitchesPressed = new Dictionary<Pitch, bool>();
        private static string pitch;
        private static int cc_channel;
        private static int cc_control;
        private static int cc_value;
        private static int output_value;

        private static int sliderOneValue = 0;
        private static int sliderTwoValue = 0;
        private static int sliderThreeValue = 0;
        private static int sliderFourValue = 0;
        private static int sliderFiveValue = 0;
        private static int sliderSixValue = 0;
        private static int sliderSevenValue = 0;
        private static int sliderEightValue = 0;
        private static int sliderNineValue = 0;

        public static InputDevice getDevice()
        {
            InputDevice output = InputDevice.InstalledDevices[0];
            return output;
        }

        public static string deviceName(InputDevice device)
        {
            string name = device.Name;
            return name;
        }

        public static bool isReceiving(InputDevice device)
        {
            bool receiving = device.IsReceiving;
            return receiving;
        }

        public static InputDevice deviceOpen(InputDevice device)
        {
            device.Open();
            return device;
        }

        public static InputDevice deviceStart(InputDevice device)
        {
            device.StartReceiving(null);
            Dynamidi d = new Dynamidi();
            device.NoteOn += new InputDevice.NoteOnHandler(d.NoteOn);
            device.NoteOff += new InputDevice.NoteOffHandler(d.NoteOff);
            device.ControlChange += new InputDevice.ControlChangeHandler(d.ControlChange);
            return device;
        }

        private void NoteOn(NoteOnMessage msg)
        {
            pitch = msg.Pitch.ToString();
        }

        private void NoteOff(NoteOffMessage msg)
        {}

        private void ControlChange(ControlChangeMessage msg)
        {
            cc_channel = (int)msg.Channel);
            cc_control = (int)msg.Control;
            cc_value = (int)msg.Value;
        }

        [CanUpdatePeriodically(true)]
        public static string NoteOut()
        {
            return pitch;
        }

        [CanUpdatePeriodically(true)]
        public static int cc_channelOut()
        {
            return cc_channel;
        }

        [CanUpdatePeriodically(true)]
        public static int cc_controlOut()
        {
            return cc_control;
        }

        [CanUpdatePeriodically(true)]
        public static int cc_valueOut()
        {
            return cc_value;
        }

        [CanUpdatePeriodically(true)]
        public static int myControl(int sliderValue)
        {
            if (cc_control == sliderValue)
            {
                output_value = cc_value;
            }
            return output_value;
        }

        [MultiReturn(new[] { "Slider 1", "Slider 2", "Slider 3", "Slider 4", "Slider 5", "Slider 6", "Slider 7", "Slider 8", "Slider 9" })]
        public static Dictionary<string, int> apcMiniSliders(int slider, int value)
        {
            if (slider == 48) { sliderOneValue = value; }
            if (slider == 49) { sliderTwoValue = value; }
            if (slider == 50) { sliderThreeValue = value; }
            if (slider == 51) { sliderFourValue = value; }
            if (slider == 52) { sliderFiveValue = value; }
            if (slider == 53) { sliderSixValue = value; }
            if (slider == 54) { sliderSevenValue = value; }
            if (slider == 55) { sliderEightValue = value; }
            if (slider == 56) { sliderNineValue = value; }

            return new Dictionary<string, int>
            {
                {"Slider 1", sliderOneValue },
                {"Slider 2", sliderTwoValue },
                {"Slider 3", sliderThreeValue },
                {"Slider 4", sliderFourValue },
                {"Slider 5", sliderFiveValue },
                {"Slider 6", sliderSixValue },
                {"Slider 7", sliderSevenValue },
                {"Slider 8", sliderEightValue },
                {"Slider 9", sliderNineValue }
            };

        }
        
    }
}
