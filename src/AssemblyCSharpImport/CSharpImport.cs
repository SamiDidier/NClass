using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ICSharpCode.NRefactory.CSharp;
using NClass.AssemblyCSharpImport.Lang;
using NClass.Core;
using NClass.CSharp;
using NClass.DiagramEditor.ClassDiagram;
using ClassType = ICSharpCode.NRefactory.CSharp.ClassType;

namespace NClass.AssemblyCSharpImport
{
    internal class CSharpImport
    {
        // ========================================================================
        // Con- / Destruction

        #region === Con- / Destruction

        /// <summary>
        ///     Initializes a new instance of <see cref="CSharpImport" />.
        /// </summary>
        public CSharpImport(Diagram diagram, ImportSettings settings)
        {
            this.diagram = diagram;
            this.settings = settings;
        }

        // ========================================================================
        // Properties

        #region === Properties

        #endregion

        #endregion

        // ========================================================================
        // Constants

        #region === Constants

        #endregion

        // ========================================================================
        // Fields

        #region === Fields

        /// <summary>
        ///     The diagram to add the new entities to.
        /// </summary>
        private readonly Diagram diagram;

        /// <summary>
        ///     An <see cref="ImportSettings" /> instance which describes which entities and members to reflect.
        /// </summary>
        private readonly ImportSettings settings;

        #endregion

        // ========================================================================
        // Methods

        #region === Methods

