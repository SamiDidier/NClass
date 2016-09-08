using System;
using System.Drawing;
using NClass.DiagramEditor.ClassDiagram;
using NClass.DiagramEditor.ClassDiagram.Connections;
using NClass.DiagramEditor.ClassDiagram.Shapes;

namespace NClass.AssemblyCSharpImport
{
    public static class Common
    {
        /// <summary>
        ///     Creates a nice arrangement for each entity.
        /// </summary>
        public static void ArrangeTypes(Diagram diagram)
        {
            const int Margin = Connection.Spacing*2;
            const int DiagramPadding = Shape.SelectionMargin;

            var shapeCount = diagram.ShapeCount;
            var columns = (int) Math.Ceiling(Math.Sqrt(shapeCount*2));
            var shapeIndex = 0;
            var top = Shape.SelectionMargin;
            var maxHeight = 0;

            foreach (var shape in diagram.Shapes)
            {
                var column = shapeIndex%columns;

                shape.Location = new Point(
                    (TypeShape.DefaultWidth + Margin)*column + DiagramPadding,
                    top);

                maxHeight = Math.Max(maxHeight, shape.Height);
                if (column == columns - 1)
                {
                    top += maxHeight + Margin;
                    maxHeight = 0;
                }
                shapeIndex++;
            }
        }
    }
}