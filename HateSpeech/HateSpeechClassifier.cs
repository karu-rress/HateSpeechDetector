using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HateSpeech;

public enum Bias
{
    [Description("Gender Bias")]
    Gender,
    [Description("Other Bias")]
    Others,
    [Description("None")]
    None,
}

public enum Hate
{
    [Description("Hate Speech")]
    Hate,
    [Description("Offensive")]
    Offensive,
    [Description("None")]
    None
}

public class HateSpeechClassifier
{
    public string? Comment { get; set; }

    private GenderBias.ModelInput genderInput;
    public GenderBias.ModelOutput genderOutput { get;set;}

    private BiasModel.ModelInput biasInput;
    public BiasModel.ModelOutput biasOutput;

    private HateModel.ModelInput hateInput;
    public HateModel.ModelOutput hateOutput;

    public bool HasGenderBias { get; private set; }
    public Bias BiasType { get; private set; }
    public bool HasBias => BiasType is not Bias.None;
    public Hate HateType { get; private set; }
    public bool HasHate => HateType is not Hate.None;
    public bool IsClean => !HasBias && !HasHate;

    public void Predict(string comment)
    {
        if (string.IsNullOrEmpty(comment))
            return;

        Comment = comment;
        
        genderInput = new() { Comments = Comment };
        biasInput = new() { Comments = Comment };
        hateInput = new() { Comments = Comment };

        genderOutput = GenderBias.Predict(genderInput);
        biasOutput = BiasModel.Predict(biasInput);
        hateOutput = HateModel.Predict(hateInput);

        HasGenderBias = genderOutput.Prediction;
        BiasType = biasOutput.Prediction switch
        {
            "gender" => Bias.Gender,
            "others" => Bias.Others,
            "none" => Bias.None,
            _ => throw new Exception()
        };
        HateType = hateOutput.Prediction switch
        {
            "hate" => Hate.Hate,
            "offensive" => Hate.Offensive,
            "none" => Hate.None,
            _ => throw new Exception()
        };
    }
}
