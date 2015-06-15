using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NClass.Translations;
using NClass.Common;


namespace CommandLine
{
    class Program
    {
        // Le dossier de travail
        private static string workingDirectory = string.Empty;
        private static bool runBatch = false;

        static void Main(string[] args)
        {
            // Arguments
            if (args == null || args.Length == 0)
            {
                DisplayVersion();
                return;
            }

            // Run program with logger
            App app = new App();
            string result;

            for(int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    // NClass project to load if exist or to create
                    case "-project":
                    case "-p":
                        result = App.FileExist(i, args.Length, args[i + 1], "-project");
                        if (string.IsNullOrWhiteSpace(result) == false)
                        {
                            Console.WriteLine(result);
                            DisplayHelp();
                            return;
                        }
                        // TO DO:
                        // Workspace.Default.OpenProject(filePath);
                        break;
                    // NClass settings to load (file must exist)
                    case "-settings":
                    case "-s":
                        result = App.FileExist(i, args.Length, args[i + 1], "-settings");
                        if (string.IsNullOrWhiteSpace(result) == false)
                        {
                            Console.WriteLine(result);
                            DisplayHelp();
                            return;
                        }
                        break;
                    case "-directory":
                    case "-d":
                        if (i + 1 >= args.Length)
                        {
                            Console.WriteLine(String.Format(Strings.MissingArgument, "-directory"));
                            return;
                        }

                        // Check if the folder exists
                        if (Directory.Exists(args[i + 1]) == false)
                        {
                            Console.WriteLine(String.Format(Strings.MissingArgument, "-directory"));
                            return;
                        }

                        // Memorize the working directory
                        workingDirectory = args[i + 1];
                        i++;
                        break;
                    case "-log_cfg":
                    case "-l":
                        app.ArgumentLog(i, args.Length, args[i + 1]);
                        break;
                    case "-batch":
                    case "-b":
                        runBatch = true;
                        break;
                    case "-version":
                    case "-v":
                        DisplayVersion();
                        return;
                    case "-help":
                    case "-h":
                        DisplayHelp();
                        return;
                    default:
                        DisplayHelp();
                        return;
                }
           }

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomainOnUnhandledException);

            // Run program with logger
            app.Start();
        }

        private static void DisplayHelp()
        {
            DisplayVersion();
            Console.WriteLine("");

            Console.WriteLine("-h / -help             Show the help to use this software.");
            Console.WriteLine("-v / -version          Show the version of this software.");
            Console.WriteLine("-p / -project          NClass project file to modify or create.");
            Console.WriteLine("-s / -setting          Settings file to load.");
            Console.WriteLine("-d / -directory        Working directory where to put log file.");
            Console.WriteLine("-l / -log_cfg          Config log file.");
            Console.WriteLine("-b / -batch            To run in batch mode without any input from user.");
        }

        private static void DisplayVersion()
        {
            Console.WriteLine("NClass modified by Samuel Didier - Copyright - 2014 - Version 1.0 beta");
            Console.WriteLine("Software to add comments, regions and summary tags based on my custom style to C# source code");
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Environment.Exit(0);
        }
    }
}
