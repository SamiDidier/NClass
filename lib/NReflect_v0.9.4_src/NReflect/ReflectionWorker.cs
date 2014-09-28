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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using NReflect.Filter;
using NReflect.Modifier;
using NReflect.NRAttributes;
using NReflect.NREntities;
using NReflect.NRMembers;
using NReflect.NRParameters;
using ParameterModifier = NReflect.Modifier.ParameterModifier;

namespace NReflect
{
  /// <summary>
  /// This is the main class of the NReflect library.
  /// </summary>
  internal class ReflectionWorker
  {
    // ========================================================================
    // Constants

    #region === Constants

    /// <summary>
    /// Bindingflags which reflect every member.
    /// </summary>
    private const BindingFlags STANDARD_BINDING_FLAGS = BindingFlags.NonPublic |
                                                        BindingFlags.Public |
                                                        BindingFlags.Static |
                                                        BindingFlags.Instance;

    #endregion

    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The path of the assembly to import.
    /// </summary>
    private string path;

    /// <summary>
    /// Assemblies at the folder of the imported assembly. Only used if the imported
    /// assembly references an assembly which can't be loaded by the CLR.
    /// </summary>
    private Dictionary<String, Assembly> assemblies;

    /// <summary>
    /// Takes mappings from the full qualified name of a type to the generated NClass-
    /// entity.
    /// </summary>
    private readonly Dictionary<Type, NRTypeBase> entities;

    /// <summary>
    /// Mapping from operator-method to operator.
    /// </summary>
    private readonly Dictionary<string, OperatorType> operatorMethodsMap = new Dictionary<string, OperatorType>();

    #endregion

    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="ReflectionWorker"/>.
    /// </summary>
    public ReflectionWorker()
    {
      entities = new Dictionary<Type, NRTypeBase>();
      Filter = new ReflectAllFilter();

      operatorMethodsMap.Add("op_UnaryPlus", OperatorType.UnaryPlus);
      operatorMethodsMap.Add("op_UnaryNegation", OperatorType.UnaryNegation);
      operatorMethodsMap.Add("op_LogicalNot", OperatorType.LogicalNot);
      operatorMethodsMap.Add("op_OnesComplement", OperatorType.OnesComplement);
      operatorMethodsMap.Add("op_Increment", OperatorType.Increment);
      operatorMethodsMap.Add("op_Decrement", OperatorType.Decrement);
      operatorMethodsMap.Add("op_True", OperatorType.True);
      operatorMethodsMap.Add("op_False", OperatorType.False);
      operatorMethodsMap.Add("op_Addition", OperatorType.Addition);
      operatorMethodsMap.Add("op_Subtraction", OperatorType.Subtraction);
      operatorMethodsMap.Add("op_Multiply", OperatorType.Multiply);
      operatorMethodsMap.Add("op_Division", OperatorType.Division);
      operatorMethodsMap.Add("op_Modulus", OperatorType.Modulus);
      operatorMethodsMap.Add("op_BitwiseAnd", OperatorType.BitwiseAnd);
      operatorMethodsMap.Add("op_BitwiseOr", OperatorType.BitwiseOr);
      operatorMethodsMap.Add("op_ExclusiveOr", OperatorType.ExclusiveOr);
      operatorMethodsMap.Add("op_LeftShift", OperatorType.LeftShift);
      operatorMethodsMap.Add("op_RightShift", OperatorType.RightShift);
      operatorMethodsMap.Add("op_Equality", OperatorType.Equality);
      operatorMethodsMap.Add("op_Inequality", OperatorType.Inequality);
      operatorMethodsMap.Add("op_LessThan", OperatorType.LessThan);
      operatorMethodsMap.Add("op_GreaterThan", OperatorType.GreaterThan);
      operatorMethodsMap.Add("op_LessThanOrEqual", OperatorType.LessThanOrEqual);
      operatorMethodsMap.Add("op_GreaterThanOrEqual", OperatorType.GreaterThanOrEqual);
      operatorMethodsMap.Add("op_Implicit", OperatorType.Implicit);
      operatorMethodsMap.Add("op_Explicit", OperatorType.Explicit);
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the type filter used to determine which types and members to reflect.
    /// </summary>
    public IFilter Filter { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Reflects the types of an assembly.
    /// </summary>
    /// <param name="fileName">The file name of the assembly to reflect.</param>
    /// <returns>The result of the reflection.</returns>
    public NRAssembly Reflect(string fileName)
    {
      path = Path.GetDirectoryName(fileName);
      AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
      Assembly assembly = Assembly.ReflectionOnlyLoadFrom(fileName);

      NRAssembly nrAssembly = Reflect(assembly);
      nrAssembly.Source = fileName;
      return nrAssembly;
    }

    /// <summary>
    /// Reflects the types of an assembly.
    /// </summary>
    /// <param name="assembly">The assembly to reflect.</param>
    /// <returns>The result of the reflection.</returns>
    public NRAssembly Reflect(Assembly assembly)
    {
      NRAssembly nrAssembly = new NRAssembly
                                {
                                  FullName = assembly.FullName,
                                  Source = "<Memory>"
                                };

      ReflectAttributes(CustomAttributeData.GetCustomAttributes(assembly), nrAssembly);
      ReflectTypes(assembly.GetTypes(), nrAssembly);
      ReflectModules(assembly.GetModules(), nrAssembly);

      return nrAssembly;
    }

    /// <summary>
    /// Reflect every given module.
    /// </summary>
    /// <param name="modules">The modules to reflect.</param>
    /// <param name="nrAssembly">This assembly will get the reflection result.</param>
    private void ReflectModules(IEnumerable<Module> modules, NRAssembly nrAssembly)
    {
      foreach(Module module in modules)
      {
        NRModule nrModule = new NRModule
                              {
                                Name = module.Name
                              };

        ReflectAttributes(CustomAttributeData.GetCustomAttributes(module), nrModule);
        ReflectTypes(module.GetTypes(), nrModule);
        foreach(FieldInfo fieldInfo in module.GetFields())
        {
          ReflectField(fieldInfo, nrModule);
        }
        foreach(MethodInfo methodInfo in module.GetMethods())
        {
          ReflectMethod(methodInfo, nrModule);
        }

        // Ask the filter if the module should be present in the result.
        if(Filter.Reflect(nrModule))
        {
          nrAssembly.Modules.Add(nrModule);
        }
      }
    }

    /// <summary>
    /// Find an assembly with the given full name. Called by the CLR if an assembly is
    /// loaded into the ReflectionOnlyContext and a referenced assebmly is needed.
    /// </summary>
    /// <param name="sender">The source of this event.</param>
    /// <param name="args">More information about the event.</param>
    /// <returns>The loaded assembly.</returns>
    private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
    {
      try
      {
        return Assembly.ReflectionOnlyLoad(args.Name);
      }
      catch(FileNotFoundException)
      {
        //No global assembly: try loading it from the current dir.
        if(assemblies == null)
        {
          assemblies = new Dictionary<string, Assembly>();

          // Lazily load all assemblies from the path
          List<string> files = new List<string>();
          files.AddRange(Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly));
          files.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly));
          foreach(string file in files)
          {
            try
            {
              Assembly assembly = Assembly.ReflectionOnlyLoadFrom(file);
              assemblies.Add(assembly.FullName, assembly);
            }
            catch
            {
              // The assembly can't be loaded. Maybe this is no CLR assembly.
            }
          }
        }
        return assemblies.ContainsKey(args.Name) ? assemblies[args.Name] : null;
      }
    }

    #region +++ Reflect entities

