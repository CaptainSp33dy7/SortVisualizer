# Sort Visualizer Documentation

# Introduction

The **Sort Visualizer** is an application designed to visually demonstrate the operation of different sorting algorithms. This tool allows users to observe the step-by-step process of sorting, providing an educational insight into how various algorithms work.

## Key Features
- Supports multiple sorting algorithms such as Bubble sort, Counting sort, Heap sort, Insertion sort, Merge sort, Quick sort, Radix sort and Selection sort.
- Real-time visualization of the sorting process.
- Ability to stop the sorting in the process and change the sorting algorithm (except Counting sort).
- Background worker integration to keep the UI responsive during sorting.
- Custom data import

### Disclaimer
To ensure that the sorting process can be visually observed, the sorting algorithms have been intentionally slowed down. As a result, algorithms that may generally be faster in a standard execution context could appear slower due to this deliberate delay.

---

# Classes

## `MainForm` Class

### Purpose

The `MainForm` class is the primary form in the SortVisualizer application. It provides a graphical user interface for visualizing sorting algorithms. The form contains various components, such as buttons, ComboBoxes, and panels, to allow the user to select a sorting algorithm, load or generate data, and visualize the sorting process step by step.

### Attributes

- `int[] numbers`: Array of integers to be sorted and visualized.
- `bool isSorting`: Flag indicating whether the sorting process is currently in progress.
- `SolidBrush whiteBrush`: Brush used to draw the sorting bars in white.
- `SolidBrush blackBrush`: Brush used to clear the sorting bars.
- `int arraySize`: Size of the array to be sorted.
- `const int maxValue`: Maximum possible value for any element in the `numbers` array, set to 1000 by default.
- `int barWidth`: Width of each bar in the visualization; set to 3 by default.

### Methods

#### Constructor

- `MainForm()`: Initializes the form, sets up UI components, loads available sorting algorithms and data files, and sets up event handlers.

#### Private Methods

- `LoadClassesIntoComboBox()`: Finds all classes that implement the `ISortEngine` interface and adds them to the sorting algorithm ComboBox (`AlgoPicker`).

- `LoadDataIntoComboBox()`: Finds all CSV files in the "test_data" directory and adds them to the data ComboBox (`DataPicker`) along with a "Random" option.

- `DrawArray(Graphics graphics)`: Draws the array of numbers as bars on the graphics panel.

- `GenerateRandomArray()`: Generates a new random array of integers based on the width of the graphics panel and the maximum value (`maxValue`).

- `LoadArrayFromCSV(string filePath)`: Loads an array of integers from a specified CSV file. If the array is too big the barWidth is adjusted to fit the array in the panel.

- `InvalidateArrayPosition(int index)`: Invalidates a specific bar in the array, triggering a redraw of that bar.

- `CheckSorted()`: Checks if the array is sorted and displays a message indicating whether it is sorted or not.

#### Event Handlers

- `AlgoPicker_SelectedIndexChanged(object sender, EventArgs e)`: Event handler that shows or hides the Stop button (`StopButton`) based on the selected sorting algorithm. (Counting sort does not support stopping.)

- `GraphicsPanel_Paint(object sender, PaintEventArgs e)`: Event handler for the `GraphicsPanel`'s `Paint` event. Redraws the array of numbers as bars if the array is not null.

- `MainForm_Resize(object sender, EventArgs e)`: Event handler for resizing the form. Adjusts the array or reloads the data based on the selected option.

- `exitToolStripMenuItem_Click(object sender, EventArgs e)`: Closes the application when the "Exit" menu item is clicked.

- `ResetButton_Click(object sender, EventArgs e)`: When the reset button is clicked, resets the array by either generating a new random array or loading data from a CSV file, depending on the selected option.

- `SortButton_Click(object sender, EventArgs e)`: When the sort button is clicked, initiates the sorting process by starting the background worker (`bgWorker`).

- `bgWorker_DoWork(object sender, DoWorkEventArgs e)`: The main sorting logic executed on a separate thread. It creates an instance of the selected sorting algorithm and calls its `Sort` method.

