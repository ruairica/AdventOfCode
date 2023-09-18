namespace Aoc.Utils
{
    public static class Helpers
    {
        public static double Median<T>(this IEnumerable<T> items) where T : struct, IComparable<T>
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var sortedList = items.OrderBy(x => x).ToList();
            int count = sortedList.Count;
            int middleIndex = count / 2;

            if (count % 2 == 0)
            {
                dynamic value1 = sortedList[middleIndex - 1];
                dynamic value2 = sortedList[middleIndex];
                return (value1 + value2) / 2.0;
            }

            return Convert.ToDouble(sortedList[middleIndex]);
        }

        public static IEnumerable<(T, int)> Enumerate<T>(this IEnumerable<T> list)
        {
            return list.Select((x, i) => (x, i));
        }

        public static int ConvertToDecimal(List<int> binary)
        {
            return binary.Select((t, m) => t * (int)Math.Pow(2, binary.Count - 1 - m)).Sum();
        }

        public static void Deconstruct<T>(this IList<T> list, out T first)
        {
            first = list.Count > 0 ? list[0] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            third = list.Count > 2 ? list[2] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            third = list.Count > 2 ? list[2] : default;
            fourth = list.Count > 3 ? list[3] : default;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third, out T fourth, out T fifth)
        {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            third = list.Count > 2 ? list[2] : default;
            fourth = list.Count > 3 ? list[3] : default;
            fifth = list.Count > 4 ? list[4] : default;
        }
    }
}