    /// <summary>
    /// Reflect all given types and create NReflect-entities.
    /// </summary>
    /// <param name="types">An array of types to reflect.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the types to.</param>
    private void ReflectTypes(IEnumerable<Type> types, IEntityContainer entityContainer)
    {
      foreach(Type type in types)
      {
        //There are some compiler generated nested classes which should not
        //be reflected. All these magic classes have the CompilerGeneratedAttribute.
        //The C#-compiler of the .NET 2.0 Compact Framework creates the classes
        //but dosn't mark them with the attribute. All classes have a "<" in their name.
        if(HasMemberCompilerGeneratedAttribute(type) || type.Name.Contains("<"))
        {
          continue;
        }
        ReflectType(type, entityContainer);
      }
    }

    /// <summary>
    /// Reflect a given type and create the corresponding NReflect entity.
    /// </summary>
    /// <param name="type">The type to reflect.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the type to.</param>
    private void ReflectType(Type type, IEntityContainer entityContainer)
    {
      if(type == null)
      {
        // Type is null - nothing to do.
        return;
      }

      if(type.IsClass)
      {
        //Could be a delegate
        if(type.BaseType == typeof(MulticastDelegate))
        {
          ReflectDelegate(type, entityContainer);
        }
        else
        {
          ReflectClass(type, entityContainer);
        }
      }
      if(type.IsInterface)
      {
        ReflectInterface(type, entityContainer);
      }
      if(type.IsEnum)
      {
        ReflectEnum(type, entityContainer);
      }
      if(type.IsValueType && !type.IsEnum)
      {
        ReflectStruct(type, entityContainer);
      }
    }

    /// <summary>
    /// Reflects the class <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A type with informations about the class which gets reflected.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the class to.</param>
    private void ReflectClass(Type type, IEntityContainer entityContainer)
    {
      NRClass nrClass;
      if(entities.ContainsKey(type))
      {
        //Class is already reflected - use the old one.
        nrClass = (NRClass)entities[type];
      }
      else
      {
        nrClass = new NRClass();

        ReflectEvents(type, nrClass);
        ReflectFields(type, nrClass);
        ReflectProperties(type, nrClass);
        ReflectConstructors(type, nrClass);
        ReflectMethods(type, nrClass);

        ReflectSingleInheritanceType(type, nrClass);
        ReflectCompositeType(type, nrClass);
        ReflectGenericType(type, nrClass);
        ReflectTypeBase(type, nrClass);

        if(type.IsAbstract && type.IsSealed)
        {
          nrClass.ClassModifier = ClassModifier.Static;
        }
        else if(type.IsAbstract)
        {
          nrClass.ClassModifier = ClassModifier.Abstract;
        }
        else if(type.IsSealed)
        {
          nrClass.ClassModifier = ClassModifier.Sealed;
        }
      }

      //Ask the filter if the class should be in the result.
      if(Filter.Reflect(nrClass))
      {
        entityContainer.Classes.Add(nrClass);
      }
    }

    /// <summary>
    /// Reflects the struct <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A type with informations about the struct which gets reflected.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the struct to.</param>
    private void ReflectStruct(Type type, IEntityContainer entityContainer)
    {
      NRStruct nrStruct;
      if(entities.ContainsKey(type))
      {
        //Struct is already reflected - use the old one.
        nrStruct = (NRStruct)entities[type];
      }
      else
      {
        nrStruct = new NRStruct();
        ReflectEvents(type, nrStruct);
        ReflectFields(type, nrStruct);
        ReflectProperties(type, nrStruct);
        ReflectConstructors(type, nrStruct);
        ReflectMethods(type, nrStruct);

        ReflectSingleInheritanceType(type, nrStruct);
        ReflectCompositeType(type, nrStruct);
        ReflectGenericType(type, nrStruct);
        ReflectTypeBase(type, nrStruct);
      }

      //Ask the filter if the struct should be in the result.
      if(Filter.Reflect(nrStruct))
      {
        entityContainer.Structs.Add(nrStruct);
      }
    }

    /// <summary>
    /// Reflects the interface <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A type with informations about the interface which gets reflected.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the interface to.</param>
    private void ReflectInterface(Type type, IEntityContainer entityContainer)
    {
      NRInterface nrInterface;
      if(entities.ContainsKey(type))
      {
        //Interface is already reflected - use the old one.
        nrInterface = (NRInterface)entities[type];
      }
      else
      {
        nrInterface = new NRInterface();
        ReflectEvents(type, nrInterface);
        ReflectProperties(type, nrInterface);
        ReflectMethods(type, nrInterface);

        ReflectCompositeType(type, nrInterface);
        ReflectGenericType(type, nrInterface);
        ReflectTypeBase(type, nrInterface);
      }

      //Ask the filter if the interface should be int the result.
      if(Filter.Reflect(nrInterface))
      {
        entityContainer.Interfaces.Add(nrInterface);
      }
    }

    /// <summary>
    /// Reflects the delegate <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A type with informations about the delegate which gets reflected.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the delgate to.</param>
    private void ReflectDelegate(Type type, IEntityContainer entityContainer)
    {
      NRDelegate nrDelegate;
      if(entities.ContainsKey(type))
      {
        //Delegate is already reflected - use the old one.
        nrDelegate = (NRDelegate)entities[type];
      }
      else
      {
        MethodInfo methodInfo = type.GetMethod("Invoke");

        nrDelegate = new NRDelegate
                       {
                         ReturnType = GetTypeUsage(methodInfo.ReturnType, methodInfo)
                       };
        ReflectParameters(methodInfo, nrDelegate.Parameters);

        ReflectGenericType(type, nrDelegate);
        ReflectTypeBase(type, nrDelegate);
      }

      //Ask the filter if the delegate should be in the result.
      if(Filter.Reflect(nrDelegate))
      {
        entityContainer.Delegates.Add(nrDelegate);
      }
    }

    /// <summary>
    /// Reflects the enum <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A type with informations about the enum which gets reflected.</param>
    /// <param name="entityContainer">An instance of <see cref="IEntityContainer"/> to add the enum to.</param>
    private void ReflectEnum(Type type, IEntityContainer entityContainer)
    {
      NREnum nrEnum;
      if(entities.ContainsKey(type))
      {
        //Enum is already reflected - use the old one.
        nrEnum = (NREnum)entities[type];
      }
      else
      {
        nrEnum = new NREnum
                   {
                     UnderlyingType = type.GetEnumUnderlyingType().FullName
                   };
        FieldInfo[] fields = type.GetFields(STANDARD_BINDING_FLAGS);
        foreach(FieldInfo field in fields)
        {
          //Sort this special field out
          if(field.Name == "value__")
          {
            continue;
          }
          NREnumValue nrEnumValue = new NREnumValue
                                      {
                                        Name = field.Name
                                      };
          object rawConstantValue = field.GetRawConstantValue();
          if(rawConstantValue != null)
          {
            nrEnumValue.Value = rawConstantValue.ToString();
          }
          ReflectAttributes(CustomAttributeData.GetCustomAttributes(field), nrEnumValue);

          // Ask the filter if the field should be reflected
          if(Filter.Reflect(nrEnumValue))
          {
            nrEnum.Values.Add(nrEnumValue);
          }
        }
        ReflectTypeBase(type, nrEnum);
      }

      //Ask the filter if the enum should be in the result.
      if(Filter.Reflect(nrEnum))
      {
        entityContainer.Enums.Add(nrEnum);
      }
    }

