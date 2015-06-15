using System;
using System.Globalization;
using System.Windows.Forms;
using NClass.AssemblyCSharpImport.Lang;
using NClass.CSharp;
using NClass.DiagramEditor.ClassDiagram;
using NClass.GUI;
using System.IO;


namespace NClass.AssemblyCSharpImport
{
    /// <summary>
    ///   Implements the PlugIn-Interface of NClass.
    /// </summary>
    public class ImportPlugin : NClass.GUI.Plugin
    {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    ///   The menu item used to start the export.
    /// </summary>
    private readonly ToolStripMenuItem menuItem;

    #endregion

    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    ///   Set up the current culture for the strings.
    /// </summary>
    static ImportPlugin()
    {
      try
      {
        Strings.Culture = CultureInfo.GetCultureInfo(NClass.GUI.Settings.Default.UILanguage);
      }
      catch(ArgumentException)
      {
        //Culture is not supported, maybe the setting is "default".
      }
    }

    /// <summary>
    ///   Constructs a new instance of NETImportPlugin.
    /// </summary>
    /// <param name = "environment">An instance of NClassEnvironment.</param>
    public ImportPlugin(NClassEnvironment environment)
      : base(environment)
    {
      menuItem = new ToolStripMenuItem
                   {
                     Text = Strings.Menu_Title,
                     ToolTipText = Strings.Menu_ToolTip
                   };
      menuItem.Click += menuItem_Click;
    }

    #endregion

    // ========================================================================
    // Event handling

    #region === Event handling

    /// <summary>
    ///   Starts the export.
    /// </summary>
    /// <param name = "sender">The sender.</param>
    /// <param name = "e">Additional information.</param>
    private void menuItem_Click(object sender, EventArgs e)
    {
      Launch();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    ///   Gets a value indicating whether the plugin can be executed at the moment.
    /// </summary>
    public override bool IsAvailable
    {
      get { return Workspace.HasActiveProject; }
    }

    /// <summary>
    ///   Gets the menu item used to start the plugin.
    /// </summary>
    public override ToolStripItem MenuItem
    {
      get { return menuItem; }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    ///   Starts the functionality of the plugin.
    /// </summary>
    protected void Launch()
    {
      if(Workspace.HasActiveProject)
      {
        ImportSettings settings = new ImportSettings();
        using(ImportSettingsForm settingsForm = new ImportSettingsForm(settings))
        {
          if(settingsForm.ShowDialog() == DialogResult.OK)
          {
            Diagram diagram = new Diagram(CSharpLanguage.Instance);

            // Is it a file or a folder?
            foreach (string item in settings.Items)
            {
                // Analyse items to know if it is :
                // a C# source file
                // a folder
                // a .NET assembly
                if (Path.HasExtension(item) == true)
                {
                    switch (Path.GetExtension(item))
                    {
                        case ".cs":
                            if (File.Exists(item) == true)
                            {
                                if (settings.NewDiagram == true)
                                    diagram = new Diagram(CSharpLanguage.Instance);

                                ImportCSharpFile(diagram, settings, item);
                            }
                            break;
                        case ".dll":
                        case ".exe":
                            if (File.Exists(item) == true)
                            {
                                if (settings.NewDiagram == true)
                                    diagram = new Diagram(CSharpLanguage.Instance);

                                ImportAssembly(diagram, settings, item);
                            }
                            break;
                        case ".sln":
                            if (File.Exists(item) == true)
                            {
                                if (settings.NewDiagram == true)
                                    diagram = new Diagram(CSharpLanguage.Instance);

                                ImportVisualStudioSolution(diagram, settings, item);
                            }
                            break;
                        case ".csproj":
                            if (File.Exists(item) == true)
                            {
                                if (settings.NewDiagram == true)
                                    diagram = new Diagram(CSharpLanguage.Instance);

                                ImportVisualStudioProject(diagram, settings, item);
                            }
                            break;
                        default:
                            // unknow extension
                            break;
                    }
                }
                else
                {
                    if (Directory.Exists(item) == true)
                        ImportFolder(diagram, settings, item);
                }
            }
          }
        }
      }
    }

    /// <summary>
    ///   Import a C# code source file.
    /// </summary>
    private void ImportCSharpFile(Diagram diagram, ImportSettings settings, string fileName)
    {
        CSharpImport importer = new CSharpImport(diagram, settings);
    
        if (importer.ImportSourceCode(fileName) == true)
            Workspace.ActiveProject.Add(diagram);
    }

    /// <summary>
    ///   Import all C# code source files in a folder and its subfolders.
    /// </summary>
    private void ImportFolder(Diagram diagram, ImportSettings settings, string folderName)
    {
        // All C# code source file in this directory 
        foreach(string file in Directory.EnumerateFiles(folderName, "*.cs"))
            ImportCSharpFile(diagram, settings, file);

        // All subfolders in this directory 
        foreach (string folder in Directory.EnumerateDirectories(folderName))
            ImportCSharpFile(diagram, settings, folder);
    }

    /// <summary>
    ///   Import a .NET assembly.
    /// </summary>
    private void ImportAssembly(Diagram diagram, ImportSettings settings, string fileName)
    {
        NETImport importer = new NETImport(diagram, settings);

        if(importer.ImportAssembly(fileName))
        {
            Workspace.ActiveProject.Add(diagram);
        }
    }

    /// <summary>
    ///   Import a Visual Studio Solution.
    /// </summary>
    private void ImportVisualStudioSolution(Diagram diagram, ImportSettings settings, string fileName)
    {
        // TO DO
        // http://stackoverflow.com/questions/707107/library-for-parsing-visual-studio-solution-files
        // MonoDevelop.Projects.Formats.MSBuild
        // SlnFileFormat.cs
    }

    /// <summary>
    ///   Import a Visual Studio Project.
    /// </summary>
    private void ImportVisualStudioProject(Diagram diagram, ImportSettings settings, string fileName)
    {
        // TO DO
    }
    #endregion
  }
}