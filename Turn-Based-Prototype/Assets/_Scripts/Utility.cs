using System.Collections.Generic;

namespace SinuousProductions
{
    public static class Utility
    {
        public static void Shuffle<T>(List<T> list) //Generics, we actually learned about ts in data structures
        {
            System.Random random = new System.Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[j], list[i]) = (list[i], list[j]); //Different way of writing out list[i] = list[j], list[j] = list[i] without the temp i suppose
                                                         //This is known as a tuple swap, and is a modern, more elegant way of swapping variables in C# versin 7 or later
            }
        }
    }
}