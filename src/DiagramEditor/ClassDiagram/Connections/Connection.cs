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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using NClass.Core;
using NClass.DiagramEditor.ClassDiagram.ContextMenus;
using NClass.DiagramEditor.ClassDiagram.Shapes;

namespace NClass.DiagramEditor.ClassDiagram.Connections
{
    public abstract class Connection : DiagramElement
    {
        public const int Spacing = 25;
        public const int PrecisionSize = 6;
        private const int PickTolerance = 4;
        protected static readonly Size TextMargin = new Size(5, 3);
        private static readonly float[] dashPattern = {5, 5};
        private static readonly Pen linePen = new Pen(Color.Black);
        private static readonly SolidBrush textBrush = new SolidBrush(Color.Black);
        private static readonly StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
        private readonly OrderedList<BendPoint> bendPoints = new OrderedList<BendPoint>();
        private bool copied;
        private LineOrientation endOrientation;

        private Point[] routeCacheArray;
        private BendPoint selectedBendPoint;
        private LineOrientation startOrientation;

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="relationship" /> is null.-or-
        ///     <paramref name="startShape" /> is null.-or-
        ///     <paramref name="endShape" /> is null.
        /// </exception>
        protected Connection(Relationship relationship, Shape startShape, Shape endShape)
        {
            if (relationship == null)
                throw new ArgumentNullException("relationship");
            if (startShape == null)
                throw new ArgumentNullException("startShape");
            if (endShape == null)
                throw new ArgumentNullException("endShape");

            StartShape = startShape;
            EndShape = endShape;
            InitOrientations();
            bendPoints.Add(new BendPoint(startShape, true));
            bendPoints.Add(new BendPoint(endShape, false));

            startShape.Move += ShapeMoving;
            startShape.Resize += StartShapeResizing;
            endShape.Move += ShapeMoving;
            endShape.Resize += EndShapeResizing;

            relationship.Modified += delegate { OnModified(EventArgs.Empty); };

            relationship.Detaching += delegate
            {
                startShape.Move -= ShapeMoving;
                startShape.Resize -= StartShapeResizing;
                endShape.Move -= ShapeMoving;
                endShape.Resize -= EndShapeResizing;
            };
            relationship.Serializing += delegate(object sender, SerializeEventArgs e) { OnSerializing(e); };
            relationship.Deserializing += delegate(object sender, SerializeEventArgs e) { OnDeserializing(e); };
            Reroute();
        }

        protected internal abstract Relationship Relationship { get; }

        protected Shape StartShape { get; private set; }

        protected Shape EndShape { get; private set; }

        public IEnumerable<BendPoint> BendPoints { get { return bendPoints; } }

        protected List<Point> RouteCache { get; } = new List<Point>();

        private BendPoint FirstBendPoint { get { return bendPoints.FirstValue; } }

        private BendPoint LastBendPoint { get { return bendPoints.LastValue; } }

        protected virtual Size StartCapSize { get { return Size.Empty; } }

        protected virtual Size EndCapSize { get { return Size.Empty; } }

        protected virtual int StartSelectionOffset { get { return 0; } }

        protected virtual int EndSelectionOffset { get { return 0; } }

        protected virtual bool IsDashed { get { return false; } }

        public event EventHandler RouteChanged;
        public event BendPointEventHandler BendPointMove;

        public void InitOrientations()
        {
            if (StartShape == EndShape)
            {
                startOrientation = LineOrientation.Horizontal;
                endOrientation = LineOrientation.Vertical;
            }
            else
            {
                var hDiff = Math.Max(StartShape.Left - EndShape.Right, EndShape.Left - StartShape.Right);
                var vDiff = Math.Max(StartShape.Top - EndShape.Bottom, EndShape.Top - StartShape.Bottom);

                if (vDiff >= Spacing*2)
                {
                    startOrientation = LineOrientation.Vertical;
                    endOrientation = LineOrientation.Vertical;
                }
                else if (hDiff >= Spacing*2)
                {
                    startOrientation = LineOrientation.Horizontal;
                    endOrientation = LineOrientation.Horizontal;
                }
                else
                {
                    startOrientation = LineOrientation.Vertical;
                    endOrientation = LineOrientation.Horizontal;
                }
            }
        }

        protected override RectangleF CalculateDrawingArea(Style style, bool printing, float zoom)
        {
            RectangleF area = GetRouteArea();

            var lineSize = (float) style.RelationshipWidth/2;
            if (IsSelected && !printing)
                lineSize = Math.Max(lineSize, (float) BendPoint.SquareSize/2/zoom);
            area.Inflate(lineSize, lineSize);

            if (StartCapSize != Size.Empty)
                area = RectangleF.Union(area, GetStartCapArea());

            if (EndCapSize != Size.Empty)
                area = RectangleF.Union(area, GetEndCapArea());

            if (Relationship.Label != null)
                area = RectangleF.Union(area, GetLabelArea(style));

            return area;
        }

        private RectangleF GetStartCapArea()
        {
            var area = new RectangleF(RouteCache[0], StartCapSize);
            var angle = GetAngle(RouteCache[0], RouteCache[1]);

            if (angle == 0 || angle == 180) // Vertical direction
            {
                area.X -= (float) StartCapSize.Width/2;
            }
            if (angle == 90 || angle == 270) // Horizontal direction
            {
                area.Y -= (float) StartCapSize.Width/2;
                area.Width = StartCapSize.Height;
                area.Height = StartCapSize.Width;
            }

            if (angle == 90) // Left
            {
                area.X -= StartCapSize.Height;
            }
            else if (angle == 180) // Up
            {
                area.Y -= StartCapSize.Height;
            }

            return area;
        }

