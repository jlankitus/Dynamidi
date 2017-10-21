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
        private static string cc_channel;
        private static string cc_control;
        private static string cc_value;

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

        public void NoteOn(NoteOnMessage msg)
        {
            pitch = msg.Pitch.ToString();
        }

        public void NoteOff(NoteOffMessage msg)
        {}

        public void ControlChange(ControlChangeMessage msg)
        {
            cc_channel = msg.Channel.ToString();
            cc_control = msg.Control.ToString();
            cc_value = msg.Value.ToString();
        }

        [CanUpdatePeriodically(true)]
        public static string NoteOut()
        {
            return pitch;
        }

        [CanUpdatePeriodically(true)]
        public static string cc_channelOut()
        {
            return cc_channel;
        }

        [CanUpdatePeriodically(true)]
        public static string cc_controlOut()
        {
            return cc_control;
        }

        [CanUpdatePeriodically(true)]
        public static string cc_valueOut()
        {
            return cc_value;
        }
    }
}
