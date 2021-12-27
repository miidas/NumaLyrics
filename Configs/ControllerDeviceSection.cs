using System.Configuration;

namespace NumaLyrics.Configs
{
    class ControllerDeviceSection : ConfigurationSection
    {
        [ConfigurationProperty("ControllerDevices", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ControllerDeviceCollection), AddItemName = "ControllerDevice")]
        public ControllerDeviceCollection Devices
        {
            get
            {
                return (ControllerDeviceCollection)base["ControllerDevices"];
            }
        }
    }
}
