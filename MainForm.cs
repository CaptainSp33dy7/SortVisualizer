using System.Reflection;

namespace SortVisualizer
{
    public partial class MainForm : Form
    {
        int[] numbers; // Array to be sorted
        Graphics graphics; // Graphics object to draw on the GraphicsPanel

        public MainForm()
        {
            InitializeComponent();
            LoadClassesIntoComboBox();
            GraphicsPanel.Paint += new PaintEventHandler(GraphicsPanel_Paint);
        }

        // Find all classes that inherit ISortEngine and add them to the ComboBox
        private void LoadClassesIntoComboBox()
        {
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
            graphics = GraphicsPanel.CreateGraphics();
            int count = GraphicsPanel.Width;
            int maxValue = GraphicsPanel.Height;
            numbers = new int[count];

            // Fill the GraphicsPanel with black background
            graphics.Clear(Color.Black);

            // Fill the array with random values
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                numbers[i] = random.Next(maxValue);
            }

            // Draw the array
            for (int i = 0; i < count; i++)
            {
                graphics.FillRectangle(new SolidBrush(Color.White), i, maxValue - numbers[i], 1, maxValue);
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
            GraphicsPanel_Paint(null, null); // Redraw the array
        }

        // Event handler for the Sort button
        private void SortButton_Click(object sender, EventArgs e)
        {
            ISortEngine sortEngine = new BubbleSort(); // Change this line to use a different sorting algorithm
            sortEngine.Sort(numbers, graphics, GraphicsPanel.Height);
        }
    }
}
