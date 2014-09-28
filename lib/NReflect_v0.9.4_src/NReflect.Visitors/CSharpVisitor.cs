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
using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;
using NReflect.NRParameters;
using NReflect.NRCode;
using System.Linq;

namespace NReflect.Visitors
{
  public class CSharpVisitor : VisitorBase, IVisitor
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// Contains the full name of the current type. This is needed for correctly
    /// rendering nested types.
    /// </summary>
    private NRTypeBase currentType;

    #endregion

    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="PrintVisitor"/>.
    /// </summary>
    public CSharpVisitor()
      : base(Console.Out)
    {
      NewLineAfterType = true;
      NewLineAfterMember = true;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="CSharpVisitor"/>.
    /// </summary>
    /// <param name="writer">This <see cref="TextWriter"/> will be used for output.</param>
    public CSharpVisitor(TextWriter writer) 
      : base(writer)
    {
      NewLineAfterType = true;
      NewLineAfterMember = true;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets a value indicating if a newline should be created after each type.
    /// </summary>
    public bool NewLineAfterType { get; set; }
    /// <summary>
    /// Gets or sets a value indicating if a newline should be created after each member.
    /// </summary>
    public bool NewLineAfterMember { get; set; }
    /// <summary>
    /// Gets or sets an array of known namespaces.
    /// </summary>
    public string[] KnownNamespaces { get; set; }

    /// <summary>
    /// Gets or sets the visited assembly. It is set automaticaly if <see cref="Visit(NReflect.NRAssembly)"/>
    /// is invoked. If not set and <see cref="Visit(NReflect.NRAssembly)"/> is not invoked, no nested types
    /// are printed.
    /// </summary>
    public NRAssembly NRAssembly { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Visit a <see cref="NRAssembly"/>.
    /// </summary>
    /// <param name="nrAssembly">The <see cref="NRAssembly"/> to visit.</param>
    public void Visit(NRAssembly nrAssembly)
    {
      NRAssembly = nrAssembly;

      if(KnownNamespaces != null)
      {
        foreach(string knownNamespace in KnownNamespaces.Where(s => !string.IsNullOrWhiteSpace(s)))
        {
          OutputLine("using " + knownNamespace + ";");
        }
        if(KnownNamespaces.Length > 0)
        {
          OutputLine("");
        }
      }
      IEnumerable<string> namespaces = (from type in nrAssembly.Types
                                        select type.Namespace).Distinct();
      foreach(string ns in namespaces)
      {
        bool nsPresent = !string.IsNullOrWhiteSpace(ns);
        if(nsPresent)
        {
          OutputLine("namespace " + ns);
          OutputLine("{");
          indent++;
        }
        foreach(NRClass nrClass in nrAssembly.Classes)
        {
          if(nrClass.Namespace == ns)
          {
            nrClass.Accept(this);
          }
        }
        foreach(NRStruct nrStruct in nrAssembly.Structs)
        {
          if(nrStruct.Namespace == ns)
          {
            nrStruct.Accept(this);
          }
        }
        foreach(NRInterface nrInterface in nrAssembly.Interfaces)
        {
          if(nrInterface.Namespace == ns)
          {
            nrInterface.Accept(this);
          }
        }
        foreach(NRDelegate nrDelegate in nrAssembly.Delegates)
        {
          if(nrDelegate.Namespace == ns)
          {
            nrDelegate.Accept(this);
          }
        }
        foreach(NREnum nrEnum in nrAssembly.Enums)
        {
          if(nrEnum.Namespace == ns)
          {
            nrEnum.Accept(this);
          }
        }
        if(nsPresent)
        {
          indent--;
          OutputLine("}");
          OutputEmptyLineAfterType();
        }
      }
    }

    /// <summary>
    /// Visit a <see cref="NRClass"/>.
    /// </summary>
    /// <param name="nrClass">The <see cref="NRClass"/> to visit.</param>
    public void Visit(NRClass nrClass)
    {
      if(!string.IsNullOrWhiteSpace(nrClass.DeclaringTypeFullName) && (currentType != null && nrClass.DeclaringTypeFullName != currentType.FullName || currentType == null))
      {
        // Do not output the class since it is nested and we are not inside the parent type.
        return;
      }
      NRTypeBase oldCurrentType = currentType;
      currentType = nrClass;

      VisitAttributes(nrClass);

      OutputLine(nrClass.Declaration());
      OutputLine("{");
      indent++;
      foreach (NRField nrField in nrClass.Fields)
      {
        nrField.Accept(this);
      }
      foreach (NRConstructor nrConstructor in nrClass.Constructors)
      {
        nrConstructor.Accept(this);
      }
      foreach (NRProperty nrProperty in nrClass.Properties)
      {
        nrProperty.Accept(this);
      }
      foreach (NREvent nrEvent in nrClass.Events)
      {
        nrEvent.Accept(this);
      }
      foreach (NRMethod nrMethod in nrClass.Methods)
      {
        nrMethod.Accept(this);
      }
      foreach (NROperator nrOperator in nrClass.Operators)
      {
        nrOperator.Accept(this);
      }
      VisitNestedTypes(nrClass);
      indent--;
      OutputLine("}");
      OutputEmptyLineAfterType();

      currentType = oldCurrentType;
    }

    /// <summary>
    /// Visit a <see cref="NRInterface"/>.
    /// </summary>
    /// <param name="nrInterface">The <see cref="NRInterface"/> to visit.</param>
    public void Visit(NRInterface nrInterface)
    {
      if(!string.IsNullOrWhiteSpace(nrInterface.DeclaringTypeFullName) && (currentType != null && nrInterface.DeclaringTypeFullName != currentType.FullName || currentType == null))
      {
        // Do not output the interface since it is nested and we are not inside the parent type.
        return;
      }
      NRTypeBase oldCurrentType = currentType;
      currentType = nrInterface;

      VisitAttributes(nrInterface);

      OutputLine(nrInterface.Declaration());
      OutputLine("{");
      indent++;
      foreach (NRProperty nrProperty in nrInterface.Properties)
      {
        nrProperty.Accept(this);
      }
      foreach (NREvent nrEvent in nrInterface.Events)
      {
        nrEvent.Accept(this);
      }
      foreach (NRMethod nrMethod in nrInterface.Methods)
      {
        nrMethod.Accept(this);
      }
      indent--;
      OutputLine("}");
      OutputEmptyLineAfterType();

      currentType = oldCurrentType;
    }

    /// <summary>
    /// Visit a <see cref="NRDelegate"/>.
    /// </summary>
    /// <param name="nrDelegate">The <see cref="NRDelegate"/> to visit.</param>
    public void Visit(NRDelegate nrDelegate)
    {
      if(!string.IsNullOrWhiteSpace(nrDelegate.DeclaringTypeFullName) && (currentType != null && nrDelegate.DeclaringTypeFullName != currentType.FullName || currentType == null))
      {
        // Do not output the delegate since it is nested and we are not inside the parent type.
        return;
      }

      VisitAttributes(nrDelegate);

      OutputLine(nrDelegate.Declaration() + ";");
      OutputEmptyLineAfterType();
    }

    /// <summary>
    /// Visit a <see cref="NRStruct"/>.
    /// </summary>
    /// <param name="nrStruct">The <see cref="NRStruct"/> to visit.</param>
    public void Visit(NRStruct nrStruct)
    {
      if(!string.IsNullOrWhiteSpace(nrStruct.DeclaringTypeFullName) && (currentType != null && nrStruct.DeclaringTypeFullName != currentType.FullName || currentType == null))
      {
        // Do not output the struct since it is nested and we are not inside the parent type.
        return;
      }
      NRTypeBase oldCurrentType = currentType;
      currentType = nrStruct;

      VisitAttributes(nrStruct);

      OutputLine(nrStruct.Declaration());
      OutputLine("{");
      indent++;
      foreach (NRField nrField in nrStruct.Fields)
      {
        nrField.Accept(this);
      }
      foreach (NRConstructor nrConstructor in nrStruct.Constructors)
      {
        nrConstructor.Accept(this);
      }
      foreach (NRProperty nrProperty in nrStruct.Properties)
      {
        nrProperty.Accept(this);
      }
      foreach (NREvent nrEvent in nrStruct.Events)
      {
        nrEvent.Accept(this);
      }
      foreach (NRMethod nrMethod in nrStruct.Methods)
      {
        nrMethod.Accept(this);
      }
      VisitNestedTypes(nrStruct);
      indent--;
      OutputLine("}");
      OutputEmptyLineAfterType();

      currentType = oldCurrentType;
    }

    /// <summary>
    /// Visit a <see cref="NREnum"/>.
    /// </summary>
    /// <param name="nrEnum">The <see cref="NREnum"/> to visit.</param>
    public void Visit(NREnum nrEnum)
    {
      if(!string.IsNullOrWhiteSpace(nrEnum.DeclaringTypeFullName) && (currentType != null && nrEnum.DeclaringTypeFullName != currentType.FullName || currentType == null))
      {
        // Do not output the enum since it is nested and we are not inside the parent type.
        return;
      }
      NRTypeBase oldCurrentType = currentType;
      currentType = nrEnum;

      VisitAttributes(nrEnum);

      OutputLine(nrEnum.Declaration());
      OutputLine("{");
      indent++;
      foreach (NREnumValue nrEnumValue in nrEnum.Values)
      {
        nrEnumValue.Accept(this);
      }
      indent--;
      OutputLine("}");
      OutputEmptyLineAfterType();

      currentType = oldCurrentType;
    }

    /// <summary>
    /// Visit a <see cref="NRField"/>.
    /// </summary>
    /// <param name="nrField">The <see cref="NRField"/> to visit.</param>
    public void Visit(NRField nrField)
    {
      VisitAttributes(nrField);

      OutputLine(nrField.Declaration() + ";");
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NRProperty"/>.
    /// </summary>
    /// <param name="nrProperty">The <see cref="NRProperty"/> to visit.</param>
    public void Visit(NRProperty nrProperty)
    {
      VisitAttributes(nrProperty);

      OutputLine(nrProperty.Declaration());
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NRMethod"/>.
    /// </summary>
    /// <param name="nrMethod">The <see cref="NRMethod"/> to visit.</param>
    public void Visit(NRMethod nrMethod)
    {
      VisitAttributes(nrMethod);
      VisitReturnValueAttributes(nrMethod);

      Output(nrMethod.Declaration());
      if (nrMethod.GenericTypes.Count > 0)
      {
        foreach (NRTypeParameter nrTypeParameter in nrMethod.GenericTypes)
        {
          nrTypeParameter.Accept(this);
        }
      }
      if(currentType is NRInterface || nrMethod.IsAbstract)
      {
        OutputLine(";", 0);
      }
      else
      {
        OutputLine("", 0);
        OutputLine("{}");
      }
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NROperator"/>.
    /// </summary>
    /// <param name="nrOperator">The <see cref="NROperator"/> to visit.</param>
    public void Visit(NROperator nrOperator)
    {
      VisitAttributes(nrOperator);
      VisitReturnValueAttributes(nrOperator);

      OutputLine(nrOperator.Declaration() + "{}");
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NRConstructor"/>.
    /// </summary>
    /// <param name="nrConstructor">The <see cref="NRConstructor"/> to visit.</param>
    public void Visit(NRConstructor nrConstructor)
    {
      VisitAttributes(nrConstructor);

      OutputLine(nrConstructor.Declaration() + "{}");
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NREvent"/>.
    /// </summary>
    /// <param name="nrEvent">The <see cref="NREvent"/> to visit.</param>
    public void Visit(NREvent nrEvent)
    {
      VisitAttributes(nrEvent);

      OutputLine(nrEvent.Declaration() + ";");
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NRParameter"/>.
    /// </summary>
    /// <param name="nrParameter">The <see cref="NRParameter"/> to visit.</param>
    public void Visit(NRParameter nrParameter)
    {
      OutputLine(nrParameter.Declaration());
    }

    /// <summary>
    /// Visit a <see cref="NRTypeParameter"/>.
    /// </summary>
    /// <param name="nrTypeParameter">The <see cref="NRTypeParameter"/> to visit.</param>
    public void Visit(NRTypeParameter nrTypeParameter)
    {
      string declaration = nrTypeParameter.Declaration();
      if (!string.IsNullOrEmpty(declaration))
      {
        OutputLine(declaration);
      }
    }

    /// <summary>
    /// Visit a <see cref="NREnumValue"/>.
    /// </summary>
    /// <param name="nrEnumValue">The <see cref="NREnumValue"/> to visit.</param>
    public void Visit(NREnumValue nrEnumValue)
    {
      VisitAttributes(nrEnumValue);

      OutputLine(nrEnumValue.Declaration() + ";");
      OutputEmptyLineAfterMember();
    }

    /// <summary>
    /// Visit a <see cref="NRAttribute"/>.
    /// </summary>
    /// <param name="nrAttribute">The <see cref="NRAttribute"/> to visit.</param>
    public void Visit(NRAttribute nrAttribute)
    {
      string attribute = nrAttribute.Declaration();
      if(!string.IsNullOrWhiteSpace(attribute))
      {
        OutputLine(attribute);
      }
    }

    /// <summary>
    /// Visit a <see cref="NRModule"/>.
    /// </summary>
    /// <param name="nrModule">The <see cref="NRModule"/> to visit.</param>
    public void Visit(NRModule nrModule)
    {
      // Don't do anything...
    }

    /// <summary>
    /// Visit a <see cref="NRTypeUsage"/>.
    /// </summary>
    /// <param name="nrTypeUsage">The <see cref="NRTypeUsage"/> to visit.</param>
    public void Visit(NRTypeUsage nrTypeUsage)
    {
      // Don't do anything...
    }

    /// <summary>
    /// Visits all types which are nested within the given type.
    /// </summary>
    /// <param name="declaringType">The declaring type.</param>
    private void VisitNestedTypes(NRTypeBase declaringType)
    {
      if(NRAssembly == null)
      {
        return;
      }
      foreach(NRTypeBase nrTypeBase in NRAssembly.Types.Where(t => t.DeclaringTypeFullName == declaringType.FullName))
      {
        nrTypeBase.Accept(this);
      }
    }

    /// <summary>
    /// Visits the attributes of the given <see cref="IAttributable"/>.
    /// </summary>
    /// <param name="attributable">The attributes of this object are visited.</param>
    private void VisitAttributes(IAttributable attributable)
    {
      foreach(NRAttribute nrAttribute in attributable.Attributes)
      {
        nrAttribute.Accept(this);
      }
    }

    /// <summary>
    /// Visits the return value attributes of the given <see cref="NRReturnValueOperation"/>.
    /// </summary>
    /// <param name="nrReturnValueOperation">The attributes of this object are visited.</param>
    private void VisitReturnValueAttributes(NRReturnValueOperation nrReturnValueOperation)
    {
      foreach(NRAttribute nrAttribute in nrReturnValueOperation.ReturnValueAttributes)
      {
        string attribute = nrAttribute.Declaration(true);
        if(!string.IsNullOrWhiteSpace(attribute))
        {
          OutputLine(attribute);
        }
      }
    }

    /// <summary>
    /// Will output an empty line if the <see cref="NewLineAfterType"/>
    /// property is set to <c>true</c>.
    /// </summary>
    private void OutputEmptyLineAfterType()
    {
      if(NewLineAfterType)
      {
        OutputLine("");
      }
    }

    /// <summary>
    /// Will output an empty line if the <see cref="NewLineAfterMember"/>
    /// property is set to <c>true</c>.
    /// </summary>
    private void OutputEmptyLineAfterMember()
    {
      if(NewLineAfterMember)
      {
        OutputLine("");
      }
    }

    #endregion
  }
}