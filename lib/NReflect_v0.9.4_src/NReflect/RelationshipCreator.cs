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
using System.Linq;
using NReflect.NREntities;
using NReflect.NRMembers;
using NReflect.NRRelationship;

namespace NReflect
{
  /// <summary>
  /// An instance of this class is able to extract the relationships between the
  /// reflected types from a <see cref="NRAssembly"/>.
  /// </summary>
  public class RelationshipCreator
  {
    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Extracts the relationships between the types of <paramref name="nrAssembly"/>.
    /// </summary>
    /// <param name="nrAssembly">The relationships are extracted from the types within
    ///                          this <see cref="NRAssembly"/>.</param>
    /// <param name="createNesting">Set to <c>true</c> to create nesting relationships.</param>
    /// <param name="createGeneralization">Set to <c>true</c> to create generalization relationships.</param>
    /// <param name="createRealization">Set to <c>true</c> to create realization relationships.</param>
    /// <param name="createAssociation">Set to <c>true</c> to create association relationships.</param>
    /// <returns>The extracted relationships.</returns>
    public NRRelationships CreateRelationships(NRAssembly nrAssembly, bool createNesting = true, bool createGeneralization = true, bool createRealization = true, bool createAssociation = true)
    {
      NRRelationships nrRelationships = new NRRelationships();
      Dictionary<string, NRTypeBase> entities = nrAssembly.Types.ToDictionary(nrTypeBase => nrTypeBase.FullName);

      //Create the nesting relationships
      if(createNesting)
      {
        foreach (NRTypeBase nrTypeBase in entities.Values)
        {
          if (!String.IsNullOrWhiteSpace(nrTypeBase.DeclaringTypeFullName))
          {
            if (entities.ContainsKey(nrTypeBase.DeclaringTypeFullName))
            {
              NRSingleInheritanceType parent = entities[nrTypeBase.DeclaringTypeFullName] as NRSingleInheritanceType;
              if (parent != null)
              {
                nrRelationships.Nestings.Add(new NRNesting(parent, nrTypeBase));
              }
            }
          }
        }
      }

      //Create the generalization relationships
      if(createGeneralization)
      {
        foreach (NRSingleInheritanceType derivedType in nrAssembly.SingleInheritanceTypes)
        {
          if (derivedType.BaseType != null && derivedType.BaseType.FullName != null)
          {
            if (entities.ContainsKey(derivedType.BaseType.FullName))
            {
              NRSingleInheritanceType baseType = entities[derivedType.BaseType.FullName] as NRSingleInheritanceType;
              if(baseType != null)
              {
                nrRelationships.Generalizations.Add(new NRGeneralization(baseType, derivedType));
              }
            }
          }
        }

        // Interfaces may derive from other interfaces as well.
        foreach(NRInterface derivedInterface in nrAssembly.Interfaces)
        {
          foreach(NRTypeUsage implementedInterface in derivedInterface.ImplementedInterfaces)
          {
            if(entities.ContainsKey(implementedInterface.FullName))
            {
              NRInterface nrInterface = entities[implementedInterface.FullName] as NRInterface;
              if(nrInterface != null)
              {
                nrRelationships.Generalizations.Add(new NRGeneralization(nrInterface, derivedInterface));
              }
            }
          }
        }
      }

      //Create the realization relationships
      if(createRealization)
      {
        foreach (NRSingleInheritanceType implementingType in nrAssembly.SingleInheritanceTypes)
        {
          foreach (NRTypeUsage implementedInterface in implementingType.ImplementedInterfaces)
          {
            if (entities.ContainsKey(implementedInterface.FullName))
            {
              NRInterface nrInterface = entities[implementedInterface.FullName] as NRInterface;
              if (nrInterface != null)
              {
                nrRelationships.Realizations.Add(new NRRealization(nrInterface, implementingType));
              }
            }
          }
        }
      }

      //Create the association relationships
      if(createAssociation)
      {
        foreach (NRSingleInheritanceType startType in nrAssembly.SingleInheritanceTypes)
        {
          foreach (NRField nrField in startType.Fields)
          {
            string fullName = nrField.TypeFullName;
            bool array = false;
            if (fullName.EndsWith("[]"))
            {
              //Array!
              fullName = fullName.Substring(0, fullName.IndexOf('['));
              array = true;
            }
            if (fullName.Contains("["))
            {
              //Generic!
              fullName = fullName.Substring(0, fullName.IndexOf('['));
            }
            if (entities.ContainsKey(fullName))
            {
              NRTypeBase endType = entities[fullName];
              NRAssociation association = new NRAssociation
              {
                StartType = startType,
                EndMultiplicity = array ? "*" : "1",
                StartRole = nrField.Name,
                EndType = endType
              };
              nrRelationships.Associations.Add(association);
            }
          }
        }
      }

      return nrRelationships;
    }

    #endregion
  }
}