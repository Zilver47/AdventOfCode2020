using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Extensions
{
    public static class ListExtensions
    {

        public static IList<T> Clone<T>(this IList<T> source) where T: ICloneable
        {
            return source.Select(item => (T)item.Clone()).ToList();
        }
    }
}
