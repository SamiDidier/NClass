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

namespace NReflect.Studio.Models
{
  /// <summary>
  /// Represents the state of a test case.
  /// </summary>
  public enum TestCaseState
  {
    /// <summary>
    /// The state is unknown i.e. it has not been run yet.
    /// </summary>
    Unknown,
    /// <summary>
    /// The test was successful.
    /// </summary>
    Success,
    /// <summary>
    /// The test failed.
    /// </summary>
    Fail,
    /// <summary>
    /// The compilation of the code failed.
    /// </summary>
    CompilationFailed,
    /// <summary>
    /// The reflection failed.
    /// </summary>
    ReflectionFailed
  }
}