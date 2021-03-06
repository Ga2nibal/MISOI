﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Logic;
using Logic.Filters;

namespace Lab1
{
    public partial class Form1 : Form
    {
         private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private Bitmap _sourceImage;
        private Bitmap _loadedBitmap;
        private Dictionary<int, List<Point>> _circlePoints;
        private Dictionary<int, List<Point>> _rowsPoints;
        private Dictionary<int, List<Point>> _objectsDictPoints; 

        public Form1()
        {
            InitializeComponent();
            _openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF,*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
        }

        private void buttonOpenImage_Click(object sender, EventArgs e)
        {
            var dialogResult = _openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var image = new Bitmap(_openFileDialog.FileName);
                _sourcePictureBox.Image = image;    

                _sourceImage = _sourcePictureBox.Image.Clone() as Bitmap;
                _loadedBitmap = _sourcePictureBox.Image.Clone() as Bitmap;
            }
        }

        private async void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            if (_sourcePictureBox.Image == null)
            {
                MessageBox.Show("Выберите ихображение");
                return;
            }
            var choosenRadioButton = groupBoxFilters.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);

            if (choosenRadioButton == null)
                return;

            var filter = FilterFactory.Create(choosenRadioButton.Tag as string);
            if (filter is MonochromeFilter)
            {
                var monochromeFilter = (MonochromeFilter)filter;
                monochromeFilter.Level = hScrollBarLevel.Value;
            }

            waitLable.Text = "Ждите";
            _filteredPictureBox.Image = await filter.AsyncFilter((Bitmap)_sourcePictureBox.Image);
            _filteredPictureBox.Update();
            buttonResetToSourcImg_Click(null, null);
            waitLable.Text = "Ok";
        }

        private void buttonResetToSourcImg_Click(object sender, EventArgs e)
        {
            _sourcePictureBox.Image = _sourceImage.Clone() as Bitmap;
            _sourcePictureBox.Update();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBarLevel_Scroll(object sender, ScrollEventArgs e)
        {
            label3.Text = Convert.ToString(hScrollBarLevel.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialogResult = _saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _filteredPictureBox.Image.Save(_saveFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _sourceImage = (Bitmap)_filteredPictureBox.Image;
            _filteredPictureBox.Image = null;
            _sourcePictureBox.Image = _sourceImage.Clone() as Bitmap;
            _sourcePictureBox.Update();
            _filteredPictureBox.Update();
        }

        private void buttonHough_Click(object sender, EventArgs e)
        {
            //var houghFinder = new HoughCircle();
            //houghFinder.init(_sourceImage, 160);
            //houghFinder.process();
            //_filteredPictureBox.Image = houghFinder.GetBitmapResult();
            //_filteredPictureBox.Update();

            var imageProcessor = new ImageProcessor();
            imageProcessor.CurrentBitmap = (Bitmap)_sourcePictureBox.Image;
            imageProcessor.SequentialScan();
            //MessageBox.Show(imageProcessor.Shapes.Count.ToString());
            _filteredPictureBox.Image = imageProcessor.GetMarkedBitmap();
            _filteredPictureBox.Update();
            MessageBox.Show("ShowCircles");
            Bitmap result;
            _circlePoints = imageProcessor.GetCirclePoints(_loadedBitmap.Clone() as Bitmap, out result);
            _filteredPictureBox.Image = result;
            _filteredPictureBox.Update();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonFindRow_Click(object sender, EventArgs e)
        {
            Bitmap result;
            _rowsPoints = ImageProcessor.GetCounterArea(_sourcePictureBox.Image.Clone() as Bitmap, _circlePoints, out result);
            _filteredPictureBox.Image = result;
            _filteredPictureBox.Update();
        }

        private void buttonfindNumber_Click(object sender, EventArgs e)
        {
            Bitmap result;
            _objectsDictPoints = ImageProcessor.DetectCounterNumbers(_sourcePictureBox.Image.Clone() as Bitmap,
                _rowsPoints, out result);
            //_filteredPictureBox.Image = result;
            //_filteredPictureBox.Update();

            Bitmap resultBitmap = new Bitmap(_loadedBitmap.Width, _loadedBitmap.Height);
            Color zeroColor = Color.FromArgb(0, 0, 0, 0);
            for (int i = 0; i < resultBitmap.Width; i++)
                for (int j = 0; j < resultBitmap.Height; j++)
                    resultBitmap.SetPixel(i, j, zeroColor);
            foreach (var rowpoints in _objectsDictPoints.Values)
            {
                foreach (Point cPoint in rowpoints)
                {
                    resultBitmap.SetPixel(cPoint.X, cPoint.Y, _loadedBitmap.GetPixel(cPoint.X, cPoint.Y));
                }
            }
            _filteredPictureBox.Image = resultBitmap;
            _filteredPictureBox.Update();

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult resultDirDialog = fbd.ShowDialog();
            if(resultDirDialog != DialogResult.OK)
                return;
            string dirPath = fbd.SelectedPath;
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            var toremove = _objectsDictPoints.Where(kv => kv.Value.Count < 30).Select(i => i.Key).ToArray();
            foreach (int i in toremove)
            {
                _objectsDictPoints.Remove(i);
            }
            List<Image> numbersImages = new List<Image>(_objectsDictPoints.Count);
            foreach (KeyValuePair<int, List<Point>> objectNumberValuePair in _objectsDictPoints)
            {
                int minX = objectNumberValuePair.Value.Min(p => p.X);
                int minY = objectNumberValuePair.Value.Min(p => p.Y);
                int maxX = objectNumberValuePair.Value.Max(p => p.X);
                int maxY = objectNumberValuePair.Value.Max(p => p.Y);
                Bitmap bitmap = new Bitmap(maxX-minX+1, maxY-minY+1);
                foreach (Point p in objectNumberValuePair.Value)
                    bitmap.SetPixel(p.X-minX, p.Y-minY, _loadedBitmap.GetPixel(p.X, p.Y));
                numbersImages.Add(bitmap);
            }
            for (int i = 0; i < numbersImages.Count; i++)
            {
                string imageFilepath = Path.Combine(dirPath, i + ".jpg");
                numbersImages[i].Save(imageFilepath);

            }
        }
    }
}
