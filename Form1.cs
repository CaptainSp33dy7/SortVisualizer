namespace SortVisualizer
{
    public partial class Form1 : Form
    {
        int[] numbers; // Array to be sorted
        Graphics graphics; // Graphics object to draw on the panel

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            // Reset the array
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

        private void SortButton_Click(object sender, EventArgs e)
        {
            ISortEngine sortEngine = new BubbleSort();
            sortEngine.Sort(numbers, graphics, GraphicsPanel.Height);
        }
    }
}
