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
using System.ComponentModel;
using NReflect.Studio.Models;
using NReflect.Studio.Panels;

namespace NReflect.Studio
{
  /// <summary>
  /// This class holds all classes needed for the view.
  /// </summary>
  public class CoreView : INotifyPropertyChanged
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The only instance of this class.
    /// </summary>
    private static CoreView instance;

    private CodePanel codePanel;
    private ObjectComparePanel objectComparePanel;
    private ObjectPanel objectPanel;
    private VisitorPanel visitorPanel;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="CoreView"/>.
    /// </summary>
    private CoreView()
    {
      MainForm = new MainForm();
      MessagePanel = new MessagePanel();
      OptionsPanel = new OptionsPanel();
      TestCasesPanel = new TestCasesPanel();

      TestCasesPanel.SelectedTestCaseChanged += (sender, args) => OnPropertyChanged("SelectedTestCase");
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the only instance of this class.
    /// </summary>
    public static CoreView Instance
    {
      get { return instance ?? (instance = new CoreView()); }
    }

    /// <summary>
    /// Gets the only instance of the main form.
    /// </summary>
    public MainForm MainForm { get; private set; }

    /// <summary>
    /// Gets the test cases panel.
    /// </summary>
    public TestCasesPanel TestCasesPanel { get; private set; }
    /// <summary>
    /// Gets the message panel.
    /// </summary>
    public MessagePanel MessagePanel { get; private set; }
    /// <summary>
    /// Gets the options panel.
    /// </summary>
    public OptionsPanel OptionsPanel { get; private set; }

    /// <summary>
    /// Gets the code panel.
    /// </summary>
    public CodePanel CodePanel
    {
      get { return codePanel ?? (codePanel = new CodePanel()); }
    }
    /// <summary>
    /// Gets the object compare panel.
    /// </summary>
    public ObjectComparePanel ObjectComparePanel
    {
      get { return objectComparePanel ?? (objectComparePanel = new ObjectComparePanel()); }
    }
    /// <summary>
    /// Gets the object panel.
    /// </summary>
    public ObjectPanel ObjectPanel
    {
      get { return objectPanel ?? (objectPanel = new ObjectPanel()); }
    }
    /// <summary>
    /// Gets the visitor panel.
    /// </summary>
    public VisitorPanel VisitorPanel
    {
      get { return visitorPanel ?? (visitorPanel = new VisitorPanel()); }
    }

    /// <summary>
    /// Gets an enumerator for all panels.
    /// </summary>
    public IEnumerator<BasePanel> Panels
    { 
      get 
      { 
        yield return TestCasesPanel;
        yield return MessagePanel;
        yield return CodePanel;
        yield return ObjectPanel;
        yield return ObjectComparePanel;
        yield return OptionsPanel;
        yield return VisitorPanel;
      }
    }

    /// <summary>
    /// Gets the currently selected test case.
    /// </summary>
    public TestCase SelectedTestCase
    {
      get { return TestCasesPanel.SelectedTestCase; }
    }

    /// <summary>
    /// Gets an enumerator for all currently selected test cases.
    /// </summary>
    public IEnumerable<TestCase> SelectedTestCases
    {
      get { return TestCasesPanel.SelectedTestCases; }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Fires the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the property which changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if(PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }

      if(propertyName == "SelectedTestCase" && SelectedTestCaseChanged != null)
      {
        SelectedTestCaseChanged(this, EventArgs.Empty);
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures after the selected test case changed.
    /// </summary>
    public event EventHandler SelectedTestCaseChanged;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}