        private RectangleF GetEndCapArea()
        {
            var lastIndex = RouteCache.Count - 1;
            var area = new RectangleF(RouteCache[lastIndex], EndCapSize);
            var angle = GetAngle(RouteCache[lastIndex], RouteCache[lastIndex - 1]);

            if (angle == 0 || angle == 180) // Up-down direction
            {
                area.X -= (float) EndCapSize.Width/2;
            }
            if (angle == 90 || angle == 270) // Left-right direction
            {
                area.Y -= (float) EndCapSize.Width/2;
                area.Width = EndCapSize.Height;
                area.Height = EndCapSize.Width;
            }

            if (angle == 90) // Left
            {
                area.X -= EndCapSize.Height;
            }
            else if (angle == 180) // Up
            {
                area.Y -= EndCapSize.Height;
            }

            return area;
        }

        private RectangleF GetLabelArea(Style style)
        {
            bool horizontal;
            var center = GetLineCenter(out horizontal);

            var size = Graphics.MeasureString(Relationship.Label,
                                              style.RelationshipTextFont,
                                              PointF.Empty,
                                              stringFormat);

            if (horizontal)
            {
                center.X -= size.Width/2;
                center.Y -= size.Height + TextMargin.Height;
            }
            else
            {
                center.Y -= size.Height/2;
                center.X += TextMargin.Width;
            }

            return new RectangleF(center.X, center.Y, size.Width, size.Height);
        }

        private Rectangle GetRouteArea()
        {
            var topLeft = RouteCache[0];
            var bottomRight = RouteCache[0];

            for (var i = 1; i < RouteCache.Count; i++)
            {
                if (topLeft.X > RouteCache[i].X)
                    topLeft.X = RouteCache[i].X;
                if (topLeft.Y > RouteCache[i].Y)
                    topLeft.Y = RouteCache[i].Y;
                if (bottomRight.X < RouteCache[i].X)
                    bottomRight.X = RouteCache[i].X;
                if (bottomRight.Y < RouteCache[i].Y)
                    bottomRight.Y = RouteCache[i].Y;
            }

            return Rectangle.FromLTRB(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);
        }

        private void ShapeMoving(object sender, MoveEventArgs e)
        {
            Reroute();
            OnRouteChanged(EventArgs.Empty);
            OnModified(EventArgs.Empty);
        }

        private void StartShapeResizing(object sender, ResizeEventArgs e)
        {
            foreach (var bendPoint in bendPoints)
            {
                if (!bendPoint.RelativeToStartShape)
                    break;
                bendPoint.ShapeResized(e.Change);
            }

            Reroute();
            OnRouteChanged(EventArgs.Empty);
            OnModified(EventArgs.Empty);
        }

        private void EndShapeResizing(object sender, ResizeEventArgs e)
        {
            foreach (var bendPoint in bendPoints.GetReversedList())
            {
                if (bendPoint.RelativeToStartShape)
                    break;
                bendPoint.ShapeResized(e.Change);
            }

            Reroute();
            OnRouteChanged(EventArgs.Empty);
            OnModified(EventArgs.Empty);
        }

        internal void AutoRoute()
        {
            if (bendPoints.Count > 0)
            {
                ClearBendPoints();
                Reroute();
                OnRouteChanged(EventArgs.Empty);
                OnModified(EventArgs.Empty);
            }
        }

        private void ClearBendPoints()
        {
            var startPoint = FirstBendPoint;
            var endPoint = LastBendPoint;

            bendPoints.Clear();
            bendPoints.Add(startPoint);
            bendPoints.Add(endPoint);
            startPoint.AutoPosition = true;
            endPoint.AutoPosition = true;
        }

        protected void Reverse()
        {
            var shape = StartShape;
            StartShape = EndShape;
            EndShape = shape;

            var orientation = startOrientation;
            startOrientation = endOrientation;
            endOrientation = orientation;

            bendPoints.Reverse();
            RouteCache.Reverse();
            foreach (var point in BendPoints)
            {
                point.RelativeToStartShape = !point.RelativeToStartShape;
            }

            NeedsRedraw = true;
        }

        internal void ShowPropertiesDialog()
        {
            //UNDONE: Connection.ShowPropertiesDialog()
            throw new NotImplementedException();
        }

        protected internal sealed override Rectangle GetLogicalArea()
        {
            return GetRouteArea();
        }

        public override void Draw(IGraphics g, bool onScreen, Style style)
        {
            DrawLine(g, onScreen, style);
            DrawCaps(g, onScreen, style);
            if (Relationship.SupportsLabel)
                DrawLabel(g, onScreen, style);
        }

        private void DrawLine(IGraphics g, bool onScreen, Style style)
        {
            if (!IsSelected || !onScreen)
            {
                linePen.Width = style.RelationshipWidth;
                linePen.Color = style.RelationshipColor;
                if (IsDashed)
                {
                    dashPattern[0] = style.RelationshipDashSize;
                    dashPattern[1] = style.RelationshipDashSize;
                    linePen.DashPattern = dashPattern;
                }
                else
                {
                    linePen.DashStyle = DashStyle.Solid;
                }

                g.DrawLines(linePen, routeCacheArray);
            }
        }

        private void DrawCaps(IGraphics g, bool onScreen, Style style)
        {
            var transformState = g.Transform;
            g.TranslateTransform(RouteCache[0].X, RouteCache[0].Y);
            g.RotateTransform(GetAngle(RouteCache[0], RouteCache[1]));
            DrawStartCap(g, onScreen, style);
            g.Transform = transformState;

            var last = RouteCache.Count - 1;
            g.TranslateTransform(RouteCache[last].X, RouteCache[last].Y);
            g.RotateTransform(GetAngle(RouteCache[last], RouteCache[last - 1]));
            DrawEndCap(g, onScreen, style);
            g.Transform = transformState;
        }

        protected float GetAngle(Point point1, Point point2)
        {
            if (point1.X == point2.X)
            {
                return point1.Y < point2.Y ? 0 : 180;
            }
            if (point1.Y == point2.Y)
            {
                return point1.X < point2.X ? 270 : 90;
            }
            return (float) (
                Math.Atan2(point2.Y - point1.Y, point2.X - point1.X)*(180/Math.PI)) - 90;
        }

        protected virtual void DrawStartCap(IGraphics g, bool onScreen, Style style)
        {
        }

        protected virtual void DrawEndCap(IGraphics g, bool onScreen, Style style)
        {
        }

