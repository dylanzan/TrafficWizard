using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TrafficWizard.helpers;
using TrafficWizard.model;
using TrafficWizard.utils;

namespace TrafficWizard.controller
{
    public class TrafficWizardCtr
    {
        
        //配置文件
        ConfigModel config = null;

        //初始化操作队列
        static Queue srcFileContentQ = new Queue();
        static Queue reportContentQ = new Queue();

        ReadSrcFileUtils rSFU = null;
        HttpClientUtils hc = null;

        public TrafficWizardCtr()
        {
            config = ConfigHelpers.GetConfigHelper().GetConfig(ConstModel.CONFIG_FILE_PATH);
            
            rSFU = new ReadSrcFileUtils(srcFileContentQ, reportContentQ,config.srcFilePath,config.tagetFilePath);
            hc = new HttpClientUtils(config.token);
        }

        public void Run()
        {
            try
            {
                this.doReadToQueue();

                this.doContentToReport();

                this.doReportToCSV();
            }
            catch
            {
                throw;
            }
           
        }

        private async Task doReadToQueue()
        {
            if (this.config == null)
            {
                return;
            }

            await Task.Run(() =>
                {
                    rSFU.ReadSrcFile();
                });
        }

        private void doContentToReport()
        {
            if (this.config == null)
            {
                return;
            }

            bool loop = true; //标志位

            while (true)
            {
                if (srcFileContentQ.Count > 0)
                {
                    rSFU.logLineHandler(config.token);
                    loop = false;
                }
                else if (srcFileContentQ.Count == 0 && !loop)
                {
                    break;
                }

            }
        }

        private void doReportToCSV()
        {
            if (this.config == null)
            {
                return;
            }

            rSFU.reportToCSV();
        }

    }
}
