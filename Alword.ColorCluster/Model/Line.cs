using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Alword.ColorCluster.Model
{
    public struct Line
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public static Line Empty { get => new Line(); }
        public Line(Point point1, Point point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
        }

        public int A { get => Point1.Y - Point2.Y; }
        public int B { get => Point2.X - Point1.X; }
        public int C { get => Point1.X * Point2.Y - Point2.X * Point1.Y; }
        public double Length { get => Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2)); }
        public double Distance(Point point) => Math.Abs((A * point.X + B * point.Y + C)) / Length;

        public Point ClosestPoint(List<Point> currentPoints)
        {
            var minDistance = Distance(currentPoints[0]);
            var minPoint = currentPoints[0];
            for (int i = 1; i < currentPoints.Count; i++)
            {
                var distance = Distance(currentPoints[i]);
                if (distance < minDistance)
                {
                    minPoint = currentPoints[i];
                    minDistance = distance;
                }
            }
            return minPoint;
        }

        public Point Bound(int bound)
            => Bound(bound, A, B, C);
        public static Point Bound(int bound, double A, double B, double C)
        {
            if (A != 0)
            {
                int y = bound;
                int x = (int)Math.Round(-(B * bound + C) / A);
                return new Point(x, y);
            }
            else if (B != 0)
            {
                int x = bound;
                int y = (int)Math.Round(-(A * bound + C) / B);
                return new Point(x, y);
            }
            else
            {
                return Point.Empty;
            }
        }

        public Line ConstructLine(Point point)
        {
            int x;
            int y;
            if (B == 0)
            {
                y = 0;
                x = (int)Math.Round((A * point.X + B * point.Y) / (double)A);
            }
            else
            {
                x = 0;
                y = (int)Math.Round((A * point.X + B * point.Y) / (double)B);
            }
            return new Line(point, new Point(x, y));
        }

        public override bool Equals(object obj)
        {
            if (obj is Line line)
            {
                return (Point1 == line.Point1) && (Point2 == line.Point2);
            }
            else
                return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(Point1, Point2);

        public static bool operator ==(Line left, Line right)
            => left.Equals(right);


        public static bool operator !=(Line left, Line right)
            => !(left == right);

    }
}