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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FastColoredTextBoxNS;
using NReflect.NRCode;
using NReflect.Visitors;

namespace NReflect.Studio.Visitor
{
  /// <summary>
  /// This class holds the configuration of the
  /// <see cref="CSharpVisitor"/>.
  /// </summary>
  [Serializable]
  public class CSharpVisitorConfig : VisitorConfig
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// A value indicating if attributes should be created.
    /// </summary>
    private bool createAttributes;
    /// <summary>
    /// A value indicating if a newline should be created after each type.
    /// </summary>
    private bool newLineAfterType;
    /// <summary>
    /// A value indicating if a newline should be created after each member.
    /// </summary>
    private bool newLineAfterMember;
    /// <summary>
    /// A value indicating if namespaces should be used while generating code.
    /// </summary>
    private bool useNamespaces;
    /// <summary>
    /// An array of known namespaces.
    /// </summary>
    private string[] knownNamespaces;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="CSharpVisitorConfig"/>.
    /// </summary>
    public CSharpVisitorConfig()
    {
      ViewHighlighting = Language.CSharp;
      KnownNamespaces = new string[0];
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the name of the visitor this configuration is for.
    /// </summary>
    public override string VisitorName
    {
      get { return typeof(CSharpVisitor).FullName; }
    }

    /// <summary>
    /// Gets an array of known namespaces.
    /// </summary>
    public string[] KnownNamespaces
    {
      get { return knownNamespaces; }
      set
      {
        knownNamespaces = value;
        OnPropertyChanged("KnownNamespaces");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if attributes will be generated.
    /// </summary>
    public bool CreateAttributes
    {
      get { return createAttributes; }
      set
      {
        createAttributes = value;
        OnPropertyChanged("CreateAttributes");
      }
    }
    /// <summary>
    /// Gets or sets a value indicating if a newline should be created after each type.
    /// </summary>
    public bool NewLineAfterType
    {
      get { return newLineAfterType; }
      set
      {
        newLineAfterType = value;
        OnPropertyChanged("NewLineAfterType");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if a newline should be created after each member.
    /// </summary>
    public bool NewLineAfterMember
    {
      get { return newLineAfterMember; }
      set
      {
        newLineAfterMember = value;
        OnPropertyChanged("NewLineAfterMember");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if namespaces should be used while generating code.
    /// </summary>
    public bool UseNamespaces
    {
      get { return useNamespaces; }
      set 
      { 
        useNamespaces = value;
        OnPropertyChanged("UseNamespaces");
      }
    }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Applies the config.
    /// </summary>
    /// <param name="visitor">The visitor to configure.</param>
    public override void Apply(IVisitor visitor)
    {
      CSharp.CreateAttributes = CreateAttributes;
      CSharp.UseNamespaces = UseNamespaces;
      CSharp.KnownNamespaces = KnownNamespaces;

      CSharpVisitor csVisitor = visitor as CSharpVisitor;
      if(csVisitor != null)
      {
        csVisitor.NewLineAfterMember = newLineAfterMember;
        csVisitor.NewLineAfterType = newLineAfterType;
        csVisitor.KnownNamespaces = KnownNamespaces;
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    #endregion
  }
}