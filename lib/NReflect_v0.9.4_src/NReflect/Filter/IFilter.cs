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

using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;

namespace NReflect.Filter
{
  /// <summary>
  /// An instance of this class is able to decide if a type is reflected.
  /// </summary>
  public interface IFilter
  {
    /// <summary>
    /// Determines if a class will be reflected.
    /// </summary>
    /// <param name="nrClass">The class to test.</param>
    /// <returns><c>True</c> if the class should be reflected.</returns>
    bool Reflect(NRClass nrClass);

    /// <summary>
    /// Determines if an interface will be reflected.
    /// </summary>
    /// <param name="nrInterface">The interface to test.</param>
    /// <returns><c>True</c> if the interface should be reflected.</returns>
    bool Reflect(NRInterface nrInterface);

    /// <summary>
    /// Determines if a struct will be reflected.
    /// </summary>
    /// <param name="nrStruct">The struct to test.</param>
    /// <returns><c>True</c> if the struct should be reflected.</returns>
    bool Reflect(NRStruct nrStruct);

    /// <summary>
    /// Determines if a delegate will be reflected.
    /// </summary>
    /// <param name="nrDelegate">The delegate to test.</param>
    /// <returns><c>True</c> if the delegate should be reflected.</returns>
    bool Reflect(NRDelegate nrDelegate);

    /// <summary>
    /// Determines if a enum will be reflected.
    /// </summary>
    /// <param name="nrEnum">The enum to test.</param>
    /// <returns><c>True</c> if the enum should be reflected.</returns>
    bool Reflect(NREnum nrEnum);

    /// <summary>
    /// Determines if a enum value will be reflected.
    /// </summary>
    /// <param name="nrEnumValue">The enum value to test.</param>
    /// <returns><c>True</c> if the enum value should be reflected.</returns>
    bool Reflect(NREnumValue nrEnumValue);

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrConstructor">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    bool Reflect(NRConstructor nrConstructor);

    /// <summary>
    /// Determines if a method will be reflected.
    /// </summary>
    /// <param name="nrMethod">The method to test.</param>
    /// <returns><c>True</c> if the method should be reflected.</returns>
    bool Reflect(NRMethod nrMethod);

    /// <summary>
    /// Determines if an operator will be reflected.
    /// </summary>
    /// <param name="nrOperator">The operator to test.</param>
    /// <returns><c>True</c> if the operator should be reflected.</returns>
    bool Reflect(NROperator nrOperator);

    /// <summary>
    /// Determines if an event will be reflected.
    /// </summary>
    /// <param name="nrEvent">The event to test.</param>
    /// <returns><c>True</c> if the event should be reflected.</returns>
    bool Reflect(NREvent nrEvent);

    /// <summary>
    /// Determines if a field will be reflected.
    /// </summary>
    /// <param name="nrField">The field to test.</param>
    /// <returns><c>True</c> if the field should be reflected.</returns>
    bool Reflect(NRField nrField);

    /// <summary>
    /// Determines if a property will be reflected.
    /// </summary>
    /// <param name="nrProperty">The property to test.</param>
    /// <returns><c>True</c> if the property should be reflected.</returns>
    bool Reflect(NRProperty nrProperty);

    /// <summary>
    /// Determines if an attribute will be reflected.
    /// </summary>
    /// <param name="nrAttribute">The attribute to test.</param>
    /// <returns><c>True</c> if the attribute should be reflected.</returns>
    bool Reflect(NRAttribute nrAttribute);

    /// <summary>
    /// Determines if a module will be reflected.
    /// </summary>
    /// <param name="nrModule">The module to test.</param>
    /// <returns><c>True</c> if the module should be reflected.</returns>
    bool Reflect(NRModule nrModule);
  }
}