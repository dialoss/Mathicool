using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour
{
    public void loadLevel(string type)
    {
        if (type == "train")
        {
            SceneManager.LoadScene("Book");
            return;
        }
        Variables.book = false;
        Variables.gameType = type;
        Variables.prevScene = "Select Level";
        Variables.currentBackground = Random.Range(0, 3);
        Variables.monsterHealth = 15;
        SceneManager.LoadScene("Game");
    }
}
