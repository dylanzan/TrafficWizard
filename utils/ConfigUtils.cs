using System;
using YamlDotNet;
using YamlDotNet.Serialization;
using TrafficWizard.model;


namespace TrafficWizard.utils
{
    public class ConfigUtils
    { 

        public static ConfigModel GetConfig(string configPath)
        {
            Deserializer condigDes = null; 

            ConfigModel config = null; 

            try
            {
                condigDes = new Deserializer();
                config = condigDes.Deserialize<ConfigModel>(configPath);
                return config;
            }catch(Exception e)
            {
                Console.WriteLine("parse config got an err :",e);
                return null;
            }
        } 

    }
}
