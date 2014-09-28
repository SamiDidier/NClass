using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net;
using log4net.Config;


namespace NClass.App
{
    public class NClassApp
    {
        // Logger for the NClass program
        private static readonly ILog log = LogManager.GetLogger(typeof(NClassApp));

        public NClassApp ()
        {
            // Get log4net config 
            var configFile = Directory.GetCurrentDirectory() + @"\log4net.config";

            // Load the log4net config file
            XmlConfigurator.Configure(new FileInfo(configFile));

            log.Info("Start NClass Application.");


            log.Info("End NClass Application.");
        }
    }
}
