using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRandom
{
    public static class NameGenerator
    {
        private static readonly string[] FirstNames = { "Cool", "Bad", "Best", "My" , "Dad", "Candy", "Black", "Gachi", "Jazz", "White", "Millionare", "Borthel", "Dark", "Darkest", "Super", "Serius", "Mans", "Water", "Blue", "Hard", "Hardbass" };

        private static readonly string[] LastNames = { "account", "deposit", "money", "cash", "dough", "readies", "choppers", "barf" };

        public static string GenerateFirstName()
        {
            return GetRandomArrayEntry(FirstNames);
        }

        public static string GenerateLastName()
        {
            return GetRandomArrayEntry(LastNames);
        }

        private static T GetRandomArrayEntry<T>(T[] array)
        {
            return array[new Random(Guid.NewGuid().GetHashCode()).Next(0, array.Length)];
        }
    }

}
