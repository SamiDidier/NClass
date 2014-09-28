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
using NReflect;
using NReflect.Filter;
using NReflect.NRRelationship;
using NReflect.Visitors;

namespace NReflect.Runner
{
  /// <summary>
  /// This is a simple example of the use of NReflect.
  /// 
  /// Call the resulting executable with one parameter: Path and name of
  /// an assembly to reflect.
  /// </summary>
  static class Program
  {
    /// <summary>
    /// Is set to true to display reflected modules.
    /// </summary>
    private static bool modules;

    /// <summary>
    /// Is set to true to display the reflected assembly.
    /// </summary>
    private static bool assembly;

    /// <summary>
    /// Is set to true to display the reflected relations.
    /// </summary>
    private static bool relations;

    /// <summary>
    /// Is set to true to display some statistics.
    /// </summary>
    private static bool statistic;

    /// <summary>
    /// The main method of the program.
    /// </summary>
    /// <param name="args">The arguments supplied at the console.</param>
    static void Main(string[] args)
    {
      string fileName = null;

      foreach(string s in args)
      {
        if(s.StartsWith("-"))
        {
          foreach(char c in s.Substring(1))
          {
            switch(c)
            {
              case 'a':
                assembly = true;
                break;
              case 'm':
                modules = true;
                break;
              case 'r':
                relations = true;
                break;
              case 's':
                statistic = true;
                break;
              default:
                PrintUsage();
                return;
            }
          }
        }
        else
        {
          if(fileName != null)
          {
            PrintUsage();
            return;
          }
          fileName = s;
        }
      }

      if (fileName == null)
      {
        PrintUsage();
        return;
      }

      Console.WriteLine("Will now reflect " + fileName);

      // Create the filter
      ReflectAllFilter allFilter = new ReflectAllFilter();

      IncludeFilter includeFilter = new IncludeFilter();
      includeFilter.Rules.Add(new FilterRule(FilterModifiers.AllModifiers, FilterElements.Class));
      includeFilter.Rules.Add(new FilterRule(FilterModifiers.AllModifiers, FilterElements.Field));
      includeFilter.Rules.Add(new FilterRule(FilterModifiers.AllModifiers, FilterElements.Attribute));

      StatisticFilter statisticFilter = new StatisticFilter(allFilter);

      // Do the reflection
      NRAssembly nrAssembly;
      IFilter filter = statisticFilter;
      try
      {
        Reflector reflector = new Reflector();
        nrAssembly = reflector.Reflect(fileName, ref filter);
      }
      catch(Exception ex)
      {
        Console.WriteLine("Exception while reflecting: " + ex.Message);
        return;
      }

      // Output of the results
      IVisitor visitor = new PrintTreeVisitor();
//      IVisitor visitor = new CSharpVisitor();
//      IVisitor visitor = new PrintVisitor();
      if (assembly)
      {
        nrAssembly.Accept(visitor);
        Console.WriteLine();
      }

      if (modules)
      {
        foreach (NRModule nrModule in nrAssembly.Modules)
        {
          nrModule.Accept(visitor);
        }
        Console.WriteLine();
      }

      if (relations)
      {
        RelationshipCreator relationshipCreator = new RelationshipCreator();
        NRRelationships nrRelationships = relationshipCreator.CreateRelationships(nrAssembly);

        Console.WriteLine("Nesting relationships:");
        foreach (NRNesting nrNestingRelationship in nrRelationships.Nestings)
        {
          Console.WriteLine(nrNestingRelationship);
        }
        Console.WriteLine();

        Console.WriteLine("Generalization relationships:");
        foreach (NRGeneralization nrGeneralizationRelationship in nrRelationships.Generalizations)
        {
          Console.WriteLine(nrGeneralizationRelationship);
        }
        Console.WriteLine();

        Console.WriteLine("Realization relationships:");
        foreach (NRRealization nrRealizationRelationship in nrRelationships.Realizations)
        {
          Console.WriteLine(nrRealizationRelationship);
        }
        Console.WriteLine();

        Console.WriteLine("Association relationships:");
        foreach (NRAssociation nrAssociationRelationship in nrRelationships.Associations)
        {
          Console.WriteLine(nrAssociationRelationship);
        }
        Console.WriteLine();
      }

      if (statistic)
      {
        statisticFilter = (StatisticFilter)filter;
        Console.WriteLine();
        Console.WriteLine("Statistic:");
        Console.WriteLine("Classes     : {0}/{1}", statisticFilter.ReflectedClasses, statisticFilter.ReflectedClasses + statisticFilter.IgnoredClasses);
        Console.WriteLine("Interfaces  : {0}/{1}", statisticFilter.ReflectedInterfaces, statisticFilter.ReflectedInterfaces + statisticFilter.IgnoredInterfaces);
        Console.WriteLine("Structures  : {0}/{1}", statisticFilter.ReflectedStructures, statisticFilter.ReflectedStructures + statisticFilter.IgnoredStructures);
        Console.WriteLine("Delegates   : {0}/{1}", statisticFilter.ReflectedDelegates, statisticFilter.ReflectedDelegates + statisticFilter.IgnoredDelegates);
        Console.WriteLine("Enums       : {0}/{1}", statisticFilter.ReflectedEnums, statisticFilter.ReflectedEnums + statisticFilter.IgnoredEnums);
        Console.WriteLine("EnumValues  : {0}/{1}", statisticFilter.ReflectedEnumValues, statisticFilter.ReflectedEnumValues + statisticFilter.IgnoredEnumValues);
        Console.WriteLine("Constructors: {0}/{1}", statisticFilter.ReflectedConstructors, statisticFilter.ReflectedConstructors + statisticFilter.IgnoredConstructors);
        Console.WriteLine("Methods     : {0}/{1}", statisticFilter.ReflectedMethods, statisticFilter.ReflectedMethods + statisticFilter.IgnoredMethods);
        Console.WriteLine("Fields      : {0}/{1}", statisticFilter.ReflectedFields, statisticFilter.ReflectedFields + statisticFilter.IgnoredFields);
        Console.WriteLine("Properties  : {0}/{1}", statisticFilter.ReflectedProperties, statisticFilter.ReflectedProperties + statisticFilter.IgnoredProperties);
        Console.WriteLine("Events      : {0}/{1}", statisticFilter.ReflectedEvents, statisticFilter.ReflectedEvents + statisticFilter.IgnoredEvents);
        Console.WriteLine("Operators   : {0}/{1}", statisticFilter.ReflectedOperators, statisticFilter.ReflectedOperators + statisticFilter.IgnoredOperators);
        Console.WriteLine("Attributes  : {0}/{1}", statisticFilter.ReflectedAttributes, statisticFilter.ReflectedAttributes + statisticFilter.IgnoredAttributes);
        Console.WriteLine("Modules     : {0}/{1}", statisticFilter.ReflectedModules, statisticFilter.ReflectedModules + statisticFilter.IgnoredModules);
      }

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey(true);
    }

    /// <summary>
    /// Prints a usage message to the screen.
    /// </summary>
    private static void PrintUsage()
    {
      Console.WriteLine("Usage:");
      Console.WriteLine("  NReflectRunner -amrs <Path to assembly>");
      Console.WriteLine("  -a display the contents of the assembly");
      Console.WriteLine("  -m display the contents of the modules of the assembyl");
      Console.WriteLine("  -r display the relations between the entities");
      Console.WriteLine("  -s display some statistical information");
    }
  }
}