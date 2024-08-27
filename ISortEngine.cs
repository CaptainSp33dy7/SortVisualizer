using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Interface for sorting algorithms
    internal interface ISortEngine
    {
        void Sort(int[] numbers, Action<int, int> updateCallback);
    }
}
