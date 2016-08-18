using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ListExtension
    {
        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            var r = new Random();
            int index = r.Next(list.Count());
            return list.ToList()[index];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var r = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
