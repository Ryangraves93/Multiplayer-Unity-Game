using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreUI : Singleton<ScoreUI>
{

    TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI HUDText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        scoreText.text = "Current score - " + GameSparksManager.Instance.GetPlayerScore().ToString();
        StartCoroutine(InstructionsText());
    }

    public void UpdateUI(int score)
    {
        scoreText.text = "Current score - " + score.ToString();
    }

    public IEnumerator InstructionsText()
    {
        HUDText.text = "1. Use the red switches on the floor to open the doors\n" +
            "2. Press E to pick up barrels and place them on switches\n" +
            "3. Press E in-front of chest to spend score on items";
        yield return new WaitForSeconds(8f);
        HUDText.text = "";
    }

    public IEnumerator LootText(string lootString)
    {
        HUDText.text = "You've looted " + lootString;
        yield return new WaitForSeconds(3f);
        HUDText.text = "";
    }


}
