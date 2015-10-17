using System.Windows.Forms;
using Lab1.Filters;

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
            this.waitLable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.radioButtonBoundary = new System.Windows.Forms.RadioButton();
            this.radioButtonSharpen = new System.Windows.Forms.RadioButton();
            this.radioButtonMonochrome = new System.Windows.Forms.RadioButton();
            this.radioButtonMedian = new System.Windows.Forms.RadioButton();
            this.hScrollBarLevel = new System.Windows.Forms.HScrollBar();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this._filteredPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._sourcePictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._filteredPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _sourcePictureBox
            // 
            //this._sourcePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sourcePictureBox.Location = new System.Drawing.Point(0, 0);
            this._sourcePictureBox.Name = "_sourcePictureBox";
            this._sourcePictureBox.Size = new System.Drawing.Size(890, 280);
            this._sourcePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._sourcePictureBox.TabIndex = 0;
            this._sourcePictureBox.TabStop = false;
            // 
            // panel1
            // 
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
            // waitLable
            // 
            this.waitLable.AutoSize = true;
            this.waitLable.Location = new System.Drawing.Point(645, 19);
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
            this.groupBoxFilters.Controls.Add(this.radioButtonBoundary);
            this.groupBoxFilters.Controls.Add(this.radioButtonSharpen);
            this.groupBoxFilters.Controls.Add(this.radioButtonMonochrome);
            this.groupBoxFilters.Controls.Add(this.radioButtonMedian);
            this.groupBoxFilters.Location = new System.Drawing.Point(146, 5);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(308, 44);
            this.groupBoxFilters.TabIndex = 3;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Choose filter";
            // 
            // radioButtonBoundary
            // 
            this.radioButtonBoundary.AutoSize = true;
            this.radioButtonBoundary.Location = new System.Drawing.Point(234, 20);
            this.radioButtonBoundary.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonBoundary.Name = "radioButtonBoundary";
            this.radioButtonBoundary.Size = new System.Drawing.Size(70, 17);
            this.radioButtonBoundary.TabIndex = 3;
            this.radioButtonBoundary.TabStop = true;
            this.radioButtonBoundary.Text = "Boundary";
            this.radioButtonBoundary.UseVisualStyleBackColor = true;
            // 
            // radioButtonSharpen
            // 
            this.radioButtonSharpen.AutoSize = true;
            this.radioButtonSharpen.Location = new System.Drawing.Point(165, 20);
            this.radioButtonSharpen.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonSharpen.Name = "radioButtonSharpen";
            this.radioButtonSharpen.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSharpen.TabIndex = 2;
            this.radioButtonSharpen.TabStop = true;
            this.radioButtonSharpen.Text = "Sharpen";
            this.radioButtonSharpen.UseVisualStyleBackColor = true;
            // 
            // radioButtonMonochrome
            // 
            this.radioButtonMonochrome.AutoSize = true;
            this.radioButtonMonochrome.Location = new System.Drawing.Point(73, 19);
            this.radioButtonMonochrome.Name = "radioButtonMonochrome";
            this.radioButtonMonochrome.Size = new System.Drawing.Size(87, 17);
            this.radioButtonMonochrome.TabIndex = 1;
            this.radioButtonMonochrome.TabStop = true;
            this.radioButtonMonochrome.Text = "Monochrome";
            this.radioButtonMonochrome.UseVisualStyleBackColor = true;
            // 
            // radioButtonMedian
            // 
            this.radioButtonMedian.AutoSize = true;
            this.radioButtonMedian.BackColor = System.Drawing.SystemColors.Control;
            this.radioButtonMedian.Checked = true;
            this.radioButtonMedian.Location = new System.Drawing.Point(7, 20);
            this.radioButtonMedian.Name = "radioButtonMedian";
            this.radioButtonMedian.Size = new System.Drawing.Size(60, 17);
            this.radioButtonMedian.TabIndex = 0;
            this.radioButtonMedian.TabStop = true;
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
            this.buttonApplyFilter.Location = new System.Drawing.Point(460, 14);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(77, 22);
            this.buttonApplyFilter.TabIndex = 1;
            this.buttonApplyFilter.Text = "Filter";
            this.buttonApplyFilter.UseVisualStyleBackColor = true;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(543, 14);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(77, 21);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open Image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // _filteredPictureBox
            // 
            //this._filteredPictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._filteredPictureBox.Location = new System.Drawing.Point(0, 288);
            this._filteredPictureBox.Name = "_filteredPictureBox";
            this._filteredPictureBox.Size = new System.Drawing.Size(890, 280);
            this._filteredPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._filteredPictureBox.TabIndex = 2;
            this._filteredPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 576);
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
    }
}

