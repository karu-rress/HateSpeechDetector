using HateSpeech;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool SetConsoleOutputCP(uint wCodePageID);

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool SetConsoleCP(uint wCodePageID);

SetConsoleCP(949);
SetConsoleOutputCP(949);


/* bias / hate
 * others / hate
 * none / none
 * gender / hate
 * none / offensive
 * 
 */

void Predict(string comment)
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

while (true)
{
    Console.WriteLine("\n혐오 표현을 탐색할 문장을 입력하세요.");
    Console.Write(">> ");
    string? input = Console.ReadLine();

    if (input is "" or null)
        return;

    Predict(input);
}