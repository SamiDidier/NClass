﻿// NClass - Free class diagram editor
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

using System.Collections.Generic;
using System.Drawing;
using NClass.Core;
using NClass.DiagramEditor.ClassDiagram.Connections;
using NClass.DiagramEditor.ClassDiagram.Shapes;

namespace NClass.DiagramEditor.ClassDiagram
{
    internal class ElementContainer : IClipboardItem
    {
        private const int BaseOffset = 20;
        private readonly List<Connection> connections = new List<Connection>();
        private int currentOffset;
        private readonly Dictionary<Shape, Shape> pastedShapes = new Dictionary<Shape, Shape>();

        private readonly List<Shape> shapes = new List<Shape>();

        void IClipboardItem.Paste(IDocument document)
        {
            var diagram = (Diagram) document;
            if (diagram != null)
            {
                var success = false;

                currentOffset += BaseOffset;
                var offset = new Size(
                    (int) ((diagram.Offset.X + currentOffset)/diagram.Zoom),
                    (int) ((diagram.Offset.Y + currentOffset)/diagram.Zoom));

                foreach (var shape in shapes)
                {
                    var pasted = shape.Paste(diagram, offset);
                    pastedShapes[shape] = pasted;
                    success |= pasted != null;
                }
                foreach (var connection in connections)
                {
                    var first = GetShape(connection.Relationship.First);
                    var second = GetShape(connection.Relationship.Second);

                    if (first != null && pastedShapes[first] != null &&
                        second != null && pastedShapes[second] != null)
                    {
                        var pasted = connection.Paste(
                            diagram,
                            offset,
                            pastedShapes[first],
                            pastedShapes[second]);
                        success |= pasted != null;
                    }
                }

                if (success)
                {
                    Clipboard.Clear();
                }
            }
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            pastedShapes.Add(shape, null);
        }

        public void AddConnection(Connection connection)
        {
            connections.Add(connection);
        }

        //TODO: legyenek inkább hivatkozások a shape-ekhez
        public Shape GetShape(IEntity entity)
        {
            foreach (var shape in shapes)
            {
                if (shape.Entity == entity)
                    return shape;
            }
            return null;
        }
    }
}