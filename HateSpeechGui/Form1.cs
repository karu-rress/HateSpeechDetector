using HateSpeech;
using System.Text;

namespace HateSpeechGui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("맨 위 박스에 분석하고 싶은 문장을 넣고, '분석' 버튼을 클릭하세요.", "더브레인", MessageBoxButtons.OK);
        }

        private void Predict()
        {
            StringBuilder sb = new();
            HateSpeechClassifier classifier = new();
            classifier.Predict(textBox1.Text);

            sb.AppendLine($"입력된 문장: {textBox1.Text}\n");

            if (classifier.HasGenderBias)
                sb.AppendLine("성차별 표현이 포함되어 있습니다.");

            if (classifier.HasBias)
                sb.AppendLine($"{classifier.BiasType} 타입의 차별 표현이 있습니다.");

            if (classifier.HasHate)
                sb.AppendLine($"{classifier.HateType} 타입의 혐오 표현이 있습니다.");

            if (classifier.IsClean)
                sb.AppendLine("혐오 표현이 감지되지 않았습니다.");

            textBox2.Text += sb.ToString() + "\n";
            textBox2.Text += Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("내용을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Predict();
        }
    }
}