    /// <summary>
    /// Reflect the information of a single inheritance type like a class or a struct.
    /// </summary>
    /// <param name="type">The information is taken from <paramref name="type"/>.</param>
    /// <param name="nrSingleInheritanceType">All information is stored in this object.</param>
    private void ReflectSingleInheritanceType(Type type, NRSingleInheritanceType nrSingleInheritanceType)
    {
      if(type.BaseType != null)
      {
        nrSingleInheritanceType.BaseType = GetTypeUsage(type.BaseType, type);
      }
    }

    /// <summary>
    /// Reflect the information of a composite type like a class, a struct or an interface.
    /// </summary>
    /// <param name="type">The information is taken from <paramref name="type"/>.</param>
    /// <param name="nrCompositeType">All information is stored in this object.</param>
    private void ReflectCompositeType(Type type, NRCompositeType nrCompositeType)
    {
      List<Type> interfacesOfBase = new List<Type>();
      if(type.BaseType != null)
      {
        // Get implemented interfaces of base class
        interfacesOfBase.AddRange(type.BaseType.GetInterfaces());
      }
      foreach(Type implementedInterface in type.GetInterfaces())
      {
        // Get implemented interfaces of implemented interfaces
        foreach(Type baseInterface in implementedInterface.GetInterfaces())
        {
          if(!interfacesOfBase.Contains(baseInterface))
          {
            interfacesOfBase.Add(baseInterface);
          }
        }
      }
      foreach(Type implementedInterface in type.GetInterfaces().Where(i => !interfacesOfBase.Contains(i)))
      {
        nrCompositeType.ImplementedInterfaces.Add(GetTypeUsage(implementedInterface, type));
      }
    }

    /// <summary>
    /// Reflects a generic type.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to reflect.</param>
    /// <param name="nrGenericType">An instance of <see cref="NRGenericType"/> which will
    /// receive the reflected results.</param>
    private void ReflectGenericType(Type type, NRGenericType nrGenericType)
    {
      if(!type.IsGenericType)
      {
        return;
      }

      int parentGenericArgsCount = 0;
      if(type.DeclaringType != null)
      {
        // This is a nested type. If the declaring type is also a generic type,
        // the generic parameters of the declaring type are at the list, too. We
        // have to handle this so they don't appear more than once at the result.
        parentGenericArgsCount = type.DeclaringType.GetGenericArguments().Length;
      }
      nrGenericType.GenericTypes.AddRange(GetTypeParameters(type.GetGenericArguments().Skip(parentGenericArgsCount)));
    }

    /// <summary>
    /// Reflect the basic type <paramref name="type"/>. All information is
    /// stored in <paramref name="nrTypeBase"/>.
    /// </summary>
    /// <param name="type">The information is taken from <paramref name="type"/>.</param>
    /// <param name="nrTypeBase">All information is stored in this TypeBase.</param>
    private void ReflectTypeBase(Type type, NRTypeBase nrTypeBase)
    {
      nrTypeBase.Name = GetRawTypeName(type);
      nrTypeBase.Namespace = type.Namespace;
      nrTypeBase.FullName = type.FullName;
      //Might set the wrong access modifier for nested classes. Will be
      //corrected when adding the nesting relationships.
      nrTypeBase.AccessModifier = GetTypeAccessModifier(type);

      ReflectAttributes(CustomAttributeData.GetCustomAttributes(type), nrTypeBase);

      //Fill up the dictionaries
      if(type.FullName != null)
      {
        entities.Add(type, nrTypeBase);
      }
      if(type.IsNested && type.DeclaringType != null)
      {
        nrTypeBase.DeclaringTypeFullName = type.DeclaringType.FullName;
      }
    }

    #endregion

    #region +++ Reflect members of types

    /// <summary>
    /// Reflects all events within the type <paramref name="type"/>. Reflected
    /// events are added to <paramref name="nrCompositeType"/>.
    /// </summary>
    /// <param name="type">The events are taken from this type.</param>
    /// <param name="nrCompositeType">Reflected events are added to this FieldContainer.</param>
    private void ReflectEvents(Type type, NRCompositeType nrCompositeType)
    {
      EventInfo[] eventInfos = type.GetEvents(STANDARD_BINDING_FLAGS);
      foreach(EventInfo eventInfo in eventInfos)
      {
        //Don't reflect derived events.
        if(eventInfo.DeclaringType != type)
        {
          continue;
        }
        //The access modifier isn't stored at the event. So we have to take
        //that from the corresponding add_XXX (or perhaps remove_XXX) method.
        MethodInfo xAddMethodInfo = eventInfo.GetAddMethod(true);
        NREvent nrEvent = new NREvent
                            {
                              Name = eventInfo.Name,
                              Type = GetTypeUsage(eventInfo.EventHandlerType, eventInfo),
                              TypeFullName = eventInfo.EventHandlerType.FullName ?? eventInfo.EventHandlerType.Name
                            };
        ReflectAttributes(CustomAttributeData.GetCustomAttributes(eventInfo), nrEvent);

        if(!(nrCompositeType is NRInterface))
        {
          nrEvent.AccessModifier = GetMethodAccessModifier(xAddMethodInfo);
          nrEvent.IsStatic = xAddMethodInfo.IsStatic;
        }

        //Ask the filter if the event should be in the result.
        if(Filter.Reflect(nrEvent))
        {
          nrCompositeType.Events.Add(nrEvent);
        }
      }
    }

    /// <summary>
    /// Reflects all fields within the type <paramref name="type"/>. Reflected
    /// fields are added to <paramref name="fieldContainer"/>.
    /// </summary>
    /// <param name="type">The fields are taken from this type.</param>
    /// <param name="fieldContainer">Reflected fields are added to this FieldContainer.</param>
    private void ReflectFields(Type type, IFieldContainer fieldContainer)
    {
      List<string> events = GetEventNames(type);

      FieldInfo[] fieldInfos = type.GetFields(STANDARD_BINDING_FLAGS);
      foreach(FieldInfo fieldInfo in fieldInfos)
      {
        //Don't reflect fields belonging to events
        if(events.Contains(fieldInfo.Name))
        {
          continue;
        }
        //Don't display derived fields.
        if(fieldInfo.DeclaringType != type)
        {
          continue;
        }
        //Don't add compiler generated fields (thanks to Luca).
        if(HasMemberCompilerGeneratedAttribute(fieldInfo))
        {
          continue;
        }

        ReflectField(fieldInfo, fieldContainer);
      }
    }

    /// <summary>
    /// Reflects all constructors of <paramref name="type"/> to <paramref name="nrSingleInheritanceType"/>.
    /// </summary>
    /// <param name="type">The type to take the constructors from.</param>
    /// <param name="nrSingleInheritanceType">The destination of the reflection.</param>
    private void ReflectConstructors(Type type, NRSingleInheritanceType nrSingleInheritanceType)
    {
      ConstructorInfo[] constructors = type.GetConstructors(STANDARD_BINDING_FLAGS);
      string typeName = type.Name;
      if(typeName.Contains("`"))
      {
        typeName = typeName.Substring(0, typeName.IndexOf("`", StringComparison.Ordinal));
      }
      foreach(ConstructorInfo constructorInfo in constructors)
      {
        NRConstructor nrConstructor = new NRConstructor
                                        {
                                          Name = typeName,
                                          AccessModifier = GetMethodAccessModifier(constructorInfo),
                                          OperationModifier = GetOperationModifier(constructorInfo)
                                        };
        ReflectParameters(constructorInfo, nrConstructor.Parameters);
        ReflectAttributes(CustomAttributeData.GetCustomAttributes(constructorInfo), nrConstructor);

        //Ask the filter if the constructor should be in the result.
        if(Filter.Reflect(nrConstructor))
        {
          nrSingleInheritanceType.Constructors.Add(nrConstructor);
        }
      }
    }

