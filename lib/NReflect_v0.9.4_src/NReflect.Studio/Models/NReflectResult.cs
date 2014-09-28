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

namespace NReflect.Studio.Models
{
  /// <summary>
  /// This class contains the results a reflection.
  /// </summary>
  [Serializable]
  public class NReflectResult
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="NReflectResult"/>.
    /// </summary>
    public NReflectResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NReflectResult"/>.
    /// </summary>
    /// <param name="nrAssembly">The assembly of the result.</param>
    /// <param name="nrRelationships">The relationships of the result.</param>
    public NReflectResult(NRAssembly nrAssembly, NRRelationships nrRelationships)
    {
      NRAssembly = nrAssembly;
      NRRelationships = nrRelationships;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the assembly of the reflection.
    /// </summary>
    public NRAssembly NRAssembly { get; set; }
    /// <summary>
    /// Gets or sets the relationships of the reflection.
    /// </summary>
    public NRRelationships NRRelationships { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods


    #endregion

    // ========================================================================
    // Events

    #region === Events


    #endregion
  }
}