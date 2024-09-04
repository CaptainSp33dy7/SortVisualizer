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
        private static readonly Random random = new Random(); // Random object for choosing a random pivot

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;
            QuickSortRecursive(0, numbers.Length - 1, updateCallback, bgWorker, e);
        }

        // Recursive Quick Sort algorithm
        private void QuickSortRecursive(int left, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            // Check if the array segment is already sorted
            if (IsSorted(left, right)) return;

            if (left < right) // Check if the array segment has more than one element
            {
                // Check if a cancellation request is pending
                if (CheckCancellation(bgWorker, e)) return;

                int pivotIndex = Partition(left, right, updateCallback); // Partition the array segment
                QuickSortRecursive(left, pivotIndex - 1, updateCallback, bgWorker, e); // Sort the left side of the pivot
                QuickSortRecursive(pivotIndex + 1, right, updateCallback, bgWorker, e); // Sort the right side of the pivot
            }
        }

        // Helper method to partition the array segment
        private int Partition(int left, int right, Action<int, int> updateCallback)
        {
            // Generate a random index between left and right
            int randomPivotIndex = random.Next(left, right + 1);

            // Swap the randomly chosen pivot element with the rightmost element
            Swap(randomPivotIndex, right);
            updateCallback(randomPivotIndex, right); // Update the visualizer

            // Use the element at the rightmost position as the pivot value
            int pivot = numbers[right];
            int i = left - 1;

            // Rearrange the array segment so that elements less than the pivot are on the left side of the pivot
            for (int j = left; j < right; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;
                    Swap(i, j);
                    updateCallback(i, j); // Update the visualizer
                    Thread.Sleep(10); // Slow down the sorting process
                }
            }

            Swap(i + 1, right); // Place the pivot element in its correct position
            updateCallback(i + 1, right); // Update the visualizer

            return i + 1; // Return the pivot index
        }

        // Helper method to swap two elements in the array
        private void Swap(int i, int j)
        {
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        // Helper method to check if the array is already sorted
        private bool IsSorted(int left, int right)
        {
            for (int i = left; i < right; i++)
            {
                // If the current element is greater than the next element, the array is not sorted
                if (numbers[i] > numbers[i + 1])
                    return false;
            }
            return true;
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
