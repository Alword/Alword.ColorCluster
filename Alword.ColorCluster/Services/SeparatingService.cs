using Alword.ColorCluster.Extentions;
using Alword.ColorCluster.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Alword.ColorCluster.Services
{
    public class SeparatingService
    {
        private readonly List<Point> primaryPoints;
        private readonly List<Point> secondaryPoints;

        public SeparatingService()
        {
            primaryPoints = new List<Point>();
            secondaryPoints = new List<Point>();
        }

        public void AddPrimary(Point point) => primaryPoints.Add(point);
        public void AddSecondary(Point point) => secondaryPoints.Add(point);

        public Line Separate(int start = 0, int end = 600)
        {
            (Line line, Point point) = FindLineWithPoint();

            Console.WriteLine($"line {line.A} {line.B} {line.C}");

            if (point != Point.Empty)
            {
                var pointLine = line.ConstructLine(point);

                Console.WriteLine($"line {pointLine.A} {pointLine.B} {pointLine.C}");

                double k = line.A / (double)pointLine.A;
                var a = pointLine.A * k;
                var b = pointLine.B * k;
                var c = pointLine.C * k;
                var halfC = (c + line.C) / 2;
                var point1 = Line.Bound(start, a, b, halfC);
                var point2 = Line.Bound(end, a, b, halfC);
                return new Line(point1, point2);
            }

            return line;
        }

        private Line SeparateColor(List<Point> currentGroup)
        {
            for (int i = 0; i < currentGroup.Count; i++)
            {
                for (int j = i; j < currentGroup.Count; j++)
                {
                    var point1 = currentGroup[i];
                    var point2 = currentGroup[j];
                    if (point1 == point2) continue;

                    var primaryOffsets = Offset(point1, point2, primaryPoints);
                    var secondaryOffsets = Offset(point1, point2, secondaryPoints);

                    var primarySign = primaryOffsets.Sign();
                    var secondarySign = secondaryOffsets.Sign();

                    var twoPrimaryDots = (primaryOffsets.Count == 0) && (secondarySign != 0);
                    var twoSecondaryDots = (secondaryOffsets.Count == 0) && (primarySign != 0);
                    var isSplitLine = (primarySign * secondarySign < 0);

                    if (isSplitLine || twoPrimaryDots || twoSecondaryDots) return new Line(point1, point2);
                }
            }
            return Line.Empty;
        }

        private List<int> Offset(Point point1, Point point2, List<Point> group)
        {
            List<int> offsets = new List<int>(group.Count);
            foreach (var point in group)
            {
                if (point == point1 || point == point2) continue;
                offsets.Add(O(point, point1, point2));
            }
            return offsets;

            static int O(Point point, Point point1, Point point2)
                => K(point.X, point1.X, point2.X) - K(point.Y, point1.Y, point2.Y);

            static int K(int x, int x1, int x2)
                => (x - x1) / (x2 - x1);
        }

        private (Line line, Point point) FindLineWithPoint()
        {
            var line = SeparateColor(primaryPoints);
            Point closestPoint = Point.Empty;
            if (line != Line.Empty)
            {
                closestPoint = line.ClosestPoint(secondaryPoints);
            }

            else
            {
                line = SeparateColor(secondaryPoints);
                if (line != Line.Empty)
                {
                    closestPoint = line.ClosestPoint(primaryPoints);
                }
            }
            return (line, closestPoint);
        }
    }
}
