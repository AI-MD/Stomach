
namespace Stomach
{
    partial class ImageDisplay
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pBimage = new System.Windows.Forms.PictureBox();
            this.iTag = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.Label();
            this.time_info = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pBimage)).BeginInit();
            this.SuspendLayout();
            // 
            // pBimage
            // 
            this.pBimage.BackColor = System.Drawing.SystemColors.Control;
            this.pBimage.Location = new System.Drawing.Point(4, 4);
            this.pBimage.Margin = new System.Windows.Forms.Padding(4);
            this.pBimage.Name = "pBimage";
            this.pBimage.Size = new System.Drawing.Size(264, 213);
            this.pBimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBimage.TabIndex = 0;
            this.pBimage.TabStop = false;
            this.pBimage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pBimage_MouseClick);
            // 
            // iTag
            // 
            this.iTag.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.iTag.AutoSize = true;
            this.iTag.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.iTag.Location = new System.Drawing.Point(106, 254);
            this.iTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.iTag.Name = "iTag";
            this.iTag.Size = new System.Drawing.Size(46, 22);
            this.iTag.TabIndex = 1;
            this.iTag.Text = "Tag";
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.Location = new System.Drawing.Point(190, 246);
            this.fileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(76, 18);
            this.fileName.TabIndex = 2;
            this.fileName.Text = "fileName";
            this.fileName.Visible = false;
            // 
            // time_info
            // 
            this.time_info.AutoSize = true;
            this.time_info.Location = new System.Drawing.Point(19, 246);
            this.time_info.Name = "time_info";
            this.time_info.Size = new System.Drawing.Size(54, 18);
            this.time_info.TabIndex = 3;
            this.time_info.Text = "label1";
            this.time_info.Visible = false;
            // 
            // ImageDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.time_info);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.iTag);
            this.Controls.Add(this.pBimage);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ImageDisplay";
            this.Size = new System.Drawing.Size(292, 278);
            ((System.ComponentModel.ISupportInitialize)(this.pBimage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pBimage;
        public System.Windows.Forms.Label iTag;
        public System.Windows.Forms.Label fileName;
        public System.Windows.Forms.Label time_info;
    }
}
