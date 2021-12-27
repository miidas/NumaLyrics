using System.Configuration;

namespace NumaLyrics.Configs
{
    class ControllerDeviceConfig : ConfigurationElement
    {
        public ControllerDeviceConfig() { }

        [ConfigurationProperty("uuid", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string uuid
        {
            get { return (string)this["uuid"]; }
            set { this["uuid"] = value; }
        }

        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = false)]
        public string name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("ButtonActions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ButtonActionCollection), AddItemName = "ButtonAction")]
        public ButtonActionCollection ButtonActions
        {
            get
            {
                return (ButtonActionCollection)this["ButtonActions"];
            }
            set { this["ButtonActions"] = value; }
        }
    }
}
