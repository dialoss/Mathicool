using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public GameObject readSection;
    public Text readText;
    public GameObject readContent;
    public GameObject selectionContent;
    private Animator readAnim;
    private List<GameObject> tiles, stars;
    public string[] types;
    public Sprite[] starCloseOpen;
    private void Awake()
    {
        readAnim = readSection.GetComponent<Animator>();
        tiles = new List<GameObject>();
        stars = new List<GameObject>();
        getStarObjects();
    }
    private void getStarObjects()
    {
        foreach (Transform child in selectionContent.transform)
        {
            foreach (Transform content in child.transform)
            {
                if (content.name == "closed")
                {
                    tiles.Add(content.gameObject);
                }
                if (content.name == "Stars")
                {
                    stars.Add(content.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        int level = PlayerPrefs.GetInt("bookLevel", 0);
        UpdateTiles(level);
    }
    private void UpdateTiles(int level)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (level >= i)
            {
                tiles[i].SetActive(false);
                stars[i].SetActive(true);
                int starLevel = PlayerPrefs.GetInt(types[i], 0);
                for (int j = 1; j <= 3; j++)
                {
                    if (j <= starLevel)
                        stars[i].transform.GetChild(j - 1).GetComponent<Image>().sprite = starCloseOpen[1];
                    else stars[i].transform.GetChild(j - 1).GetComponent<Image>().sprite = starCloseOpen[0];
                }
            }
            else
            {
                tiles[i].SetActive(true);
                stars[i].SetActive(false);
            }
        }
    }
    public void openReadSection(GameObject textObject)
    {
        readContent.transform.position = new Vector3(0, 1554, 0);
        readAnim.SetTrigger("open");
        Text txt = textObject.GetComponentInChildren<Text>();
        readText.font = txt.font;
        readText.text = txt.text;
		readText.fontStyle = txt.fontStyle;
		readText.alignment = txt.alignment;
        readText.fontSize = txt.fontSize;
        readText.color = txt.color;
    }
    public void closeReadSection()
    {
        readAnim.SetTrigger("close");
    }
    public void playButton(int level)
    {
        Variables.book = true;
        Variables.gameType = types[level];
        string type = types[level];
        Variables.prevScene = "Book";
        Variables.bookLevel = level;
        Variables.bookStar = Mathf.Min(PlayerPrefs.GetInt(type, 0), 3);
        Variables.currentBackground = Mathf.Min(Variables.bookStar, 2);
        PlayerPrefs.SetInt(type, Variables.bookStar);
        Variables.monsterHealth = 4 + Variables.bookStar * 3;
        SceneManager.LoadScene("Game");
    }
}
