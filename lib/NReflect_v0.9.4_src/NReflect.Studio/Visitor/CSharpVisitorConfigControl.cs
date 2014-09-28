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

using System.Windows.Forms;
using NReflect.Visitors;

namespace NReflect.Studio.Visitor
{
  /// <summary>
  /// A config panel to configure the <see cref="CSharpVisitor"/>.
  /// </summary>
  public partial class CSharpVisitorConfigControl : VisitorConfigPanel<CSharpVisitor>
  {
    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="CSharpVisitorConfigControl"/>.
    /// </summary>
    public CSharpVisitorConfigControl()
    {
      InitializeComponent();

      Text = "C# Visitor";

      CSharpVisitorConfig config = VisitorManager.Instance.GetVisitorConfig(typeof(CSharpVisitor).FullName) as CSharpVisitorConfig;

      if(config != null)
      {
        chkCreateAttributes.DataBindings.Add("Checked", config, "CreateAttributes", false, DataSourceUpdateMode.OnPropertyChanged);
        chkNewLineAfterType.DataBindings.Add("Checked", config, "NewLineAfterType", false, DataSourceUpdateMode.OnPropertyChanged);
        chkNewLineAfterMember.DataBindings.Add("Checked", config, "NewLineAfterMember", false, DataSourceUpdateMode.OnPropertyChanged);
        chkUseNamespaces.DataBindings.Add("Checked", config, "UseNamespaces", false, DataSourceUpdateMode.OnPropertyChanged);
        txtKnownNamespaces.DataBindings.Add("Lines", config, "KnownNamespaces", true, DataSourceUpdateMode.OnPropertyChanged);
      }
    }

    #endregion
  }
}