        /// <summary>
        ///     The main entry point of this class. Imports the C# file which is given
        ///     as the parameter.
        /// </summary>
        /// <param name="fileName">The file path of C# source code to import.</param>
        /// <returns><c>True</c>, if the import was successful.</returns>
        public bool ImportSourceCode(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(Strings.Error_NoCSharpFile,
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            try
            {
                diagram.Name = Path.GetFileName(fileName);
                diagram.RedrawSuspended = true;

                var parser = new CSharpParser();

                // Open the C# source file to read
                using (var sr = new StreamReader(fileName))
                {
                    // Parse the C# file
                    var syntaxTree = parser.Parse(sr, fileName);

                    if (parser.HasErrors == false)
                    {
                        foreach (var tp in syntaxTree.Descendants.OfType<TypeDeclaration>())
                        {
                            switch (tp.ClassType)
                            {
                                case ClassType.Class:
                                    AddClass(tp);
                                    continue;
                                case ClassType.Struct:
                                    AddStrct(tp);
                                    continue;
                                case ClassType.Interface:
                                    AddInterface(tp);
                                    continue;
                                case ClassType.Enum:
                                    AddEnum(tp);
                                    continue;
                                default:
                                    continue;
                            }
                        }

                        foreach (var dd in syntaxTree.Descendants.OfType<DelegateDeclaration>())
                            AddDelegate(dd);

                        Common.ArrangeTypes(diagram);

                        /*
                        RelationshipCreator relationshipCreator = new RelationshipCreator();
                    
                        NRRelationships nrRelationships = relationshipCreator.CreateRelationships(nrAssembly, settings.CreateNestings,
                                                                                                    settings.CreateGeneralizations,
                                                                                                    settings.CreateRealizations,
                                                                                                    settings.CreateAssociations);
                        AddRelationships();
                        */
                    }
                    else
                    {
                        MessageBox.Show(
                            string.Format(Strings.Error_CSharpParsing,
                                          fileName,
                                          parser.Errors.Select(err => err.Message)),
                            Strings.Error_MessageBoxTitle,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.Error_GeneralException, ex),
                                Strings.Error_MessageBoxTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                diagram.RedrawSuspended = false;
            }

            return true;
        }

        /// <summary>
        ///     Adds the relationships from <paramref name="" /> to the
        ///     diagram.
        /// </summary>
        private void AddRelationships()
        {
        }

        #region --- Entities

        /// <summary>
        ///     Adds the submitted classes to the diagram.
        /// </summary>
        private void AddClass(TypeDeclaration classTp)
        {
            var classType = diagram.AddClass();
            classType.Name = classTp.Name;
            classType.AccessModifier = classTp.Modifiers.ToNClass();
            classType.Modifier = classTp.Modifiers.ToNClassFromClass();

            AddFields(classType, classTp);
            AddProperties(classType, classTp);
            AddEvents(classType, classTp);
            AddConstructors(classType, classTp);
            AddDestructors(classType, classTp);
            AddMethods(classType, classTp);
            AddOperators(classType, classTp);
        }

        /// <summary>
        ///     Adds the submitted structs to the diagram.
        /// </summary>
        private void AddStrct(TypeDeclaration strctTp)
        {
            var structureType = diagram.AddStructure();
            structureType.Name = strctTp.Name;
            structureType.AccessModifier = strctTp.Modifiers.ToNClass();

            AddFields(structureType, strctTp);
            AddProperties(structureType, strctTp);
            AddEvents(structureType, strctTp);
            AddConstructors(structureType, strctTp);
            AddDestructors(structureType, strctTp);
            AddMethods(structureType, strctTp);
            AddOperators(structureType, strctTp);
        }

        /// <summary>
        ///     Adds the submitted interfaces to the diagram.
        /// </summary>
        private void AddInterface(TypeDeclaration interfaceTp)
        {
            var interfaceType = diagram.AddInterface();
            interfaceType.Name = interfaceTp.Name;
            interfaceType.AccessModifier = interfaceTp.Modifiers.ToNClass();

            AddProperties(interfaceType, interfaceTp);
            AddEvents(interfaceType, interfaceTp);
            AddMethods(interfaceType, interfaceTp);
        }

        /// <summary>
        ///     Adds the submitted delegates to the diagram.
        /// </summary>
        private void AddDelegate(DelegateDeclaration dd)
        {
            var delegateType = diagram.AddDelegate();
            delegateType.Name = dd.Name;
            delegateType.AccessModifier = dd.Modifiers.ToNClass();
            delegateType.ReturnType = dd.ReturnType.ToString();

            foreach (var ichParameter in dd.Parameters)
                delegateType.AddParameter(ichParameter.ToString()); // To Check
        }

        /// <summary>
        ///     Adds the submitted enums to the diagram.
        /// </summary>
        private void AddEnum(TypeDeclaration enumTp)
        {
            var enumType = diagram.AddEnum();
            enumType.Name = enumTp.Name;
            enumType.AccessModifier = enumTp.Modifiers.ToNClass();

            AddEnumValues(enumType, enumType.Values);
        }

        #endregion

        #region --- Member

        /// <summary>
        ///     Adds the given fields to the given type.
        /// </summary>
        /// <param name="type">The entity to add the fields to.</param>
        /// <param name="tp">.</param>
        private void AddFields(SingleInharitanceType type, TypeDeclaration tp)
        {
            foreach (var fp in tp.Descendants.OfType<FieldDeclaration>())
            {
                var variable = fp.Variables.First();
                if (variable == null)
                    continue;

                var fld = type.AddField();

                fld.Name = variable.Name;
                fld.AccessModifier = fp.Modifiers.ToNClass();
                fld.Type = fp.ReturnType.ToString();
                fld.InitialValue = variable.LastChild.ToString();
            }
        }

        /// <summary>
        ///     Adds the given properties to the given type.
        /// </summary>
        /// <param name="type">The entity to add the fields to.</param>
        /// <param name="tp">.</param>
        private void AddProperties(CompositeType type, TypeDeclaration tp)
        {
            foreach (var pp in tp.Descendants.OfType<PropertyDeclaration>())
            {
                var prop = type.AddProperty();

                prop.Name = pp.Name;
                prop.AccessModifier = pp.Modifiers.ToNClass();
                ;
                prop.Type = pp.ReturnType.ToString();
            }
        }

        /// <summary>
        ///     Adds the given events to the given type.
        /// </summary>
        /// <param name="type">The entity to add the fields to.</param>
        /// <param name="tp">.</param>
        private void AddEvents(CompositeType type, TypeDeclaration tp)
        {
            foreach (var ep in tp.Descendants.OfType<EventDeclaration>())
            {
                var variable = ep.Variables.First();
                if (variable == null)
                    continue;

                var ev = type.AddEvent();

                ev.Name = variable.Name;
                ev.AccessModifier = ep.Modifiers.ToNClass();
                ev.Type = ep.ReturnType.ToString();
            }
        }

        /// <summary>
        ///     Adds the given constructors to the given type.
        /// </summary>
        /// <param name="type">The entity to add the fields to.</param>
        /// <param name="tp">.</param>
        private void AddConstructors(CompositeType type, TypeDeclaration tp)
        {
            foreach (var cp in tp.Descendants.OfType<ConstructorDeclaration>())
            {
                var cons = type.AddConstructor();

                cons.Name = cp.Name;
                cons.AccessModifier = cp.Modifiers.ToNClass();

                var Arg = new CSharpArgumentList();

                foreach (var ichParameter in cp.Parameters)
                    Arg.Add(ichParameter.Name,
                            ichParameter.Type.ToString(),
                            ichParameter.ParameterModifier.ToNClass(),
                            ichParameter.DefaultExpression.ToString());

                cons.ArgumentList = Arg;
            }
        }

        /// <summary>
        ///     Adds the given destructor to the given type.
        /// </summary>
        /// <param name="type">The entity to add the fields to.</param>
        /// <param name="tp">.</param>
        private void AddDestructors(CompositeType type, TypeDeclaration tp)
        {
            foreach (var dp in tp.Descendants.OfType<DestructorDeclaration>())
            {
                var des = type.AddDestructor();

                des.Name = dp.Name;
            }
        }

        /// <summary>
        ///     Adds the given methods to the given type.
        /// </summary>
        private void AddMethods(CompositeType type, TypeDeclaration tp)
        {
            foreach (var mp in tp.Descendants.OfType<MethodDeclaration>())
            {
                var method = type.AddMethod();

                method.Name = mp.Name;

                method.Type = mp.ReturnType.ToString();
                method.AccessModifier = mp.Modifiers.ToNClass();

                var Arg = new CSharpArgumentList();

                foreach (var ichParameter in mp.Parameters)
                {
                    var defaultValue = string.Empty;
                    if (ichParameter.DefaultExpression is PrimitiveExpression)
                    {
                        var defaultExpression = (PrimitiveExpression) ichParameter.DefaultExpression;
                        defaultValue = defaultExpression.Value.ToString();
                    }

                    Arg.Add(ichParameter.Name,
                            ichParameter.Type.ToString(),
                            ichParameter.ParameterModifier.ToNClass(),
                            defaultValue);
                }

                method.ArgumentList = Arg;
            }
        }

        /// <summary>
        ///     Adds the given operators to the given type.
        /// </summary>
        private void AddOperators(CompositeType type, TypeDeclaration tp)
        {
            foreach (var op in tp.Descendants.OfType<OperatorDeclaration>())
            {
                var method = type.AddMethod();

                method.Name = op.Name;
                method.Type = op.ReturnType.ToString();
                method.AccessModifier = op.Modifiers.ToNClass();

                var Arg = new CSharpArgumentList();

                foreach (var ichParameter in op.Parameters)
                    Arg.Add(ichParameter.Name,
                            ichParameter.Type.ToString(),
                            ichParameter.ParameterModifier.ToNClass(),
                            ichParameter.DefaultExpression.ToString());

                method.ArgumentList = Arg;
            }
        }

        /// <summary>
        ///     Adds the given enum values to the given type.
        /// </summary>
        /// <param name="type">The enum to add the enum values to.</param>
        /// <param name="values">A list of enum values to add.</param>
        private void AddEnumValues(EnumType type, IEnumerable<EnumValue> values)
        {
            foreach (var enumValue in values)
                type.AddValue(enumValue.Name);
        }

        #endregion

        #endregion
    }
}