using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    // Class for the Bubble Sort algorithm
    internal class BubbleSort : ISortEngine
    {
        // Initialization
        private int[] numbers;
        private bool sorted = false;

        // Main sort method
        public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)
        {
            numbers = numbers_in;

            // Bubble sort algorithm
            while(!sorted)
            {
                sorted = true;
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    // Check if a cancellation request is pending
                    if (bgWorker.CancellationPending)
                    {
                        e.Cancel = true; // Set the Cancel flag to true
                        return; // Exit the sort method
                    }

                    if (numbers[i] > numbers[i + 1])
                    {
                        Swap(i, i + 1);
                        sorted = false;
                        updateCallback(i, i + 1);
                    }
                }
                Thread.Sleep(10); // Slow down the sorting process
            }
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
