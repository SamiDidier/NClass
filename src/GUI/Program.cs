﻿// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CrashReporterDotNET;
using NClass.Common;
using NClass.Translations;

namespace NClass.GUI
{
    internal static class Program
    {
        public static readonly Version CurrentVersion =
            Assembly.GetExecutingAssembly().GetName().Version;

        public static readonly string AppDataDirectory =
            Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                         "NClass");

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Run program with logger
            var app = new App();
            string result;
            var projectFiles = new List<string>();

            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-projects":
                    case "-p":
                        result = App.FileExist(i, args.Length, args[i + 1], "-projects");
                        if (string.IsNullOrWhiteSpace(result) == false)
                            return;

                        // Do we have other project behind the fist one
                        for (var j = i + 2; j < args.Length; j++)
                        {
                            // If another arg is present
                            if (args[j].StartsWith("-"))
                                break;

                            result = App.FileExist(j, args.Length, args[j], "-projects");

                            if (string.IsNullOrWhiteSpace(result) == false)
                                continue;

                            projectFiles.Add(args[j]);
                        }
                        break;
                    case "-log_cfg":
                    case "-l":
                        app.ArgumentLog(i, args.Length, args[i + 1]);
                        break;
                }
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += ApplicationThreadException;

            app.Start();
            UpdateSettings();

            // Set the user interface language
            var language = UILanguage.CreateUILanguage(Settings.Default.UILanguage);
            if (language != null)
                Strings.Culture = language.Culture;

            // Some GUI settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();
            ToolStripManager.VisualStylesEnabled = false;

            // Launch the application
            LoadProjects(projectFiles.ToArray());
            Application.Run(new MainForm());

            // Save application settings
            DiagramEditor.Settings.Default.Save();
            Settings.Default.Save();
        }

        private static void UpdateSettings()
        {
            if (Settings.Default.CallUpgrade)
            {
                Settings.Default.Upgrade();
                Settings.Default.CallUpgrade = false;
            }

            if (Settings.Default.OpenedProjects == null)
                Settings.Default.OpenedProjects = new StringCollection();
            if (Settings.Default.RecentFiles == null)
                Settings.Default.RecentFiles = new StringCollection();
        }

        public static string GetVersionString()
        {
            if (CurrentVersion.Minor == 0)
            {
                return string.Format("NClass {0}.0", CurrentVersion.Major);
            }
            return string.Format("NClass {0}.{1:00}",
                                 CurrentVersion.Major,
                                 CurrentVersion.Minor);
        }

        private static void LoadProjects(string[] args)
        {
            if (args.Length >= 1)
            {
                foreach (var filePath in args)
                {
                    Workspace.Default.OpenProject(filePath);
                }
            }
            else
            {
                if (Settings.Default.RememberOpenProjects)
                    Workspace.Default.Load();
            }
        }

        // Crash handling
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ReportCrash((Exception) e.ExceptionObject);
            Environment.Exit(0);
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ReportCrash(e.Exception);
        }

        private static void ReportCrash(Exception exception)
        {
            var reportCrash = new ReportCrash
            {
                ToEmail = "13300sam@gmail.com"
            };

            reportCrash.Send(exception);
        }
    }
}