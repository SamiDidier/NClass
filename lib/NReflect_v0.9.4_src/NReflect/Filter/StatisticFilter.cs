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
using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;

namespace NReflect.Filter
{
  /// <summary>
  /// This filter counts the reflected elements and those which are blocked
  /// by a child filter.
  /// </summary>
  [Serializable]
  public class StatisticFilter : IFilter
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="StatisticFilter"/>.
    /// </summary>
    /// <param name="filter">The filter whose filter results will be counted.</param>
    public StatisticFilter(IFilter filter)
    {
      Filter = filter;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the filter whose filter results will be counted.
    /// </summary>
    public IFilter Filter { get; set; }

    /// <summary>
    /// The count of reflected classes.
    /// </summary>
    public int ReflectedClasses { get; private set; }

    /// <summary>
    /// The count of classes which are not reflected by the filter.
    /// </summary>
    public int IgnoredClasses { get; private set; }

    /// <summary>
    /// The count of reflected interfaces.
    /// </summary>
    public int ReflectedInterfaces { get; private set; }

    /// <summary>
    /// The count of interfaces which are not reflected by the filter.
    /// </summary>
    public int IgnoredInterfaces { get; private set; }

    /// <summary>
    /// The count of reflected structures.
    /// </summary>
    public int ReflectedStructures { get; private set; }

    /// <summary>
    /// The count of structures which are not reflected by the filter.
    /// </summary>
    public int IgnoredStructures { get; private set; }

    /// <summary>
    /// The count of reflected delegates.
    /// </summary>
    public int ReflectedDelegates { get; private set; }

    /// <summary>
    /// The count of delegates which are not reflected by the filter.
    /// </summary>
    public int IgnoredDelegates { get; private set; }

    /// <summary>
    /// The count of reflected enums.
    /// </summary>
    public int ReflectedEnums { get; private set; }

    /// <summary>
    /// The count of enums which are not reflected by the filter.
    /// </summary>
    public int IgnoredEnums { get; private set; }

    /// <summary>
    /// The count of reflected enum Values.
    /// </summary>
    public int ReflectedEnumValues { get; private set; }

    /// <summary>
    /// The count of enum Values which are not reflected by the filter.
    /// </summary>
    public int IgnoredEnumValues { get; private set; }

    /// <summary>
    /// The count of reflected constructors.
    /// </summary>
    public int ReflectedConstructors { get; private set; }

    /// <summary>
    /// The count of constructors which are not reflected by the filter.
    /// </summary>
    public int IgnoredConstructors { get; private set; }

    /// <summary>
    /// The count of reflected methods.
    /// </summary>
    public int ReflectedMethods { get; private set; }

    /// <summary>
    /// The count of methods which are not reflected by the filter.
    /// </summary>
    public int IgnoredMethods { get; private set; }

    /// <summary>
    /// The count of reflected operators.
    /// </summary>
    public int ReflectedOperators { get; private set; }

    /// <summary>
    /// The count of operators which are not reflected by the filter.
    /// </summary>
    public int IgnoredOperators { get; private set; }

    /// <summary>
    /// The count of reflected events.
    /// </summary>
    public int ReflectedEvents { get; private set; }

    /// <summary>
    /// The count of events which are not reflected by the filter.
    /// </summary>
    public int IgnoredEvents { get; private set; }

    /// <summary>
    /// The count of reflected fields.
    /// </summary>
    public int ReflectedFields { get; private set; }

    /// <summary>
    /// The count of fields which are not reflected by the filter.
    /// </summary>
    public int IgnoredFields { get; private set; }

    /// <summary>
    /// The count of reflected properties.
    /// </summary>
    public int ReflectedProperties { get; private set; }

    /// <summary>
    /// The count of properties which are not reflected by the filter.
    /// </summary>
    public int IgnoredProperties { get; private set; }

    /// <summary>
    /// The count of reflected attributes.
    /// </summary>
    public int ReflectedAttributes { get; private set; }

    /// <summary>
    /// The count of attributes which are not reflected by the filter.
    /// </summary>
    public int IgnoredAttributes { get; private set; }

    /// <summary>
    /// The count of reflected modules.
    /// </summary>
    public int ReflectedModules { get; private set; }

    /// <summary>
    /// The count of modules which are not reflected by the filter.
    /// </summary>
    public int IgnoredModules { get; private set; }

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
      if (Filter.Reflect(nrClass))
      {
        ReflectedClasses++;
        return true;
      }
      IgnoredClasses++;
      return false;
    }

    /// <summary>
    /// Determines if an interface will be reflected.
    /// </summary>
    /// <param name="nrInterface">The interface to test.</param>
    /// <returns><c>True</c> if the interface should be reflected.</returns>
    public bool Reflect(NRInterface nrInterface)
    {
      if(Filter.Reflect(nrInterface))
      {
        ReflectedInterfaces++;
        return true;
      }
      IgnoredInterfaces++;
      return false;
    }

