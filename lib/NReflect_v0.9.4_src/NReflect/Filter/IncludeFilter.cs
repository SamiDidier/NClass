// NReflect - Easy assembly reflection
// Copyright (C) 2010-2011 Malte Ried
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
using System.Linq;
using NReflect.Modifier;
using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;

namespace NReflect.Filter
{
  /// <summary>
  /// This filter uses exclusion rules to determine which element to import.
  /// </summary>
  [Serializable]
  public class IncludeFilter : IFilter
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="IncludeFilter"/>.
    /// </summary>
    public IncludeFilter()
    {
      Rules = new List<FilterRule>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets the list of exception rules.
    /// </summary>
    public List<FilterRule> Rules { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Determines if a class will be reflected.
    /// </summary>
    /// <param name="nrClass">The class to test.</param>
    /// <returns><c>True</c> if the class should be reflected.</returns>
    public bool Reflect(NRClass nrClass)
    {
      FilterModifiers filterModifiers = GetFilterModifier(nrClass.AccessModifier);
      filterModifiers |= nrClass.ClassModifier == ClassModifier.Static
                           ? FilterModifiers.Static
                           : FilterModifiers.Instance;
      return RuleMatch(FilterElements.Class, filterModifiers);
    }

    /// <summary>
    /// Determines if an interface will be reflected.
    /// </summary>
    /// <param name="nrInterface">The interface to test.</param>
    /// <returns><c>True</c> if the interface should be reflected.</returns>
    public bool Reflect(NRInterface nrInterface)
    {
      return Reflect(FilterElements.Interface, nrInterface);
    }

    /// <summary>
    /// Determines if a struct will be reflected.
    /// </summary>
    /// <param name="nrStruct">The struct to test.</param>
    /// <returns><c>True</c> if the struct should be reflected.</returns>
    public bool Reflect(NRStruct nrStruct)
    {
      return Reflect(FilterElements.Struct, nrStruct);
    }

    /// <summary>
    /// Determines if a delegate will be reflected.
    /// </summary>
    /// <param name="nrDelegate">The delegate to test.</param>
    /// <returns><c>True</c> if the delegate should be reflected.</returns>
    public bool Reflect(NRDelegate nrDelegate)
    {
      return Reflect(FilterElements.Delegate, nrDelegate);
    }

    /// <summary>
    /// Determines if a enum will be reflected.
    /// </summary>
    /// <param name="nrEnum">The enum to test.</param>
    /// <returns><c>True</c> if the enum should be reflected.</returns>
    public bool Reflect(NREnum nrEnum)
    {
      return Reflect(FilterElements.Enum, nrEnum);
    }

    /// <summary>
    /// Determines if a enum value will be reflected.
    /// </summary>
    /// <param name="nrEnumValue">The enum value to test.</param>
    /// <returns><c>True</c> if the enum value should be reflected.</returns>
    public bool Reflect(NREnumValue nrEnumValue)
    {
      return RuleMatch(FilterElements.EnumValue, FilterModifiers.Public);
    }

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrConstructor">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    public bool Reflect(NRConstructor nrConstructor)
    {
      return Reflect(FilterElements.Constructor, nrConstructor);
    }

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrMethod">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    public bool Reflect(NRMethod nrMethod)
    {
      return Reflect(FilterElements.Method, nrMethod);
    }

    /// <summary>
    /// Determines if an operator will be reflected.
    /// </summary>
    /// <param name="nrOperator">The operator to test.</param>
    /// <returns><c>True</c> if the operator should be reflected.</returns>
    public bool Reflect(NROperator nrOperator)
    {
      return Reflect(FilterElements.Operator, nrOperator);
    }

    /// <summary>
    /// Determines if an event will be reflected.
    /// </summary>
    /// <param name="nrEvent">The event to test.</param>
    /// <returns><c>True</c> if the event should be reflected.</returns>
    public bool Reflect(NREvent nrEvent)
    {
      return Reflect(FilterElements.Event, nrEvent);
    }

    /// <summary>
    /// Determines if a field will be reflected.
    /// </summary>
    /// <param name="nrField">The field to test.</param>
    /// <returns><c>True</c> if the field should be reflected.</returns>
    public bool Reflect(NRField nrField)
    {
      FilterModifiers filterModifiers = GetFilterModifier(nrField.AccessModifier);
      filterModifiers |= nrField.FieldModifier == FieldModifier.Static
                           ? FilterModifiers.Static
                           : FilterModifiers.Instance;
      FilterElements filterElement = nrField.IsConstant ? FilterElements.Constant : FilterElements.Field;
      return RuleMatch(filterElement, filterModifiers);
    }

    /// <summary>
    /// Determines if a property will be reflected.
    /// </summary>
    /// <param name="nrProperty">The property to test.</param>
    /// <returns><c>True</c> if the property should be reflected.</returns>
    public bool Reflect(NRProperty nrProperty)
    {
      return Reflect(FilterElements.Property, nrProperty);
    }

    /// <summary>
    /// Determines if an attribute will be reflected.
    /// </summary>
    /// <param name="nrAttribute">The attribute to test.</param>
    /// <returns><c>True</c> if the attribute should be reflected.</returns>
    public bool Reflect(NRAttribute nrAttribute)
    {
      return Rules.Any(filterRule => filterRule.Element == FilterElements.Attribute) |
        Rules.Any(filterRule => filterRule.Element == FilterElements.AllElements);
    }

    /// <summary>
    /// Determines if a module will be reflected.
    /// </summary>
    /// <param name="nrModule">The module to test.</param>
    /// <returns><c>True</c> if the module should be reflected.</returns>
    public bool Reflect(NRModule nrModule)
    {
      return
        Rules.Any(
                  filterRule =>
                  filterRule.Element == FilterElements.AllElements | filterRule.Element == FilterElements.Module);
    }

    /// <summary>
    /// Checks if there is a matching rule for the specified type.
    /// </summary>
    /// <param name="nrElement">The type of the element to check.</param>
    /// <param name="nrType">The type to check.</param>
    /// <returns><c>True</c> if there is a matching rule.</returns>
    private bool Reflect(FilterElements nrElement, NRTypeBase nrType)
    {
      FilterModifiers filterModifiers = GetFilterModifier(nrType.AccessModifier);
      return RuleMatch(nrElement, filterModifiers);
    }

    /// <summary>
    /// Checks if there is a matching rule for the specified operation.
    /// </summary>
    /// <param name="nrElement">The type of the element to check.</param>
    /// <param name="nrOperation">The operation to check.</param>
    /// <returns><c>True</c> if there is a matching rule.</returns>
    private bool Reflect(FilterElements nrElement, NROperation nrOperation)
    {
      FilterModifiers filterModifiers = GetFilterModifier(nrOperation.AccessModifier);
      filterModifiers |= nrOperation.OperationModifier == OperationModifier.Static
                           ? FilterModifiers.Static
                           : FilterModifiers.Instance;
      return RuleMatch(nrElement, filterModifiers);
    }

    /// <summary>
    /// Checks if there is a rule matching the given values.
    /// </summary>
    /// <param name="element">The element of the rule to check.</param>
    /// <param name="modifier">The modifier of the rule to check.</param>
    /// <returns><c>True</c> if there is a rule.</returns>
    private bool RuleMatch(FilterElements element, FilterModifiers modifier)
    {
      foreach(FilterRule rule in Rules)
      {
        if(rule.Element == FilterElements.AllElements || rule.Element == element)
        {
          if(rule.Modifier == FilterModifiers.AllModifiers || (rule.Modifier & modifier) != 0)
          {
            return true;
          }
        }
      }
      return false;
      //return Rules.Where(rule => rule.Element == FilterElements.AllElements || rule.Element == element)
      //  .Any(rule => rule.Modifier == FilterModifiers.AllModifiers || (rule.Modifier & modifier) != 0);
    }

    /// <summary>
    /// Gets the <see cref="FilterModifiers"/> for a given <see cref="AccessModifier"/>.
    /// </summary>
    /// <param name="accessModifier">The <see cref="AccessModifier"/> to convert.</param>
    /// <returns>The corresponding <see cref="FilterModifiers"/></returns>
    private static FilterModifiers GetFilterModifier(AccessModifier accessModifier)
    {
      switch(accessModifier)
      {
        case AccessModifier.Default:
          return FilterModifiers.Default;
        case AccessModifier.Public:
          return FilterModifiers.Public;
        case AccessModifier.ProtectedInternal:
          return FilterModifiers.ProtectedInternal;
        case AccessModifier.Internal:
          return FilterModifiers.Internal;
        case AccessModifier.Protected:
          return FilterModifiers.Protected;
        case AccessModifier.Private:
          return FilterModifiers.Private;
        default:
          return FilterModifiers.Default;
      }
    }

    #endregion
  }
}