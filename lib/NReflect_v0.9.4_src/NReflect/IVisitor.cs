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

using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;
using NReflect.NRParameters;

namespace NReflect
{
  /// <summary>
  /// Classes implementing this interface are able to visit entities of NReflect.
  /// </summary>
  public interface IVisitor
  {
    /// <summary>
    /// Visit a <see cref="NRAssembly"/>.
    /// </summary>
    /// <param name="nrAssembly">The <see cref="NRAssembly"/> to visit.</param>
    void Visit(NRAssembly nrAssembly);

    /// <summary>
    /// Visit a <see cref="NRClass"/>.
    /// </summary>
    /// <param name="nrClass">The <see cref="NRClass"/> to visit.</param>
    void Visit(NRClass nrClass);

    /// <summary>
    /// Visit a <see cref="NRInterface"/>.
    /// </summary>
    /// <param name="nrInterface">The <see cref="NRInterface"/> to visit.</param>
    void Visit(NRInterface nrInterface);

    /// <summary>
    /// Visit a <see cref="NRDelegate"/>.
    /// </summary>
    /// <param name="nrDelegate">The <see cref="NRDelegate"/> to visit.</param>
    void Visit(NRDelegate nrDelegate);

    /// <summary>
    /// Visit a <see cref="NRStruct"/>.
    /// </summary>
    /// <param name="nrStruct">The <see cref="NRStruct"/> to visit.</param>
    void Visit(NRStruct nrStruct);

    /// <summary>
    /// Visit a <see cref="NREnum"/>.
    /// </summary>
    /// <param name="nrEnum">The <see cref="NREnum"/> to visit.</param>
    void Visit(NREnum nrEnum);

    /// <summary>
    /// Visit a <see cref="NRField"/>.
    /// </summary>
    /// <param name="nrField">The <see cref="NRField"/> to visit.</param>
    void Visit(NRField nrField);

    /// <summary>
    /// Visit a <see cref="NRProperty"/>.
    /// </summary>
    /// <param name="nrProperty">The <see cref="NRProperty"/> to visit.</param>
    void Visit(NRProperty nrProperty);

    /// <summary>
    /// Visit a <see cref="NRMethod"/>.
    /// </summary>
    /// <param name="nrMethod">The <see cref="NRMethod"/> to visit.</param>
    void Visit(NRMethod nrMethod);

    /// <summary>
    /// Visit a <see cref="NROperator"/>.
    /// </summary>
    /// <param name="nrOperator">The <see cref="NROperator"/> to visit.</param>
    void Visit(NROperator nrOperator);

    /// <summary>
    /// Visit a <see cref="NRConstructor"/>.
    /// </summary>
    /// <param name="nrConstructor">The <see cref="NRConstructor"/> to visit.</param>
    void Visit(NRConstructor nrConstructor);

    /// <summary>
    /// Visit a <see cref="NREvent"/>.
    /// </summary>
    /// <param name="nrEvent">The <see cref="NREvent"/> to visit.</param>
    void Visit(NREvent nrEvent);

    /// <summary>
    /// Visit a <see cref="NRParameter"/>.
    /// </summary>
    /// <param name="nrParameter">The <see cref="NRParameter"/> to visit.</param>
    void Visit(NRParameter nrParameter);

    /// <summary>
    /// Visit a <see cref="NRTypeParameter"/>.
    /// </summary>
    /// <param name="nrTypeParameter">The <see cref="NRTypeParameter"/> to visit.</param>
    void Visit(NRTypeParameter nrTypeParameter);

    /// <summary>
    /// Visit a <see cref="NREnumValue"/>.
    /// </summary>
    /// <param name="nrEnumValue">The <see cref="NREnumValue"/> to visit.</param>
    void Visit(NREnumValue nrEnumValue);

    /// <summary>
    /// Visit a <see cref="NRAttribute"/>.
    /// </summary>
    /// <param name="nrAttribute">The <see cref="NRAttribute"/> to visit.</param>
    void Visit(NRAttribute nrAttribute);

    /// <summary>
    /// Visit a <see cref="NRModule"/>.
    /// </summary>
    /// <param name="nrModule">The <see cref="NRModule"/> to visit.</param>
    void Visit(NRModule nrModule);

    /// <summary>
    /// Visit a <see cref="NRTypeUsage"/>.
    /// </summary>
    /// <param name="nrTypeUsage">The <see cref="NRTypeUsage"/> to visit.</param>
    void Visit(NRTypeUsage nrTypeUsage);
  }
}