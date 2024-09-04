using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Insertion Sort algorithm
    internal class InsertionSort : ISortEngine
    {
        // Initialization
        private int[] numbers;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Insertion sort algorithm
            for (int i = 1; i < numbers.Length; i++)
            {
                // Check if a cancellation request is pending
                if (CheckCancellation(bgWorker, e)) return;

                int key = numbers[i];
                int j = i - 1;

                // Rearrange the array elements to place the key in its correct position
                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;

                    updateCallback(j + 1, j + 2); // Update the visualizer
                }
                numbers[j + 1] = key; 
                updateCallback(j + 1, i); // Update the visualizer

                Thread.Sleep(15); // Slow down the sorting process
            }
        }

        // Helper method to check if a cancellation request is pending
        private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            // Check if a cancellation request is pending
            if (bgWorker.CancellationPending)
            {
                e.Cancel = true; // Set the Cancel flag to true
                return true;
            }
            else { return false; }
        }
    }
}
