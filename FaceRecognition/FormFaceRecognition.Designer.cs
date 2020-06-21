namespace FaceRecognitionWinForm
{
    partial class FormFaceRecognition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboVideoDevices = new System.Windows.Forms.ComboBox();
            this.btnOperateCamera = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureFace1 = new System.Windows.Forms.PictureBox();
            this.pictureFace2 = new System.Windows.Forms.PictureBox();
            this.btnCompareFaces = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPersonName = new System.Windows.Forms.TextBox();
            this.btnSavePortrait = new System.Windows.Forms.Button();
            this.btnLoadFaces = new System.Windows.Forms.Button();
            this.btnNextFace = new System.Windows.Forms.Button();
            this.btnIdentify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboVideoDevices
            // 
            this.comboVideoDevices.FormattingEnabled = true;
            this.comboVideoDevices.Location = new System.Drawing.Point(498, 845);
            this.comboVideoDevices.Name = "comboVideoDevices";
            this.comboVideoDevices.Size = new System.Drawing.Size(184, 28);
            this.comboVideoDevices.TabIndex = 51;
            this.comboVideoDevices.SelectedIndexChanged += new System.EventHandler(this.comboVideoDevices_SelectedIndexChanged);
            // 
            // btnOperateCamera
            // 
            this.btnOperateCamera.Location = new System.Drawing.Point(291, 840);
            this.btnOperateCamera.Name = "btnOperateCamera";
            this.btnOperateCamera.Size = new System.Drawing.Size(186, 40);
            this.btnOperateCamera.TabIndex = 50;
            this.btnOperateCamera.Text = "Start Camera";
            this.btnOperateCamera.UseVisualStyleBackColor = true;
            this.btnOperateCamera.Click += new System.EventHandler(this.btnOperateCamera_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(40, 845);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 49;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(40, 25);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1119, 811);
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // pictureFace1
            // 
            this.pictureFace1.Location = new System.Drawing.Point(1184, 28);
            this.pictureFace1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureFace1.Name = "pictureFace1";
            this.pictureFace1.Size = new System.Drawing.Size(332, 314);
            this.pictureFace1.TabIndex = 52;
            this.pictureFace1.TabStop = false;
            // 
            // pictureFace2
            // 
            this.pictureFace2.Location = new System.Drawing.Point(1179, 460);
            this.pictureFace2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureFace2.Name = "pictureFace2";
            this.pictureFace2.Size = new System.Drawing.Size(334, 331);
            this.pictureFace2.TabIndex = 53;
            this.pictureFace2.TabStop = false;
            // 
            // btnCompareFaces
            // 
            this.btnCompareFaces.Location = new System.Drawing.Point(826, 845);
            this.btnCompareFaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCompareFaces.Name = "btnCompareFaces";
            this.btnCompareFaces.Size = new System.Drawing.Size(333, 35);
            this.btnCompareFaces.TabIndex = 54;
            this.btnCompareFaces.Text = "Compare Faces";
            this.btnCompareFaces.UseVisualStyleBackColor = true;
            this.btnCompareFaces.Click += new System.EventHandler(this.btnCompareFaces_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(1184, 352);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 20);
            this.lblName.TabIndex = 55;
            this.lblName.Text = "Name";
            // 
            // txtPersonName
            // 
            this.txtPersonName.Location = new System.Drawing.Point(1245, 351);
            this.txtPersonName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPersonName.Name = "txtPersonName";
            this.txtPersonName.Size = new System.Drawing.Size(266, 26);
            this.txtPersonName.TabIndex = 56;
            // 
            // btnSavePortrait
            // 
            this.btnSavePortrait.Location = new System.Drawing.Point(1188, 391);
            this.btnSavePortrait.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSavePortrait.Name = "btnSavePortrait";
            this.btnSavePortrait.Size = new System.Drawing.Size(162, 35);
            this.btnSavePortrait.TabIndex = 57;
            this.btnSavePortrait.Text = "Save Portrait";
            this.btnSavePortrait.UseVisualStyleBackColor = true;
            this.btnSavePortrait.Click += new System.EventHandler(this.btnSavePortrait_Click);
            // 
            // btnLoadFaces
            // 
            this.btnLoadFaces.Location = new System.Drawing.Point(1179, 798);
            this.btnLoadFaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadFaces.Name = "btnLoadFaces";
            this.btnLoadFaces.Size = new System.Drawing.Size(112, 35);
            this.btnLoadFaces.TabIndex = 58;
            this.btnLoadFaces.Text = "Load Faces";
            this.btnLoadFaces.UseVisualStyleBackColor = true;
            this.btnLoadFaces.Click += new System.EventHandler(this.btnLoadFaces_Click);
            // 
            // btnNextFace
            // 
            this.btnNextFace.Location = new System.Drawing.Point(1401, 798);
            this.btnNextFace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNextFace.Name = "btnNextFace";
            this.btnNextFace.Size = new System.Drawing.Size(112, 35);
            this.btnNextFace.TabIndex = 59;
            this.btnNextFace.Text = "Next Face";
            this.btnNextFace.UseVisualStyleBackColor = true;
            this.btnNextFace.Click += new System.EventHandler(this.btnNextFace_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.Location = new System.Drawing.Point(1359, 391);
            this.btnIdentify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(154, 35);
            this.btnIdentify.TabIndex = 60;
            this.btnIdentify.Text = "Who am I?";
            this.btnIdentify.UseVisualStyleBackColor = true;
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // FormFaceRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 928);
            this.Controls.Add(this.btnIdentify);
            this.Controls.Add(this.btnNextFace);
            this.Controls.Add(this.btnLoadFaces);
            this.Controls.Add(this.btnSavePortrait);
            this.Controls.Add(this.txtPersonName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCompareFaces);
            this.Controls.Add(this.pictureFace2);
            this.Controls.Add(this.pictureFace1);
            this.Controls.Add(this.comboVideoDevices);
            this.Controls.Add(this.btnOperateCamera);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormFaceRecognition";
            this.Text = "Face Recognition";
            this.Load += new System.EventHandler(this.FormFaceRecognition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboVideoDevices;
        private System.Windows.Forms.Button btnOperateCamera;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureFace1;
        private System.Windows.Forms.PictureBox pictureFace2;
        private System.Windows.Forms.Button btnCompareFaces;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtPersonName;
        private System.Windows.Forms.Button btnSavePortrait;
        private System.Windows.Forms.Button btnLoadFaces;
        private System.Windows.Forms.Button btnNextFace;
        private System.Windows.Forms.Button btnIdentify;
    }
}