    /// <summary>
    /// Reflects all methods of <paramref name="type"/> to <paramref name="nrCompositeType"/>.
    /// </summary>
    /// <param name="type">The type to take the methods from.</param>
    /// <param name="nrCompositeType">The destination of the reflection.</param>
    private void ReflectMethods(Type type, NRCompositeType nrCompositeType)
    {
      MethodInfo[] methods = type.GetMethods(STANDARD_BINDING_FLAGS);
      foreach(MethodInfo methodInfo in methods)
      {
        //Don't display derived Methods.
        if(methodInfo.DeclaringType != type)
        {
          continue;
        }
        //There are sometimes some magic methods like '<.ctor>b__0'. Those
        //methods are generated by the compiler and shouldn't be reflected.
        //Those methods have an attribute CompilerGeneratedAttribute.
        if(HasMemberCompilerGeneratedAttribute(methodInfo))
        {
          continue;
        }

        if(methodInfo.IsSpecialName)
        {
          //SpecialName means that this method is an automaticaly generated
          //method. This can be get_XXX and set_XXX for properties or
          //add_XXX and remove_XXX for events. There are also special name
          //methods starting with op_ for operators.
          if(!methodInfo.Name.StartsWith("op_"))
          {
            continue;
          }
          //!method.Name starts with 'op_' and so it is an operator.

          NRSingleInheritanceType singleInheritanceType = nrCompositeType as NRSingleInheritanceType;
          if(singleInheritanceType != null)
          {
            ReflectOperator(methodInfo, singleInheritanceType);
          }
        }
        else
        {
          ReflectMethod(methodInfo, nrCompositeType);
        }
      }
    }

    /// <summary>
    /// Reflects all properties of <paramref name="type"/> to <paramref name="nrCompositeType"/>.
    /// </summary>
    /// <param name="type">The type to take the properties from.</param>
    /// <param name="nrCompositeType">The destination of the reflection.</param>
    private void ReflectProperties(Type type, NRCompositeType nrCompositeType)
    {
      PropertyInfo[] properties = type.GetProperties(STANDARD_BINDING_FLAGS);
      foreach(PropertyInfo propertyInfo in properties)
      {
        //Don't display derived Methods.
        if(propertyInfo.DeclaringType != type)
        {
          continue;
        }

        //The access modifier for a property isn't stored at the property.
        //We have to use the access modifier from the corresponding get_XXX /
        //set_XXX Method. The one whith the lowest restrictivity is the one
        //of the property.
        MethodInfo getMethod = propertyInfo.GetGetMethod(true);
        MethodInfo setMethod = propertyInfo.GetSetMethod(true);

        NRProperty nrProperty = new NRProperty();

        //The access modifier for the whole property is the most non destrictive one.
        if(!(nrCompositeType is NRInterface))
        {
          if(propertyInfo.CanRead && propertyInfo.CanWrite)
          {
            nrProperty.GetterModifier = GetMethodAccessModifier(getMethod);
            nrProperty.SetterModifier = GetMethodAccessModifier(setMethod);
            nrProperty.AccessModifier = nrProperty.GetterModifier > nrProperty.SetterModifier
                                          ? nrProperty.SetterModifier
                                          : nrProperty.GetterModifier;
            nrProperty.OperationModifier = GetOperationModifier(getMethod);
          }
          else if(propertyInfo.CanRead)
          {
            nrProperty.GetterModifier = GetMethodAccessModifier(getMethod);
            nrProperty.AccessModifier = nrProperty.GetterModifier;
            nrProperty.OperationModifier = GetOperationModifier(getMethod);
          }
          else
          {
            nrProperty.SetterModifier = GetMethodAccessModifier(setMethod);
            nrProperty.AccessModifier = nrProperty.SetterModifier;
            nrProperty.OperationModifier = GetOperationModifier(setMethod);
          }
        }
        if(!(nrCompositeType is NRInterface))
        {
          ChangeOperationModifierIfOverwritten(type, propertyInfo.CanRead ? getMethod : setMethod, nrProperty);
        }
        nrProperty.Type = GetTypeUsage(propertyInfo.PropertyType, propertyInfo);
        nrProperty.TypeFullName = propertyInfo.PropertyType.FullName ?? propertyInfo.PropertyType.Name;
        //Is this an Item-property (public int this[int i])?
        if(propertyInfo.GetIndexParameters().Length > 0)
        {
          nrProperty.Name = "this";
          ReflectParameters(propertyInfo.CanRead ? getMethod : setMethod, nrProperty.Parameters);
        }
        else
        {
          nrProperty.Name = propertyInfo.Name;
        }

        nrProperty.HasGetter = propertyInfo.CanRead;
        nrProperty.HasSetter = propertyInfo.CanWrite;

        ReflectAttributes(CustomAttributeData.GetCustomAttributes(propertyInfo), nrProperty);

        //Ask the filter if the property should be in the result.
        if(Filter.Reflect(nrProperty))
        {
          nrCompositeType.Properties.Add(nrProperty);
        }
      }
    }

    /// <summary>
    /// Adds all parameters of <paramref name="methodBase"/> to the list of <see cref="NRParameter"/>.
    /// </summary>
    /// <param name="methodBase">The paramters of this methods are extracted.</param>
    /// <param name="nrParameters">A list to add the parameters to.</param>
    private void ReflectParameters(MethodBase methodBase, List<NRParameter> nrParameters)
    {
      ParameterInfo[] parameters = methodBase.GetParameters();
      foreach(ParameterInfo parameter in parameters)
      {
        NRParameter nrParameter = new NRParameter
                                    {
                                      Name = parameter.Name,
                                      Type = GetTypeUsage(parameter.ParameterType, parameter, methodBase.DeclaringType),
                                      TypeFullName = parameter.ParameterType.FullName ?? parameter.ParameterType.Name,
                                      ParameterModifier = ParameterModifier.In
                                    };
        ReflectAttributes(CustomAttributeData.GetCustomAttributes(parameter), nrParameter);
        if(parameter.ParameterType.Name.EndsWith("&"))
        {
          //This is a out or ref-parameter, otherwise it would not have the '&'
          nrParameter.ParameterModifier = parameter.IsOut ? ParameterModifier.Out : ParameterModifier.InOut;
        }
        else if(HasParamterAttribute(parameter, typeof(ParamArrayAttribute)))
        {
          nrParameter.ParameterModifier = ParameterModifier.Params;
        }
        else if(parameter.IsOptional)
        {
          nrParameter.ParameterModifier = ParameterModifier.Optional;
          object rawDefaultValue = parameter.RawDefaultValue;
          if(rawDefaultValue != null)
          {
            nrParameter.DefaultValue = rawDefaultValue.ToString();
          }
        }

        nrParameters.Add(nrParameter);
      }
    }

    /// <summary>
    /// Reflects the given attributes and stores them into the given
    /// <see cref="IAttributable"/> instance.
    /// </summary>
    /// <param name="attributeDatas">The attributes to reflect.</param>
    /// <param name="attributable">An instance of <see cref="IAttributable"/>
    /// to store the reflected attributes to.</param>
    private void ReflectAttributes(IEnumerable<CustomAttributeData> attributeDatas, IAttributable attributable)
    {
      attributable.Attributes.AddRange(GetAttributes(attributeDatas));
    }

    #endregion

    #region +++ Reflect members

