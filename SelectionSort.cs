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

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Selection sort algorithm
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                // Check if a cancellation request is pending
                if (CheckCancellation(bgWorker, e)) return;

                // Find the minimum element in the unsorted part of the array
                int minIndex = i;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Swap the found minimum element to be the last element of the sorted part
                if (minIndex != i)
                {
                    Swap(i, minIndex);
                    updateCallback(i, minIndex); // Update the visualizer
                }

                Thread.Sleep(15); // Slow down the sorting process
            }
        }

        // Helper method to swap two elements in the array and draw the changes
        private void Swap(int i, int j)
        {
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
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