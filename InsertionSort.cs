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
        private bool sorted = false;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Insertion sort algorithm
            for (int i = 1; i < numbers.Length; i++)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }

                int key = numbers[i];
                int j = i - 1;

                // Move elements of numbers[0..i-1], that are greater than key, to one position ahead of their current position
                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;

                    // Call the update callback with the current positions
                    updateCallback(j + 1, j + 2);
                }
                Thread.Sleep(15); // Slow down the sorting process
                numbers[j + 1] = key;

                // Call the update callback after placing the key in its correct position
                updateCallback(j + 1, i);
            }

            // After the last pass, the array is sorted
            sorted = true;
        }
    }
}