    /// <summary>
    /// Reflects the given field. The result is added to <paramref name="fieldContainer"/>.
    /// </summary>
    /// <param name="fieldInfo">The field to reflect.</param>
    /// <param name="fieldContainer">The reflected field is added to this <see cref="IFieldContainer"/>.</param>
    private void ReflectField(FieldInfo fieldInfo, IFieldContainer fieldContainer)
    {
      NRField nrField = new NRField
                          {
                            Name = fieldInfo.Name,
                            AccessModifier = GetFieldAccessModifier(fieldInfo),
                            IsReadonly = fieldInfo.IsInitOnly,
                            IsStatic = fieldInfo.IsStatic,
                            Type = GetTypeUsage(fieldInfo.FieldType, fieldInfo),
                            TypeFullName = fieldInfo.FieldType.FullName ?? fieldInfo.FieldType.Name
                          };
      ReflectAttributes(CustomAttributeData.GetCustomAttributes(fieldInfo), nrField);

      Type[] customModifiers = fieldInfo.GetRequiredCustomModifiers();
      if(customModifiers.Contains(typeof(IsVolatile)))
      {
        nrField.IsVolatile = true;
      }

      if(fieldInfo.DeclaringType != null && IsFieldOverwritten(fieldInfo.DeclaringType.BaseType, fieldInfo))
      {
        nrField.IsHider = true;
      }

      if(fieldInfo.IsLiteral)
      {
        object rawConstantValue = fieldInfo.GetRawConstantValue();
        if(rawConstantValue != null)
        {
          nrField.InitialValue = rawConstantValue.ToString();
        }
        nrField.IsStatic = false;
        nrField.IsConstant = true;
      }

      //Ask the filter if the field should be in the result.
      if(Filter.Reflect(nrField))
      {
        fieldContainer.Fields.Add(nrField);
      }
    }

    /// <summary>
    /// Reflects a single method.
    /// </summary>
    /// <param name="methodInfo">The method to reflect.</param>
    /// <param name="methodContainer">The reflected method will be added to this <see cref="IMethodContainer"/>.</param>
    private void ReflectMethod(MethodInfo methodInfo, IMethodContainer methodContainer)
    {
      NRMethod nrMethod = new NRMethod
                            {
                              Name = methodInfo.Name
                            };

      ReflectReturnValueOperation(methodInfo, nrMethod, methodContainer);

      nrMethod.IsExtensionMethod = HasMemberAttribute(methodInfo, typeof(ExtensionAttribute));
      if(nrMethod.IsExtensionMethod)
      {
        nrMethod.Parameters[0].IsExtensionParameter = true;
      }
      nrMethod.GenericTypes.AddRange(GetTypeParameters(methodInfo.GetGenericArguments()));

      //Ask the filter if the method should be in the result.
      if(Filter.Reflect(nrMethod))
      {
        methodContainer.Methods.Add(nrMethod);
      }
    }

    /// <summary>
    /// Reflects a single operator.
    /// </summary>
    /// <param name="methodInfo">The operator to reflect.</param>
    /// <param name="singleInheritanceType">The reflected operator will be added to this <see cref="NRSingleInheritanceType"/>.</param>
    private void ReflectOperator(MethodInfo methodInfo, NRSingleInheritanceType singleInheritanceType)
    {
      NROperator nrOperator = new NROperator();

      //We store the method name here so it is much easier to take care about operators
      string methodName = methodInfo.Name;
      //We have to get the operator type here.
      nrOperator.OperatorType = operatorMethodsMap.ContainsKey(methodName) ? operatorMethodsMap[methodName] : OperatorType.Unknown;

      nrOperator.Name = methodName;

      ReflectReturnValueOperation(methodInfo, nrOperator, singleInheritanceType);

      //Ask the filter if the method should be in the result.
      if(Filter.Reflect(nrOperator))
      {
        singleInheritanceType.Operators.Add(nrOperator);
      }
    }

    /// <summary>
    /// Reflects the information of a <see cref="NRReturnValueOperation"/>.
    /// </summary>
    /// <param name="methodInfo">The method to reflect.</param>
    /// <param name="nrOperation">The reflected information will be added to this <see cref="NRReturnValueOperation"/>.</param>
    /// <param name="methodContainer">This will be used to determine if the method is part of an
    ///                               interface or not. Nothing will be added to this container.</param>
    private void ReflectReturnValueOperation(MethodInfo methodInfo, NRReturnValueOperation nrOperation,
                                             IMethodContainer methodContainer)
    {
      nrOperation.Type = GetTypeUsage(methodInfo.ReturnType, methodInfo);
      nrOperation.TypeFullName = methodInfo.ReturnType.FullName ?? methodInfo.ReturnType.Name;

      ReflectParameters(methodInfo, nrOperation.Parameters);
      ReflectAttributes(CustomAttributeData.GetCustomAttributes(methodInfo), nrOperation);
      nrOperation.ReturnValueAttributes.AddRange(GetAttributes(CustomAttributeData.GetCustomAttributes(methodInfo)));

      if(!(methodContainer is NRInterface))
      {
        nrOperation.AccessModifier = GetMethodAccessModifier(methodInfo);
        nrOperation.OperationModifier = GetOperationModifier(methodInfo);
      }

      ChangeOperationModifierIfOverwritten(methodInfo.DeclaringType, methodInfo, nrOperation);
    }

    #endregion

    #region === Help methods

    #region +++ bool Is...

    /// <summary>
    /// Tests recursiv if the <paramref name="memberInfo"/> or its declaring type
    /// has the CompilerGeneratedAttribute.
    /// </summary>
    /// <param name="memberInfo">The member info to test</param>
    /// <returns>True, if <paramref name="memberInfo"/> has the CompilerGeneratedAttribute.</returns>
    private static bool HasMemberCompilerGeneratedAttribute(MemberInfo memberInfo)
    {
      if(memberInfo == null)
      {
        return false;
      }
      if(HasMemberAttribute(memberInfo, typeof(CompilerGeneratedAttribute)))
      {
        return true;
      }
      return HasMemberCompilerGeneratedAttribute(memberInfo.DeclaringType);
    }

    /// <summary>
    /// Checks if the memberInfo contains an attribute of the given type.
    /// </summary>
    /// <param name="memberInfo">The MemberInfo</param>
    /// <param name="type">The type of the attribute.</param>
    /// <returns>True if the memeber contains the attribute, false otherwise.</returns>
    private static bool HasMemberAttribute(MemberInfo memberInfo, Type type)
    {
      if(memberInfo == null)
      {
        return false;
      }
      IList<CustomAttributeData> attributeDatas = CustomAttributeData.GetCustomAttributes(memberInfo);
      return attributeDatas.Any(attributeData => attributeData.Constructor.DeclaringType != null && attributeData.Constructor.DeclaringType.FullName == type.FullName);
    }

    /// <summary>
    /// Checks if the parameterInfo contains an attribute of the given type.
    /// </summary>
    /// <param name="parameterInfo">The ParameterInfo</param>
    /// <param name="type">The type of the attribute.</param>
    /// <returns>True if the parameter contains the attribute, false otherwise.</returns>
    private static bool HasParamterAttribute(ParameterInfo parameterInfo, Type type)
    {
      if(parameterInfo == null)
      {
        return false;
      }
      IList<CustomAttributeData> attributeDatas = CustomAttributeData.GetCustomAttributes(parameterInfo);
      return attributeDatas.Any(attributeData => attributeData.Constructor.DeclaringType == type);
    }

