using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Selection Sort algorithm
    internal class SelectionSort : ISortEngine
    {
        // Initialization
        private int[] numbers;
        private bool sorted = false;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Selection sort algorithm
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }

                int minIndex = i;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    Swap(i, minIndex);
                    updateCallback(i, minIndex);
                }

                Thread.Sleep(15); // Slow down the sorting process
            }

            // After the last pass, the array is sorted
            sorted = true;
        }

        // Helper method to swap two elements in the array and draw the changes
        private void Swap(int i, int j)
        {
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }
    }
}