using System;
using YamlDotNet.Serialization;

namespace TrafficWizard.model
{
    public class ConfigModel
    {
        [YamlMember(Alias = "SrcFilePath")]
        private string srcFilePath;

        [YamlMember(Alias = "TagetFilePath")]
        private string tagetFilePath;

        [YamlMember(Alias = "Token")]
        private string token;

        public ConfigModel()
        {
        }

        public string SrcFilePath { get => srcFilePath; set => srcFilePath = value; }
        public string TagetFilePath { get => tagetFilePath; set => tagetFilePath = value; }
        public string Token { get => token; set => token = value; }

        public override bool Equals(object obj)
        {
            return obj is ConfigModel model &&
                   srcFilePath == model.srcFilePath &&
                   tagetFilePath == model.tagetFilePath &&
                   token == model.token;
        }
    }
}
