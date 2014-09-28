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

namespace NReflect.Studio
{
  /// <summary>
  /// Holds more information about the status events.
  /// </summary>
  public class StatusEventArgs : EventArgs
  {
    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="StatusEventArgs"/>.
    /// </summary>
    public StatusEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="StatusEventArgs"/>.
    /// </summary>
    /// <param name="statusMessage">The status message.</param>
    public StatusEventArgs(string statusMessage)
    {
      StatusMessage = statusMessage;
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the status message.
    /// </summary>
    public string StatusMessage { get; set; }

    #endregion
  }
}