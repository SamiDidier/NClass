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

namespace NReflect.Studio
{
  public class Message
  {
    // ========================================================================
    // Fields

    #region === Fields

     

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="Message"/>.
    /// </summary>
    public Message()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Message"/>.
    /// </summary>
    /// <param name="severity">The severity of the message.</param>
    /// <param name="message">The text of the message.</param>
    public Message(MessageSeverity severity, string message)
    {
      Severity = severity;
      MessageText = message;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the severity of the message.
    /// </summary>
    public MessageSeverity Severity { get; set; }
    /// <summary>
    /// Gets or sets the text of the message.
    /// </summary>
    public string MessageText { get; set; }
    /// <summary>
    /// The line belonging to the message.
    /// </summary>
    public int? Line { get; set; }
    /// <summary>
    /// The column belonging to the message.
    /// </summary>
    public int? Column { get; set; }
    /// <summary>
    /// The file belonging to the message.
    /// </summary>
    public string File { get; set; }

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