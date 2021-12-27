using System.Configuration;

namespace NumaLyrics.Configs
{
    class ButtonActionConfig : ConfigurationElement
    {
        public ButtonActionConfig() { }

        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public string id
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("action", IsRequired = true, IsKey = false)]
        public string action
        {
            get { return (string)this["action"]; }
            set { this["action"] = value; }
        }
    }
}
