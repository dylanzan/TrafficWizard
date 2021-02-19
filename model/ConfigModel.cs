using System;
using YamlDotNet.Serialization;

namespace TrafficWizard.model
{
    public class ConfigModel
    {
        [YamlMember(Alias = "SrcFilePath",ApplyNamingConventions = false)]
        public string srcFilePath { set; get; }

        [YamlMember(Alias = "TagetFilePath", ApplyNamingConventions = false)]
        public string tagetFilePath { set; get; }

        [YamlMember(Alias = "Token", ApplyNamingConventions = false)]
        public string token { set; get; }

        public ConfigModel()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is ConfigModel model &&
                   srcFilePath == model.srcFilePath &&
                   tagetFilePath == model.tagetFilePath &&
                   token == model.token;
        }
    }
}