    /// <summary>
    /// Determines if the method <paramref name="method"/> is already
    /// defined within <paramref name="type"/> or above.
    /// </summary>
    /// <param name="type">The type which could define <paramref name="method"/> already.</param>
    /// <param name="method">The method wich should be checked</param>
    /// <returns>True, if <paramref name="method"/> is defined in <paramref name="type"/> or above.</returns>
    private static bool IsMethodOverwritten(Type type, MethodBase method)
    {
      if(type == null)
      {
        return false;
      }
      ParameterInfo[] parameters = method.GetParameters();
      Type[] parameterTypes = new Type[parameters.Length];
      for(int i = 0; i < parameters.Length; i++)
      {
        parameterTypes[i] = parameters[i].ParameterType;
      }
      IEnumerable<MethodInfo> methodInfos = type.GetMethods(method.Name, parameterTypes);
      if(methodInfos != null && methodInfos.Any())
      {
        return true;
      }
      return IsMethodOverwritten(type.BaseType, method);
    }

    /// <summary>
    /// Determines if the field <paramref name="fieldInfo"/> is already
    /// defined within <paramref name="type"/> or above.
    /// </summary>
    /// <param name="type">The type which could define <paramref name="fieldInfo"/> already.</param>
    /// <param name="fieldInfo">The field wich will be checked.</param>
    /// <returns><c>True</c> if <paramref name="fieldInfo"/> is defined in <paramref name="type"/> or above.</returns>
    private static bool IsFieldOverwritten(Type type, FieldInfo fieldInfo)
    {
      if(type == null)
      {
        return false;
      }
      FieldInfo parentField = type.GetField(fieldInfo.Name, STANDARD_BINDING_FLAGS);
      if(parentField != null)
      {
        return true;
      }
      return IsFieldOverwritten(type.BaseType, fieldInfo);
    }

    #endregion

    #region +++ GetTypeUsage

    /// <summary>
    /// Returns an instance of <see cref="NRTypeUsage"/> which is initialized with the
    /// values for the given type.
    /// </summary>
    /// <param name="type">The type which will be represented by the resulting <see cref="NRTypeUsage"/>.</param>
    /// <param name="declaringType">A <see cref="Type"/> which defines the type which is used.</param>
    /// <returns>The initialized <see cref="NRTypeUsage"/>.</returns>
    private static NRTypeUsage GetTypeUsage(Type type, Type declaringType)
    {
      return GetTypeUsage(type, CustomAttributeData.GetCustomAttributes(declaringType), declaringType);
    }

    /// <summary>
    /// Returns an instance of <see cref="NRTypeUsage"/> which is initialized with the
    /// values for the given type.
    /// </summary>
    /// <param name="type">The type which will be represented by the resulting <see cref="NRTypeUsage"/>.</param>
    /// <param name="memberInfo">A <see cref="MemberInfo"/> which is used to determine if the type is dynamic.</param>
    /// <returns>The initialized <see cref="NRTypeUsage"/>.</returns>
    private static NRTypeUsage GetTypeUsage(Type type, MemberInfo memberInfo)
    {
      return GetTypeUsage(type, CustomAttributeData.GetCustomAttributes(memberInfo), memberInfo.DeclaringType);
    }

    /// <summary>
    /// Returns an instance of <see cref="NRTypeUsage"/> which is initialized with the
    /// values for the given type.
    /// </summary>
    /// <param name="type">The type which will be represented by the resulting <see cref="NRTypeUsage"/>.</param>
    /// <param name="methodInfo">A <see cref="MethodInfo"/> which is used to determine if the type is dynamic.</param>
    /// <returns>The initialized <see cref="NRTypeUsage"/>.</returns>
    private static NRTypeUsage GetTypeUsage(Type type, MethodInfo methodInfo)
    {
      return GetTypeUsage(type, (ParameterInfo)methodInfo.ReturnTypeCustomAttributes, methodInfo.DeclaringType);
    }

    /// <summary>
    /// Returns an instance of <see cref="NRTypeUsage"/> which is initialized with the
    /// values for the given type.
    /// </summary>
    /// <param name="type">The type which will be represented by the resulting <see cref="NRTypeUsage"/>.</param>
    /// <param name="parameterInfo">A <see cref="ParameterInfo"/> which is used to determine if the type is dynamic.</param>
    /// <param name="currentType">The current type the type to get is used in.</param>
    /// <returns>The initialized <see cref="NRTypeUsage"/>.</returns>
    private static NRTypeUsage GetTypeUsage(Type type, ParameterInfo parameterInfo, Type currentType)
    {
      return GetTypeUsage(type, CustomAttributeData.GetCustomAttributes(parameterInfo), currentType);
    }

    /// <summary>
    /// Returns an instance of <see cref="NRTypeUsage"/> which is initialized with the
    /// values for the given type.
    /// </summary>
    /// <param name="type">The type which will be represented by the resulting <see cref="NRTypeUsage"/>.</param>
    /// <param name="customAttributeDatas">An <see cref="IEnumerable{T}"/> with attribute of th type which is used to determine if the type is dynamic.</param>
    /// <param name="declaringType">A <see cref="Type"/> which defines the type which is used.</param>
    /// <returns>The initialized <see cref="NRTypeUsage"/>.</returns>
    private static NRTypeUsage GetTypeUsage(Type type, IEnumerable<CustomAttributeData> customAttributeDatas, Type declaringType)
    {
      CustomAttributeData dynamicAttributeData = customAttributeDatas.FirstOrDefault(ad => ad.Constructor != null && ad.Constructor.DeclaringType != null && ad.Constructor.DeclaringType.FullName != null && ad.Constructor.DeclaringType.FullName.Equals(typeof(DynamicAttribute).FullName));
      List<bool> dynamics;
      if(dynamicAttributeData != null)
      {
        if(dynamicAttributeData.ConstructorArguments.Count == 0)
        {
          dynamics = new List<bool>(new[] {true});
        }
        else
        {
          ReadOnlyCollection<CustomAttributeTypedArgument> arguments =
            (ReadOnlyCollection<CustomAttributeTypedArgument>)dynamicAttributeData.ConstructorArguments[0].Value;
          dynamics = (from arg in arguments select arg.Value).Cast<bool>().ToList();
        }
      }
      else
      {
        dynamics = new List<bool>(new[] {false});
      }
      return GetTypeUsage(type, ref dynamics, declaringType, null);
    }

