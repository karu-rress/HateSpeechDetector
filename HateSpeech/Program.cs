using HateSpeech;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool SetConsoleOutputCP(uint wCodePageID);

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool SetConsoleCP(uint wCodePageID);

SetConsoleCP(949);
SetConsoleOutputCP(949);

void Predict(string comment)
{
    BiasModel.ModelInput biasInput = new() { Comments = comment };
    BiasModel.ModelOutput biasOutput = BiasModel.Predict(biasInput);
    string bias = biasOutput.Prediction;

    GenderBias.ModelInput genderInput = new() { Comments = comment };
    GenderBias.ModelOutput genderOutput = GenderBias.Predict(genderInput);
    bool gender = genderOutput.Prediction;

    HateModel.ModelInput hateInput = new() { Comments = comment };
    HateModel.ModelOutput hateOutput = HateModel.Predict(hateInput);
    string hate = hateOutput.Prediction;

    var hate_speech = false;

    if (gender)
    {
        Console.WriteLine("성차별 표현이 포함되어 있습니다.");
        hate_speech = true;
    }


    if (bias is not "none")
    {
        Console.WriteLine($"{bias} 타입의 차별 표현이 있습니다.");
        hate_speech = true;
    }

    if (hate is not "none")
    {
        Console.WriteLine($"{hate} 타입의 혐오 표현이 있습니다.");
        hate_speech = true;
    }

    if (hate_speech is false)
        Console.WriteLine("혐오 표현이 감지되지 않았습니다.");
}

while (true)
{
    Console.WriteLine("\n혐오 표현을 탐색할 문장을 입력하세요.");
    Console.Write(">> ");
    string? input = Console.ReadLine();

    if (input is "" or null)
        return;

    Predict(input);
}