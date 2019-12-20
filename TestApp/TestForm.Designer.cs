namespace TestApp
{
    partial class TestForm
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
            this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.PB_Graph = new System.Windows.Forms.PictureBox();
            this.PB_Code = new System.Windows.Forms.PictureBox();
            this.B_Update = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.TLP_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Code)).BeginInit();
            this.SuspendLayout();
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 2;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Main.Controls.Add(this.PB_Graph, 0, 0);
            this.TLP_Main.Controls.Add(this.PB_Code, 1, 0);
            this.TLP_Main.Controls.Add(this.B_Update, 0, 1);
            this.TLP_Main.Controls.Add(this.B_Save, 1, 1);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 2;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.TLP_Main.Size = new System.Drawing.Size(1268, 731);
            this.TLP_Main.TabIndex = 0;
            // 
            // PB_Graph
            // 
            this.PB_Graph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_Graph.Location = new System.Drawing.Point(3, 3);
            this.PB_Graph.Name = "PB_Graph";
            this.PB_Graph.Size = new System.Drawing.Size(628, 688);
            this.PB_Graph.TabIndex = 0;
            this.PB_Graph.TabStop = false;
            // 
            // PB_Code
            // 
            this.PB_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_Code.Location = new System.Drawing.Point(637, 3);
            this.PB_Code.Name = "PB_Code";
            this.PB_Code.Size = new System.Drawing.Size(628, 688);
            this.PB_Code.TabIndex = 1;
            this.PB_Code.TabStop = false;
            // 
            // B_Update
            // 
            this.B_Update.Dock = System.Windows.Forms.DockStyle.Right;
            this.B_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Update.Location = new System.Drawing.Point(446, 697);
            this.B_Update.Name = "B_Update";
            this.B_Update.Size = new System.Drawing.Size(185, 31);
            this.B_Update.TabIndex = 2;
            this.B_Update.Text = "Update";
            this.B_Update.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            this.B_Save.Dock = System.Windows.Forms.DockStyle.Left;
            this.B_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Save.Location = new System.Drawing.Point(637, 697);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(185, 31);
            this.B_Save.TabIndex = 3;
            this.B_Save.Text = "Save Program Code";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 731);
            this.Controls.Add(this.TLP_Main);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.TLP_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_Graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Code)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_Main;
        private System.Windows.Forms.PictureBox PB_Graph;
        private System.Windows.Forms.PictureBox PB_Code;
        private System.Windows.Forms.Button B_Update;
        private System.Windows.Forms.Button B_Save;
    }
}

