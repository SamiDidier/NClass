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
using NReflect.NRRelationship;

namespace NReflect
{
  /// <summary>
  /// An instance of this class contains all found relationships between the reflected types
  /// of a <see cref="NRAssembly"/>.
  /// </summary>
  [Serializable]
  public class NRRelationships
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRRelationships"/>.
    /// </summary>
    public NRRelationships()
    {
      Nestings = new List<NRNesting>();
      Generalizations = new List<NRGeneralization>();
      Realizations = new List<NRRealization>();
      Associations = new List<NRAssociation>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets a list of nesting relationships.
    /// </summary>
    public List<NRNesting> Nestings { get; private set; }

    /// <summary>
    /// Gets a list of generalization relationships.
    /// </summary>
    public List<NRGeneralization> Generalizations { get; private set; }

    /// <summary>
    /// Gets a list of realization relationships.
    /// </summary>
    public List<NRRealization> Realizations { get; private set; }

    /// <summary>
    /// Gets a list of association relationships.
    /// </summary>
    public List<NRAssociation> Associations { get; private set; }

    #endregion
  }
}