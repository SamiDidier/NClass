// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System.Text.RegularExpressions;
using NClass.Core;
using NClass.Translations;

namespace NClass.CSharp
{
    public class CSharpArgumentList : ArgumentList
    {
        // [<modifiers>] <type> <name> [,]
        private const string ParameterPattern =
            @"(?<modifier>out|ref|params)?(?(modifier)\s+|)" +
            @"(?<type>" + CSharpLanguage.GenericTypePattern2 + @")\s+" +
            @"(?<name>" + CSharpLanguage.NamePattern + @")" +
            @"(\s*=\s*(?<defval>([^,""]+|""(\\""|[^""])*"")))?";

        private const string ParameterStringPattern = @"^\s*(" + ParameterPattern + @"\s*(,\s*|$))*$";

        private static readonly Regex parameterRegex =
            new Regex(ParameterPattern, RegexOptions.ExplicitCapture);

        private static readonly Regex singleParamterRegex =
            new Regex("^" + ParameterPattern + "$", RegexOptions.ExplicitCapture);

        private static readonly Regex parameterStringRegex =
            new Regex(ParameterStringPattern, RegexOptions.ExplicitCapture);

        public CSharpArgumentList()
        {
        }

        private CSharpArgumentList(int capacity)
            : base(capacity)
        {
        }

        /// <exception cref="ReservedNameException">
        ///     The parameter name is already exists.
        /// </exception>
        public override Parameter Add(string name, string type, ParameterModifier modifier, string defaultValue)
        {
            if (IsReservedName(name))
                throw new ReservedNameException(name);

            Parameter parameter = new CSharpParameter(name, type, modifier, defaultValue);
            InnerList.Add(parameter);

            return parameter;
        }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="declaration" /> does not fit to the syntax.
        /// </exception>
        /// <exception cref="ReservedNameException">
        ///     The parameter name is already exists.
        /// </exception>
        public override Parameter Add(string declaration)
        {
            var match = singleParamterRegex.Match(declaration);

            if (match.Success)
            {
                var nameGroup = match.Groups["name"];
                var typeGroup = match.Groups["type"];
                var modifierGroup = match.Groups["modifier"];
                var defvalGroup = match.Groups["defval"];

                if (IsReservedName(nameGroup.Value))
                    throw new ReservedNameException(nameGroup.Value);

                Parameter parameter = new CSharpParameter(nameGroup.Value,
                                                          typeGroup.Value,
                                                          ParseParameterModifier(modifierGroup.Value),
                                                          defvalGroup.Value);
                InnerList.Add(parameter);

                return parameter;
            }
            throw new BadSyntaxException(
                Strings.ErrorInvalidParameterDeclaration);
        }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="declaration" /> does not fit to the syntax.
        /// </exception>
        /// <exception cref="ReservedNameException">
        ///     The parameter name is already exists.
        /// </exception>
        public override Parameter ModifyParameter(Parameter parameter, string declaration)
        {
            var match = singleParamterRegex.Match(declaration);
            var index = InnerList.IndexOf(parameter);

            if (index < 0)
                return parameter;

            if (match.Success)
            {
                var nameGroup = match.Groups["name"];
                var typeGroup = match.Groups["type"];
                var modifierGroup = match.Groups["modifier"];
                var defvalGroup = match.Groups["defval"];

                if (IsReservedName(nameGroup.Value, index))
                    throw new ReservedNameException(nameGroup.Value);

                Parameter newParameter = new CSharpParameter(nameGroup.Value,
                                                             typeGroup.Value,
                                                             ParseParameterModifier(modifierGroup.Value),
                                                             defvalGroup.Value);
                InnerList[index] = newParameter;
                return newParameter;
            }
            throw new BadSyntaxException(
                Strings.ErrorInvalidParameterDeclaration);
        }

        private ParameterModifier ParseParameterModifier(string modifierString)
        {
            switch (modifierString)
            {
                case "ref":
                    return ParameterModifier.Inout;

                case "out":
                    return ParameterModifier.Out;

                case "params":
                    return ParameterModifier.Params;

                case "in":
                default:
                    return ParameterModifier.In;
            }
        }

        public override ArgumentList Clone()
        {
            var argumentList = new CSharpArgumentList(Capacity);
            foreach (Parameter parameter in InnerList)
            {
                argumentList.InnerList.Add(parameter.Clone());
            }
            return argumentList;
        }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="declaration" /> does not fit to the syntax.
        /// </exception>
        public override void InitFromString(string declaration)
        {
            if (parameterStringRegex.IsMatch(declaration))
            {
                Clear();

                var optionalPart = false;
                foreach (Match match in parameterRegex.Matches(declaration))
                {
                    var nameGroup = match.Groups["name"];
                    var typeGroup = match.Groups["type"];
                    var modifierGroup = match.Groups["modifier"];
                    var defvalGroup = match.Groups["defval"];

                    if (defvalGroup.Success)
                        optionalPart = true;
                    else if (optionalPart)
                        throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);

                    InnerList.Add(new CSharpParameter(nameGroup.Value,
                                                      typeGroup.Value,
                                                      ParseParameterModifier(modifierGroup.Value),
                                                      defvalGroup.Value));
                }
            }
            else
            {
                throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
            }
        }
    }
}