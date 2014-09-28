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
using System.IO;
using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;
using NReflect.NRParameters;
using System.Linq;

namespace NReflect.Visitors
{
  /// <summary>
  /// This class implements the <see cref="IVisitor"/> interface to print
  /// the contents of a <see cref="NRAssembly"/> to a <see cref="TextWriter"/>
  /// in a tree form.
  /// </summary>
  public class PrintTreeVisitor : VisitorBase, IVisitor
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="PrintTreeVisitor"/>.
    /// </summary>
    public PrintTreeVisitor()
      : base(Console.Out)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="PrintTreeVisitor"/>.
    /// </summary>
    /// <param name="writer">This <see cref="TextWriter"/> will be used for output.</param>
    public PrintTreeVisitor(TextWriter writer)
      : base(writer)
    {
    }

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
      OutputLine("NRAssembly");
      indent++;
      nrAssembly.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      VisitEnities(nrAssembly);
    }

    /// <summary>
    /// Visit a <see cref="NRClass"/>.
    /// </summary>
    /// <param name="nrClass">The <see cref="NRClass"/> to visit.</param>
    public void Visit(NRClass nrClass)
    {
      OutputLine("NRClass");
      indent++;
      PrintMembers(nrClass);
      OutputLine("ClassModifier: " + nrClass.ClassModifier);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRInterface"/>.
    /// </summary>
    /// <param name="nrInterface">The <see cref="NRInterface"/> to visit.</param>
    public void Visit(NRInterface nrInterface)
    {
      OutputLine("NRInterface");
      indent++;
      PrintMembers(nrInterface);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRDelegate"/>.
    /// </summary>
    /// <param name="nrDelegate">The <see cref="NRDelegate"/> to visit.</param>
    public void Visit(NRDelegate nrDelegate)
    {
      OutputLine("NRDelegate");
      indent++;
      PrintMembers(nrDelegate);
      nrDelegate.Parameters.ForEach(nrParameter => nrParameter.Accept(this));
      nrDelegate.ReturnType.Accept(this);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRStruct"/>.
    /// </summary>
    /// <param name="nrStruct">The <see cref="NRStruct"/> to visit.</param>
    public void Visit(NRStruct nrStruct)
    {
      OutputLine("NRStruct");
      indent++;
      PrintMembers(nrStruct);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NREnum"/>.
    /// </summary>
    /// <param name="nrEnum">The <see cref="NREnum"/> to visit.</param>
    public void Visit(NREnum nrEnum)
    {
      OutputLine("NREnum");
      indent++;
      PrintMembers(nrEnum);
      nrEnum.Values.ForEach(nrEnumValue => nrEnumValue.Accept(this));
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRField"/>.
    /// </summary>
    /// <param name="nrField">The <see cref="NRField"/> to visit.</param>
    public void Visit(NRField nrField)
    {
      OutputLine("NRField");
      indent++;
      PrintMembers(nrField);
      OutputLine("FieldModifier: " + nrField.FieldModifier);
      OutputLine("InitialValue: " + nrField.InitialValue);
      OutputLine("IsConstant: " + nrField.IsConstant);
      OutputLine("IsHider: " + nrField.IsHider);
      OutputLine("IsReadonly: " + nrField.IsReadonly);
      OutputLine("IsStatic: " + nrField.IsStatic);
      OutputLine("IsVolatile: " + nrField.IsVolatile);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRProperty"/>.
    /// </summary>
    /// <param name="nrProperty">The <see cref="NRProperty"/> to visit.</param>
    public void Visit(NRProperty nrProperty)
    {
      OutputLine("NRProperty");
      indent++;
      PrintMembers(nrProperty);
      OutputLine("GetterModifier: " + nrProperty.GetterModifier);
      OutputLine("HasGetter: " + nrProperty.HasGetter);
      OutputLine("HasSetter: " + nrProperty.HasSetter);
      OutputLine("SetterModifier: " + nrProperty.SetterModifier);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRMethod"/>.
    /// </summary>
    /// <param name="nrMethod">The <see cref="NRMethod"/> to visit.</param>
    public void Visit(NRMethod nrMethod)
    {
      OutputLine("NRMethod");
      indent++;
      PrintMembers(nrMethod);
      nrMethod.GenericTypes.ForEach(nrTypeParameter => nrTypeParameter.Accept(this));
      OutputLine("IsExtensionMethod: " + nrMethod.IsExtensionMethod);
      OutputLine("IsGeneric: " + nrMethod.IsGeneric);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NROperator"/>.
    /// </summary>
    /// <param name="nrOperator">The <see cref="NROperator"/> to visit.</param>
    public void Visit(NROperator nrOperator)
    {
      OutputLine("NROperator");
      indent++;
      PrintMembers(nrOperator);
      OutputLine("OperatorType: " + nrOperator.OperatorType);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRConstructor"/>.
    /// </summary>
    /// <param name="nrConstructor">The <see cref="NRConstructor"/> to visit.</param>
    public void Visit(NRConstructor nrConstructor)
    {
      OutputLine("NRConstructor");
      indent++;
      PrintMembers(nrConstructor);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NREvent"/>.
    /// </summary>
    /// <param name="nrEvent">The <see cref="NREvent"/> to visit.</param>
    public void Visit(NREvent nrEvent)
    {
      OutputLine("NREvent");
      indent++;
      PrintMembers(nrEvent);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRParameter"/>.
    /// </summary>
    /// <param name="nrParameter">The <see cref="NRParameter"/> to visit.</param>
    public void Visit(NRParameter nrParameter)
    {
      OutputLine("NRParameter");
      indent++;
      nrParameter.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      OutputLine("DefaultValue: " + nrParameter.DefaultValue);
      OutputLine("Name: " + nrParameter.Name);
      OutputLine("ParameterModifier: " + nrParameter.ParameterModifier);
      if(nrParameter.Type != null)
      {
        OutputLine("Type:");
        indent++;
        nrParameter.Type.Accept(this);
        indent--;
      }
      OutputLine("TypeFullName: " + nrParameter.TypeFullName);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRTypeParameter"/>.
    /// </summary>
    /// <param name="nrTypeParameter">The <see cref="NRTypeParameter"/> to visit.</param>
    public void Visit(NRTypeParameter nrTypeParameter)
    {
      OutputLine("NRTypeParameter");
      indent++;
      nrTypeParameter.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      nrTypeParameter.BaseTypes.Aggregate("", (first, second) => first + ", " + second);
      OutputLine("IsClass: " + nrTypeParameter.IsClass);
      OutputLine("IsConstructor: " + nrTypeParameter.IsConstructor);
      OutputLine("IsIn: " + nrTypeParameter.IsIn);
      OutputLine("IsOut: " + nrTypeParameter.IsOut);
      OutputLine("IsStruct: " + nrTypeParameter.IsStruct);
      OutputLine("Name: " + nrTypeParameter.Name);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NREnumValue"/>.
    /// </summary>
    /// <param name="nrEnumValue">The <see cref="NREnumValue"/> to visit.</param>
    public void Visit(NREnumValue nrEnumValue)
    {
      OutputLine("NREnumValue");
      indent++;
      nrEnumValue.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      OutputLine("Name: " + nrEnumValue.Name);
      OutputLine("Value: " + nrEnumValue.Value);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRAttribute"/>.
    /// </summary>
    /// <param name="nrAttribute">The <see cref="NRAttribute"/> to visit.</param>
    public void Visit(NRAttribute nrAttribute)
    {
      OutputLine("NRAttribute");
      indent++;
      OutputLine("Name: " + nrAttribute.Name);
      foreach(NRAttributeValue nrAttributeValue in nrAttribute.Values)
      {
        OutputLine("Value: " + nrAttributeValue.Value + " : " + nrAttributeValue.Type);
      }
      foreach(string key in nrAttribute.NamedValues.Keys)
      {
        OutputLine("NamedValue " + key + ": " + nrAttribute.NamedValues[key].Value + " : " + nrAttribute.NamedValues[key].Type);
      }
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRModule"/>.
    /// </summary>
    /// <param name="nrModule">The <see cref="NRModule"/> to visit.</param>
    public void Visit(NRModule nrModule)
    {
      OutputLine("NRModule");
      indent++;
      nrModule.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      VisitEnities(nrModule);
      nrModule.Fields.ForEach(nrField => nrField.Accept(this));
      nrModule.Methods.ForEach(nrMethod => nrMethod.Accept(this));
      OutputLine("Name: " + nrModule.Name);
      indent--;
    }

    /// <summary>
    /// Visit a <see cref="NRTypeUsage"/>.
    /// </summary>
    /// <param name="nrTypeUsage">The <see cref="NRTypeUsage"/> to visit.</param>
    public void Visit(NRTypeUsage nrTypeUsage)
    {
      OutputLine("NRTypeUsage");
      indent++;
      OutputLine("ArrayRanks: " + nrTypeUsage.ArrayRanks.Aggregate("", (first, second) => first + ", " + second));
      nrTypeUsage.GenericParameters.ForEach(nrGenericParameter => nrGenericParameter.Accept(this));
      OutputLine("IsArray: " + nrTypeUsage.IsArray);
      OutputLine("IsDynamic: " + nrTypeUsage.IsDynamic);
      OutputLine("IsNullable: " + nrTypeUsage.IsNullable);
      OutputLine("Name: " + nrTypeUsage.Name);
      OutputLine("Namespace: " + nrTypeUsage.Namespace);
      OutputLine("FullName: " + nrTypeUsage.FullName);
      if(nrTypeUsage.DeclaringType != null)
      {
        OutputLine("DeclaringType:");
        indent++;
        nrTypeUsage.DeclaringType.Accept(this);
        indent--;
      }
      indent--;
    }

    /// <summary>
    /// Visits all entities at the entity container.
    /// </summary>
    /// <param name="entityContainer">The entities of this container ar visited.</param>
    private void VisitEnities(IEntityContainer entityContainer)
    {
      entityContainer.Classes.ForEach(nrClass => nrClass.Accept(this));
      entityContainer.Structs.ForEach(nrStruct => nrStruct.Accept(this));
      entityContainer.Interfaces.ForEach(nrInterface => nrInterface.Accept(this));
      entityContainer.Enums.ForEach(nrEnum => nrEnum.Accept(this));
      entityContainer.Delegates.ForEach(nrDelegate => nrDelegate.Accept(this));
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NRReturnValueOperation"/>.
    /// </summary>
    /// <param name="nrReturnValueOperation">The members of this <see cref="NRReturnValueOperation"/> are printed.</param>
    private void PrintMembers(NRReturnValueOperation nrReturnValueOperation)
    {
      PrintMembers((NROperation)nrReturnValueOperation);

      nrReturnValueOperation.ReturnValueAttributes.ForEach(nrAttribute => nrAttribute.Accept(this));
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NROperation"/>.
    /// </summary>
    /// <param name="nrOperation">The members of this <see cref="NROperation"/> are printed.</param>
    private void PrintMembers(NROperation nrOperation)
    {
      PrintMembers((NRMember)nrOperation);

      OutputLine("IsAbstract: " + nrOperation.IsAbstract);
      OutputLine("IsHider: " + nrOperation.IsHider);
      OutputLine("IsOverride: " + nrOperation.IsOverride);
      OutputLine("IsSealed: " + nrOperation.IsSealed);
      OutputLine("IsStatic: " + nrOperation.IsStatic);
      OutputLine("IsVirtual: " + nrOperation.IsVirtual);
      OutputLine("OperationModifier: " + nrOperation.OperationModifier);
      nrOperation.Parameters.ForEach(nrParameter => nrParameter.Accept(this));
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NRMember"/>.
    /// </summary>
    /// <param name="nrMember">The members of this <see cref="NRMember"/> are printed.</param>
    private void PrintMembers(NRMember nrMember)
    {
      OutputLine("AccessModifier: " + nrMember.AccessModifier);
      nrMember.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      OutputLine("Name: " + nrMember.Name);
      if(nrMember.Type != null)
      {
        OutputLine("Type:");
        indent++;
        nrMember.Type.Accept(this);
        indent--;
      }
      OutputLine("TypeFullName: " + nrMember.TypeFullName);
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NRSingleInheritanceType"/>.
    /// </summary>
    /// <param name="nrSingleInheritanceType">The members of this <see cref="NRSingleInheritanceType"/> are printed.</param>
    private void PrintMembers(NRSingleInheritanceType nrSingleInheritanceType)
    {
      PrintMembers((NRCompositeType)nrSingleInheritanceType);

      OutputLine("BaseType: ");
      indent++;
      nrSingleInheritanceType.BaseType.Accept(this);
      indent--;
      nrSingleInheritanceType.Constructors.ForEach(nrConstructor => nrConstructor.Accept(this));
      nrSingleInheritanceType.Fields.ForEach(nrField => nrField.Accept(this));
      nrSingleInheritanceType.Operators.ForEach(nrOperator => nrOperator.Accept(this));
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NRCompositeType"/>.
    /// </summary>
    /// <param name="nrCompositeType">The members of this <see cref="NRCompositeType"/> are printed.</param>
    private void PrintMembers(NRCompositeType nrCompositeType)
    {
      PrintMembers((NRGenericType)nrCompositeType);

      nrCompositeType.Events.ForEach(nrEvent => nrEvent.Accept(this));
      OutputLine("ImplementedInterfaces:");
      indent++;
      foreach(NRTypeUsage nrTypeUsage in nrCompositeType.ImplementedInterfaces)
      {
        nrTypeUsage.Accept(this);
      }
      indent--;
      nrCompositeType.Methods.ForEach(nrMethod => nrMethod.Accept(this));
      nrCompositeType.Properties.ForEach(nrProperty => nrProperty.Accept(this));
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NRGenericType"/>.
    /// </summary>
    /// <param name="nrGenericType">The members of this <see cref="NRGenericType"/> are printed.</param>
    private void PrintMembers(NRGenericType nrGenericType)
    {
      PrintMembers((NRTypeBase)nrGenericType);

      nrGenericType.GenericTypes.ForEach(nrTypeParameter => nrTypeParameter.Accept(this));
      OutputLine("IsGeneric: " + nrGenericType.IsGeneric);
    }

    /// <summary>
    /// Prints the members of the supplied <see cref="NRTypeBase"/>.
    /// </summary>
    /// <param name="nrTypeBase">The members of this <see cref="NRTypeBase"/> are printed.</param>
    private void PrintMembers(NRTypeBase nrTypeBase)
    {
      OutputLine("AccessModifier: " + nrTypeBase.AccessModifier);
      nrTypeBase.Attributes.ForEach(nrAttribute => nrAttribute.Accept(this));
      OutputLine("FullName: " + nrTypeBase.FullName);
      OutputLine("Name: " + nrTypeBase.Name);
      OutputLine("DeclaringTypeFullName: " + nrTypeBase.DeclaringTypeFullName);
    }

    #endregion
  }
}