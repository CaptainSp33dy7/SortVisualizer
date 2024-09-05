using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace SortVisualizer
{
    public partial class MainForm : Form
    {
        int[] numbers; // Array to be sorted
        bool isSorting = false; // Flag to indicate whether the array is being sorted
        private SolidBrush whiteBrush = new SolidBrush(Color.White); // Brush for drawing the bars
        private SolidBrush blackBrush = new SolidBrush(Color.Black); // Brush for erasing the bars
        private int arraySize; // The size of the array to be sorted
        private const int maxValue = 1000; // Maximum value for each element
        private int barWidth; // The width of each bar

        // Constructor
        public MainForm()
        {
            barWidth = 3; // The width of each bar set to 3 by default, for bigger arrays it gets reduced
            InitializeComponent();
            this.MinimumSize = new Size(900, 200); // Set the minimum size of the form
            LoadClassesIntoComboBox(); // Load the sorting algorithms into the ComboBox
            LoadDataIntoComboBox(); // Load the data files into the ComboBox
            GraphicsPanel.Paint += new PaintEventHandler(GraphicsPanel_Paint); // Add the Paint event handler
            this.Resize += MainForm_Resize; // Add the Resize event handler
            AlgoPicker.SelectedIndexChanged += AlgoPicker_SelectedIndexChanged; // Add the SelectedIndexChanged event handler for AlgoPicker
            GenerateRandomArray(); // Generate a random array
        }

        // Find all classes that inherit ISortEngine and add them to the ComboBox
        private void LoadClassesIntoComboBox()
        {
            AlgoPicker.Items.Clear();
            // Loop through all types in the assembly to find the ones that implement ISortEngine
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterfaces().Contains(typeof(ISortEngine)))
                {
                    AlgoPicker.Items.Add(type.Name);
                }
            }
            AlgoPicker.SelectedIndex = 0; // Select the first item by default
        }

        // Find all csv files in the directory and add them to the ComboBox along with Random option
        private void LoadDataIntoComboBox()
        {
            DataPicker.Items.Clear();
            DataPicker.Items.Add("Random");
            string[] files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "test_data"), "*.csv");
            foreach (string file in files)
            {
                DataPicker.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
            DataPicker.SelectedIndex = 0; // Select the first item by default
        }

        // Event handler for the AlgoPicker's SelectedIndexChanged event
        private void AlgoPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hides the StopButton if the selected algorithm is CountingSort because of the problematic cancellation

            // Check if the selected algorithm is "CountingSort"
            if (AlgoPicker.SelectedItem.ToString() == "CountingSort")
            {
                StopButton.Visible = false; // Hide the StopButton
            }
            else
            {
                StopButton.Visible = true; // Show the StopButton
            }
        }

        // Event handler for redrawing the array
        private void GraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            // If the array is not null, draw it
            if (numbers != null)
            {
                DrawArray(e.Graphics);
            }
        }

        // Draw the array of numbers as bars
        private void DrawArray(Graphics graphics)
        {
            int maxHeight = GraphicsPanel.Height; // The height of the panel

            graphics.Clear(Color.Black); // Clear the panel

            // Draw each bar
            for (int i = 0; i < numbers.Length; i++)
            {
                int x = i * barWidth; // Calculate the x position of the bar
                int barHeight = (int)((double)numbers[i] / maxValue * maxHeight); // Calculate the height of the bar
                graphics.FillRectangle(whiteBrush, x, maxHeight - barHeight, barWidth, barHeight); // Draw the bar
            }
        }

        // Event handler for resizing the window
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // If the window is being resized while sorting, do not resize the array
            if (!isSorting)
            {
                string selectedItem = DataPicker.SelectedItem.ToString();

                // If the selected item is Random, generate a new random array
                if (selectedItem == "Random")
                {
                    barWidth = 3;
                    GenerateRandomArray();
                }
                else // Otherwise, load the selected CSV file
                {
                    string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                    string testsFolder = Path.Combine(appFolder, "test_data");
                    string filePath = Path.Combine(testsFolder, selectedItem) + ".csv";
                    LoadArrayFromCSV(filePath);
                }
            }

            GraphicsPanel.Invalidate(); // Redraw the array
        }

        // Generate a new random array
        private void GenerateRandomArray()
        {
            // Calculate the size of the array based on the width of the panel
            arraySize = GraphicsPanel.Width / barWidth;
            numbers = new int[arraySize + 1]; // Create a bigger array to avoid out gap
            Random random = new Random();
            // Fill the array with random numbers
            for (int i = 0; i < arraySize + 1; i++)
            {
                numbers[i] = random.Next(maxValue);
            }
        }

        // Event handler for the DataPicker's SelectedIndexChanged event
        private void LoadArrayFromCSV(string filePath)
        {
            try
            {
                // Read the lines from the CSV file
                string[] lines = File.ReadAllLines(filePath);
                numbers = new int[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    numbers[i] = int.Parse(lines[i]);
                }

                // If the array is too big, reduce the bar width
                if (lines.Length > 600 && lines.Length < 1200)
                {
                    barWidth = 2;
                }
                else if (lines.Length >= 1200)
                {
                    barWidth = 1;
                }
                else
                {
                    barWidth = 3;
                }
            }
            catch (Exception ex)
            {
                // Display an error message if the file cannot be loaded
                MessageBox.Show("Error loading CSV file: " + ex.Message);
            }
        }

        // Helper method to invalidate the position of a bar in the array
        private void InvalidateArrayPosition(int index)
        {
            if (index >= 0 && index < numbers.Length)
            {
                // Calculate the rectangle area for the specific bar that needs to be redrawn
                int x = index * barWidth;
                Rectangle rect = new Rectangle(x, 0, barWidth, GraphicsPanel.Height);

                // Invalidate only this rectangle
                GraphicsPanel.Invalidate(rect);
            }
        }

        // Event handler for the Exit menu item
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the application
        }

        // Event handler for the Reset button
        private void ResetButton_Click(object sender, EventArgs e)
        {
            // If the array is not being sorted, either load or generate array
            if (!isSorting)
            {
                string selectedItem = DataPicker.SelectedItem.ToString();

                if (selectedItem == "Random")
                {
                    barWidth = 3; // Reset the bar width
                    GenerateRandomArray();
                }
                else
                {
                    string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                    string testsFolder = Path.Combine(appFolder, "test_data");
                    string filePath = Path.Combine(testsFolder, selectedItem) + ".csv";
                    LoadArrayFromCSV(filePath); // Load the selected CSV file
                }

                GraphicsPanel.Invalidate(); // Redraw the array
            }
        }

        // Event handler for the Sort button
        private void SortButton_Click(object sender, EventArgs e)
        {
            // If the array is not being sorted, start sorting
            if (isSorting) return;
            bgWorker.RunWorkerAsync();
        }

        // Event handler for the background worker's DoWork event
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // is called by bgWorker.RunWorkerAsync() in SortButton_Click
            // this method is executed on a separate thread

            // Get the selected sorting algorithm using UI thread
            Type selectedSort = null;
            try
            {
                this.Invoke((Action)delegate
                {
                    selectedSort = Assembly.GetExecutingAssembly().GetType("SortVisualizer." + AlgoPicker.SelectedItem.ToString());
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting sorting algorithm: " + ex.Message);
            }

            if (selectedSort == null) return;

            isSorting = true; // Set the flag to indicate that the array is being sorted

            // Create an instance of the selected sorting algorithm
            ISortEngine sortEngine = (ISortEngine)Activator.CreateInstance(selectedSort);
            sortEngine.Sort(numbers, (i, j) =>
            {
                bgWorker.ReportProgress(i, j); // Report the progress to the UI thread
            }, bgWorker, e); // Call the Sort method of the sorting algorithm
        }

        // Event handler for the background worker's ProgressChanged event
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // is called by bgWorker.ReportProgress(i, j) when updateCallback is called in Sort method

            // Redraw the two bars that have been swapped
            InvalidateArrayPosition(e.ProgressPercentage); // first index (ProgressPercentage because bgWorker's arguments, useful because it is int)
            InvalidateArrayPosition(e.UserState as int? ?? 0); // second index (default to 0)
        }

        // Event handler for the background worker's RunWorkerCompleted event
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isSorting = false; // Set the flag to indicate that the array is no longer being sorted

            if (e.Cancelled)
            {
                // The operation was canceled
                MessageBox.Show("Sorting was canceled.");
            }
            else if (e.Error != null)
            {
                // An error occurred during the operation
                MessageBox.Show("An error occurred: " + e.Error.Message);
            }
            else
            {
                CheckSorted(); // Final check if the array is sorted
            }
            bgWorker.Dispose(); // Dispose of the background worker
        }

        // Event handler for the Cancel button
        private void StopButton_Click(object sender, EventArgs e)
        {
            // If the array is being sorted, cancel the sorting process
            if (isSorting)
            {
                bgWorker.CancelAsync(); // raises e.Cancel = true in bgWorker_DoWork and shows cancel message in bgWorker_RunWorkerCompleted
            }
        }

        // Final check if the array is sorted
        private void CheckSorted()
        {
            // Check if the array is sorted
            bool sorted = true;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] > numbers[i + 1])
                {
                    sorted = false;
                    break;
                }
            }

            if (sorted)
            {
                // Display a message if the array is sorted
                MessageBox.Show("The array is sorted.");
            }
            else
            {
                // Display a message if the array is not sorted
                MessageBox.Show("The array is not sorted.");
            }
        }

        // Open the README file when the README menu item is clicked
        private void rEADMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current application folder (e.g., SortVisualizer/bin/Debug/net8.0-windows/)
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;

                // Move up four levels to reach the SortVisualizer directory
                string rootFolder = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(appFolder).FullName).FullName).FullName).FullName;

                // Combine the SortVisualizer folder path with README.md
                string docPath = Path.Combine(rootFolder, "README.md");

                Process.Start(new ProcessStartInfo(docPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Open my GitHub profile when the About menu item is clicked
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // URL of my GitHub profile
                string url = "https://github.com/CaptainSp33dy7";

                // Open the URL in the default web browser
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
