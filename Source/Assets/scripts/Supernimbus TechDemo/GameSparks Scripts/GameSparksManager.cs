using GameSparks.Core;
using System.Collections.Generic;
using UnityEngine;

/*Class is written to handle any communication between client and gamesparks backend. Extends 
 a singleton class to ensure a single instance and grant global access point for getting the 
 player score.*/
public class GameSparksManager : Singleton<GameSparksManager>
{
    
    string userName;
    int score = 10;
    int previousScore;

  
    /*Create an API request to gamesparks to trigger the lootdrop event and request 1 data entry
    The data is then parsed to retrieve the short code of the item and display that as a string
    to the user, then subtract 10 from the score. The user can pull there differe items based on a percent drop.*/
    public void RetrieveFromLootTable()
    {
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("lootDrop")
            .SetEventAttribute("amount", 1)
            .Send((response) =>
            {
                GSData scriptData = response.ScriptData;

                List <GSData> objList = scriptData.GetGSDataList("lootDrop");
                Debug.Log(objList);
                string shortCode = objList[0].GetString("shortCode");

                if (response.HasErrors)
                {
                    Debug.Log(response.ScriptData.JSON.ToString());
                }
                else
                {
                    StartCoroutine(ScoreUI.Instance.LootText(shortCode));
                    Debug.Log("You've been awarded " + shortCode);
                    //SavePlayerItem(shortCode);
                }
            });

        score -= 10;
        Debug.Log("Your new score is now " + score);

    }

    /*Create an API request to gamesparks to post the user score to a leaderboard, is called 
     at the end of the game*/
    public void PostToLeaderboard()
    {
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("ScoreEvent")
            .SetEventAttribute("Score", score)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log(response.Errors.JSON.ToString());
                }
                else
                {

                    Debug.Log("Post to leaderboard with a score of " + score);
                }
            });
    }

    /*Create an API request to gamesparks to trigger the SAVE_PLAYER event which stores the user score 
     as player experience on gamesparks.*/
    public void SavePlayer()
    {
        int ScoreToBeSaved = score + previousScore;
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("SAVE_PLAYER")
            .SetEventAttribute("XP", ScoreToBeSaved)
            .Send((response) =>
        {
            if (!response.HasErrors)
            {
                Debug.Log("Player Saved To GameSparks with a score of " + ScoreToBeSaved);
            }
            else
            {
                Debug.Log("Error Saving Player Data...");
            }
        });
    }

    /*Create an API request to gamesparks to trigger the LOAD_PLAYER event which stores the users
      current score in the previousScore variable. Is called at the start of the level*/
    public void LoadPlayerData()
    {
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("LOAD_PLAYER")
            
            .Send((response) =>
        {
            if (!response.HasErrors)
            {
                GSData scriptData = response.ScriptData;

                GSData obj = scriptData.GetGSData("Player_Data");

                previousScore = (int)obj.GetInt("playerXP");

                Debug.Log("Player loaded from GameSparks with a score of  - " + previousScore);

            }
            else
            {
                Debug.Log("Error Loading Player Data...");
            }
        });
    }

    /*Publically available getter used to retrieve the player score*/
    public int GetPlayerScore()
    {
        return this.score;
    }

    /*Publically available setter used to set the player score*/
    public void SetPlayerScore(int _score)
    {
        this.score = _score;
        ScoreUI.Instance.UpdateUI(this.score);
        Debug.Log("Score updated from AWS Lambda. Current score is " + this.score);
    }

}
