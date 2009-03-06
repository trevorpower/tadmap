using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageManipulation;

namespace ThumbNails
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Image oImage = Bitmap.FromFile("../../TestImageTwo.png", true);
                pictureBox1.Visible = false;

                int iSize = 256;
                int iZoomLevel = 0;

                do
                {
                    List<List<Bitmap>> oTiles; 
                    
                    oTiles = ImageManipulator.CreateTiles(oImage as Bitmap, iSize);

                    for (int i = 0; i < oTiles.Count; i++)
                    {
                        for (int j = 0; j < oTiles[i].Count; j++)
                        {
                            PictureBox oNewPicture = new PictureBox();
                            oNewPicture.Size = oTiles[i][j].Size;
                            oNewPicture.Image = oTiles[i][j];
                            Controls.Add(oNewPicture);
                            oNewPicture.Left = (j * iSize) + j * (12 - iZoomLevel * 4);
                            oNewPicture.Top = (i * iSize) + i * (12 - iZoomLevel * 4);
                        }
                    }

                    iZoomLevel++;
                    oImage = ImageManipulator.Resize(oImage as Bitmap, oImage.Width / 2, oImage.Height / 2);
                }
                while (oImage.Width > iSize);
            }
            catch (Exception oException)
            {
                MessageBox.Show(oException.Message);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 10; i < Width; i += 10)
            {
                e.Graphics.DrawLine(Pens.Blue, i, 0, i, Height);
            }

            for (int i = 10; i < Height; i += 10)
            {
                e.Graphics.DrawLine(Pens.Blue, 0, i, Width, i);
            }
        }
    }
}
