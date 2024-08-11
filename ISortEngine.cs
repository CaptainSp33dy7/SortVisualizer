using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    internal interface ISortEngine
    {
        void Sort(int[] numbers, Graphics graphics, int maxValue);
    }
}
