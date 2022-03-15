namespace STTM_PJ01
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_run = new System.Windows.Forms.Button();
            this.tb_exposedRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_infectedRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_e2i_rate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_i2r_rate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_realworld = new System.Windows.Forms.CheckBox();
            this.checkBox_synthetic = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_pop_flow_data = new System.Windows.Forms.TextBox();
            this.tb_pop_structure_data = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_dailyResult = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_statisticResult = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(1016, 506);
            this.btn_run.Margin = new System.Windows.Forms.Padding(4);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(100, 29);
            this.btn_run.TabIndex = 0;
            this.btn_run.Text = "Run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_exposedRate
            // 
            this.tb_exposedRate.Location = new System.Drawing.Point(276, 31);
            this.tb_exposedRate.Margin = new System.Windows.Forms.Padding(4);
            this.tb_exposedRate.Name = "tb_exposedRate";
            this.tb_exposedRate.Size = new System.Drawing.Size(437, 25);
            this.tb_exposedRate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Infection rate of the exposed:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tb_infectedRate
            // 
            this.tb_infectedRate.Location = new System.Drawing.Point(276, 65);
            this.tb_infectedRate.Margin = new System.Windows.Forms.Padding(4);
            this.tb_infectedRate.Name = "tb_infectedRate";
            this.tb_infectedRate.Size = new System.Drawing.Size(437, 25);
            this.tb_infectedRate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Infection rate of the infected";
            // 
            // tb_e2i_rate
            // 
            this.tb_e2i_rate.Location = new System.Drawing.Point(476, 99);
            this.tb_e2i_rate.Margin = new System.Windows.Forms.Padding(4);
            this.tb_e2i_rate.Name = "tb_e2i_rate";
            this.tb_e2i_rate.Size = new System.Drawing.Size(237, 25);
            this.tb_e2i_rate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(431, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Transformation rate from the exposed to the infected:";
            // 
            // tb_i2r_rate
            // 
            this.tb_i2r_rate.Location = new System.Drawing.Point(476, 132);
            this.tb_i2r_rate.Margin = new System.Windows.Forms.Padding(4);
            this.tb_i2r_rate.Name = "tb_i2r_rate";
            this.tb_i2r_rate.Size = new System.Drawing.Size(237, 25);
            this.tb_i2r_rate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(431, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Transformation rate from the infected to the removed:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_realworld);
            this.groupBox1.Controls.Add(this.checkBox_synthetic);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_pop_flow_data);
            this.groupBox1.Controls.Add(this.tb_pop_structure_data);
            this.groupBox1.Location = new System.Drawing.Point(19, 66);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1124, 129);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input dataset";
            // 
            // checkBox_realworld
            // 
            this.checkBox_realworld.AutoSize = true;
            this.checkBox_realworld.Location = new System.Drawing.Point(476, 101);
            this.checkBox_realworld.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_realworld.Name = "checkBox_realworld";
            this.checkBox_realworld.Size = new System.Drawing.Size(173, 19);
            this.checkBox_realworld.TabIndex = 3;
            this.checkBox_realworld.Text = "Real-world dataset";
            this.checkBox_realworld.UseVisualStyleBackColor = true;
            this.checkBox_realworld.CheckedChanged += new System.EventHandler(this.checkBox_realworld_CheckedChanged);
            // 
            // checkBox_synthetic
            // 
            this.checkBox_synthetic.AutoSize = true;
            this.checkBox_synthetic.Location = new System.Drawing.Point(276, 101);
            this.checkBox_synthetic.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_synthetic.Name = "checkBox_synthetic";
            this.checkBox_synthetic.Size = new System.Drawing.Size(165, 19);
            this.checkBox_synthetic.TabIndex = 3;
            this.checkBox_synthetic.Text = "Synthetic dataset";
            this.checkBox_synthetic.UseVisualStyleBackColor = true;
            this.checkBox_synthetic.CheckedChanged += new System.EventHandler(this.checkBox_synthetic_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(223, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Population OD flow dataset:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Population structure dataset:";
            // 
            // tb_pop_flow_data
            // 
            this.tb_pop_flow_data.Location = new System.Drawing.Point(264, 64);
            this.tb_pop_flow_data.Margin = new System.Windows.Forms.Padding(4);
            this.tb_pop_flow_data.Name = "tb_pop_flow_data";
            this.tb_pop_flow_data.Size = new System.Drawing.Size(852, 25);
            this.tb_pop_flow_data.TabIndex = 1;
            // 
            // tb_pop_structure_data
            // 
            this.tb_pop_structure_data.Location = new System.Drawing.Point(264, 30);
            this.tb_pop_structure_data.Margin = new System.Windows.Forms.Padding(4);
            this.tb_pop_structure_data.Name = "tb_pop_structure_data";
            this.tb_pop_structure_data.Size = new System.Drawing.Size(852, 25);
            this.tb_pop_structure_data.TabIndex = 1;
            this.tb_pop_structure_data.TextChanged += new System.EventHandler(this.tb_pop_structure_data_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(289, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(600, 32);
            this.label5.TabIndex = 4;
            this.label5.Text = "SpatioTemporal Transmission Model(STTM) ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tb_exposedRate);
            this.groupBox2.Controls.Add(this.tb_infectedRate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_e2i_rate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_i2r_rate);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(19, 202);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(736, 175);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input parameters";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tb_dailyResult);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tb_statisticResult);
            this.groupBox3.Location = new System.Drawing.Point(19, 386);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1124, 101);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output result";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 70);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Statistic result:";
            // 
            // tb_dailyResult
            // 
            this.tb_dailyResult.Location = new System.Drawing.Point(167, 25);
            this.tb_dailyResult.Margin = new System.Windows.Forms.Padding(4);
            this.tb_dailyResult.Name = "tb_dailyResult";
            this.tb_dailyResult.Size = new System.Drawing.Size(949, 25);
            this.tb_dailyResult.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Daily result:";
            // 
            // tb_statisticResult
            // 
            this.tb_statisticResult.Location = new System.Drawing.Point(167, 59);
            this.tb_statisticResult.Margin = new System.Windows.Forms.Padding(4);
            this.tb_statisticResult.Name = "tb_statisticResult";
            this.tb_statisticResult.Size = new System.Drawing.Size(949, 25);
            this.tb_statisticResult.TabIndex = 1;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(864, 506);
            this.btn_close.Margin = new System.Windows.Forms.Padding(4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(100, 29);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_Click_close);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1182, 564);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_run);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1200, 611);
            this.MinimumSize = new System.Drawing.Size(786, 611);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpatioTemporal Transmission Model(STTM) ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.TextBox tb_exposedRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_infectedRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_e2i_rate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_i2r_rate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_pop_flow_data;
        private System.Windows.Forms.TextBox tb_pop_structure_data;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_dailyResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_statisticResult;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.CheckBox checkBox_realworld;
        private System.Windows.Forms.CheckBox checkBox_synthetic;
    }
}

