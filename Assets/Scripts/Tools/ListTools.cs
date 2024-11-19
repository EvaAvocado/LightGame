using System;
using System.Collections.Generic;

namespace Tools
{
    public abstract class ListTools
    {
        public static void Shuffle<T>(List<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}