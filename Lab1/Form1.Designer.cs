using System.Windows.Forms;
using Logic.Filters;

namespace Lab1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this._sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonFindRow = new System.Windows.Forms.Button();
            this.buttonHough = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.waitLable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.radioButtonGray = new System.Windows.Forms.RadioButton();
            this.radioButtonRevert = new System.Windows.Forms.RadioButton();
            this.radioButtonBox = new System.Windows.Forms.RadioButton();
            this.radioButtonBoundary = new System.Windows.Forms.RadioButton();
            this.radioButtonSharpen = new System.Windows.Forms.RadioButton();
            this.radioButtonMonochrome = new System.Windows.Forms.RadioButton();
            this.radioButtonMedian = new System.Windows.Forms.RadioButton();
            this.hScrollBarLevel = new System.Windows.Forms.HScrollBar();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this._filteredPictureBox = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonfindNumber = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._sourcePictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._filteredPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _sourcePictureBox
            // 
            this._sourcePictureBox.Location = new System.Drawing.Point(0, 50);
            this._sourcePictureBox.Name = "_sourcePictureBox";
            this._sourcePictureBox.Size = new System.Drawing.Size(890, 250);
            this._sourcePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._sourcePictureBox.TabIndex = 0;
            this._sourcePictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonfindNumber);
            this.panel1.Controls.Add(this.buttonFindRow);
            this.panel1.Controls.Add(this.buttonHough);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.waitLable);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBoxFilters);
            this.panel1.Controls.Add(this.hScrollBarLevel);
            this.panel1.Controls.Add(this.buttonApplyFilter);
            this.panel1.Controls.Add(this.buttonOpenImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 60);
            this.panel1.TabIndex = 1;
            // 
            // buttonFindRow
            // 
            this.buttonFindRow.Location = new System.Drawing.Point(803, 32);
            this.buttonFindRow.Name = "buttonFindRow";
            this.buttonFindRow.Size = new System.Drawing.Size(75, 23);
            this.buttonFindRow.TabIndex = 9;
            this.buttonFindRow.Text = "findRow";
            this.buttonFindRow.UseVisualStyleBackColor = true;
            this.buttonFindRow.Click += new System.EventHandler(this.buttonFindRow_Click);
            // 
            // buttonHough
            // 
            this.buttonHough.Location = new System.Drawing.Point(721, 30);
            this.buttonHough.Name = "buttonHough";
            this.buttonHough.Size = new System.Drawing.Size(75, 23);
            this.buttonHough.TabIndex = 8;
            this.buttonHough.Text = "hough";
            this.buttonHough.UseVisualStyleBackColor = true;
            this.buttonHough.Click += new System.EventHandler(this.buttonHough_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(636, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "change";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(802, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // waitLable
            // 
            this.waitLable.AutoSize = true;
            this.waitLable.Location = new System.Drawing.Point(646, 36);
            this.waitLable.Name = "waitLable";
            this.waitLable.Size = new System.Drawing.Size(0, 13);
            this.waitLable.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "127";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "255";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBoxFilters
            // 
            this.groupBoxFilters.Controls.Add(this.radioButtonGray);
            this.groupBoxFilters.Controls.Add(this.radioButtonRevert);
            this.groupBoxFilters.Controls.Add(this.radioButtonBox);
            this.groupBoxFilters.Controls.Add(this.radioButtonBoundary);
            this.groupBoxFilters.Controls.Add(this.radioButtonSharpen);
            this.groupBoxFilters.Controls.Add(this.radioButtonMonochrome);
            this.groupBoxFilters.Controls.Add(this.radioButtonMedian);
            this.groupBoxFilters.Location = new System.Drawing.Point(146, 5);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(295, 44);
            this.groupBoxFilters.TabIndex = 3;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Choose filter";
            // 
            // radioButtonGray
            // 
            this.radioButtonGray.AutoSize = true;
            this.radioButtonGray.Location = new System.Drawing.Point(195, 13);
            this.radioButtonGray.Name = "radioButtonGray";
            this.radioButtonGray.Size = new System.Drawing.Size(47, 17);
            this.radioButtonGray.TabIndex = 5;
            this.radioButtonGray.TabStop = true;
            this.radioButtonGray.Tag = "Gray";
            this.radioButtonGray.Text = "Gray";
            this.radioButtonGray.UseVisualStyleBackColor = true;
            this.radioButtonGray.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonRevert
            // 
            this.radioButtonRevert.AutoSize = true;
            this.radioButtonRevert.Location = new System.Drawing.Point(168, 27);
            this.radioButtonRevert.Name = "radioButtonRevert";
            this.radioButtonRevert.Size = new System.Drawing.Size(57, 17);
            this.radioButtonRevert.TabIndex = 4;
            this.radioButtonRevert.TabStop = true;
            this.radioButtonRevert.Tag = "Revert";
            this.radioButtonRevert.Text = "Revert";
            this.radioButtonRevert.UseVisualStyleBackColor = true;
            // 
            // radioButtonBox
            // 
            this.radioButtonBox.AutoSize = true;
            this.radioButtonBox.Location = new System.Drawing.Point(146, 13);
            this.radioButtonBox.Name = "radioButtonBox";
            this.radioButtonBox.Size = new System.Drawing.Size(43, 17);
            this.radioButtonBox.TabIndex = 4;
            this.radioButtonBox.TabStop = true;
            this.radioButtonBox.Tag = "BoxFilter";
            this.radioButtonBox.Text = "Box";
            this.radioButtonBox.UseVisualStyleBackColor = true;
            // 
            // radioButtonBoundary
            // 
            this.radioButtonBoundary.AutoSize = true;
            this.radioButtonBoundary.Location = new System.Drawing.Point(71, 13);
            this.radioButtonBoundary.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonBoundary.Name = "radioButtonBoundary";
            this.radioButtonBoundary.Size = new System.Drawing.Size(70, 17);
            this.radioButtonBoundary.TabIndex = 3;
            this.radioButtonBoundary.TabStop = true;
            this.radioButtonBoundary.Tag = "BoundaryFilter";
            this.radioButtonBoundary.Text = "Boundary";
            this.radioButtonBoundary.UseVisualStyleBackColor = true;
            // 
            // radioButtonSharpen
            // 
            this.radioButtonSharpen.AutoSize = true;
            this.radioButtonSharpen.Location = new System.Drawing.Point(98, 25);
            this.radioButtonSharpen.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonSharpen.Name = "radioButtonSharpen";
            this.radioButtonSharpen.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSharpen.TabIndex = 2;
            this.radioButtonSharpen.TabStop = true;
            this.radioButtonSharpen.Tag = "SharpenFilter";
            this.radioButtonSharpen.Text = "Sharpen";
            this.radioButtonSharpen.UseVisualStyleBackColor = true;
            // 
            // radioButtonMonochrome
            // 
            this.radioButtonMonochrome.AutoSize = true;
            this.radioButtonMonochrome.Location = new System.Drawing.Point(6, 26);
            this.radioButtonMonochrome.Name = "radioButtonMonochrome";
            this.radioButtonMonochrome.Size = new System.Drawing.Size(87, 17);
            this.radioButtonMonochrome.TabIndex = 1;
            this.radioButtonMonochrome.TabStop = true;
            this.radioButtonMonochrome.Tag = "MonochromeFilter";
            this.radioButtonMonochrome.Text = "Monochrome";
            this.radioButtonMonochrome.UseVisualStyleBackColor = true;
            // 
            // radioButtonMedian
            // 
            this.radioButtonMedian.AutoSize = true;
            this.radioButtonMedian.BackColor = System.Drawing.SystemColors.Control;
            this.radioButtonMedian.Checked = true;
            this.radioButtonMedian.Location = new System.Drawing.Point(6, 13);
            this.radioButtonMedian.Name = "radioButtonMedian";
            this.radioButtonMedian.Size = new System.Drawing.Size(60, 17);
            this.radioButtonMedian.TabIndex = 0;
            this.radioButtonMedian.TabStop = true;
            this.radioButtonMedian.Tag = "MedianFilter";
            this.radioButtonMedian.Text = "Median";
            this.radioButtonMedian.UseVisualStyleBackColor = false;
            // 
            // hScrollBarLevel
            // 
            this.hScrollBarLevel.Location = new System.Drawing.Point(9, 5);
            this.hScrollBarLevel.Maximum = 255;
            this.hScrollBarLevel.Name = "hScrollBarLevel";
            this.hScrollBarLevel.Size = new System.Drawing.Size(131, 17);
            this.hScrollBarLevel.TabIndex = 2;
            this.hScrollBarLevel.Value = 127;
            this.hScrollBarLevel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarLevel_Scroll);
            // 
            // buttonApplyFilter
            // 
            this.buttonApplyFilter.Location = new System.Drawing.Point(636, 0);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(77, 22);
            this.buttonApplyFilter.TabIndex = 1;
            this.buttonApplyFilter.Text = "Filter";
            this.buttonApplyFilter.UseVisualStyleBackColor = true;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(719, 3);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(77, 21);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open Image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // _filteredPictureBox
            // 
            this._filteredPictureBox.Location = new System.Drawing.Point(0, 310);
            this._filteredPictureBox.Name = "_filteredPictureBox";
            this._filteredPictureBox.Size = new System.Drawing.Size(890, 250);
            this._filteredPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._filteredPictureBox.TabIndex = 2;
            this._filteredPictureBox.TabStop = false;
            // 
            // buttonfindNumber
            // 
            this.buttonfindNumber.Location = new System.Drawing.Point(555, -1);
            this.buttonfindNumber.Name = "buttonfindNumber";
            this.buttonfindNumber.Size = new System.Drawing.Size(75, 23);
            this.buttonfindNumber.TabIndex = 10;
            this.buttonfindNumber.Text = "findNumber";
            this.buttonfindNumber.UseVisualStyleBackColor = true;
            this.buttonfindNumber.Click += new System.EventHandler(this.buttonfindNumber_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 600);
            this.Controls.Add(this._filteredPictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._sourcePictureBox);
            this.Name = "Form1";
            this.Text = "MISOI 1";
            ((System.ComponentModel.ISupportInitialize)(this._sourcePictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._filteredPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _sourcePictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOpenImage;
        private System.Windows.Forms.Button buttonApplyFilter;
        private System.Windows.Forms.HScrollBar hScrollBarLevel;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.RadioButton radioButtonMonochrome;
        private System.Windows.Forms.RadioButton radioButtonMedian;
        private System.Windows.Forms.RadioButton radioButtonSharpen;
        private System.Windows.Forms.RadioButton radioButtonBoundary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Label waitLable;
        private PictureBox _filteredPictureBox;
        private RadioButton radioButtonBox;
        private Button button1;
        private SaveFileDialog saveFileDialog1;
        private Button button2;
        private RadioButton radioButtonRevert;
        private Button buttonHough;
        private RadioButton radioButtonGray;
        private Button buttonFindRow;
        private Button buttonfindNumber;
    }
}

