using System.IO;
using log4net;
using log4net.Config;
using NClass.Core;
using NClass.Translations;

namespace NClass.Common
{
    public class App
    {
        // Log config file
        private const string DefaultCfgLogFile = "NClass_log_cfg";

        // Logger for the NClass program
        private static readonly ILog Logger = LogManager.GetLogger(typeof (App));
        private string config_log_file = string.Empty;

        public LanguageManager LngMg;

        public App()
        {
            var LngMg = new LanguageManager();
        }

        public bool ArgumentLog(int i, int length, string nextArg)
        {
            if (string.IsNullOrWhiteSpace(FileExist(i, length, nextArg, "-log_cfg")))
                return false;
            config_log_file = nextArg;

            if (string.IsNullOrWhiteSpace(config_log_file))
                config_log_file = DefaultCfgLogFile;

            return true;
        }

        public void Start()
        {
            // Get log4net config 
            var configFile = Directory.GetCurrentDirectory() + config_log_file;

            // Load the log4net config file
            XmlConfigurator.Configure(new FileInfo(configFile));

            Logger.Info("Start NClass Application.");


            Logger.Info("End NClass Application.");
        }

        public static string FileExist(int currentIndex, int length, string value, string arg)
        {
            if (currentIndex + 1 >= length)
            {
                var errMsg = string.Format(Strings.MissingArgument, arg);
                Logger.Error(errMsg);
                return errMsg;
            }

            // Check if the folder exists
            if (File.Exists(value) == false)
            {
                var errMsg = string.Format(Strings.FileDoesntExist, value);
                Logger.Error(errMsg);
                return errMsg;
            }

            return string.Empty;
        }
    }
}