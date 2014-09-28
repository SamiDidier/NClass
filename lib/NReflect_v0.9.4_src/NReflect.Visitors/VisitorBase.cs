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

using System.IO;

namespace NReflect.Visitors
{
  public class VisitorBase
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The writer to output the text to.
    /// </summary>
    protected TextWriter writer;
    /// <summary>
    /// The current indention.
    /// </summary>
    protected int indent;

    #endregion

    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="VisitorBase"/>.
    /// </summary>
    /// <param name="writer">This <see cref="TextWriter"/> will be used for output.</param>
    protected VisitorBase(TextWriter writer)
    {
      this.writer = writer;
      indent = 0;
      IndentSize = 2;
      IndentChar = ' ';
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets a value defining th indention size.
    /// </summary>
    public int IndentSize { get; set; }
    /// <summary>
    /// Gets or sets the character used for indention.
    /// </summary>
    public char IndentChar { get; set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Prints the given string to the current writer using the current indention.
    /// </summary>
    /// <param name="text">The text to print.</param>
    protected void Output(string text)
    {
      Output(text, indent);
    }

    /// <summary>
    /// Prints the given string to the current writer using the given indention.
    /// </summary>
    /// <param name="text">The text to print.</param>
    /// <param name="indention">The indention of the text.</param>
    protected void Output(string text, int indention)
    {
      writer.Write("{0}{1}", new string(IndentChar, indention * IndentSize), text);
    }

    /// <summary>
    /// Prints the given string to the current writer using the current indention
    /// and starts a new line.
    /// </summary>
    /// <param name="text">The text to print.</param>
    protected void OutputLine(string text)
    {
      OutputLine(text, indent);
    }

    /// <summary>
    /// Prints the given string to the current writer using the given indention
    /// and starts a new line.
    /// </summary>
    /// <param name="text">The text to print.</param>
    /// <param name="indention">The indention of the text.</param>
    protected void OutputLine(string text, int indention)
    {
      writer.WriteLine("{0}{1}", new string(IndentChar, indention * IndentSize), text);
    }

    #endregion
  }
}