namespace Spider1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.txt_href = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_a = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_depth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_dqUrl = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Url：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Content：";
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(114, 37);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(429, 21);
            this.txt_url.TabIndex = 2;
            // 
            // txt_href
            // 
            this.txt_href.Location = new System.Drawing.Point(113, 87);
            this.txt_href.Multiline = true;
            this.txt_href.Name = "txt_href";
            this.txt_href.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_href.Size = new System.Drawing.Size(550, 143);
            this.txt_href.TabIndex = 3;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(588, 35);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "抓取数据";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "状态：";
            // 
            // txt_a
            // 
            this.txt_a.Location = new System.Drawing.Point(113, 290);
            this.txt_a.Multiline = true;
            this.txt_a.Name = "txt_a";
            this.txt_a.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_a.Size = new System.Drawing.Size(131, 26);
            this.txt_a.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "深度：";
            // 
            // txt_depth
            // 
            this.txt_depth.Location = new System.Drawing.Point(114, 243);
            this.txt_depth.Name = "txt_depth";
            this.txt_depth.Size = new System.Drawing.Size(100, 21);
            this.txt_depth.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "当前Url：";
            // 
            // txt_dqUrl
            // 
            this.txt_dqUrl.Location = new System.Drawing.Point(113, 343);
            this.txt_dqUrl.Name = "txt_dqUrl";
            this.txt_dqUrl.Size = new System.Drawing.Size(550, 21);
            this.txt_dqUrl.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 386);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(328, 455);
            this.textBox1.TabIndex = 11;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(387, 386);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(490, 455);
            this.textBox2.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 749);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txt_dqUrl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_depth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_a);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.txt_href);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Spider";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.TextBox txt_href;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_a;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_depth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_dqUrl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

