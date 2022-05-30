using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HateSpeech;

namespace HateSpeechWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show()
        }

        private void Predict(string comment)
        {
            HateSpeechClassifier classifier = new();
            classifier.Predict(comment);

            if (classifier.HasGenderBias)
                Console.WriteLine("성차별 표현이 포함되어 있습니다.");

            if (classifier.HasBias)
                Console.WriteLine($"{classifier.BiasType} 타입의 차별 표현이 있습니다.");

            if (classifier.HasHate)
                Console.WriteLine($"{classifier.HateType} 타입의 혐오 표현이 있습니다.");

            if (classifier.IsClean)
                Console.WriteLine("혐오 표현이 감지되지 않았습니다.");
        }
    }
}
