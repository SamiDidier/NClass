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
using NReflect.Studio.ObjectTree.Model;

namespace NReflect.Studio.ObjectTree.Comparer
{
  public class ObjectTreeModelComparer
  {
    // ========================================================================
    // Fields

    #region === Fields

    /// <summary>
    /// The results of the comparison.
    /// </summary>
    private readonly Dictionary<ObjectField, CompareResult> results;

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectTreeModelComparer"/>.
    /// </summary>
    public ObjectTreeModelComparer()
    {
      results = new Dictionary<ObjectField, CompareResult>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties


    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Gets the result for a specific field.
    /// </summary>
    /// <param name="objectField">The field to get the result for.</param>
    /// <returns>The result of the specified field.</returns>
    public CompareResult GetResult(ObjectField objectField)
    {
      if(results.ContainsKey(objectField))
      {
        return results[objectField];
      }
      return new CompareResult(ResultState.Unknown, 0);
    }

    /// <summary>
    /// Compares the two given <see cref="ObjectTreeModel"/>s.
    /// </summary>
    /// <param name="expected">The expected <see cref="ObjectTreeModel"/>.</param>
    /// <param name="current">The current <see cref="ObjectTreeModel"/> to compare.</param>
    public void Compare(ObjectTreeModel expected, ObjectTreeModel current)
    {
      results.Clear();
      CompareRecur(expected.Root, current.Root);
    }

    /// <summary>
    /// Compares the two given <see cref="ObjectField"/>s.
    /// </summary>
    /// <param name="expected">The expected <see cref="ObjectField"/>.</param>
    /// <param name="current">The current <see cref="ObjectField"/> to compare.</param>
    public void Compare(ObjectField expected, ObjectField current)
    {
      results.Clear();
      CompareRecur(expected, current);
    }

    private CompareResult CompareRecur(ObjectField expected, ObjectField current)
    {
      CompareFieldsEventArgs eventArgs = new CompareFieldsEventArgs(expected, current);
      OnCompareFields(eventArgs);
      if (eventArgs.CompareResult != null)
      {
        AddResult(expected, current, eventArgs.CompareResult);
        return eventArgs.CompareResult;
      }

      if (expected == null || current == null)
      {
        return new CompareResult(ResultState.Unknown, 0);
      }
      if(expected.GetType() != current.GetType())
      {
        // Type is different.
        return AddResult(expected, current, ResultState.NotEqual, 0);
      }

      if (expected.Value != null && current.Value == null)
      {
        // The field is missing.
        return AddResult(expected, current, ResultState.Missing, 0);
      }
      if (expected.Value == null && current.Value != null)
      {
        // The field is new.
        return AddResult(expected, current, ResultState.New, 0);
      }

      ObjectFieldValue expectedValue = expected as ObjectFieldValue;
      ObjectFieldValue currentValue = current as ObjectFieldValue;
      if(expectedValue != null && currentValue != null)
      {
        if(expectedValue.Name != currentValue.Name)
        {
          // Fields are different
          return AddResult(expected, current, ResultState.NotEqual, 0);
        }
        if(expectedValue.Value != null && currentValue.Value != null && !expectedValue.Value.Equals(currentValue.Value))
        {
          // Value is different
          return AddResult(expected, current, ResultState.NotEqual, 0);
        }
        return AddResult(expected, current, ResultState.Equal, 1);
      }

      IEnumerator<ObjectField> expectedEnumerator = expected.Childs.GetEnumerator();
      IEnumerator<ObjectField> currentEnumerator = current.Childs.GetEnumerator();
      ResultState resultState = ResultState.Equal;
      double resultPercent = 0;
      int count = 0;
      while(expectedEnumerator.MoveNext() & currentEnumerator.MoveNext())
      {
        CompareResult currentResult = CompareRecur(expectedEnumerator.Current, currentEnumerator.Current);
        if (currentResult.State != ResultState.Equal && currentResult.State != ResultState.Ignored)
        {
          resultState = ResultState.NotEqual;
        }
        if(currentResult.State != ResultState.Ignored)
        {
          resultPercent += currentResult.Percent;
          count++;
        }
      }

      if (expectedEnumerator.Current != null)
      {
        do
        {
          AddResult(expectedEnumerator.Current, null, ResultState.Missing, 0);
          count++;
        } while (expectedEnumerator.MoveNext());
      }
      if (currentEnumerator.Current != null)
      {
        do
        {
          AddResult(null, currentEnumerator.Current, ResultState.New, 0);
          count++;
        } while (currentEnumerator.MoveNext());
      }
      resultPercent = count != 0 ? resultPercent / count : 1;

      return AddResult(expected, current, resultState, resultPercent);
    }

    /// <summary>
    /// Adds a result to the list and returns the state.
    /// </summary>
    /// <param name="expected">The expected <see cref="ObjectField"/> to set the result for.</param>
    /// <param name="current">The current <see cref="ObjectField"/> to set the result for.</param>
    /// <param name="state">The state of the result.</param>
    /// <param name="percent">The percentage of equal fields.</param>
    /// <returns>The <see cref="CompareResult"/> added to the list.</returns>
    private CompareResult AddResult(ObjectField expected, ObjectField current, ResultState state, double percent)
    {
      CompareResult compareResult = new CompareResult(state, percent);
      return AddResult(expected, current, compareResult);
    }

    /// <summary>
    /// Adds a result to the list and returns the state.
    /// </summary>
    /// <param name="expected">The expected <see cref="ObjectField"/> to set the result for.</param>
    /// <param name="current">The current <see cref="ObjectField"/> to set the result for.</param>
    /// <param name="compareResult">The result to add.</param>
    /// <returns>The <see cref="CompareResult"/> added to the list.</returns>
    private CompareResult AddResult(ObjectField expected, ObjectField current, CompareResult compareResult)
    {
      if(expected != null)
      {
        results.Add(expected, compareResult);
      }
      if(current != null)
      {
        results.Add(current, compareResult);
      }
      return compareResult;
    }

    #region --- OnXXX

    /// <summary>
    /// Fires the <see cref="CompareFields"/> event.
    /// </summary>
    /// <param name="eventArgs">The event args.</param>
    protected void OnCompareFields(CompareFieldsEventArgs eventArgs)
    {
      if (CompareFields != null)
      {
        CompareFields(this, eventArgs);
      }
    }

    #endregion

    #endregion

    // ========================================================================
    // Events

    #region === Events

    /// <summary>
    /// Occures when two fields will be compared.
    /// </summary>
    public event EventHandler<CompareFieldsEventArgs> CompareFields;

    #endregion
  }

  public class CompareFieldsEventArgs : EventArgs
  {
    public CompareFieldsEventArgs(ObjectField expected, ObjectField current)
    {
      ExpectedField = expected;
      CurrentField = current;
    }

    public ObjectField ExpectedField { get; private set; }

    public ObjectField CurrentField { get; private set; }

    public CompareResult CompareResult { get; set; }
  }
}