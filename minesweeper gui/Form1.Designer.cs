
namespace minesweeper_gui
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.heightTB = new System.Windows.Forms.TextBox();
            this.widthTB = new System.Windows.Forms.TextBox();
            this.heightLbl = new System.Windows.Forms.Label();
            this.widthLbl = new System.Windows.Forms.Label();
            this.bombLbl = new System.Windows.Forms.Label();
            this.bombTB = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.warnLbl = new System.Windows.Forms.Label();
            this.endLbl = new System.Windows.Forms.Label();
            this.restartBtn = new System.Windows.Forms.Button();
            this.bmbCountLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // heightTB
            // 
            this.heightTB.Location = new System.Drawing.Point(12, 27);
            this.heightTB.Name = "heightTB";
            this.heightTB.Size = new System.Drawing.Size(100, 23);
            this.heightTB.TabIndex = 0;
            this.heightTB.Text = "30";
            // 
            // widthTB
            // 
            this.widthTB.Location = new System.Drawing.Point(12, 71);
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(100, 23);
            this.widthTB.TabIndex = 1;
            this.widthTB.Text = "30";
            // 
            // heightLbl
            // 
            this.heightLbl.AutoSize = true;
            this.heightLbl.Location = new System.Drawing.Point(12, 9);
            this.heightLbl.Name = "heightLbl";
            this.heightLbl.Size = new System.Drawing.Size(41, 15);
            this.heightLbl.TabIndex = 2;
            this.heightLbl.Text = "height";
            // 
            // widthLbl
            // 
            this.widthLbl.AutoSize = true;
            this.widthLbl.Location = new System.Drawing.Point(12, 53);
            this.widthLbl.Name = "widthLbl";
            this.widthLbl.Size = new System.Drawing.Size(37, 15);
            this.widthLbl.TabIndex = 3;
            this.widthLbl.Text = "width";
            // 
            // bombLbl
            // 
            this.bombLbl.AutoSize = true;
            this.bombLbl.Location = new System.Drawing.Point(12, 97);
            this.bombLbl.Name = "bombLbl";
            this.bombLbl.Size = new System.Drawing.Size(103, 15);
            this.bombLbl.TabIndex = 4;
            this.bombLbl.Text = "number of bombs";
            // 
            // bombTB
            // 
            this.bombTB.Location = new System.Drawing.Point(12, 115);
            this.bombTB.Name = "bombTB";
            this.bombTB.Size = new System.Drawing.Size(100, 23);
            this.bombTB.TabIndex = 5;
            this.bombTB.Text = "100";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 144);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 6;
            this.startBtn.Text = "start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // warnLbl
            // 
            this.warnLbl.AutoSize = true;
            this.warnLbl.ForeColor = System.Drawing.Color.Red;
            this.warnLbl.Location = new System.Drawing.Point(93, 148);
            this.warnLbl.Name = "warnLbl";
            this.warnLbl.Size = new System.Drawing.Size(146, 15);
            this.warnLbl.TabIndex = 7;
            this.warnLbl.Text = "please enter correct values";
            this.warnLbl.Click += new System.EventHandler(this.label1_Click);
            // 
            // endLbl
            // 
            this.endLbl.AutoSize = true;
            this.endLbl.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.endLbl.Location = new System.Drawing.Point(333, 339);
            this.endLbl.Name = "endLbl";
            this.endLbl.Size = new System.Drawing.Size(62, 37);
            this.endLbl.TabIndex = 8;
            this.endLbl.Text = "end";
            // 
            // restartBtn
            // 
            this.restartBtn.Location = new System.Drawing.Point(333, 379);
            this.restartBtn.Name = "restartBtn";
            this.restartBtn.Size = new System.Drawing.Size(75, 23);
            this.restartBtn.TabIndex = 9;
            this.restartBtn.Text = "restart";
            this.restartBtn.UseVisualStyleBackColor = true;
            this.restartBtn.Visible = false;
            // 
            // bmbCountLbl
            // 
            this.bmbCountLbl.AutoSize = true;
            this.bmbCountLbl.Location = new System.Drawing.Point(93, 9);
            this.bmbCountLbl.Name = "bmbCountLbl";
            this.bmbCountLbl.Size = new System.Drawing.Size(104, 15);
            this.bmbCountLbl.TabIndex = 10;
            this.bmbCountLbl.Text = "bombs remaining:";
            this.bmbCountLbl.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 744);
            this.Controls.Add(this.bmbCountLbl);
            this.Controls.Add(this.restartBtn);
            this.Controls.Add(this.endLbl);
            this.Controls.Add(this.warnLbl);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.bombTB);
            this.Controls.Add(this.bombLbl);
            this.Controls.Add(this.widthLbl);
            this.Controls.Add(this.heightLbl);
            this.Controls.Add(this.widthTB);
            this.Controls.Add(this.heightTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox heightTB;
        private System.Windows.Forms.TextBox widthTB;
        private System.Windows.Forms.Label heightLbl;
        private System.Windows.Forms.Label widthLbl;
        private System.Windows.Forms.Label bombLbl;
        private System.Windows.Forms.TextBox bombTB;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label warnLbl;
        private System.Windows.Forms.Label endLbl;
        private System.Windows.Forms.Button restartBtn;
        private System.Windows.Forms.Label bmbCountLbl;
    }
}

