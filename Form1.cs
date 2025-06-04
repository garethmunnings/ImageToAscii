using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AssciImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            Bitmap copyBitmap = new Bitmap((Bitmap)pictureBox1.Image);
            processImage(copyBitmap);
        }
        bool processImage(Bitmap bmp)
        {
            char[] density = {'Ñ', '@', '#', 'W', '$', '9', '8', '7', '6', '5', '4', '3', '2', '1', '0', '?', '!', 'a', 'b', 'c', ';', ':', '+', '=', '-', ',', '.', '_', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
            char[,] output = new char[480,480];
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color bmpColor = bmp.GetPixel(j, i);
                    int red = bmpColor.R;
                    int green = bmpColor.G;
                    int blue = bmpColor.B;
                    int avgRGB = (red + green + blue) / 3;
                    int posDensity = avgRGB / density.Length;
                    output[j,i] = density[posDensity];
                }
                
            }
            Write(output);
            return true;
        }
        void Write(char[,] output)
        {
            StreamWriter wr = new StreamWriter("Output");           
            for(int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 480; j++)
                {
                    wr.Write(output[j, i]);
                }
                wr.Write("\n");
            }
            wr.Close();
        }
        
    }
}
