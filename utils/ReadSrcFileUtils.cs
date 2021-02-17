using System;
using System.Collections;
using TrafficWizard.model;
using System.IO;

namespace TrafficWizard.utils
{
    public class ReadSrcFileUtils
    {
        //读取源文件，符合正常请求，则入队；失败则清空队，并反回异常
        public static string ReadSrcFile(string srcFilepath, Queue fileContextQueue)
        {

            if (fileContextQueue == null)
            {
                return ConstModel.QUEUE_IS_NULL; 
            }
            string line = "";

            StreamReader file = null;
            

            try
            {
                file=new StreamReader(srcFilepath);

                while ((line = file.ReadLine()) != null)
                {
                    fileContextQueue.Enqueue(line);
                }

            }
            catch(Exception e)
            {
                fileContextQueue.Clear();
                return e.ToString();
            }
            finally
            {
                file.Close();
            }
            return "";
        }
    }
}
