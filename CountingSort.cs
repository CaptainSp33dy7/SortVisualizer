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
        private bool sorted = false;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Find the maximum value in the array
            int maxValue = numbers.Max();

            // Create a count array to store the count of each unique element
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
                    // Check if a cancellation request is pending
                    if (bgWorker.CancellationPending)
                    {
                        e.Cancel = true; // Set the Cancel flag to true
                        return; // Exit the sort method
                    }

                    numbers[index] = i;
                    count[i]--;
                    updateCallback(index, i); // Update callback for visualization
                    index++;

                    Thread.Sleep(10); // Slow down the sorting process
                }
            }

            // After processing, the array is sorted
            sorted = true;
        }
    }
}
