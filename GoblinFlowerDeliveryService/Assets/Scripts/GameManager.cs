using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class GameManager : MonoBehaviour
{
    // Quest alerts
    public Canvas QuestAlertCanvas;
    public TextMeshProUGUI RiddleComponent;
    public TextMeshProUGUI AddressComponent;

    public GameObject FlowerAttachPoint;

    public enum Materials
    {
        FlyAgaric,
        Tulip,
        Buttercup,
        MorningGlory,
        Morel,
        Enoki,
        Cactus
    }

    public List<Materials> _correctArrangement;

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
        Debug.Log(ingredients);
        Debug.Log(score);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickMailbox()
    {
        ActivateNewQuest();
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
            Materials.Cactus
        };

        var clues = new string[] {
            "surprisingly grounded, mottled, and rounded",
            "the bulb doesn't glow but from it this grows",
            "yellow blooming weeds fit for a princess",
            "best before night, may bloom at first light(this is terrible i know)",
            "a winsome ridged treat you'll be tempted to eat",
            "worthy of a pallid ballad, soup, or salad",
            "a prickly pear for the adventurous one" };

        var riddle = new StringBuilder("Your arrangment must include ingredients matching these descriptions\n");
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
        return addresses[index];
    }

}
