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
        private bool sorted = false;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;
            MergeSortRecursive(0, numbers.Length - 1, updateCallback, bgWorker, e);

            // After the merge process, the array is sorted
            sorted = true;
        }

        // Recursive merge sort method
        private void MergeSortRecursive(int left, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            // Check if a cancellation request is pending
            if (bgWorker.CancellationPending)
            {
                e.Cancel = true; // Set the Cancel flag to true
                return; // Exit the sort method
            }

            if (left < right)
            {
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
            int leftSize = middle - left + 1;
            int rightSize = right - middle;

            int[] leftArray = new int[leftSize];
            int[] rightArray = new int[rightSize];

            // Copy data to temporary arrays
            Array.Copy(numbers, left, leftArray, 0, leftSize);
            Array.Copy(numbers, middle + 1, rightArray, 0, rightSize);

            int i = 0, j = 0, k = left;

            // Merge the temp arrays back into the original array
            while (i < leftSize && j < rightSize)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }

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

                // Call the update callback to reflect changes
                updateCallback(k, k);
                k++;
            }
            

            // Copy any remaining elements of leftArray, if any
            while (i < leftSize)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }

                numbers[k] = leftArray[i];
                i++;
                k++;

                // Call the update callback to reflect changes
                updateCallback(k - 1, k - 1);
                Thread.Sleep(10); // Slow down the sorting process
            }

            // Copy any remaining elements of rightArray, if any
            while (j < rightSize)
            {
                // Check if a cancellation request is pending
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true; // Set the Cancel flag to true
                    return; // Exit the sort method
                }

                numbers[k] = rightArray[j];
                j++;
                k++;

                // Call the update callback to reflect changes
                updateCallback(k - 1, k - 1);
                Thread.Sleep(10); // Slow down the sorting process
            }
        }
    }
}
