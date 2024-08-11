using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    internal class BubbleSort : ISortEngine
    {
        private int[] numbers;
        private Graphics graphics;
        private int maxValue;
        private bool sorted = false;
        private SolidBrush blackBrush = new SolidBrush(Color.Black);
        private SolidBrush whiteBrush = new SolidBrush(Color.White);

        public void Sort(int[] numbers_in, Graphics graphics_in, int maxValue_in)
        {
            numbers = numbers_in;
            graphics = graphics_in;
            maxValue = maxValue_in;

            while(!sorted)
            {
                sorted = true;
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] > numbers[i + 1])
                    {
                        Swap(i, i + 1);
                        sorted = false;
                    }
                }
            }
        }

        private void Swap(int i, int j)
        { 
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;

            graphics.FillRectangle(blackBrush, i, 0, 1, maxValue);
            graphics.FillRectangle(blackBrush, j, 0, 1, maxValue);

            graphics.FillRectangle(whiteBrush, i, maxValue - numbers[i], 1, maxValue);
            graphics.FillRectangle(whiteBrush, j, maxValue - numbers[j], 1, maxValue);
        }
    }
}
