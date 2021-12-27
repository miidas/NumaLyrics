using System;
using System.Configuration;

namespace NumaLyrics.Configs
{
    class ControllerDeviceCollection : ConfigurationElementCollection
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ControllerDeviceConfig this[int index]
        {
            get { return (ControllerDeviceConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public ControllerDeviceConfig Get(string key)
        {
            return (ControllerDeviceConfig)BaseGet(key);
        }

        public string[] AllKeys { 
            get {
                return Array.ConvertAll(BaseGetAllKeys(), key => (string)key);
            }
        }

        public void Add(ControllerDeviceConfig serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ControllerDeviceConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ControllerDeviceConfig)element).uuid;
        }

        public void Remove(ControllerDeviceConfig serviceConfig)
        {
            BaseRemove(serviceConfig.uuid);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}
