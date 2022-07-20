using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace TegridyUtils.Extensions
{
    public static class EnumerableExtension
    {
        public static T GetRandomElement<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new Exception("source is empty");

            var length = source.Count();
            
            if (length == 0) 
                throw new Exception("source is empty");
            
            var randomIndex = Random.Range(0, length);

            return source.ElementAt(randomIndex);
        }

        public static List<T> GetRandomRange<T>(this IEnumerable<T> source, int range = -1)
        {
            if (source is null)
                throw new Exception("source is empty");
            
            var length = source.Count();

            if (range > length)
                throw new Exception("range must me less than length");

            if (range == -1)
                range = length;
            
            var copiedSource = new List<T>(source);
            var result = new List<T>(range);

            for (int i = 0; i < range; i++)
            {
                var j = Random.Range(0, length - i) + i;
                
                (copiedSource[i], copiedSource[j]) = (copiedSource[j], copiedSource[i]);
                result.Add(copiedSource[i]);
            }

            return result;
        }
    }
}