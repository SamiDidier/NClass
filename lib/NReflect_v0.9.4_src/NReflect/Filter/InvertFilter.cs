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
  /// This filter inverts the filter results of another filter.
  /// </summary>
  [Serializable]
  public class InvertFilter : IFilter
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="InvertFilter"/>.
    /// </summary>
    /// <param name="filter">The filter whose filter results will be inverted.</param>
    public InvertFilter(IFilter filter)
    {
      Filter = filter;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the filter whose filter results will be inverted.
    /// </summary>
    public IFilter Filter { get; set; }

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
      return !Filter.Reflect(nrClass);
    }

    /// <summary>
    /// Determines if an interface will be reflected.
    /// </summary>
    /// <param name="nrInterface">The interface to test.</param>
    /// <returns><c>True</c> if the interface should be reflected.</returns>
    public bool Reflect(NRInterface nrInterface)
    {
      return !Filter.Reflect(nrInterface);
    }

    /// <summary>
    /// Determines if a struct will be reflected.
    /// </summary>
    /// <param name="nrStruct">The struct to test.</param>
    /// <returns><c>True</c> if the struct should be reflected.</returns>
    public bool Reflect(NRStruct nrStruct)
    {
      return !Filter.Reflect(nrStruct);
    }

    /// <summary>
    /// Determines if a delegate will be reflected.
    /// </summary>
    /// <param name="nrDelegate">The delegate to test.</param>
    /// <returns><c>True</c> if the delegate should be reflected.</returns>
    public bool Reflect(NRDelegate nrDelegate)
    {
      return !Filter.Reflect(nrDelegate);
    }

    /// <summary>
    /// Determines if a enum will be reflected.
    /// </summary>
    /// <param name="nrEnum">The enum to test.</param>
    /// <returns><c>True</c> if the enum should be reflected.</returns>
    public bool Reflect(NREnum nrEnum)
    {
      return !Filter.Reflect(nrEnum);
    }

    /// <summary>
    /// Determines if a enum value will be reflected.
    /// </summary>
    /// <param name="nrEnumValue">The enum value to test.</param>
    /// <returns><c>True</c> if the enum value should be reflected.</returns>
    public bool Reflect(NREnumValue nrEnumValue)
    {
      return !Filter.Reflect(nrEnumValue);
    }

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrMethod">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    public bool Reflect(NRMethod nrMethod)
    {
      return !Filter.Reflect(nrMethod);
    }

    /// <summary>
    /// Determines if an operator will be reflected.
    /// </summary>
    /// <param name="nrOperator">The operator to test.</param>
    /// <returns><c>True</c> if the operator should be reflected.</returns>
    public bool Reflect(NROperator nrOperator)
    {
      return !Filter.Reflect(nrOperator);
    }

    /// <summary>
    /// Determines if an event will be reflected.
    /// </summary>
    /// <param name="nrEvent">The event to test.</param>
    /// <returns><c>True</c> if the event should be reflected.</returns>
    public bool Reflect(NREvent nrEvent)
    {
      return !Filter.Reflect(nrEvent);
    }

    /// <summary>
    /// Determines if a field will be reflected.
    /// </summary>
    /// <param name="nrField">The field to test.</param>
    /// <returns><c>True</c> if the field should be reflected.</returns>
    public bool Reflect(NRField nrField)
    {
      return !Filter.Reflect(nrField);
    }

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrConstructor">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    public bool Reflect(NRConstructor nrConstructor)
    {
      return !Filter.Reflect(nrConstructor);
    }

    /// <summary>
    /// Determines if a property will be reflected.
    /// </summary>
    /// <param name="nrProperty">The property to test.</param>
    /// <returns><c>True</c> if the property should be reflected.</returns>
    public bool Reflect(NRProperty nrProperty)
    {
      return !Filter.Reflect(nrProperty);
    }

    /// <summary>
    /// Determines if an attribute will be reflected.
    /// </summary>
    /// <param name="nrAttribute">The attribute to test.</param>
    /// <returns><c>True</c> if the attribute should be reflected.</returns>
    public bool Reflect(NRAttribute nrAttribute)
    {
      return !Filter.Reflect(nrAttribute);
    }

    /// <summary>
    /// Determines if a module will be reflected.
    /// </summary>
    /// <param name="nrModule">The module to test.</param>
    /// <returns><c>True</c> if the module should be reflected.</returns>
    public bool Reflect(NRModule nrModule)
    {
      return !Filter.Reflect(nrModule);
    }

    #endregion
  }
}