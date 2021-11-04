using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace 一维码识别
{ 
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        HObject ho_image, ho_Region;
        HTuple hv_WindowHandle = null, hv_BarCodeHandle = null;
        HTuple hv_DecodedDataStrings = null, hv_Row1 = null, hv_Column1 = null;
        HTuple hv_Row2 = null, hv_Column2 = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string Imagepath;
            //图象选取
            openFileDialog1.Filter = "JPEG文件|*.jpg*|BMP文件|*.bmp*|PNG文件|*.png*";
            openFileDialog1.RestoreDirectory = true;
            HOperatorSet.GenEmptyObj(out ho_image);
            HOperatorSet.GenEmptyObj(out ho_Region);
            ho_image.Dispose();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Imagepath = openFileDialog1.FileName;
                HOperatorSet.ReadImage(out ho_image, Imagepath);

            }
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            HOperatorSet.OpenWindow(0, 0, hWindowControl1.Width,hWindowControl1.Height,hWindowControl1.HalconWindow, "visible", "", out hv_WindowHandle);
            HDevWindowStack.Push(hv_WindowHandle);
            //创建条码模型
            HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out hv_BarCodeHandle);
            ho_Region.Dispose();
            HOperatorSet.FindBarCode(ho_image, out ho_Region, hv_BarCodeHandle,"auto", out hv_DecodedDataStrings);
            HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row1, out hv_Column1,out hv_Row2, out hv_Column2);
            //显示
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetLineWidth(HDevWindowStack.GetActive(), 3);
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_image, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Region, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_DecodedDataStrings, "image",hv_Row2, hv_Column1, "black", new HTuple(), new HTuple());
            }
            //清除模型
            HOperatorSet.ClearBarCodeModel(hv_BarCodeHandle);
            ho_image.Dispose();
            ho_Region.Dispose();

        }

    }
}
