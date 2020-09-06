using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Alword.ColorCluster.Extentions
{
    public static class ListExtentions
    {
        public static int Sign(this List<int> collection)
        {
            if (collection == null || collection.Count == 0) return 0;
            else if (collection.Count == 1) return collection[0];
            else
                for (int i = 1; i < collection.Count; i++)
                {
                    if (collection[i] * collection[0] < 0) return 0;
                }
            return collection[0];
        }
    }
}
