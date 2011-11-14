namespace TransparencyControlEx
{
    partial class PropertyForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.transpStep = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.namesList = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.useExtendClientArea = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.transpStep)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // transpStep
            // 
            this.transpStep.Location = new System.Drawing.Point(199, 124);
            this.transpStep.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.transpStep.Name = "transpStep";
            this.transpStep.Size = new System.Drawing.Size(100, 20);
            this.transpStep.TabIndex = 2;
            this.transpStep.Validated += new System.EventHandler(this.transpStep_Validated);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 101);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_HotKeysGroupBox;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_SwitchGlass;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_DecTransparencyHotKey;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_IncTransparencyHotKey;
            // 
            // namesList
            // 
            this.namesList.Location = new System.Drawing.Point(313, 42);
            this.namesList.Multiline = true;
            this.namesList.Name = "namesList";
            this.namesList.Size = new System.Drawing.Size(282, 125);
            this.namesList.TabIndex = 6;
            this.namesList.Text = global::TransparencyControlEx.Properties.Settings.Default.Names;
            this.namesList.Validating += new System.ComponentModel.CancelEventHandler(this.captionsList_Validating);
            // 
            // label3
            // 
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TransparencyControlEx.Properties.Settings.Default, "PF_ProcessNamesCaption", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label3.Location = new System.Drawing.Point(313, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_ProcessNamesCaption;
            // 
            // useExtendClientArea
            // 
            this.useExtendClientArea.AutoSize = true;
            this.useExtendClientArea.Checked = global::TransparencyControlEx.Properties.Settings.Default.UseExtendClientArea;
            this.useExtendClientArea.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TransparencyControlEx.Properties.Settings.Default, "UseExtendClientArea", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.useExtendClientArea.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TransparencyControlEx.Properties.Settings.Default, "PF_UseExtendClientArea", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.useExtendClientArea.Location = new System.Drawing.Point(12, 150);
            this.useExtendClientArea.Name = "useExtendClientArea";
            this.useExtendClientArea.Size = new System.Drawing.Size(180, 17);
            this.useExtendClientArea.TabIndex = 3;
            this.useExtendClientArea.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_UseExtendClientArea;
            this.useExtendClientArea.UseVisualStyleBackColor = true;
            this.useExtendClientArea.CheckedChanged += new System.EventHandler(this.useExtendClientArea_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_TransparencyStep;
            // 
            // PropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 182);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.namesList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.useExtendClientArea);
            this.Controls.Add(this.transpStep);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PropertyForm";
            this.Text = global::TransparencyControlEx.Properties.Settings.Default.PF_Caption;
            this.Load += new System.EventHandler(this.PropertyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.transpStep)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown transpStep;
        private System.Windows.Forms.CheckBox useExtendClientArea;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox namesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

