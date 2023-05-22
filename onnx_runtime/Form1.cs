using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onnx_runtime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var yolo = new Yolo("E:/vinh_C#/onnx_runtime/test/model_roller_back.onnx");

            // setup labels of onnx model 
            yolo.SetupYoloDefaultLabels();

            Image image = Image.FromFile("E:/vinh_C#/onnx_runtime/test/check_09gio_40phut_25giay.jpg");
            var predictions = yolo.Predict(image);

            //Image image1 = Image.FromFile("E:/vinh_C#/zidane.jpg");

            // draw box
            using var graphics = Graphics.FromImage(image);
            foreach (var prediction in predictions) // iterate predictions to draw results
            {
                if (prediction.Score > 0.5)
                {
                    double score = Math.Round(prediction.Score, 2);
                    graphics.DrawRectangles(new Pen(prediction.Label.Color, 1), new[] { prediction.Rectangle });
                    var (x, y) = (prediction.Rectangle.X - 3, prediction.Rectangle.Y - 23);
                    graphics.DrawString($"{prediction.Label.Name} ({score})",
                                    new Font("Consolas", 16, GraphicsUnit.Pixel), new SolidBrush(prediction.Label.Color),
                                    new PointF(x, y));
                }
            }

            pictureBox.Image = image;

        }

    }
}
