using System;
using YamlDotNet;
using YamlDotNet.Serialization;
using TrafficWizard.model;


namespace TrafficWizard.helpers
{
    public class ConfigHelpers
    {

        private static ConfigHelpers ch;

        private ConfigHelpers() { }

        public static ConfigHelpers GetConfigHelper()
        {
            if (ch == null)
            {
                ch = new ConfigHelpers();
            }
            return ch;
        }

        public ConfigModel GetConfig(string configPath)
        {

            Deserializer configDes = null; 

            ConfigModel config = null; 

            try
            {
                configDes = new Deserializer();
                config = configDes.Deserialize<ConfigModel>(configPath);
                return config;
            }catch(Exception e)
            {
                throw;
                //Console.WriteLine("parse config got an err :",e);
                //return null;
            }
        } 

    }
}
