using System.Configuration;

namespace NumaLyrics.Configs
{
    class ButtonActionCollection : ConfigurationElementCollection
    {
        public ButtonActionConfig this[int index]
        {
            get { return (ButtonActionConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public ButtonActionConfig Get(string key)
        {
            return (ButtonActionConfig)BaseGet(key);
        }

        public void Add(ButtonActionConfig serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ButtonActionConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ButtonActionConfig)element).id;
        }

        public void Remove(ButtonActionConfig serviceConfig)
        {
            BaseRemove(serviceConfig.id);
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
