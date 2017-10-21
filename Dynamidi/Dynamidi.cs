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
        // Midi variables
        private const int SysExBufferSize = 128;
        private static InputDevice midiController = null;
        private static SynchronizationContext context;


        public static string getController()
        {
            string output = "";

            if (InputDevice.InstalledDevices.Count == 0)
            {
                output = "no device available";
            }
            else
            {
                try
                {
                    InputDevice inputDevice = InputDevice.InstalledDevices[0];
                    if (!inputDevice.IsOpen)
                    {
                        inputDevice.Open();
                    }
                        
                    inputDevice.StartReceiving(null);

                    output = "device connected";
                }
                catch (Exception ex)
                {
                    output = "device recognized but failed to connect";
                    output += "\n";
                    output += ex.Message.ToString();
                    output += InputDevice.InstalledDevices.Count.ToString();
                }
            }
            
            return output;

        }

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
            return device;
        }

    }
}
