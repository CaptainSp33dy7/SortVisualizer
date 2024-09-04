using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Counting Sort algorithm
    internal class CountingSort : ISortEngine
    {
        // Initialization
        private int[] numbers;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Find the maximum value in the array
            int maxValue = numbers.Max();

            // Create a count array to store the count of each unique number
            int[] count = new int[maxValue + 1];

            // Initialize the count array
            for (int i = 0; i < numbers.Length; i++)
            {
                count[numbers[i]]++;
            }

            // Modify the original array based on the count array
            int index = 0;
            for (int i = 0; i <= maxValue; i++)
            {
                while (count[i] > 0)
                {
                    numbers[index] = i;
                    count[i]--;
                    updateCallback(index, index); // Update the visualizer
                    index++;
                    Thread.Sleep(10); // Slow down the sorting process
                }

                // Check if a cancellation request is pending
                // if (CheckCancellation(bgWorker, e)) return;
                // possible to uncomment to cancel the sorting process but there is data loss in the next sorting with this same algorithm
                // due to the count array getting redundant values from the previous sorting (if you decide to uncomment this, you should
                // make the StopButton visible in the main form)
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
