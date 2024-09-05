using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SortVisualizer
{
    [TestClass]
    public class SortEngineTests
    {
        // Test data folder path
        private const string TestDataFolder = @"./test_data"; // @ is used to avoid escaping the backslashes

        // List of sorting algorithms to test
        public static IEnumerable<object[]> SortingAlgorithms =>
            new List<object[]>
            {
                new object[] { new BubbleSort() },
                new object[] { new CountingSort() },
                new object[] { new HeapSort() },
                new object[] { new InsertionSort() },
                new object[] { new MergeSort() },
                new object[] { new QuickSort() },
                new object[] { new RadixSort() },
                new object[] { new SelectionSort() }
            };

        // Test methods
        // [DynamicData(nameof(SortingAlgorithms))] attribute is used to run the test for each sorting algorithm
        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test01(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test01.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test02(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test02.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test03(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test03.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test04_Double(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test04_double.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test05_Double(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test05_double.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test06_Triple(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test06_triple.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test07_Quadruple(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test07_quadruple.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test08_Quintuple(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test08_quintuple.csv");
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_Test09_Sextuple(ISortEngine sortEngine)
        {
            RunSortEngineTest(sortEngine, "test09_sextuple.csv");
        }

        // Hard-coded test cases
        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_TestOneElement(ISortEngine sortEngine)
        {
            int[] numbers = new int[] { 1 }; // Create an array with one element
            sortEngine.Sort(numbers, (i, j) => { }, new System.ComponentModel.BackgroundWorker(), new System.ComponentModel.DoWorkEventArgs(null)); // Sort the array
            Assert.AreEqual(1, numbers[0]); // Check if the array is sorted
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_TestEmptyArray(ISortEngine sortEngine)
        {
            int[] numbers = new int[] { }; // Create an empty array
            sortEngine.Sort(numbers, (i, j) => { }, new System.ComponentModel.BackgroundWorker(), new System.ComponentModel.DoWorkEventArgs(null)); // Sort the array
            Assert.AreEqual(0, numbers.Length); // Check if the array is empty
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_TestInverseArray(ISortEngine sortEngine)
        {
            int[] numbers = new int[1000]; // Create an array with 1000 elements

            // Fill the array with numbers in descending order
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = numbers.Length - i;
            }

            sortEngine.Sort(numbers, (i, j) => { }, new System.ComponentModel.BackgroundWorker(), new System.ComponentModel.DoWorkEventArgs(null)); // Sort the array

            // Check if the array is sorted
            IsSorted(numbers);
        }

        [TestMethod]
        [DynamicData(nameof(SortingAlgorithms))]
        public void SortEngine_TestArrayWithSameNumber(ISortEngine sortEngine)
        {
            int[] numbers = new int[1000]; // Create an array with 1000 elements

            // Fill the array with the same number
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = 42;
            }

            sortEngine.Sort(numbers, (i, j) => { }, new System.ComponentModel.BackgroundWorker(), new System.ComponentModel.DoWorkEventArgs(null)); // Sort the array

            // Check if the array is sorted
            IsSorted(numbers);
        }

        // Helper method to run the Bubble Sort test for a given CSV file
        private void RunSortEngineTest(ISortEngine sortEngine, string filename)
        {
            // Read the test data from the CSV file
            string filePath = Path.Combine(TestDataFolder, filename);
            string[] lines = File.ReadAllLines(filePath);
            int[] test_numbers = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                test_numbers[i] = int.Parse(lines[i]);
            }

            // Sort the test data
            sortEngine.Sort(test_numbers, (i, j) => { }, new System.ComponentModel.BackgroundWorker(), new System.ComponentModel.DoWorkEventArgs(null));

            // Check if the test data is sorted
            IsSorted(test_numbers);
        }

        // Helper method to check if the array is correctly sorted
        private void IsSorted(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                Assert.IsTrue(numbers[i] <= numbers[i + 1]);
            }
        }
    }
}
