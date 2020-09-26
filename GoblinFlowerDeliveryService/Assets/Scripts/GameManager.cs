using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    // Quest alerts
    public Canvas QuestAlertCanvas;
    public TextMeshProUGUI RiddleComponent;
    public TextMeshProUGUI AddressComponent;
    public TextMeshProUGUI NameComponent;

    public Canvas InstructionCanvas;

    public GameObject FlowerAttachPoint;

    public enum Materials
    {
        FlyAgaric,
        Tulip,
        Buttercup,
        MorningGlory,
        Morel,
        Enoki,
        Cactus,
        Daisy
    }

    private string[] _goblinNames = new string []
    {
        "Krusk",
        "Blifee",
        "Wrunk"
    };

    private int _goblinIndex = 0;

    public List<Materials> _correctArrangement;

    public int Score = 0;
    public string GoblinName = "";

    public void ScoreArrangement()
    {
        var score = 0;
        var uniqueMaterials = new HashSet<Materials>();
        foreach(var ingredient in FlowerAttachPoint.GetComponentsInChildren<Ingredient>())
            uniqueMaterials.Add(ingredient.material);
        var ingredients = new StringBuilder();
        foreach(var ingredient in uniqueMaterials)
        {
            ingredients.Append(ingredient.ToString());
            if (_correctArrangement.Contains(ingredient)) score++;
            else score--;
        }
        Score = score;
        Debug.Log(ingredients);
        Debug.Log(score);
    }

    public void CloseInstructions()
    {
        InstructionCanvas.enabled = false;
    }

    public void NewQuest()
    {
        ActivateNewQuest();
    }
    public void ClickMailbox()
    {
        QuestAlertCanvas.enabled = true;
    }

    public void CloseQuestWindow()
    {
        QuestAlertCanvas.enabled = false;
    }

    // Quest alert helpers
    private void ActivateNewQuest()
    {
        AddressComponent.SetText(GenerateAddress());
        RiddleComponent.SetText(GenerateRiddle());
        NameComponent.SetText(GenerateName());
    }

    private string GenerateName()
    {
        var index = new System.Random().Next(0, _goblinNames.Length);
        _goblinIndex = index;
        GoblinName = _goblinNames[index];
        return "Name: " + _goblinNames[index];
    }

    private string GenerateRiddle()
    {
        _correctArrangement = new List<Materials>();

        var answers = new Materials[]
        {
            Materials.FlyAgaric,
            Materials.Tulip,
            Materials.Buttercup,
            Materials.MorningGlory,
            Materials.Morel,
            Materials.Enoki,
            Materials.Cactus,
            Materials.Daisy
        };

        var clues = new string[] {
            "-Surprisingly grounded, mottled, and rounded",
            "-The bulb doesn't glow but from it this grows",
            "-Yellow blooming weeds fit for a princess",
            "-Best before night, may bloom at first light",
            "-A winsome ridged treat you'll be tempted to eat",
            "-Worthy of a pallid ballad, soup, or salad",
            "-A prickly pear for the adventurous one",
            "-Looks sweet upon the seat of a bicycle built for two" };

        var riddle = new StringBuilder("Your arrangment must include ingredients matching these descriptions\n\n");
        var rand = new System.Random();
        var used = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int index = -1;
            do
            {
                index = rand.Next(0, clues.Length);
            } while (used.Contains(index));
            used.Add(index);
            riddle.Append(clues[index] + "\n");
            _correctArrangement.Add(answers[index]);
        }

        var answerString = new StringBuilder("Answer: ");
        foreach(var i in used)
        {
            answerString.Append(answers[i].ToString() + ", ");
        }
        Debug.Log(answerString.ToString());

        return riddle.ToString();
    }

    private string GenerateAddress()
    {
        var addresses = new string[] { "1020", "1021", "1022", "4041", "4042", "4043", "4044" };
        var index = new System.Random().Next(0, addresses.Length);
        return "Address: " + addresses[index];
    }

}