        private void DrawLabel(IGraphics g, bool onScreen, Style style)
        {
            if (Relationship.Label != null)
            {
                bool horizontal;
                var center = GetLineCenter(out horizontal);

                textBrush.Color = style.RelationshipTextColor;
                if (horizontal)
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    center.Y -= TextMargin.Height;
                }
                else
                {
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    center.X += TextMargin.Width;
                }
                g.DrawString(Relationship.Label,
                             style.RelationshipTextFont,
                             textBrush,
                             center,
                             stringFormat);
            }
        }

        private PointF GetLineCenter(out bool horizontal)
        {
            var lineLength = 0;
            for (var i = 0; i < RouteCache.Count - 1; i++)
            {
                if (RouteCache[i].X == RouteCache[i + 1].X)
                    lineLength += Math.Abs(RouteCache[i].Y - RouteCache[i + 1].Y);
                else
                    lineLength += Math.Abs(RouteCache[i].X - RouteCache[i + 1].X);
            }

            var distance = lineLength/2;
            var index = 0;
            horizontal = true;
            while (distance >= 0)
            {
                if (RouteCache[index].X == RouteCache[index + 1].X)
                {
                    distance -= Math.Abs(RouteCache[index].Y - RouteCache[index + 1].Y);
                    horizontal = false;
                }
                else
                {
                    distance -= Math.Abs(RouteCache[index].X - RouteCache[index + 1].X);
                    horizontal = true;
                }
                index++;
            }

            return new PointF(
                (float) (RouteCache[index - 1].X + RouteCache[index].X)/2,
                (float) (RouteCache[index - 1].Y + RouteCache[index].Y)/2
                );
        }

        protected internal override void DrawSelectionLines(Graphics g, float zoom, Point offset)
        {
            if (IsSelected)
            {
                var route = RouteCache.ToArray();
                var length = route.Length;
                for (var i = 0; i < route.Length; i++)
                {
                    route[i].X = (int) (route[i].X*zoom) - offset.X;
                    route[i].Y = (int) (route[i].Y*zoom) - offset.Y;
                }

                // Cut the line's start section
                var startOffset = (int) (StartSelectionOffset*zoom);
                if (route[0].X == route[1].X)
                {
                    if (route[0].Y < route[1].Y)
                        route[0].Y += startOffset;
                    else
                        route[0].Y -= startOffset;
                }
                else
                {
                    if (route[0].X < route[1].X)
                        route[0].X += startOffset;
                    else
                        route[0].X -= startOffset;
                }
                // Cut the line's end section
                var endOffset = (int) (EndSelectionOffset*zoom);
                if (route[length - 1].X == route[length - 2].X)
                {
                    if (route[length - 1].Y < route[length - 2].Y)
                        route[length - 1].Y += endOffset;
                    else
                        route[length - 1].Y -= endOffset;
                }
                else
                {
                    if (route[length - 1].X < route[length - 2].X)
                        route[length - 1].X += endOffset;
                    else
                        route[length - 1].X -= endOffset;
                }

                g.DrawLines(Diagram.SelectionPen, route);

                if (zoom > UndreadableZoom)
                {
                    foreach (var point in bendPoints)
                        point.Draw(g, true, zoom, offset);
                }
            }
        }

        protected virtual void Reroute()
        {
            RecalculateOrientations();
            RelocateAutoBendPoints();
            RerouteFromBendPoints();
        }

        private void RecalculateOrientations()
        {
            if (!FirstBendPoint.AutoPosition)
            {
                if (FirstBendPoint.X >= StartShape.Left && FirstBendPoint.X <= StartShape.Right)
                {
                    startOrientation = LineOrientation.Vertical;
                }
                else if (FirstBendPoint.Y >= StartShape.Top && FirstBendPoint.Y <= StartShape.Bottom)
                {
                    startOrientation = LineOrientation.Horizontal;
                }
            }
            if (!LastBendPoint.AutoPosition)
            {
                if (LastBendPoint.X >= EndShape.Left && LastBendPoint.X <= EndShape.Right)
                {
                    endOrientation = LineOrientation.Vertical;
                }
                else if (LastBendPoint.Y >= EndShape.Top && LastBendPoint.Y <= EndShape.Bottom)
                {
                    endOrientation = LineOrientation.Horizontal;
                }
            }
        }

