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

        public Line Separate()
        {
            var currentPoint = primaryPoints;
            for (int i = 0; i < currentPoint.Count; i++)
            {
                for (int j = i; j < currentPoint.Count; j++)
                {
                    var point1 = currentPoint[i];
                    var point2 = currentPoint[j];
                    if (point1 == point2) continue;

                    var primaryOffsets = Offset(point1, point2, primaryPoints);
                    var secondaryOffsets = Offset(point1, point2, secondaryPoints);

                    var primarySign = primaryOffsets.Sign();
                    var secondarySign = secondaryOffsets.Sign();

                    var isSplitLine = (primarySign * secondarySign != 0) && (secondarySign != primarySign);
                    if (isSplitLine)
                        return new Line(point1, point2);
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
    }
}