    /// <summary>
    /// Returns an instance of <see cref="NRTypeUsage"/> which is initialized with the
    /// values for the given type.
    /// </summary>
    /// <param name="type">The type which will be represented by the resulting <see cref="NRTypeUsage"/>.</param>
    /// <param name="dynamicAttributeData">The custom attribute data of the dynamic attribute of the type if any.</param>
    /// <param name="currentType">The current type the type to get is used in.</param>
    /// <param name="genericArguments">A list of generic arguments used while using the type (if any).</param>
    /// <returns>The initialized <see cref="NRTypeUsage"/>.</returns>
    private static NRTypeUsage GetTypeUsage(Type type, ref List<bool> dynamicAttributeData, Type currentType,
                                  List<Type> genericArguments)
    {
      if(genericArguments == null)
      {
        genericArguments = type.GetGenericArguments().ToList();
      }

      // Get the array ranks first since the dynamic flags start with the ones for the array.
      List<int> arrayRanks = new List<int>();
      while(type.IsArray)
      {
        arrayRanks.Add(type.GetArrayRank());
        type = type.GetElementType();
        if(dynamicAttributeData.Count > 0)
        {
          dynamicAttributeData.RemoveAt(0);
        }
      }

      // Check if the type is a nullable type. If so, it is of System.Nullable<type>.
      bool nullable = false;
      if(type.FullName != null && type.FullName.StartsWith("System.Nullable"))
      {
        // It is a nullable type, so rmove the System.Nullable and use the first generic
        // argument of this type which holds the real type.
        type = genericArguments[0];
        genericArguments = type.GetGenericArguments().ToList();
        nullable = true;
        if(dynamicAttributeData.Count > 0)
        {
          dynamicAttributeData.RemoveAt(0);
        }
      }

      bool isDynamic = dynamicAttributeData.Count > 0 && dynamicAttributeData[0];
      NRTypeUsage declaringTypeUsage = null;
      int declaringTypeGenericArgsCount = 0;
      // Do we have to save a declaring type?
      if(type.DeclaringType != null && !type.IsGenericParameter)
      {
        // First we have to find out if the type which is used was declared within a type
        // which is defined itself somewhere in the hirarchy above the current type.
        bool sameDeclaringType = false;
        Type declaringType = currentType;
        while(declaringType != null)
        {
          if(declaringType == type.DeclaringType)
          {
            // OK, type and current type share a declaring parent.
            sameDeclaringType = true;
            break;
          }
          declaringType = declaringType.DeclaringType;
        }
        if(type.DeclaringType != null)
        {
          // This is a nested type. If the declaring type is also a generic type,
          // the generic parameters of the declaring type are at the list, too. We
          // have to handle this so they don't appear more than once at the result.
          declaringTypeGenericArgsCount = type.DeclaringType.GetGenericArguments().Length;
        }
        bool declaringTypeUsesOnlyGenericParam = genericArguments.Take(declaringTypeGenericArgsCount).All(t => t.IsGenericParameter);

        if(!sameDeclaringType || !declaringTypeUsesOnlyGenericParam)
        {
          declaringTypeUsage = GetTypeUsage(type.DeclaringType, ref dynamicAttributeData, currentType, genericArguments);
          // While getting the type usage of the parent type, the dynamic flag of the type itself was droped. Add it again.
          dynamicAttributeData.Insert(0, isDynamic);
        }
        else if(declaringType.IsGenericType)
        {
          genericArguments.RemoveRange(0, declaringTypeGenericArgsCount);
        }
      }

      NRTypeUsage nrTypeUsage = new NRTypeUsage
                        {
                          Name = GetTypeName(type),
                          Namespace = type.IsGenericParameter ? null : type.Namespace,
                          IsNullable = nullable,
                          IsDynamic = isDynamic,
                          DeclaringType = declaringTypeUsage,
                          FullName = type.FullName
                        };
      // Remove first dynamic flag if there is one
      if(dynamicAttributeData.Count > 0)
      {
        dynamicAttributeData.RemoveAt(0);
      }

      // Add the array ranks
      nrTypeUsage.ArrayRanks.AddRange(arrayRanks);

      // If we have a generic type, we have to recurse into the generic parameter.
      if(type.GetGenericArguments().Length > 0)
      {
        for(int i = 0; i < type.GetGenericArguments().Length - declaringTypeGenericArgsCount; ++i)
        {
          nrTypeUsage.GenericParameters.Add(GetTypeUsage(genericArguments[0], ref dynamicAttributeData, currentType, null));
          genericArguments.RemoveAt(0);
        }
      }

      return nrTypeUsage;
    }

    #endregion

    /// <summary>
    /// Returns a string containing the name of the type <paramref name="type"/>
    /// in C# syntax. Is especially responsible to solve problems with generic
    /// types.
    /// </summary>
    /// <param name="type">The type name is returned for this <see cref="Type"/>.</param>
    /// <returns>The name of <paramref name="type"/> as a string.</returns>
    private static string GetTypeName(Type type)
    {
      StringBuilder typeName = new StringBuilder(type.Name);
      if(type.IsArray)
      {
        typeName = new StringBuilder(GetTypeName(type.GetElementType()));
      }
      //openvenom - This part is especially added to handle ref Nullable<T> (ex: ref int32?)
      //kind method parameter types
      //To Malte Ried: Thank you very much for providing such a nice utility...
      else if(!type.IsGenericType && type.IsByRef && type.GetGenericArguments().Length > 0
              && type.FullName != null && type.FullName.StartsWith("System.Nullable`1")
              && type.FullName.EndsWith("&"))
      {
        typeName = new StringBuilder();
        typeName.Append(type.GetGenericArguments()[0].Name); //This gives us the Int32 part
        typeName.Append("?");
      }
      else if(type.GetGenericArguments().Length > 0)
      {
        if(typeName.ToString().LastIndexOf('`') > 0)
        {
          //Generics get names like "List`1"
          typeName.Remove(typeName.ToString().LastIndexOf('`'),
                          typeName.Length - typeName.ToString().LastIndexOf('`'));
        }
      }
      return typeName.ToString();
    }

    /// <summary>
    /// Gets the raw name of a type. That is, special generics stuff is removed.
    /// </summary>
    /// <param name="type">The type to get the raw name of.</param>
    /// <returns>The raw name as a <see cref="string"/>.</returns>
    private string GetRawTypeName(Type type)
    {
      if(type.IsGenericType)
      {
        if(type.Name.LastIndexOf('`') > 0)
        {
          //Generics get names like "List`1"
          return type.Name.Substring(0, type.Name.LastIndexOf('`'));
        }
      }
      return type.Name;
    }

    /// <summary>
    /// Gets a list of the names of all events declared by the given type.
    /// </summary>
    /// <param name="type">The event names are extrected from this type.</param>
    /// <returns>A list of event names.</returns>
    private static List<string> GetEventNames(Type type)
    {
      EventInfo[] eventInfos = type.GetEvents(STANDARD_BINDING_FLAGS);
      List<string> eventNames = new List<string>(from eventInfo in eventInfos
                                                 where eventInfo.DeclaringType == type
                                                 select eventInfo.Name);
      return eventNames;
    }

    /// <summary>
    /// Reflects the given attributes and returns them as a list.
    /// </summary>
    /// <param name="attributeDatas">The attributes to reflect.</param>
    /// <returns>A list containing the reflected attributes.</returns>
    private IEnumerable<NRAttribute> GetAttributes(IEnumerable<CustomAttributeData> attributeDatas)
    {
      List<NRAttribute> attributes = new List<NRAttribute>();
      foreach(CustomAttributeData attributeData in attributeDatas)
      {
        Type attributeType = attributeData.Constructor.DeclaringType;
        if(attributeType != null && (attributeType == typeof(DynamicAttribute) || attributeType == typeof(ExtensionAttribute) || attributeType == typeof(OutAttribute) || attributeType == typeof(ParamArrayAttribute) || attributeType == typeof(OptionalAttribute)))
        {
          continue;
        }
        NRAttribute nrAttribute = new NRAttribute
                                    {
                                      Name = GetTypeName(attributeType),
                                      Namespace = attributeType != null ? attributeType.Namespace : ""
                                    };
        foreach(CustomAttributeTypedArgument argument in attributeData.ConstructorArguments)
        {
          nrAttribute.Values.Add(GetAttributeValue(argument));
        }
        if(attributeData.NamedArguments != null)
        {
          foreach(CustomAttributeNamedArgument argument in attributeData.NamedArguments)
          {
            nrAttribute.NamedValues.Add(argument.MemberInfo.Name, GetAttributeValue(argument.TypedValue));
          }
        }

        //Ask the filter if the attribute should be in the result.
        if(Filter.Reflect(nrAttribute))
        {
          attributes.Add(nrAttribute);
        }
      }

      return attributes;
    }