    /// <summary>
    /// Determines if a struct will be reflected.
    /// </summary>
    /// <param name="nrStruct">The struct to test.</param>
    /// <returns><c>True</c> if the struct should be reflected.</returns>
    public bool Reflect(NRStruct nrStruct)
    {
      if(Filter.Reflect(nrStruct))
      {
        ReflectedStructures++;
        return true;
      }
      IgnoredStructures++;
      return false;
    }

    /// <summary>
    /// Determines if a delegate will be reflected.
    /// </summary>
    /// <param name="nrDelegate">The delegate to test.</param>
    /// <returns><c>True</c> if the delegate should be reflected.</returns>
    public bool Reflect(NRDelegate nrDelegate)
    {
      if(Filter.Reflect(nrDelegate))
      {
        ReflectedDelegates++;
        return true;
      }
      IgnoredDelegates++;
      return false;
    }

    /// <summary>
    /// Determines if a enum will be reflected.
    /// </summary>
    /// <param name="nrEnum">The enum to test.</param>
    /// <returns><c>True</c> if the enum should be reflected.</returns>
    public bool Reflect(NREnum nrEnum)
    {
      if(Filter.Reflect(nrEnum))
      {
        ReflectedEnums++;
        return true;
      }
      IgnoredEnums++;
      return false;
    }

    /// <summary>
    /// Determines if a enum value will be reflected.
    /// </summary>
    /// <param name="nrEnumValue">The enum value to test.</param>
    /// <returns><c>True</c> if the enum value should be reflected.</returns>
    public bool Reflect(NREnumValue nrEnumValue)
    {
      if(Filter.Reflect(nrEnumValue))
      {
        ReflectedEnumValues++;
        return true;
      }
      IgnoredEnumValues++;
      return false;
    }

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrConstructor">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    public bool Reflect(NRConstructor nrConstructor)
    {
      if(Filter.Reflect(nrConstructor))
      {
        ReflectedConstructors++;
        return true;
      }
      IgnoredConstructors++;
      return false;
    }

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrMethod">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    public bool Reflect(NRMethod nrMethod)
    {
      if(Filter.Reflect(nrMethod))
      {
        ReflectedMethods++;
        return true;
      }
      IgnoredMethods++;
      return false;
    }

    /// <summary>
    /// Determines if an operator will be reflected.
    /// </summary>
    /// <param name="nrOperator">The operator to test.</param>
    /// <returns><c>True</c> if the operator should be reflected.</returns>
    public bool Reflect(NROperator nrOperator)
    {
      if(Filter.Reflect(nrOperator))
      {
        ReflectedOperators++;
        return true;
      }
      IgnoredOperators++;
      return false;
    }

    /// <summary>
    /// Determines if an event will be reflected.
    /// </summary>
    /// <param name="nrEvent">The event to test.</param>
    /// <returns><c>True</c> if the event should be reflected.</returns>
    public bool Reflect(NREvent nrEvent)
    {
      if(Filter.Reflect(nrEvent))
      {
        ReflectedEvents++;
        return true;
      }
      IgnoredEvents++;
      return false;
    }

    /// <summary>
    /// Determines if a field will be reflected.
    /// </summary>
    /// <param name="nrField">The field to test.</param>
    /// <returns><c>True</c> if the field should be reflected.</returns>
    public bool Reflect(NRField nrField)
    {
      if(Filter.Reflect(nrField))
      {
        ReflectedFields++;
        return true;
      }
      IgnoredFields++;
      return false;
    }

    /// <summary>
    /// Determines if a property will be reflected.
    /// </summary>
    /// <param name="nrProperty">The property to test.</param>
    /// <returns><c>True</c> if the property should be reflected.</returns>
    public bool Reflect(NRProperty nrProperty)
    {
      if(Filter.Reflect(nrProperty))
      {
        ReflectedProperties++;
        return true;
      }
      IgnoredProperties++;
      return false;
    }

    /// <summary>
    /// Determines if an attribute will be reflected.
    /// </summary>
    /// <param name="nrAttribute">The attribute to test.</param>
    /// <returns><c>True</c> if the attribute should be reflected.</returns>
    public bool Reflect(NRAttribute nrAttribute)
    {
      if (Filter.Reflect(nrAttribute))
      {
        ReflectedAttributes++;
        return true;
      }
      IgnoredAttributes++;
      return false;
    }

    /// <summary>
    /// Determines if a module will be reflected.
    /// </summary>
    /// <param name="nrModule">The module to test.</param>
    /// <returns><c>True</c> if the module should be reflected.</returns>
    public bool Reflect(NRModule nrModule)
    {
      if(Filter.Reflect(nrModule))
      {
        ReflectedModules++;
        return true;
      }
      IgnoredModules++;
      return false;
    }

    #endregion
  }
}