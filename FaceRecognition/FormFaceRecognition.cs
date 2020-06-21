using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using FaceRecognitionDataAccess.Models;
using FaceRecognitionDataAccess.Repositories.Implementations;
using FaceRecognitionDataAccess.Repositories.Interfaces;
using FaceRecognitionDotNet;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognitionWinForm
{
    public partial class FormFaceRecognition : Form
    {
        #region Private Members

        private IPersonPortraitRepository mPersonPortraitRepository;

        private Emgu.CV.CascadeClassifier mCascadeClassifier;
        private FilterInfoCollection mCaptureDevices;
        private VideoCaptureDevice mSelectedCaptureDevice;

        private List<PersonPortrait> mPersonPortraits;
        private int mNextFaceIndex;


        private FaceRecognition mFaceRecognition;
        private SpeechSynthesizer mSpeechSynthesizer;

        private List<PersonSimilarity> mPersonSimilarities;

        private List<Image<Gray, Byte>> mTrainingImages;

        #endregion Private Members

        #region Constructors

        public FormFaceRecognition()
        {
            InitializeComponent();

            mPersonPortraitRepository = new PersonPortraitRepository();

            mCascadeClassifier = new Emgu.CV.CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
            mSpeechSynthesizer = new SpeechSynthesizer();
        }

        #endregion Constructors

        #region Event Handlers

        private void btnStart_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "JPEG|*.jpg|PNG|*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(ofd.FileName);
                    Bitmap picBitmap = new Bitmap(pictureBox1.Image);

                    Rectangle[] rectangles = DetectFaces(picBitmap);

                    foreach (Rectangle rect in rectangles)
                    {
                        using (Graphics graphics = Graphics.FromImage(picBitmap))
                        {
                            using (Pen pen = new Pen(Color.Red, 1))
                            {
                                graphics.DrawRectangle(pen, rect);
                            }
                        }
                    }

                    pictureBox1.Image = picBitmap;

                }
            }
        }

        private void btnOperateCamera_Click(object sender, EventArgs e)
        {
            if (mSelectedCaptureDevice.IsRunning)
            {
                mSelectedCaptureDevice.Stop();
                btnOperateCamera.Text = "Start Camera";

                Bitmap bitmap = new Bitmap(pictureBox1.Image);

                Rectangle[] rectangles = DetectFaces(bitmap);

                foreach (Rectangle rect in rectangles)
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        using (Pen pen = new Pen(Color.Red, 1))
                        {
                            graphics.DrawRectangle(pen, rect);
                        }
                    }
                }

                pictureBox1.Image = bitmap;

                //Take the first detected face
                if (rectangles.Count() > 0)
                {
                    //Bitmap bitmapFace = new Bitmap(pictureFace1.Width, pictureFace1.Height);
                    //Rectangle faceRectangle = new Rectangle(0, 0, pictureFace1.Width, pictureFace1.Height);
                    //using (Graphics graphicsCapturedFace = Graphics.FromImage(bitmapFace))
                    //{
                    //    graphicsCapturedFace.DrawImage(pictureBox1.Image, faceRectangle, rectangles[0], GraphicsUnit.Pixel);
                    //}

                    //pictureFace1.Image = bitmapFace;


                    CopyImage(pictureBox1, pictureFace1, rectangles[0]);

                }
            }
            else
            {
                mSelectedCaptureDevice.Start();
                btnOperateCamera.Text = "Take Picture";
            }

        }

        private void FormFaceRecognition_Load(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            mFaceRecognition = FaceRecognition.Create(curDir);

            mCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in mCaptureDevices)
            {
                comboVideoDevices.Items.Add(filterInfo.Name);
            }
            comboVideoDevices.SelectedIndex = 0;

            mPersonPortraits = mPersonPortraitRepository.Read();
            mPersonSimilarities = new List<PersonSimilarity>();
            foreach (PersonPortrait pp in mPersonPortraits)
            {
                var mat = Cv2.ImDecode(pp.Portrait, ImreadModes.Color);
                var bytes = new byte[mat.Rows * mat.Cols * mat.ElemSize()];
                Marshal.Copy(mat.Data, bytes, 0, bytes.Length);
                FaceRecognitionDotNet.Image imageFace = FaceRecognition.LoadImage(bytes, mat.Rows, mat.Cols, mat.ElemSize());

                IEnumerable<FaceEncoding> encodings = mFaceRecognition.FaceEncodings(imageFace);
                if(encodings.Count() > 0)
                {
                    mPersonSimilarities.Add(new PersonSimilarity { CandidateName = pp.Name, CandidateFaceEncoding = encodings.First() });
                }
            }
        }
            

        private void comboVideoDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectedCaptureDevice = new VideoCaptureDevice(mCaptureDevices[comboVideoDevices.SelectedIndex].MonikerString);
            mSelectedCaptureDevice.NewFrame += SelectedCaptureDevice_NewFrame;
        }

        private void SelectedCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            Bitmap bitmap = new Bitmap((Bitmap)eventArgs.Frame.Clone());
            Rectangle[] rectangles = DetectFaces(bitmap);

            if (rectangles.Count() > 0)
            {
                foreach (Rectangle rect in rectangles)
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        using (Pen pen = new Pen(Color.Red, 1))
                        {
                            graphics.DrawRectangle(pen, rect);
                        }
                    }
                }

                pictureBox1.Image = bitmap;

                //Capture the face
                //Bitmap bitmapFace = new Bitmap(pictureFace1.Width, pictureFace1.Height);
                //Rectangle faceRectangle = new Rectangle(0, 0, pictureFace1.Width, pictureFace1.Height);
                //using (Graphics graphicsCapturedFace = Graphics.FromImage(bitmapFace))
                //{
                //    graphicsCapturedFace.DrawImage(pictureBox1.Image, faceRectangle, rectangles[0], GraphicsUnit.Pixel);
                //}

                //pictureFace1.Image = bitmapFace;

                CopyImage(pictureBox1, pictureFace1, rectangles[0]);


                ////////////TEST///////////
                btnIdentify_Click(null, null);
            }
        }

        private void FormFaceRecognition_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mSelectedCaptureDevice.IsRunning)
            {
                mSelectedCaptureDevice.Stop();
            }

        }

        private void btnCompareFaces_Click(object sender, EventArgs e)
        {
            byte[] bytesFaces1 = ConvertImageToBinary(pictureFace1.Image);
            var mat1 = Cv2.ImDecode(bytesFaces1, ImreadModes.Color);
            var bytes1 = new byte[mat1.Rows * mat1.Cols * mat1.ElemSize()];
            Marshal.Copy(mat1.Data, bytes1, 0, bytes1.Length);
            FaceRecognitionDotNet.Image imageFace1 = FaceRecognition.LoadImage(bytes1, mat1.Rows, mat1.Cols, mat1.ElemSize());


            byte[] bytesFaces2 = ConvertImageToBinary(pictureFace2.Image);
            var mat2 = Cv2.ImDecode(bytesFaces2, ImreadModes.Color);
            var bytes2 = new byte[mat2.Rows * mat2.Cols * mat2.ElemSize()];
            Marshal.Copy(mat2.Data, bytes2, 0, bytes2.Length);
            FaceRecognitionDotNet.Image imageFace2 = FaceRecognition.LoadImage(bytes2, mat2.Rows, mat2.Cols, mat2.ElemSize());

            IEnumerable<FaceEncoding> encodings1 = mFaceRecognition.FaceEncodings(imageFace1);
            IEnumerable<FaceEncoding> encodings2 = mFaceRecognition.FaceEncodings(imageFace2);

            var distance = FaceRecognition.FaceDistance(encodings1.First(), encodings2.First());

            foreach(var enc in encodings1)
            {
                enc.Dispose();
            }
            foreach (var enc in encodings2)
            {
                enc.Dispose();
            }
        }

        private void btnSavePortrait_Click(object sender, EventArgs e)
        {
            PersonPortrait personPortrait = new PersonPortrait
            {
                Name = txtPersonName.Text,
                Portrait = (pictureFace1.Image == null ? null : ConvertImageToBinary(pictureFace1.Image))
            };

            try
            {
                int newId = mPersonPortraitRepository.Save(personPortrait);
                MessageBox.Show("Save Portrait succeeded.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadFaces_Click(object sender, EventArgs e)
        {
            mNextFaceIndex = 0;

            if(mPersonPortraits.Count > 0)
            {
                pictureFace2.Image = ConvertBinaryToImage(mPersonPortraits[mNextFaceIndex].Portrait);
            }
        }

        private void btnNextFace_Click(object sender, EventArgs e)
        {
            mNextFaceIndex++;
            if (mNextFaceIndex >= mPersonPortraits.Count)
            {
                mNextFaceIndex = 0;
            }
            pictureFace2.Image = ConvertBinaryToImage(mPersonPortraits[mNextFaceIndex].Portrait);
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytesFaces1 = ConvertImageToBinary(pictureFace1.Image);
                var mat1 = Cv2.ImDecode(bytesFaces1, ImreadModes.Color);
                var bytes1 = new byte[mat1.Rows * mat1.Cols * mat1.ElemSize()];
                Marshal.Copy(mat1.Data, bytes1, 0, bytes1.Length);
                FaceRecognitionDotNet.Image imageFace1 = FaceRecognition.LoadImage(bytes1, mat1.Rows, mat1.Cols, mat1.ElemSize());
                IEnumerable<FaceEncoding> encodings1 = mFaceRecognition.FaceEncodings(imageFace1);

                //List<PersonSimilarity> personSimilarities = new List<PersonSimilarity>();
                //foreach (PersonPortrait pp in mPersonPortraits)
                //{
                //    var mat2 = Cv2.ImDecode(pp.Portrait, ImreadModes.Color);
                //    var bytes2 = new byte[mat2.Rows * mat2.Cols * mat2.ElemSize()];
                //    Marshal.Copy(mat2.Data, bytes2, 0, bytes2.Length);
                //    FaceRecognitionDotNet.Image imageFace2 = FaceRecognition.LoadImage(bytes2, mat2.Rows, mat2.Cols, mat2.ElemSize());

                //    IEnumerable<FaceEncoding> encodings2 = mFaceRecognition.FaceEncodings(imageFace2);
                //    personSimilarities.Add(new PersonSimilarity { CandidateName = pp.Name, Distance = FaceRecognition.FaceDistance(encodings1.First(), encodings2.First()) });
                //}


                FaceEncoding capturedFaceEncoding = encodings1.First();

                foreach (var ps in mPersonSimilarities)
                {
                    ps.Distance = FaceRecognition.FaceDistance(capturedFaceEncoding, ps.CandidateFaceEncoding);

                }
            }
            catch(Exception ex)
            {
//                MessageBox.Show(ex.Message);

                mPersonSimilarities.ForEach(x => x.Distance = 0);
                return;
            }

            PersonSimilarity closestPerson = mPersonSimilarities.OrderBy(x => x.Distance).FirstOrDefault();
            if (closestPerson != null)
            {
                if (closestPerson.Distance < 0.55)
                {
                    mSpeechSynthesizer.SpeakAsync("Hello, " + closestPerson.CandidateName);
                }
                else
                {
//                    mSpeechSynthesizer.SpeakAsync("Please come closer! ");
                }
            }
        }
        
        #endregion Event Handlers

        #region Helper Functions

        private byte[] ConvertImageToBinary(System.Drawing.Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Bitmap newBitmap = new Bitmap(image);
                newBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }

        private System.Drawing.Image ConvertBinaryToImage(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return System.Drawing.Image.FromStream(memoryStream);
            }
        }

        private Rectangle[] DetectFaces(Bitmap newBitmap)
        {
            Image<Bgr, Byte> grayImage = new Image<Bgr, Byte>(newBitmap);
            Rectangle[] rectangles = mCascadeClassifier.DetectMultiScale(grayImage, 1.4, 0);

            return rectangles;
        }

        private void CopyImage(PictureBox srcPictureBox, PictureBox destPictureBox, Rectangle srcRectangle)
        {
//            Bitmap sourceImage = (Bitmap)(srcPictureBox.Image.Clone());
            using (Bitmap sourceImage = (Bitmap)(srcPictureBox.Image.Clone()))
            {
                Bitmap destBitmap = new Bitmap(pictureFace1.Width, destPictureBox.Height);
                Rectangle destRectangle = new Rectangle(0, 0, destPictureBox.Width, destPictureBox.Height);
                using (Graphics graphics = Graphics.FromImage(destBitmap))
                {
                    graphics.DrawImage(sourceImage, destRectangle, srcRectangle, GraphicsUnit.Pixel);
                }

                destPictureBox.Image = destBitmap;
            }
        }

        #endregion Helper Functions

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Bitmap picBitmap = new Bitmap(pictureBox1.Image);

            Rectangle[] rectangles = DetectFaces(picBitmap);

            foreach(Rectangle rect in rectangles)
            {
                if(rect.Left < e.X && rect.Right > e.X && rect.Top < e.Y && rect.Bottom > e.Y)
                {
                    //Bitmap bitmapFace = new Bitmap(pictureFace1.Width, pictureFace1.Height);
                    //Rectangle faceRectangle = new Rectangle(0, 0, pictureFace1.Width, pictureFace1.Height);
                    //using (Graphics graphicsCapturedFace = Graphics.FromImage(bitmapFace))
                    //{
                    //    graphicsCapturedFace.DrawImage(pictureBox1.Image, faceRectangle, rect, GraphicsUnit.Pixel);
                    //}

                    //pictureFace1.Image = bitmapFace;

                    CopyImage(pictureBox1, pictureFace1, rect);

                    break;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
