using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Merge Sort algorithm
    internal class MergeSort : ISortEngine
    {
        // Initialization
        private int[] numbers;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;
            MergeSortRecursive(0, numbers.Length - 1, updateCallback, bgWorker, e);
        }

        // Recursive merge sort method
        private void MergeSortRecursive(int left, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            // Check if a cancellation request is pending
            if (CheckCancellation(bgWorker, e)) return;

            if (left < right)
            {
                if (IsSorted(left, right)) return; // Return early if already sorted

                int middle = (left + right) / 2;

                // Sort the first and second halves
                MergeSortRecursive(left, middle, updateCallback, bgWorker, e);
                MergeSortRecursive(middle + 1, right, updateCallback, bgWorker, e);

                // Merge the sorted halves
                Merge(left, middle, right, updateCallback, bgWorker, e);
            }
        }

        // Helper method to merge two sorted halves
        private void Merge(int left, int middle, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            // Check if a cancellation request is pending
            if (CheckCancellation(bgWorker, e)) return;

            int leftSize = middle - left + 1;
            int rightSize = right - middle;

            // Create temporary arrays
            int[] leftArray = new int[leftSize];
            int[] rightArray = new int[rightSize];

            // Copy data to temporary arrays
            Array.Copy(numbers, left, leftArray, 0, leftSize);
            Array.Copy(numbers, middle + 1, rightArray, 0, rightSize);

            // Initial indexes of the temporary arrays
            int i = 0;
            int j = 0;
            int k = left;

            // Merge the temp arrays back into the original array
            while (i < leftSize && j < rightSize)
            {
                // Check if a cancellation request is pending
                // if (CheckCancellation(bgWorker, e)) return;
                // possible to uncomment to cancel the sorting process without delay but there is data loss in the array!!!

                if (leftArray[i] <= rightArray[j])
                {
                    numbers[k] = leftArray[i];
                    i++;
                    Thread.Sleep(1); // Slow down the sorting process
                }
                else
                {
                    numbers[k] = rightArray[j];
                    j++;
                }

                updateCallback(k, k); // Update the visualizer
                k++;
            }
            

            // Copy any remaining elements of leftArray, if any
            while (i < leftSize)
            {
                // Check if a cancellation request is pending
                // if (CheckCancellation(bgWorker, e)) return;
                // possible to uncomment to cancel the sorting process without delay but there is data loss in the array!!!

                numbers[k] = leftArray[i];
                i++;
                k++;

                updateCallback(k - 1, k - 1); // Update the visualizer
                Thread.Sleep(10); // Slow down the sorting process
            }

            // Copy any remaining elements of rightArray, if any
            while (j < rightSize)
            {
                // Check if a cancellation request is pending
                // if (CheckCancellation(bgWorker, e)) return;
                // possible to uncomment to cancel the sorting process without delay but there is data loss in the array!!!

                numbers[k] = rightArray[j];
                j++;
                k++;

                updateCallback(k - 1, k - 1); // Update the visualizer
                Thread.Sleep(10); // Slow down the sorting process
            }
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
