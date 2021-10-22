﻿using System.Collections.Specialized;
using System.Configuration;

namespace ECMSS.Utilities
{
    public static class ConfigHelper
    {
        public static string Read(string key)
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
        }

        public static void Write(string key, string value)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
                settings[key].Value = value;
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
        }
    }
}