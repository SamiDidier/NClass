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

using System.Drawing;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NReflect.Studio.Models;

namespace NReflect.Studio
{
  public class TestCaseStateNodeControl : BindableControl
  {
    // ========================================================================
    // Fields

    #region === Fields

    #endregion

    // ========================================================================
    // Con- / Destructors

    #region === Con- / Destructors

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Provides the size of the control.
    /// </summary>
    /// <param name="node">The node which will be displayed by the control.</param>
    /// <param name="context">A <see cref="DrawContext"/> which holds information about the drawing.</param>
    /// <returns>The size of the control.</returns>
    public override Size MeasureSize(TreeNodeAdv node, DrawContext context)
    {
      return new Size(10, context.Font.Height);
    }

    /// <summary>
    /// Draws the control (= node).
    /// </summary>
    /// <param name="node">The node to take the data from.</param>
    /// <param name="context">A <see cref="DrawContext"/> which holds information about the drawing.</param>
    public override void Draw(TreeNodeAdv node, DrawContext context)
    {
      TestCaseBase testCase = GetValue(node) as TestCaseBase;
      if(testCase != null)
      {
        // Draw a white background
        Rectangle rect = new Rectangle(context.Bounds.X + 1, context.Bounds.Y + 1, context.Bounds.Width - 3,
                                       context.Bounds.Height - 2);
        context.Graphics.FillRectangle(Brushes.White, rect);

        StringFormat format = new StringFormat(StringFormatFlags.NoWrap) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        if (testCase.State == TestCaseState.Unknown)
        {
          context.Graphics.DrawString("-", context.Font, Brushes.Black, rect, format);
        }
        else
        {
          SolidBrush bgBrush =
            new SolidBrush(Color.FromArgb((int)((1 - testCase.Percent) * 255), (int)(testCase.Percent * 255), 0));
          context.Graphics.FillRectangle(bgBrush, rect.X, rect.Y,
                                         (int)(rect.Width * testCase.Percent), rect.Height);
          context.Graphics.DrawString(string.Format("{0}%", (int)(testCase.Percent * 100)), context.Font,
                                      Brushes.Black, rect, format);
        }
      }
    }

    #endregion

    // ========================================================================
    // Events

    #region === Events

    #endregion
  }
}