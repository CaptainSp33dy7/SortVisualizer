using System;
using System.ComponentModel;
using System.Threading;

namespace SortVisualizer
{
    // Class for the Quick Sort algorithm
    internal class QuickSort : ISortEngine
    {
        // Initialization
        private int[] numbers;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;
            QuickSortRecursive(0, numbers.Length - 1, updateCallback, bgWorker, e);
        }

        // Recursive Quick Sort algorithm
        private void QuickSortRecursive(int left, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            if (left < right)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }

                int pivotIndex = Partition(left, right, updateCallback);
                QuickSortRecursive(left, pivotIndex - 1, updateCallback, bgWorker, e); // Sort the left side of the pivot
                QuickSortRecursive(pivotIndex + 1, right, updateCallback, bgWorker, e); // Sort the right side of the pivot
            }
        }

        // Helper method to partition the array
        private int Partition(int left, int right, Action<int, int> updateCallback)
        {
            int pivot = numbers[right]; // Choose the rightmost element as pivot
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;
                    Swap(i, j);
                    updateCallback(i, j);
                    Thread.Sleep(10); // Slow down the sorting process
                }
            }

            Swap(i + 1, right);
            updateCallback(i + 1, right);

            return i + 1; // Return the pivot index
        }

        // Helper method to swap two elements in the array
        private void Swap(int i, int j)
        {
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }
    }
}