        private void RelocateAutoBendPoints()
        {
            if (FirstBendPoint.AutoPosition && LastBendPoint.AutoPosition)
            {
                if (startOrientation == endOrientation && StartShape == EndShape)
                {
                    startOrientation = LineOrientation.Horizontal;
                    endOrientation = LineOrientation.Vertical;
                }

                if (startOrientation == LineOrientation.Horizontal &&
                    endOrientation == LineOrientation.Horizontal)
                {
                    if (StartShape.Right <= EndShape.Left - 2*Spacing)
                    {
                        FirstBendPoint.X = StartShape.Right + Spacing;
                        LastBendPoint.X = EndShape.Left - Spacing;
                    }
                    else if (StartShape.Left >= EndShape.Right + 2*Spacing)
                    {
                        FirstBendPoint.X = StartShape.Left - Spacing;
                        LastBendPoint.X = EndShape.Right + Spacing;
                    }
                    else
                    {
                        if (Math.Abs(StartShape.Left - EndShape.Left) <
                            Math.Abs(StartShape.Right - EndShape.Right))
                        {
                            FirstBendPoint.X = StartShape.Left - Spacing;
                            LastBendPoint.X = EndShape.Left - Spacing;
                        }
                        else
                        {
                            FirstBendPoint.X = StartShape.Right + Spacing;
                            LastBendPoint.X = EndShape.Right + Spacing;
                        }
                    }

                    Shape smallerShape, biggerShape;
                    if (StartShape.Height < EndShape.Height)
                    {
                        smallerShape = StartShape;
                        biggerShape = EndShape;
                    }
                    else
                    {
                        smallerShape = EndShape;
                        biggerShape = StartShape;
                    }

                    if (biggerShape.Top <= smallerShape.VerticalCenter &&
                        biggerShape.Bottom >= smallerShape.VerticalCenter)
                    {
                        var center = (
                            Math.Max(StartShape.Top, EndShape.Top) +
                            Math.Min(StartShape.Bottom, EndShape.Bottom))/2;

                        FirstBendPoint.Y = center;
                        LastBendPoint.Y = center;
                    }
                    else
                    {
                        FirstBendPoint.Y = StartShape.VerticalCenter;
                        LastBendPoint.Y = EndShape.VerticalCenter;
                    }
                }
                else if (startOrientation == LineOrientation.Vertical &&
                         endOrientation == LineOrientation.Vertical)
                {
                    if (StartShape.Bottom <= EndShape.Top - 2*Spacing)
                    {
                        FirstBendPoint.Y = StartShape.Bottom + Spacing;
                        LastBendPoint.Y = EndShape.Top - Spacing;
                    }
                    else if (StartShape.Top >= EndShape.Bottom + 2*Spacing)
                    {
                        FirstBendPoint.Y = StartShape.Top - Spacing;
                        LastBendPoint.Y = EndShape.Bottom + Spacing;
                    }
                    else
                    {
                        if (Math.Abs(StartShape.Top - EndShape.Top) <
                            Math.Abs(StartShape.Bottom - EndShape.Bottom))
                        {
                            FirstBendPoint.Y = StartShape.Top - Spacing;
                            LastBendPoint.Y = EndShape.Top - Spacing;
                        }
                        else
                        {
                            FirstBendPoint.Y = StartShape.Bottom + Spacing;
                            LastBendPoint.Y = EndShape.Bottom + Spacing;
                        }
                    }

                    Shape smallerShape, biggerShape;
                    if (StartShape.Width < EndShape.Width)
                    {
                        smallerShape = StartShape;
                        biggerShape = EndShape;
                    }
                    else
                    {
                        smallerShape = EndShape;
                        biggerShape = StartShape;
                    }

                    if (biggerShape.Left <= smallerShape.HorizontalCenter &&
                        biggerShape.Right >= smallerShape.HorizontalCenter)
                    {
                        var center = (
                            Math.Max(StartShape.Left, EndShape.Left) +
                            Math.Min(StartShape.Right, EndShape.Right))/2;

                        FirstBendPoint.X = center;
                        LastBendPoint.X = center;
                    }
                    else
                    {
                        FirstBendPoint.X = StartShape.HorizontalCenter;
                        LastBendPoint.X = EndShape.HorizontalCenter;
                    }
                }
                else
                {
                    if (startOrientation == LineOrientation.Horizontal)
                    {
                        FirstBendPoint.Y = StartShape.VerticalCenter;
                        LastBendPoint.X = EndShape.HorizontalCenter;

                        if (LastBendPoint.X >= StartShape.HorizontalCenter)
                            FirstBendPoint.X = StartShape.Right + Spacing;
                        else
                            FirstBendPoint.X = StartShape.Left - Spacing;

                        if (FirstBendPoint.Y >= EndShape.VerticalCenter)
                            LastBendPoint.Y = EndShape.Bottom + Spacing;
                        else
                            LastBendPoint.Y = EndShape.Top - Spacing;
                    }
                    else
                    {
                        FirstBendPoint.X = StartShape.HorizontalCenter;
                        LastBendPoint.Y = EndShape.VerticalCenter;

                        if (LastBendPoint.Y >= StartShape.VerticalCenter)
                            FirstBendPoint.Y = StartShape.Bottom + Spacing;
                        else
                            FirstBendPoint.Y = StartShape.Top - Spacing;

                        if (FirstBendPoint.X >= EndShape.HorizontalCenter)
                            LastBendPoint.X = EndShape.Right + Spacing;
                        else
                            LastBendPoint.X = EndShape.Left - Spacing;
                    }
                }
            }
            else if (FirstBendPoint.AutoPosition)
            {
                if (startOrientation == LineOrientation.Horizontal)
                {
                    if (bendPoints.SecondValue.X < StartShape.HorizontalCenter)
                        FirstBendPoint.X = StartShape.Left - Spacing;
                    else
                        FirstBendPoint.X = StartShape.Right + Spacing;

                    if (bendPoints.SecondValue.Y >= StartShape.Top &&
                        bendPoints.SecondValue.Y <= StartShape.Bottom)
                    {
                        FirstBendPoint.Y = bendPoints.SecondValue.Y;
                    }
                    else
                    {
                        FirstBendPoint.Y = StartShape.VerticalCenter;
                    }
                }
                else
                {
                    if (bendPoints.SecondValue.Y < StartShape.VerticalCenter)
                        FirstBendPoint.Y = StartShape.Top - Spacing;
                    else
                        FirstBendPoint.Y = StartShape.Bottom + Spacing;

                    if (bendPoints.SecondValue.X >= StartShape.Left &&
                        bendPoints.SecondValue.X <= StartShape.Right)
                    {
                        FirstBendPoint.X = bendPoints.SecondValue.X;
                    }
                    else
                    {
                        FirstBendPoint.X = StartShape.HorizontalCenter;
                    }
                }
            }
            else if (LastBendPoint.AutoPosition)
            {
                if (endOrientation == LineOrientation.Horizontal)
                {
                    if (bendPoints.SecondLastValue.X < EndShape.HorizontalCenter)
                        LastBendPoint.X = EndShape.Left - Spacing;
                    else
                        LastBendPoint.X = EndShape.Right + Spacing;

                    if (bendPoints.SecondLastValue.Y >= EndShape.Top &&
                        bendPoints.SecondLastValue.Y <= EndShape.Bottom)
                    {
                        LastBendPoint.Y = bendPoints.SecondLastValue.Y;
                    }
                    else
                    {
                        LastBendPoint.Y = EndShape.VerticalCenter;
                    }
                }
                else
                {
                    if (bendPoints.SecondLastValue.Y < EndShape.VerticalCenter)
                        LastBendPoint.Y = EndShape.Top - Spacing;
                    else
                        LastBendPoint.Y = EndShape.Bottom + Spacing;

                    if (bendPoints.SecondLastValue.X >= EndShape.Left &&
                        bendPoints.SecondLastValue.X <= EndShape.Right)
                    {
                        LastBendPoint.X = bendPoints.SecondLastValue.X;
                    }
                    else
                    {
                        LastBendPoint.X = EndShape.HorizontalCenter;
                    }
                }
            }
        }

