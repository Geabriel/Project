﻿namespace OK.ru_Serf
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.startSearchButton = new System.Windows.Forms.Button();
            this.stoptSearchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.countPiple = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startSearchButton
            // 
            this.startSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startSearchButton.Location = new System.Drawing.Point(499, 294);
            this.startSearchButton.Name = "startSearchButton";
            this.startSearchButton.Size = new System.Drawing.Size(75, 23);
            this.startSearchButton.TabIndex = 0;
            this.startSearchButton.Text = "Start";
            this.startSearchButton.UseVisualStyleBackColor = true;
            this.startSearchButton.Click += new System.EventHandler(this.startSearchButton_Click);
            // 
            // stoptSearchButton
            // 
            this.stoptSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stoptSearchButton.Location = new System.Drawing.Point(499, 323);
            this.stoptSearchButton.Name = "stoptSearchButton";
            this.stoptSearchButton.Size = new System.Drawing.Size(75, 23);
            this.stoptSearchButton.TabIndex = 1;
            this.stoptSearchButton.Text = "BrauserClose";
            this.stoptSearchButton.UseVisualStyleBackColor = true;
            this.stoptSearchButton.Click += new System.EventHandler(this.stoptSearchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTextBox.Location = new System.Drawing.Point(12, 12);
            this.searchTextBox.Multiline = true;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.ReadOnly = true;
            this.searchTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.searchTextBox.Size = new System.Drawing.Size(481, 334);
            this.searchTextBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "Check e-mail";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // countPiple
            // 
            this.countPiple.Location = new System.Drawing.Point(499, 239);
            this.countPiple.Name = "countPiple";
            this.countPiple.Size = new System.Drawing.Size(75, 20);
            this.countPiple.TabIndex = 4;
            this.countPiple.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.countPiple_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(496, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Count of people";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 358);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.countPiple);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.stoptSearchButton);
            this.Controls.Add(this.startSearchButton);
            this.Name = "Form1";
            this.Text = "OK.RU";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSearchButton;
        private System.Windows.Forms.Button stoptSearchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox countPiple;
        private System.Windows.Forms.Label label1;
    }
}