- `bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)`: Callback from the sorting thread to update the visualization by redrawing the bars that were swapped during sorting.

- `bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)`: Handles the completion of the sorting process. Displays appropriate messages based on the outcome (sorted, canceled, or error).

- `StopButton_Click(object sender, EventArgs e)`: When the stop button is clicked, cancels the ongoing sorting process.

- `rEADMEToolStripMenuItem_Click(object sender, EventArgs e)`: Opens the README file in the default application associated with `.md` files when the "README" menu item is clicked.

- `aboutToolStripMenuItem_Click(object sender, EventArgs e)`: Opens the GitHub profile of the application author in the default web browser when the "About" menu item is clicked.




## `ISortEngine` Interface

### Purpose

The `ISortEngine` interface defines a contract for sorting algorithms that can be used in the SortVisualizer application. It ensures that any sorting algorithm implementing this interface provides a method for sorting an array of integers and for updating the user interface during the sorting process.

### Methods

- `void Sort(int[] numbers, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: This method sorts the given array of integers using a specific sorting algorithm.

  - **Parameters:**
    - `int[] numbers`: Reference to the array of integers to be sorted.
    - `Action<int, int> updateCallback`: Callback action that takes two integers (indices) as parameters. This action is called whenever the user interface needs to be updated (e.g., when two elements are swapped).
    - `BackgroundWorker bgWorker`: The `BackgroundWorker` instance responsible for running the sorting process on a separate thread, allowing for asynchronous execution.
    - `DoWorkEventArgs e`: The event arguments associated with the `bgWorker_DoWork` event. This parameter is used to handle cancellation.



## `BubbleSort` Class

### Purpose

The `BubbleSort` class implements the `ISortEngine` interface and provides the Bubble Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

- `private bool sorted`: Flag indicating whether the array has been fully sorted.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Bubble Sort algorithm on the given array of integers. It swaps elements if they are in the wrong order and updates the visualization.

- `private void Swap(int i, int j)`: Swaps two elements in the array and updates the visualization by calling the `updateCallback` action.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `CountingSort` Class

### Purpose

The `CountingSort` class also implements the `ISortEngine` interface and provides the Counting Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

- `private int maxValue`: Maximum value in the array.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Counting Sort algorithm on the given array of integers. It counts the occurrences of each unique number, modifies the original array based on the count array, and updates the visualization by calling the `updateCallback` action.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `HeapSort` Class

### Purpose

The `HeapSort` class implements the `ISortEngine` interface and provides the Heap Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

- `private bool slowed`: Flag to slow down the building of the heap.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Heap Sort algorithm on the given array of integers. It builds the heap in the array and extracts elements one by one from the heap, updating the visualization during the process by calling the `updateCallback` action.

- `private void Heapify(int n, int i, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e, bool slowed)`: Helper method to maintain the heap property by recursively heapifying the affected sub-tree.

- `private void Swap(int i, int j)`: Helper method to swap two elements in the array.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `InsertionSort` Class

### Purpose

The `InsertionSort` class implements the `ISortEngine` interface and provides the Insertion Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Insertion Sort algorithm on the given array of integers. It rearranges the array elements to place the key in its correct position and updates the visualization during the process by calling the `updateCallback` action.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `MergeSort` Class

### Purpose

The `MergeSort` class implements the `ISortEngine` interface and provides the Merge Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Merge Sort algorithm on the given array of integers. It recursively divides the array into two halves, sorts them, and merges them back together, updating the visualization during the process by calling the `updateCallback` action.

- `private void MergeSortRecursive(int left, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Recursive method to perform the merge sort operation on the array.

- `private void Merge(int left, int middle, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to merge two sorted halves of the array.

- `private bool IsSorted(int left, int right)`: Helper method to check if the array is already sorted.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `QuickSort` Class

### Purpose

The `QuickSort` class implements the `ISortEngine` interface and provides the Quick Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

- `private static readonly Random random = new Random()`: Random object for choosing a random pivot.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Quick Sort algorithm on the given array of integers. It partitions the array based on a pivot element and recursively sorts the subarrays, updating the visualization during the process by calling the `updateCallback` action.

- `private void QuickSortRecursive(int left, int right, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Recursive method to perform the quick sort operation on the array.

- `private int Partition(int left, int right, Action<int, int> updateCallback)`: Helper method to partition the array segment based on a pivot element.

- `private void Swap(int i, int j)`: Helper method to swap two elements in the array.

- `private bool IsSorted(int left, int right)`: Helper method to check if the array is already sorted.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `RadixSort` Class

### Purpose

The `RadixSort` class implements the `ISortEngine` interface and provides the Radix Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Radix Sort algorithm on the given array of integers. It sorts the array based on individual digits, updating the visualization during the process by calling the `updateCallback` action.

- `private void CountingSort(int exp, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to perform counting sort based on the digit represented by `exp`. `Exp` is `10^i` where `i` is the current digit number.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.



## `SelectionSort` Class

### Purpose

The `SelectionSort` class implements the `ISortEngine` interface and provides the Selection Sort algorithm for sorting an array of integers. It is responsible for performing the sorting operation and updating the user interface during the sorting process.

### Attributes

- `private int[] numbers`: Array of integers to be sorted.

### Methods

- `public void Sort(int[] numbers_in, Action<int, int> updateCallback, BackgroundWorker bgWorker, DoWorkEventArgs e)`: Executes the Selection Sort algorithm on the given array of integers. It finds the minimum element in the unsorted part of the array and moves it to the end of the sorted part also updating the visualization during the process by calling the `updateCallback` action.

- `private void Swap(int i, int j)`: Helper method to swap two elements in the array and draw the changes.

- `private bool CheckCancellation(BackgroundWorker bgWorker, DoWorkEventArgs e)`: Helper method to check if a cancellation request is pending.

---

# Tests

The Sort Visualizer application includes unit tests to verify the correctness of the implemented sorting algorithms. The tests are designed to validate the sorting algorithms under various conditions, including different input datasets and edge cases.

## `SortEngineTests` Class

### Purpose

The `SortEngineTests` class contains unit tests for the sorting algorithms implemented in the Sort Visualizer application. It uses test data files to validate the correctness of the sorting algorithms under different scenarios. There are also hardcoded tests for specific cases such as empty array, array with a single element, array with all elements equal and an array inversely sorted.

### Attributes

- `private const string TestDataFolder = @"./test_data";`: Specifies the folder path for test data.

- `public static IEnumerable<object[]> SortingAlgorithms`: Provides a list of sorting algorithms to be tested.

### Methods

`[DynamicData(nameof(SortingAlgorithms))]`: This attribute is used to run the test for each sorting algorithm from `SortingAlgorithms` list. <br>
Test data files are located in the `SortVisualizer/bin/Debug/net8.0-windows/test_data` directory.

- `public void SortEngine_Test01(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test01.csv". (300 numbers)

- `public void SortEngine_Test02(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test02.csv". (300 numbers)

- `public void SortEngine_Test03(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test03.csv". (300 numbers)

- `public void SortEngine_Test04_Double(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test04_double.csv". (600 numbers)

- `public void SortEngine_Test05_Double(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test05_double.csv". (600 numbers)

- `public void SortEngine_Test06_Triple(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test06_triple.csv". (900 numbers)

- `public void SortEngine_Test07_Quadruple(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test07_quadruple.csv". (1200 numbers)

- `public void SortEngine_Test08_Quintuple(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test08_quintuple.csv". (1500 numbers)

- `public void SortEngine_Test09_Sextuple(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using the test data from "test09_sextuple.csv". (1800 numbers)

- `public void SortEngine_TestOneElement(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using an array with a single element.

- `public void SortEngine_TestEmptyArray(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using an empty array.

- `public void SortEngine_TestInverseArray(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using an array with elements in descending order.

- `public void SortEngine_TestArrayWithSameNumber(ISortEngine sortEngine)`: Runs a test for a specific sorting algorithm using an array with all elements equal.

- `private void RunSortEngineTest(ISortEngine sortEngine, string filename)`: Helper method to run a test for a specific sorting algorithm using the test data from a CSV file.

- `private void IsSorted(int[] numbers)`: Helper method to check if the array is correctly sorted.