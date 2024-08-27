using System.ComponentModel;
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
        private const int barWidth = 3; // The width of each bar

        // Constructor
        public MainForm()
        {
            InitializeComponent();
            LoadClassesIntoComboBox();
            GraphicsPanel.Paint += new PaintEventHandler(GraphicsPanel_Paint);
            this.Resize += MainForm_Resize;
            GenerateRandomArray();
        }

        // Find all classes that inherit ISortEngine and add them to the ComboBox
        private void LoadClassesIntoComboBox()
        {
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
                int x = i * barWidth;
                int barHeight = (int)((double)numbers[i] / maxValue * maxHeight);
                graphics.FillRectangle(whiteBrush, x, maxHeight - barHeight, barWidth, barHeight);
            }
        }

        // Event handler for resizing the window
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // If the window is being resized while sorting, do not resize the array
            if (!isSorting)
            {
                GenerateRandomArray();
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
            this.Close();
        }

        // Event handler for the Reset button
        private void ResetButton_Click(object sender, EventArgs e)
        {
            // If the array is not being sorted, generate a new random array
            if (!isSorting)
            {
                GenerateRandomArray();
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
            // Get the selected sorting algorithm
            Type selectedSort = Assembly.GetExecutingAssembly().GetType("SortVisualizer." + AlgoPicker.SelectedItem.ToString());
            if (selectedSort == null) return;

            isSorting = true; // Set the flag to indicate that the array is being sorted

            // Create an instance of the selected sorting algorithm
            ISortEngine sortEngine = (ISortEngine)Activator.CreateInstance(selectedSort);
            sortEngine.Sort(numbers, (i, j) =>
            {
                bgWorker.ReportProgress(i, j); // Report the progress to the UI thread
            });
        }

        // Event handler for the background worker's ProgressChanged event
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Redraw the two bars that have been swapped
            InvalidateArrayPosition(e.ProgressPercentage);
            InvalidateArrayPosition(e.UserState as int? ?? 0);
        }

        // Event handler for the background worker's RunWorkerCompleted event
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isSorting = false; // Set the flag to indicate that the array is no longer being sorted
        }
    }
}
