using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Radix Sort algorithm
    internal class RadixSort : ISortEngine
    {
        // Initialization
        private int[] numbers;
        private bool sorted = false;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Get the maximum number to know the number of digits
            int max = numbers.Max();

            // Do counting sort for every digit. Instead of passing digit number,
            // exp is passed. exp is 10^i where i is the current digit number
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(exp, updateCallback, bgWorker, e);
            }

            // After the radix sort process, the array is sorted
            sorted = true;
        }

        // A method to do counting sort of numbers[] according to the digit represented by exp
        private void CountingSort(int exp, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            int n = numbers.Length;
            int[] output = new int[n]; // Output array
            int[] count = new int[10]; // There are 10 possible digits (0-9)

            /*// Check if a cancellation request is pending
            if (bgWorker.CancellationPending)
            {
                e.Cancel = true; // Set the Cancel flag to true
                return; // Exit the sort method
            }*/

            // Store count of occurrences in count[]
            for (int i = 0; i < n; i++)
            {
                int digit = (numbers[i] / exp) % 10;
                count[digit]++;
                updateCallback(i, i); // Update the visualizer
            }

            // Change count[i] so that it now contains actual position of this digit in output[]
            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            // Build the output array
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (numbers[i] / exp) % 10;
                output[count[digit] - 1] = numbers[i];
                count[digit]--;

                // Update the visualizer for each placement
                updateCallback(i, count[digit]);
                //Thread.Sleep(10); // Slow down the sorting process
            }

            // Copy the output array to numbers[], so that numbers[] contains sorted numbers according to current digit
            for (int i = 0; i < n; i++)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }
                
                numbers[i] = output[i];

                // Update the visualizer after copying each element
                updateCallback(i, i);
                Thread.Sleep(10); // Slow down the sorting process
            }
        }
    }
}
