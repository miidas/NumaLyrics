using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumaLyrics
{
    public static class AppConfig
    {
        public static string FontFamily
        {
            get { return getAppSetting("FontFamily"); }
            set { updateAppSetting("FontFamily", value); }
        }

        public static float FontSize
        {
            get { return float.Parse(getAppSetting("FontSize")); }
            set { updateAppSetting("FontSize", value.ToString()); }
        }

        public static int FontStyle
        {
            get { return int.Parse(getAppSetting("FontStyle")); }
            set { updateAppSetting("FontStyle", value.ToString()); }
        }

        public static string FontColor
        {
            get { return getAppSetting("FontColor"); }
            set { updateAppSetting("FontColor", value); }
        }

        public static string FontOutlineColor
        {
            get { return getAppSetting("FontOutlineColor"); }
            set { updateAppSetting("FontOutlineColor", value); }
        }

        public static float FontOutlineWidth
        {
            get { return float.Parse(getAppSetting("FontOutlineWidth")); }
            set { updateAppSetting("FontOutlineWidth", value.ToString("F1")); }
        }

        public static int DisplayIndex
        {
            get { return int.Parse(getAppSetting("DisplayIndex")); }
            set { updateAppSetting("DisplayIndex", value.ToString()); }
        }

        public static float DisplayPositionX
        {
            get { return float.Parse(getAppSetting("DisplayPositionX")); }
            set { updateAppSetting("DisplayPositionX", value.ToString("F3")); }
        }

        public static float DisplayPositionY
        {
            get { return float.Parse(getAppSetting("DisplayPositionY")); }
            set { updateAppSetting("DisplayPositionY", value.ToString("F3")); }
        }

        public static int LyricsTimeOffsetMS
        {
            get { return int.Parse(getAppSetting("LyricsTimeOffsetMS")); }
            set { updateAppSetting("LyricsTimeOffsetMS", value.ToString()); }
        }

        public static bool EnableNowPlayingFeature
        {
            get { return bool.Parse(getAppSetting("EnableNowPlayingFeature")); }
            set { updateAppSetting("EnableNowPlayingFeature", value.ToString()); }
        }

        public static String ControllerCOMName
        {
            get { return getAppSetting("ControllerCOMName"); }
            set { updateAppSetting("ControllerCOMName", value.ToString()); }
        }

        public static string getAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void updateAppSetting(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings.AllKeys.Contains(key))
            {
                settings[key].Value = value;
            }
            else
            {
                settings.Add(key, value);
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
