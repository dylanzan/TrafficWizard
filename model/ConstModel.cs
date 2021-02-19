using System;
namespace TrafficWizard.model
{
    public class ConstModel
    {
        //配置文件相对路径
        public const string CONFIG_FILE_PATH = "TrafficWizard/config/config.yaml";

        public const string VOID_PARAMS = "Please check if the parameter format you entered is correct.";
        public const string VOID_PORT_NUM = "Incorrect port number format";

        public const string VOID_VALUE = "Invalid value";
        public const string NO_VALUE = "Value is empty !";

        public const string REQUEST_ERROR = "Request exception";
        public const string NETWORK_ERROR = "Network connection is abnormal";

        //提示
        public const string PROMPT_RETRY = "Please try again";

        //Params
        public const string IPV4 = "ipv4";
        public const string IPV6 = "ipv6";
        public const string NOTHING = "nothing";

        //异常常量
        public const string DATA_FORMAT_ERROR = "System.FormatException";
        public const string QUEUE_IS_NULL = "Queus is null";
    }
}
