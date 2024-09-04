using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Interface for sorting algorithms
    internal interface ISortEngine
    {
        void Sort(int[] numbers, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e); // numbers: reference to the array of numbers to sort, updateCallback: callback to update the UI, bgWorker: background worker, e: bgWorker_DoWork event arguments
    }
}
