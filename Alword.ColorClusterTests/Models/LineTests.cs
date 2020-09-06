using Alword.ColorCluster.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xunit;

namespace Alword.ColorClusterTests.Models
{
    public class LineTests
    {
        [Fact]
        public void ABC_Ok()
        {
            var line = new Line(new Point(-2, 0), new Point(0, -2));
            Assert.Equal(2, line.A);
            Assert.Equal(2, line.B);
            Assert.Equal(4, line.C);
        }
    }
}
