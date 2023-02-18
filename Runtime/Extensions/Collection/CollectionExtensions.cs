using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.Collection {
    public static class CollectionExtensions {
        static readonly Random Random = new((int)DateTime.Now.Ticks);

        /// <summary>  
        /// Returns a random element from a list
        /// </summary>
        ///  <param name="list"></param>
        ///  <typeparam name="T"></typeparam>
        ///  <returns></returns>    
        public static T GetRandomItem<T>(this IList<T> list) {
            return list[Random.Next(list.Count)];
        }

        /// <summary>  
        /// Returns a random element from an array
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>    
        public static T GetRandomItem<T>(this T[] array) {
            return array[Random.Next(array.Length)];
        }

        /// <summary>  
        /// Gets a random pair of key value from a dict
        /// </summary>
        /// <param name="dict"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>    
        public static KeyValuePair<T, T> GetRandomPair<T>(this Dictionary<T, T> dict) {
            var pairIndex = Random.Next(dict.Count);
            return dict.ElementAt(pairIndex);
        }

        /// <summary>
        /// Uses the Fisher-Yates shuffle to rearrange an array of items
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this T[] array) {
            var n = array.Length;
            while (n > 1) {
                var k = Random.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
    }
}