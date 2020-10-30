using System.Collections.Generic;

namespace ClassLibraryForYa.Services
{
    internal class Range
    {
        public Range( int minValue, int maxValue )
        {
            MinValue = minValue;
            MaxValue = maxValue;
            UsedNumbers = new List<int>();
        }

        public int MinValue
        {
            get;
        }

        public int MaxValue
        {
            get;
        }

        public List<int> UsedNumbers
        {
            get;
        }
    }
}