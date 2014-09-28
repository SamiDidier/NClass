// NReflect - Easy assembly reflection
// Copyright (C) 2010-2013 Malte Ried
//
// This file is part of NReflect.
//
// NReflect is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// NReflect is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with NReflect. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using NReflect.Visitors;

namespace NReflect.Studio.Visitor
{
  /// <summary>
  /// This singleton manages the usable visitors and gives the ability
  /// to run them.
  /// </summary>
  public class VisitorManager
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The only instance of this class.
    /// </summary>
    private static VisitorManager instance;

    /// <summary>
    /// A dictionary containing all visitors.
    /// </summary>
    private readonly Dictionary<string, Type> visitorTypes;
    /// <summary>
    /// A dictionary containing all known visitor configs.
    /// </summary>
    private readonly Dictionary<string, VisitorConfig> visitorConfigs; 

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="VisitorManager"/>.
    /// </summary>
    private VisitorManager()
    {
      visitorTypes = new Dictionary<string, Type>();
      visitorConfigs = new Dictionary<string, VisitorConfig>();
      VisitorNames = new List<string>();
      VisitorTypes = new List<Type>();

      LoadVisitors();

      VisitorConfig config = new CSharpVisitorConfig();
      visitorConfigs.Add(config.VisitorName, config);

      LoadConfigs();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the only instance of the <see cref="VisitorManager"/>.
    /// </summary>
    public static VisitorManager Instance
    {
      get { return instance ?? (instance = new VisitorManager()); }
    }

    /// <summary>
    /// Gets a list of visitor names.
    /// </summary>
    public List<string> VisitorNames { get; private set; }
    /// <summary>
    /// Gets a list of visitor types.
    /// </summary>
    public List<Type> VisitorTypes { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets a configuration for the given visitor. Returns null if no configuration
    /// is known.
    /// </summary>
    /// <param name="visitorName">The name of the visitor of the returned config.</param>
    /// <returns>The config of null if no config is known.</returns>
    public VisitorConfig GetVisitorConfig(string visitorName)
    {
      if(visitorConfigs.ContainsKey(visitorName))
      {
        return visitorConfigs[visitorName];
      }
      return null;
    }

    /// <summary>
    /// Invokes the given visitor for the given assembly.
    /// </summary>
    /// <param name="nrAssembly">The assembly to run the visitor for.</param>
    /// <param name="visitorName">The name of the visitor to run.</param>
    /// <returns>The result of the visitor.</returns>
    public string RunVisitor(NRAssembly nrAssembly, string visitorName)
    {
      if(!visitorTypes.ContainsKey(visitorName))
      {
        throw new ArgumentException("Visitor '" + visitorName + "' is unknown.");
      }

      StringWriter stringWriter = new StringWriter();
      IVisitor visitor = Activator.CreateInstance(visitorTypes[visitorName], stringWriter) as IVisitor;
      VisitorConfig config = GetVisitorConfig(visitorName);
      if(config != null)
      {
        config.Apply(visitor);
      }
      nrAssembly.Accept(visitor);

      return stringWriter.ToString();
    }

    /// <summary>
    /// Stores all configs to the local user app data folder.
    /// </summary>
    public void StoreConfigs()
    {
      BinaryFormatter formatter = new BinaryFormatter();
      foreach(VisitorConfig config in visitorConfigs.Values)
      {
        string file = Path.Combine(Application.LocalUserAppDataPath, config.VisitorName + ".cfg");
        Stream stream = new FileStream(file, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, config);
        stream.Close();
      }
    }

    /// <summary>
    /// Loads all known configs from the local user app data folder.
    /// </summary>
    private void LoadConfigs()
    {
      BinaryFormatter formatter = new BinaryFormatter();
      List<VisitorConfig> loadedConfigs = new List<VisitorConfig>();
      foreach(VisitorConfig config in visitorConfigs.Values)
      {
        string file = Path.Combine(Application.LocalUserAppDataPath, config.VisitorName + ".cfg");
        if(File.Exists(file))
        {
          Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
          try
          {
            loadedConfigs.Add(formatter.Deserialize(stream) as VisitorConfig);
          }
          catch
          {
            // Deserialisation failed. Do nothing.
          }
          stream.Close();
        }
      }
      foreach(VisitorConfig loadedConfig in loadedConfigs)
      {
        if(visitorConfigs.ContainsKey(loadedConfig.VisitorName))
        {
          visitorConfigs.Remove(loadedConfig.VisitorName);
        }
        visitorConfigs.Add(loadedConfig.VisitorName, loadedConfig);
      }
    }

    /// <summary>
    /// Finds all usable visitors.
    /// </summary>
    private void LoadVisitors()
    {
      foreach (Type type in Assembly.GetAssembly(typeof(VisitorBase)).GetTypes().Where(type => type.IsSubclassOf(typeof(VisitorBase)) && type.GetInterfaces().Contains(typeof(IVisitor))))
      {
        visitorTypes.Add(type.ToString(), type);
        VisitorNames.Add(type.ToString());
        VisitorTypes.Add(type);
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}