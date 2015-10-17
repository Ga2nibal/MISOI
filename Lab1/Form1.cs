using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab1.Filters;

namespace Lab1
{
    public partial class Form1 : Form
    {
         private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private Bitmap _sourceImage;

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

            var filter = FilterFactory.Create(choosenRadioButton.Text);
            if (choosenRadioButton.Text == "Monochrome")
            {
                var monochromeFilter = filter as MonochromeFilter;
                if (monochromeFilter != null) monochromeFilter.Level = hScrollBarLevel.Value;
            }

            waitLable.Text = "Ждите";
            _filteredPictureBox.Image = await filter.AsyncFilter((Bitmap)_sourcePictureBox.Image);
            waitLable.Text = "Ok";
        }

        private void buttonResetToSourcImg_Click(object sender, EventArgs e)
        {
            _sourcePictureBox.Image = _sourceImage.Clone() as Bitmap;
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
    }
}