        private void RerouteFromBendPoints()
        {
            RouteCache.Clear();

            var direction = AddStartSegment();

            var current = bendPoints.First;
            while (current != bendPoints.Last)
            {
                direction = AddInnerSegment(current, direction);
                current = current.Next;
            }

            AddEndSegment();

            routeCacheArray = RouteCache.ToArray();
            Array.Reverse(routeCacheArray);
        }

        private FlowDirection AddInnerSegment(LinkedListNode<BendPoint> current, FlowDirection direction)
        {
            var activePoint = current.Value;
            var nextPoint = current.Next.Value;

            if (nextPoint.X == activePoint.X)
            {
                RouteCache.Add(nextPoint.Location);

                if (nextPoint.Y < activePoint.Y)
                    return FlowDirection.BottomUp;
                return FlowDirection.TopDown;
            }
            if (nextPoint.Y == activePoint.Y)
            {
                RouteCache.Add(nextPoint.Location);

                if (nextPoint.X < activePoint.X)
                    return FlowDirection.RightToLeft;
                return FlowDirection.LeftToRight;
            }

            if (direction == FlowDirection.TopDown)
            {
                if (nextPoint.Y < activePoint.Y)
                {
                    RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.BottomUp;
                }
                var nextNextPoint = GetNextNextPoint(current);

                if (current.Next.Next == null &&
                    nextNextPoint.X == nextPoint.X &&
                    nextNextPoint.Y >= nextPoint.Y)
                {
                    var center = (nextPoint.Y + activePoint.Y)/2;
                    RouteCache.Add(new Point(activePoint.X, center));
                    RouteCache.Add(new Point(nextPoint.X, center));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.TopDown;
                }
                if (nextPoint.X < activePoint.X)
                {
                    if (nextNextPoint.X >= activePoint.X ||
                        (nextNextPoint.Y >= nextPoint.Y &&
                         nextNextPoint.X > nextPoint.X))
                    {
                        RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                        RouteCache.Add(nextPoint.Location);
                        return FlowDirection.TopDown;
                    }
                    RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.RightToLeft;
                }
                if (nextNextPoint.X <= activePoint.X ||
                    (nextNextPoint.Y >= nextPoint.Y &&
                     nextNextPoint.X < nextPoint.X))
                {
                    RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.TopDown;
                }
                RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                RouteCache.Add(nextPoint.Location);
                return FlowDirection.LeftToRight;
            }
            if (direction == FlowDirection.BottomUp)
            {
                if (nextPoint.Y > activePoint.Y)
                {
                    RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.TopDown;
                }
                var nextNextPoint = GetNextNextPoint(current);

                if (current.Next.Next == null &&
                    nextNextPoint.X == nextPoint.X &&
                    nextNextPoint.Y <= nextPoint.Y)
                {
                    var center = (nextPoint.Y + activePoint.Y)/2;
                    RouteCache.Add(new Point(activePoint.X, center));
                    RouteCache.Add(new Point(nextPoint.X, center));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.BottomUp;
                }
                if (nextPoint.X > activePoint.X)
                {
                    if (nextNextPoint.X <= activePoint.X ||
                        (nextNextPoint.Y <= nextPoint.Y &&
                         nextNextPoint.X < nextPoint.X))
                    {
                        RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                        RouteCache.Add(nextPoint.Location);
                        return FlowDirection.BottomUp;
                    }
                    RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.LeftToRight;
                }
                if (nextNextPoint.X >= activePoint.X ||
                    (nextNextPoint.Y <= nextPoint.Y &&
                     nextNextPoint.X > nextPoint.X))
                {
                    RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.BottomUp;
                }
                RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                RouteCache.Add(nextPoint.Location);
                return FlowDirection.RightToLeft;
            }
            if (direction == FlowDirection.LeftToRight)
            {
                if (nextPoint.X < activePoint.X)
                {
                    RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.RightToLeft;
                }
                var nextNextPoint = GetNextNextPoint(current);

                if (current.Next.Next == null &&
                    nextNextPoint.Y == nextPoint.Y &&
                    nextNextPoint.X >= nextPoint.X)
                {
                    var center = (nextPoint.X + activePoint.X)/2;
                    RouteCache.Add(new Point(center, activePoint.Y));
                    RouteCache.Add(new Point(center, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.LeftToRight;
                }
                if (nextPoint.Y > activePoint.Y)
                {
                    if (nextNextPoint.Y <= activePoint.Y ||
                        (nextNextPoint.X >= nextPoint.X &&
                         nextNextPoint.Y < nextPoint.Y))
                    {
                        RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                        RouteCache.Add(nextPoint.Location);
                        return FlowDirection.LeftToRight;
                    }
                    RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.TopDown;
                }
                if (nextNextPoint.Y >= activePoint.Y ||
                    (nextNextPoint.X >= nextPoint.X &&
                     nextNextPoint.Y > nextPoint.Y))
                {
                    RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.LeftToRight;
                }
                RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                RouteCache.Add(nextPoint.Location);
                return FlowDirection.BottomUp;
            }
            if (direction == FlowDirection.RightToLeft)
            {
                if (nextPoint.X > activePoint.X)
                {
                    RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.LeftToRight;
                }
                var nextNextPoint = GetNextNextPoint(current);

                if (current.Next.Next == null &&
                    nextNextPoint.Y == nextPoint.Y &&
                    nextNextPoint.X <= nextPoint.X)
                {
                    var center = (nextPoint.X + activePoint.X)/2;
                    RouteCache.Add(new Point(center, activePoint.Y));
                    RouteCache.Add(new Point(center, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.RightToLeft;
                }
                if (nextPoint.Y < activePoint.Y)
                {
                    if (nextNextPoint.Y >= activePoint.Y ||
                        (nextNextPoint.X <= nextPoint.X &&
                         nextNextPoint.Y > nextPoint.Y))
                    {
                        RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                        RouteCache.Add(nextPoint.Location);
                        return FlowDirection.RightToLeft;
                    }
                    RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.BottomUp;
                }
                if (nextNextPoint.Y <= activePoint.Y ||
                    (nextNextPoint.X <= nextPoint.X &&
                     nextNextPoint.Y < nextPoint.Y))
                {
                    RouteCache.Add(new Point(activePoint.X, nextPoint.Y));
                    RouteCache.Add(nextPoint.Location);
                    return FlowDirection.RightToLeft;
                }
                RouteCache.Add(new Point(nextPoint.X, activePoint.Y));
                RouteCache.Add(nextPoint.Location);
                return FlowDirection.TopDown;
            }
            RouteCache.Add(nextPoint.Location);
            return direction;
        }

        private Point GetNextNextPoint(LinkedListNode<BendPoint> current)
        {
            var next = current.Next;
            var nextNext = next.Next;

            if (nextNext != null)
            {
                return nextNext.Value.Location;
            }
            var nextNextPoint = next.Value.Location;

            if (nextNextPoint.X < EndShape.Left)
                nextNextPoint.X = EndShape.Left;
            else if (nextNextPoint.X > EndShape.Right)
                nextNextPoint.X = EndShape.Right;
            if (nextNextPoint.Y < EndShape.Top)
                nextNextPoint.Y = EndShape.Top;
            else if (nextNextPoint.Y > EndShape.Bottom)
                nextNextPoint.Y = EndShape.Bottom;

            return nextNextPoint;
        }

        private FlowDirection AddStartSegment()
        {
            if (startOrientation == LineOrientation.Horizontal)
            {
                int startX, startY;

                if (FirstBendPoint.X < StartShape.HorizontalCenter)
                    startX = StartShape.Left;
                else
                    startX = StartShape.Right;

                if (FirstBendPoint.Y >= StartShape.Top &&
                    FirstBendPoint.Y <= StartShape.Bottom)
                {
                    startY = FirstBendPoint.Y;
                    RouteCache.Add(new Point(startX, startY));
                    RouteCache.Add(FirstBendPoint.Location);

                    if (startX == StartShape.Left)
                        return FlowDirection.RightToLeft;
                    return FlowDirection.LeftToRight;
                }
                startY = StartShape.VerticalCenter;
                RouteCache.Add(new Point(startX, startY));
                RouteCache.Add(new Point(FirstBendPoint.X, startY));
                RouteCache.Add(FirstBendPoint.Location);

                if (FirstBendPoint.Y < startY)
                    return FlowDirection.BottomUp;
                return FlowDirection.TopDown;
            }
            else
            {
                int startX, startY;

                if (FirstBendPoint.Y < StartShape.VerticalCenter)
                    startY = StartShape.Top;
                else
                    startY = StartShape.Bottom;

                if (FirstBendPoint.X >= StartShape.Left &&
                    FirstBendPoint.X <= StartShape.Right)
                {
                    startX = FirstBendPoint.X;
                    RouteCache.Add(new Point(startX, startY));
                    RouteCache.Add(FirstBendPoint.Location);

                    if (startY == StartShape.Top)
                        return FlowDirection.BottomUp;
                    return FlowDirection.TopDown;
                }
                startX = StartShape.HorizontalCenter;
                RouteCache.Add(new Point(startX, startY));
                RouteCache.Add(new Point(startX, FirstBendPoint.Y));
                RouteCache.Add(FirstBendPoint.Location);

                if (FirstBendPoint.X < startX)
                    return FlowDirection.RightToLeft;
                return FlowDirection.LeftToRight;
            }
        }

        private void AddEndSegment()
        {
            if (endOrientation == LineOrientation.Horizontal)
            {
                int endX, endY;

                if (LastBendPoint.X < EndShape.HorizontalCenter)
                    endX = EndShape.Left;
                else
                    endX = EndShape.Right;

                if (LastBendPoint.Y >= EndShape.Top &&
                    LastBendPoint.Y <= EndShape.Bottom)
                {
                    endY = LastBendPoint.Y;
                }
                else
                {
                    endY = EndShape.VerticalCenter;
                    RouteCache.Add(new Point(LastBendPoint.X, endY));
                }
                RouteCache.Add(new Point(endX, endY));
            }
            else
            {
                int endX, endY;

                if (LastBendPoint.Y < EndShape.VerticalCenter)
                    endY = EndShape.Top;
                else
                    endY = EndShape.Bottom;

                if (LastBendPoint.X >= EndShape.Left &&
                    LastBendPoint.X <= EndShape.Right)
                {
                    endX = LastBendPoint.X;
                }
                else
                {
                    endX = EndShape.HorizontalCenter;
                    RouteCache.Add(new Point(endX, LastBendPoint.Y));
                }
                RouteCache.Add(new Point(endX, endY));
            }
        }

        protected internal sealed override bool TrySelect(RectangleF frame)
        {
            if (Picked(frame))
            {
                IsSelected = true;
                return true;
            }
            return false;
        }

        protected internal sealed override void Offset(Size offset)
        {
            // Do nothing
        }

        protected internal override Size GetMaximalOffset(Size offset, int padding)
        {
            if (!IsSelected && !StartShape.IsSelected && !EndShape.IsSelected)
                return offset;

            foreach (var bendPoint in bendPoints)
            {
                if (IsSelected || (bendPoint.RelativeToStartShape && StartShape.IsSelected) ||
                    (!bendPoint.RelativeToStartShape && EndShape.IsSelected))
                {
                    var newLocation = bendPoint.Location + offset;

                    if (newLocation.X < padding)
                        offset.Width += padding - newLocation.X;
                    if (newLocation.Y < padding)
                        offset.Height += padding - newLocation.Y;
                }
            }
            return offset;
        }

        [Obsolete]
        protected internal sealed override void Serialize(XmlElement node)
        {
            OnSerializing(new SerializeEventArgs(node));
        }

        [Obsolete]
        protected internal sealed override void Deserialize(XmlElement node)
        {
            OnDeserializing(new SerializeEventArgs(node));
        }

        private BendPoint GetBendPoint(AbsoluteMouseEventArgs e)
        {
            if (e.Zoom <= UndreadableZoom)
                return null;

            foreach (var point in bendPoints)
            {
                if (point.Contains(e.Location, e.Zoom))
                    return point;
            }
            return null;
        }

        private bool BendPointPressed(AbsoluteMouseEventArgs e)
        {
            var point = GetBendPoint(e);

            selectedBendPoint = point;
            if (point != null)
            {
                if (point.AutoPosition)
                {
                    point.AutoPosition = false;
                    Reroute();
                    OnRouteChanged(EventArgs.Empty);
                    OnModified(EventArgs.Empty);
                }
                return true;
            }
            return false;
        }

        private bool BendPointDoubleClicked(AbsoluteMouseEventArgs e)
        {
            var point = GetBendPoint(e);

            if (point != null)
            {
                if (!point.AutoPosition)
                {
                    if (point == FirstBendPoint && !bendPoints.SecondValue.RelativeToStartShape ||
                        point == LastBendPoint && bendPoints.SecondLastValue.RelativeToStartShape)
                    {
                        point.AutoPosition = true;
                    }
                    else
                    {
                        bendPoints.Remove(point);
                    }
                    Reroute();
                    OnRouteChanged(EventArgs.Empty);
                    OnModified(EventArgs.Empty);
                }
                e.Handled = true;
                return true;
            }
            return false;
        }

        private bool Picked(PointF mouseLocation, float zoom)
        {
            var tolerance = PickTolerance/zoom;

            for (var i = 0; i < RouteCache.Count - 1; i++)
            {
                var x = mouseLocation.X;
                var y = mouseLocation.Y;
                float x1 = RouteCache[i].X;
                float y1 = RouteCache[i].Y;
                float x2 = RouteCache[i + 1].X;
                float y2 = RouteCache[i + 1].Y;

                if (x1 == x2)
                {
                    if ((x >= x1 - tolerance) && (x <= x1 + tolerance) &&
                        (y >= y1 && y <= y2 || y >= y2 && y <= y1))
                    {
                        return true;
                    }
                }
                else // y1 == y2
                {
                    if ((y >= y1 - tolerance) && (y <= y1 + tolerance) &&
                        (x >= x1 && x <= x2 || x >= x2 && x <= x1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Picked(RectangleF rectangle)
        {
            for (var i = 0; i < RouteCache.Count - 1; i++)
            {
                if (rectangle.Contains(RouteCache[i]) || rectangle.Contains(RouteCache[i + 1]))
                    return true;

                float x1 = RouteCache[i].X;
                float y1 = RouteCache[i].Y;
                float x2 = RouteCache[i + 1].X;
                float y2 = RouteCache[i + 1].Y;

                if (x1 == x2)
                {
                    if (x1 >= rectangle.Left && x1 <= rectangle.Right && (
                        y1 < rectangle.Top && y2 > rectangle.Bottom ||
                        y2 < rectangle.Top && y1 > rectangle.Bottom))
                    {
                        return true;
                    }
                }
                else // y1 == y2
                {
                    if (y1 >= rectangle.Top && y1 <= rectangle.Bottom && (
                        x1 < rectangle.Left && x2 > rectangle.Right ||
                        x2 < rectangle.Left && x1 > rectangle.Right))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal override void MousePressed(AbsoluteMouseEventArgs e)
        {
            if (!e.Handled)
            {
                var pressed = Picked(e.Location, e.Zoom);

                if (e.Button == MouseButtons.Left)
                    pressed |= IsSelected && BendPointPressed(e);

                if (pressed)
                {
                    e.Handled = true;
                    OnMouseDown(e);
                }
            }
        }

        internal override void MouseMoved(AbsoluteMouseEventArgs e)
        {
            if (!e.Handled)
            {
                var moved = IsMousePressed;

                if (moved)
                {
                    e.Handled = true;
                    OnMouseMove(e);
                }
            }
        }

        internal override void MouseUpped(AbsoluteMouseEventArgs e)
        {
            if (!e.Handled)
            {
                var upped = IsMousePressed;

                if (upped)
                {
                    e.Handled = true;
                    OnMouseUp(e);
                }
            }
        }

        internal override void DoubleClicked(AbsoluteMouseEventArgs e)
        {
            var doubleClicked = Picked(e.Location, e.Zoom);

            if (e.Button == MouseButtons.Left)
                doubleClicked |= IsSelected && BendPointDoubleClicked(e);

            if (doubleClicked)
            {
                OnDoubleClick(e);
                e.Handled = true;
            }
        }

        protected override void OnMouseDown(AbsoluteMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsActive = true;
            }
            base.OnMouseDown(e);
            copied = false;
        }

        protected override void OnMouseMove(AbsoluteMouseEventArgs e)
        {
            base.OnMouseMove(e);

            //TODO: szebb lenne külön eljárásba tenni
            if (e.Button == MouseButtons.Left && selectedBendPoint != null)
            {
                var newLocation = Point.Truncate(e.Location);

                if (selectedBendPoint.Location != newLocation)
                {
                    if (!copied && Control.ModifierKeys == Keys.Control)
                    {
                        var newPoint = (BendPoint) selectedBendPoint.Clone();
                        newPoint.Location = newLocation;
                        if (selectedBendPoint.RelativeToStartShape)
                            bendPoints.AddAfter(bendPoints.Find(selectedBendPoint), newPoint);
                        else
                            bendPoints.AddBefore(bendPoints.Find(selectedBendPoint), newPoint);
                        selectedBendPoint = newPoint;
                        copied = true;
                        OnBendPointMove(new BendPointEventArgs(selectedBendPoint));
                    }
                    else
                    {
                        selectedBendPoint.Location = newLocation;
                        OnBendPointMove(new BendPointEventArgs(selectedBendPoint));
                    }

                    Reroute();
                    OnRouteChanged(EventArgs.Empty);
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public void ValidatePosition(int padding)
        {
            var bendPoint = bendPoints.First;
            var index = 0;

            while (bendPoint != null && index < RouteCache.Count - 1)
            {
                var point = bendPoint.Value;
                while (point.Location != RouteCache[index])
                    index++;

                if (point.X < padding)
                {
                    if (point.RelativeToStartShape)
                        StartShape.X += padding - point.X;
                    else
                        EndShape.X += padding - point.X;
                    return;
                }
                if (point.Y < padding)
                {
                    if (point.RelativeToStartShape)
                        StartShape.Y += padding - point.Y;
                    else
                        EndShape.Y += padding - point.Y;
                    return;
                }

                bendPoint = bendPoint.Next;
            }
        }

        protected internal Connection Paste(Diagram diagram,
                                            Size offset,
                                            Shape first,
                                            Shape second)
        {
            if (CloneRelationship(diagram, first, second))
            {
                var connection = diagram.ConnectionList.FirstValue;
                connection.IsSelected = true;

                connection.startOrientation = startOrientation;
                connection.endOrientation = endOrientation;
                connection.bendPoints.Clear();
                foreach (var point in bendPoints)
                {
                    var relativeShape = point.RelativeToStartShape ? first : second;
                    var newPoint = new BendPoint(relativeShape,
                                                 point.RelativeToStartShape,
                                                 point.AutoPosition);
                    newPoint.Location = point.Location + offset;
                    connection.bendPoints.Add(newPoint);
                }
                connection.Reroute();

                return connection;
            }
            return null;
        }

        protected abstract bool CloneRelationship(Diagram diagram, Shape first, Shape second);

        protected internal override IEnumerable<ToolStripItem> GetContextMenuItems(Diagram diagram)
        {
            return ConnectionContextMenu.Default.GetMenuItems(diagram);
        }

        protected override void OnMouseUp(AbsoluteMouseEventArgs e)
        {
            base.OnMouseUp(e);
            selectedBendPoint = null;
        }

        protected virtual void OnRouteChanged(EventArgs e)
        {
            if (RouteChanged != null)
                RouteChanged(this, e);
        }

        protected virtual void OnBendPointMove(BendPointEventArgs e)
        {
            if (BendPointMove != null)
                BendPointMove(this, e);
        }

        protected virtual void OnSerializing(SerializeEventArgs e)
        {
            var document = e.Node.OwnerDocument;

            var startNode = document.CreateElement("StartOrientation");
            startNode.InnerText = startOrientation.ToString();
            e.Node.AppendChild(startNode);

            var endNode = document.CreateElement("EndOrientation");
            endNode.InnerText = endOrientation.ToString();
            e.Node.AppendChild(endNode);

            foreach (var point in bendPoints)
            {
                if (!point.AutoPosition)
                {
                    var node = document.CreateElement("BendPoint");
                    node.SetAttribute("relativeToStartShape", point.RelativeToStartShape.ToString());
                    point.Serialize(node);
                    e.Node.AppendChild(node);
                }
            }
        }

        protected virtual void OnDeserializing(SerializeEventArgs e)
        {
            // Old file format
            var oldStartNode = e.Node["StartNode"];
            var oldEndNode = e.Node["EndNode"];
            if (oldStartNode != null && oldEndNode != null)
            {
                bool isHorizontal;
                bool.TryParse(oldStartNode.GetAttribute("isHorizontal"), out isHorizontal);
                startOrientation = isHorizontal ? LineOrientation.Horizontal : LineOrientation.Vertical;
                bool.TryParse(oldEndNode.GetAttribute("isHorizontal"), out isHorizontal);
                endOrientation = isHorizontal ? LineOrientation.Horizontal : LineOrientation.Vertical;

                int startLocation, endLocation;
                int.TryParse(oldStartNode.GetAttribute("location"), out startLocation);
                int.TryParse(oldEndNode.GetAttribute("location"), out endLocation);

                Reroute();
                if (startOrientation == LineOrientation.Vertical)
                    FirstBendPoint.X = StartShape.Left + startLocation;
                else
                    FirstBendPoint.Y = StartShape.Top + startLocation;

                if (endOrientation == LineOrientation.Vertical)
                    LastBendPoint.X = EndShape.Left + endLocation;
                else
                    LastBendPoint.Y = EndShape.Top + endLocation;

                FirstBendPoint.AutoPosition = false;
                LastBendPoint.AutoPosition = false;
                Reroute();
            }
            else
            {
                // New file format
                var startNode = e.Node["StartOrientation"];
                if (startNode != null)
                {
                    if (startNode.InnerText == "Horizontal")
                        startOrientation = LineOrientation.Horizontal;
                    else
                        startOrientation = LineOrientation.Vertical;
                }
                var endNode = e.Node["EndOrientation"];
                if (endNode != null)
                {
                    if (endNode.InnerText == "Horizontal")
                        endOrientation = LineOrientation.Horizontal;
                    else
                        endOrientation = LineOrientation.Vertical;
                }

                if (startNode != null && endNode != null) // To be sure it's the new file format
                {
                    bendPoints.Clear();

                    var nodes = e.Node.SelectNodes("child::BendPoint");
                    foreach (XmlElement node in nodes)
                    {
                        bool relativeToStartShape;
                        bool.TryParse(node.GetAttribute("relativeToStartShape"), out relativeToStartShape);
                        var relativeShape = relativeToStartShape ? StartShape : EndShape;

                        var point = new BendPoint(relativeShape, relativeToStartShape, false);
                        point.Deserialize(node);
                        bendPoints.Add(point);
                    }
                    if (bendPoints.Count == 0 || !FirstBendPoint.RelativeToStartShape)
                        bendPoints.AddFirst(new BendPoint(StartShape, true));
                    if (LastBendPoint.RelativeToStartShape)
                        bendPoints.Add(new BendPoint(EndShape, false));
                }
                Reroute();
            }
        }

        public override string ToString()
        {
            return Relationship.ToString();
        }

        private enum LineOrientation
        {
            Horizontal,
            Vertical
        }
    }
}