    /// <summary>
    /// Gets an instance of <see cref="NRAttributeValue"/> initialized with
    /// values of the given <see cref="CustomAttributeTypedArgument"/>.
    /// </summary>
    /// <param name="argument">A <see cref="CustomAttributeTypedArgument"/> to take the values from.</param>
    /// <returns>A new and initialized <see cref="NRAttributeValue"/>.</returns>
    private NRAttributeValue GetAttributeValue(CustomAttributeTypedArgument argument)
    {
      NRAttributeValue nrAttributeValue = new NRAttributeValue
                                            {
                                              Type = argument.ArgumentType.FullName,
                                              Value = argument.Value
                                            };
      if(argument.ArgumentType.FullName == "System.Type")
      {
        nrAttributeValue.Value = argument.Value.ToString();
      }

      return nrAttributeValue;
    }

    /// <summary>
    /// Gets a list containing the the type parameters which are created from
    /// the given types.
    /// </summary>
    /// <param name="genericArguments">The types of a generic type.</param>
    /// <returns>A list containing the type parameters.</returns>
    private IEnumerable<NRTypeParameter> GetTypeParameters(IEnumerable<Type> genericArguments)
    {
      List<NRTypeParameter> nrTypeParameters = new List<NRTypeParameter>();
      foreach(Type genericArgument in genericArguments)
      {
        GenericParameterAttributes attributes = genericArgument.GenericParameterAttributes;

        NRTypeParameter nrTypeParameter = new NRTypeParameter
                                            {
                                              Name = genericArgument.Name,
                                              IsStruct =
                                                (attributes & GenericParameterAttributes.NotNullableValueTypeConstraint) !=
                                                0,
                                              IsClass =
                                                (attributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0,
                                              IsIn = (attributes & GenericParameterAttributes.Contravariant) != 0,
                                              IsOut = (attributes & GenericParameterAttributes.Covariant) > 0
                                            };
        nrTypeParameter.IsConstructor = (attributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0 &&
                                        !nrTypeParameter.IsStruct;
        Type[] genericParameterConstraints = genericArgument.GetGenericParameterConstraints();
        foreach(Type genericParameterConstraint in genericParameterConstraints)
        {
          if(genericParameterConstraint != typeof(ValueType))
          {
            nrTypeParameter.BaseTypes.Add(GetTypeUsage(genericParameterConstraint, genericArgument.DeclaringType));
          }
        }

        ReflectAttributes(CustomAttributeData.GetCustomAttributes(genericArgument), nrTypeParameter);

        nrTypeParameters.Add(nrTypeParameter);
      }

      return nrTypeParameters;
    }

    /// <summary>
    /// If the method <paramref name="method"/> is overwritten in type
    /// <paramref name="type"/> the operation modifiers are changed to 
    /// reflect this.
    /// </summary>
    /// <param name="type">The type the method is declared in.</param>
    /// <param name="method">The method to check.</param>
    /// <param name="nrOperation">The operation which has to be changed.</param>
    private static void ChangeOperationModifierIfOverwritten(Type type, MethodBase method, NROperation nrOperation)
    {
      if(type != null && IsMethodOverwritten(type.BaseType, method))
      {
        if(method.IsVirtual &&
           (method.Attributes & MethodAttributes.VtableLayoutMask) != MethodAttributes.VtableLayoutMask)
        {
          if(method.IsFinal)
          {
            nrOperation.IsSealed = true;
          }
          nrOperation.IsOverride = true;
        }
        //It's not possible to distinguish between virtual and virtual new
        //in the assembly, because virtual methods get implicitly virtual new.
        else
        {
          nrOperation.IsHider = true;
        }
      }
    }

    #region +++ Get modifiers

    /// <summary>
    /// Returns the operation modifier for <paramref name="method"/>.
    /// </summary>
    /// <param name="method">The operation modifiers is returned for this MethodBase.</param>
    /// <returns>The OperationModifier of <paramref name="method"/>.</returns>
    private static OperationModifier GetOperationModifier(MethodBase method)
    {
      if(method.DeclaringType != null && method.DeclaringType.IsValueType)
      {
        return OperationModifier.None;
      }

      OperationModifier result = OperationModifier.None;
      if(method.IsStatic)
      {
        result |= OperationModifier.Static;
      }
      if(method.IsAbstract)
      {
        result |= OperationModifier.Abstract;
      }
      // lytico: possible value is: IsFinal AND IsVirtual
      if(method.IsFinal && method.IsVirtual)
      {
        return OperationModifier.None;
      }
      if(method.IsFinal)
      {
        result |= OperationModifier.Sealed;
      }
      if(!method.IsAbstract && method.IsVirtual)
      {
        result |= OperationModifier.Virtual;
      }
      return result;
    }

    /// <summary>
    /// Returns the access modifier for the type <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The access modifier is returned for this Type.</param>
    /// <returns>The AccessModifier of <paramref name="type"/>.</returns>
    private static AccessModifier GetTypeAccessModifier(Type type)
    {
      if(type.IsNested)
      {
        if(type.IsNestedPublic)
        {
          return AccessModifier.Public;
        }
        if(type.IsNestedPrivate)
        {
          return AccessModifier.Private;
        }
        if(type.IsNestedAssembly)
        {
          return AccessModifier.Internal;
        }
        if(type.IsNestedFamily)
        {
          return AccessModifier.Protected;
        }
        if(type.IsNestedFamORAssem)
        {
          return AccessModifier.ProtectedInternal;
        }
        return AccessModifier.Default;
      }
      if(type.IsPublic)
      {
        return AccessModifier.Public;
      }
      if(type.IsNotPublic)
      {
        return AccessModifier.Internal;
      }
      if(!type.IsVisible)
      {
        return AccessModifier.Internal;
      }
      return AccessModifier.Default;
    }

    /// <summary>
    /// Returns the access modifier for the field <paramref name="field"/>.
    /// </summary>
    /// <param name="field">The access modifier is returned for this FieldInfo.</param>
    /// <returns>The AccessModifier of <paramref name="field"/>.</returns>
    private static AccessModifier GetFieldAccessModifier(FieldInfo field)
    {
      if(field.IsPublic)
      {
        return AccessModifier.Public;
      }
      if(field.IsPrivate)
      {
        return AccessModifier.Private;
      }
      if(field.IsAssembly)
      {
        return AccessModifier.Internal;
      }
      if(field.IsFamily)
      {
        return AccessModifier.Protected;
      }
      if(field.IsFamilyOrAssembly)
      {
        return AccessModifier.ProtectedInternal;
      }
      return AccessModifier.Default;
    }

    /// <summary>
    /// Returns the access modifier for the method <paramref name="methodBase"/>.
    /// </summary>
    /// <param name="methodBase">The access modifier is returned for this MethodBase.</param>
    /// <returns>The AccessModifier of <paramref name="methodBase"/>.</returns>
    private static AccessModifier GetMethodAccessModifier(MethodBase methodBase)
    {
      if(methodBase.Name.Contains(".") && !methodBase.IsConstructor)
      {
        //explicit interface implementation
        return AccessModifier.Default;
      }
      if(methodBase.IsPublic)
      {
        return AccessModifier.Public;
      }
      if(methodBase.IsPrivate)
      {
        return AccessModifier.Private;
      }
      if(methodBase.IsAssembly)
      {
        return AccessModifier.Internal;
      }
      if(methodBase.IsFamily)
      {
        return AccessModifier.Protected;
      }
      if(methodBase.IsFamilyOrAssembly)
      {
        return AccessModifier.ProtectedInternal;
      }
      return AccessModifier.Default;
    }

    #endregion

    #endregion

    #endregion
  }
}