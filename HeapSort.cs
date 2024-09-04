using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Heap Sort algorithm
    internal class HeapSort : ISortEngine
    {
        // Initialization
        private int[] numbers;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;
            int n = numbers.Length;
            bool slowed = true; // Flag to slow down the building of the heap

            // Build the heap in the array
            for (int i = n / 2 - 1; i >= 0; i--) // Start from the last non-leaf node towards the root
            {
                Heapify(n, i, updateCallback, bgWorker, e, slowed);
            }

            // Extract elements one by one from the heap
            for (int i = n - 1; i > 0; i--)
            {
                // Check if a cancellation request is pending
                if (CheckCancellation(bgWorker, e)) return;

                // Move current root to end
                Swap(0, i);
                updateCallback(0, i); // Update the visualizer
                Thread.Sleep(10); // Slow down the sorting process

                slowed = false; // No need to slow down the extraction process
                // Call max heapify on the reduced heap
                Heapify(i, 0, updateCallback, bgWorker, e, slowed);
            }
        }

        // Helper method to maintain the heap property
        private void Heapify(int n, int i, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e, bool slowed)
        {
            int largest = i; // Initialize largest as root
            int left = 2 * i + 1; // left child = 2*i + 1
            int right = 2 * i + 2; // right child = 2*i + 2

            // Check if left child is larger than root
            if (left < n && numbers[left] > numbers[largest])
            {
                largest = left;
            }

            // Check if right child is larger than largest so far
            if (right < n && numbers[right] > numbers[largest])
            {
                largest = right;
            }

            // If largest is not root
            if (largest != i)
            {
                Swap(i, largest);
                updateCallback(i, largest); // Update the visualizer
                if (slowed) { Thread.Sleep(10); } // Slow down the sorting process

                // Recursively heapify the affected sub-tree
                Heapify(n, largest, updateCallback, bgWorker, e, slowed);
            }
        }

        // Helper method to swap two elements in the array
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
