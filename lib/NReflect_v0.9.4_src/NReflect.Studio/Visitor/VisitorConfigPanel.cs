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
using System.Windows.Forms;

namespace NReflect.Studio.Visitor
{
  /// <summary>
  /// Classes derived from this class can be used to configure a visitor.
  /// </summary>
  /// <typeparam name="T">The type of the visitor the config panel is used for.</typeparam>
  public abstract class VisitorConfigPanel<T> : UserControl
  {
    /// <summary>
    /// Gets the type of the visitor the panel is used for.
    /// </summary>
    public Type TypeOfVisitor
    {
      get { return typeof(T); }
    }
  }
}