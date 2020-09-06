using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public override bool Equals(object obj)
        {
            if (obj is Line line)
            {
                return Point1 == line.Point1 && Point2 == line.Point2;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Point1, Point2);
        }

        public static bool operator ==(Line left, Line right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Line left, Line right)
        {
            return !(left == right);
        }
    }
}