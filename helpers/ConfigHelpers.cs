using System;
using YamlDotNet;
using YamlDotNet.Serialization;
using TrafficWizard.model;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;

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

            Deserializer confsigDes = null; 

            ConfigModel config = null; 

            try
            {
                using(var input = File.OpenText(configPath))
                {
                    var deserializerBuilder = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention());
                    var deserializer = deserializerBuilder.Build();
                    config = deserializer.Deserialize<ConfigModel>(input);
                }

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
