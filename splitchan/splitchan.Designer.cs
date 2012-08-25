namespace splitchan {
    partial class splitchan {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tag_name = new System.Windows.Forms.TextBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.sound_drop = new System.Windows.Forms.Button();
            this.img_drop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tag_name
            // 
            this.tag_name.Location = new System.Drawing.Point(12, 133);
            this.tag_name.Name = "tag_name";
            this.tag_name.Size = new System.Drawing.Size(100, 20);
            this.tag_name.TabIndex = 3;
            this.tag_name.Text = "Tag";
            this.tag_name.Enter += new System.EventHandler(this.tag_name_Enter);
            this.tag_name.Leave += new System.EventHandler(this.tag_name_Leave);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(118, 131);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(100, 23);
            this.save_btn.TabIndex = 4;
            this.save_btn.Text = "Save";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // sound_drop
            // 
            this.sound_drop.AllowDrop = true;
            this.sound_drop.Location = new System.Drawing.Point(12, 25);
            this.sound_drop.Name = "sound_drop";
            this.sound_drop.Size = new System.Drawing.Size(100, 100);
            this.sound_drop.TabIndex = 1;
            this.sound_drop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.sound_drop.UseVisualStyleBackColor = true;
            this.sound_drop.Click += new System.EventHandler(this.sound_drop_Click);
            this.sound_drop.DragDrop += new System.Windows.Forms.DragEventHandler(this.sound_drop_DragDrop);
            this.sound_drop.DragOver += new System.Windows.Forms.DragEventHandler(this.sound_drop_DragOver);
            // 
            // img_drop
            // 
            this.img_drop.AllowDrop = true;
            this.img_drop.Location = new System.Drawing.Point(118, 25);
            this.img_drop.Name = "img_drop";
            this.img_drop.Size = new System.Drawing.Size(100, 100);
            this.img_drop.TabIndex = 2;
            this.img_drop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.img_drop.UseVisualStyleBackColor = true;
            this.img_drop.Click += new System.EventHandler(this.img_drop_Click);
            this.img_drop.DragDrop += new System.Windows.Forms.DragEventHandler(this.img_drop_DragDrop);
            this.img_drop.DragOver += new System.Windows.Forms.DragEventHandler(this.img_drop_DragOver);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Drag & Drop works too";
            this.label1.UseMnemonic = false;
            // 
            // splitchan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 162);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.img_drop);
            this.Controls.Add(this.sound_drop);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.tag_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "splitchan";
            this.Text = "SplitChan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tag_name;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button sound_drop;
        private System.Windows.Forms.Button img_drop;
        private System.Windows.Forms.Label label1;
    }
}

