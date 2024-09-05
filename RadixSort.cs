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

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;
            int max;

            // Find the maximum value in the array to get the number of digits
            if (numbers.Length > 0) { max = numbers.Max(); }
            else { return; }

            // Do counting sort for every digit
            // Instead of passing digit number, exp is passed
            // exp is 10^i where i is the current digit number
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSort(exp, updateCallback, bgWorker, e);
            }
        }

        // Method to do counting sort of numbers array according to the digit represented by exp
        private void CountingSort(int exp, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            int n = numbers.Length;
            int[] output = new int[n]; // Output array
            int[] count = new int[10]; // There are 10 possible digits (0-9)

            // Store count of occurrences in count[]
            for (int i = 0; i < n; i++)
            {
                int digit = (numbers[i] / exp) % 10;
                count[digit]++;
            }

            // Calculate the cumulative count of each digit
            // This changes the count array so that count[i] now contains the actual position of this digit in the output array
            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            // Check if a cancellation request is pending
            if (CheckCancellation(bgWorker, e)) return;

            // Build the output array
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (numbers[i] / exp) % 10;
                output[count[digit] - 1] = numbers[i];
                count[digit]--;
            }

            // Check if a cancellation request is pending
            if (CheckCancellation(bgWorker, e)) return;

            // Copy the output array to numbers array, so that numbers array contains sorted numbers according to current digit
            for (int i = 0; i < n; i++)
            {
                numbers[i] = output[i];

                updateCallback(i, i); // Update the visualizer
                Thread.Sleep(10); // Slow down the sorting process
            }

            // Check if a cancellation request is pending
            if (CheckCancellation(bgWorker, e)) return;
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
