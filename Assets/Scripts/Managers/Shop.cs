using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public LayerMask skinsLayer;
    [SerializeField] private string[] skinNames;
    [SerializeField] private int[] prices;
    [SerializeField] private GameObject[] skins;
    [SerializeField] private Text moneyCount;
    private int currentMoney = 0;
    private GameObject[] costs;
    private GameObject[] selections;
    private string currentSkin;

    private Color black = new Color((float)30 / 255, (float)30 / 255, (float)30 / 255, 1);

    private void Awake()
    {
        currentSkin = PlayerPrefs.GetString("currentSkin", "Starter");
        costs = new GameObject[skins.Length];
        selections = new GameObject[skins.Length];
        for (int i = 0; i < skins.Length; i++)
        {
            foreach (Transform child in skins[i].transform)
            {
                if (child.gameObject.name == "Selection")
                {
                    selections[i] = child.gameObject;
                }
                if (child.gameObject.name == "Cost")
                {
                    costs[i] = child.gameObject;
                }
            }
        }
    }
    public void SelectSkin(string name)
    {
        int ind = CheckName(name);
        if (ind >= 0 && currentMoney >= prices[ind])
        {
            if (ind == 4 && PlayerPrefs.GetInt("allStars", 0) < Variables.bookLevels * 3) return;
            PlayerPrefs.SetString("currentSkin", name);
            currentSkin = name;
        }
    }
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        currentMoney = PlayerPrefs.GetInt("playerMoney", 0);
        moneyCount.text = currentMoney.ToString();
        for (int i = 0; i < skins.Length; i++)
        {
            Image tmp = skins[i].GetComponent<Image>();
            if ((i <= 3 && currentMoney >= prices[i]) || (PlayerPrefs.GetInt("allStars", 0) >= Variables.bookLevels * 3 && i == 4))
            {
                costs[i].SetActive(false);
                if (tmp == null) skins[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                else tmp.color = Color.white;
            }
            else
            {
                costs[i].SetActive(true);
                if (tmp == null) skins[i].transform.GetChild(0).GetComponent<Image>().color = black;
                else tmp.color = black;
            }
            if (skinNames[i] == currentSkin)
            {
                selections[i].SetActive(true);
            }
            else
            {
                selections[i].SetActive(false);
            }
        }
    }
   
    private int CheckName(string name)
    {
        for (int i = 0; i < skinNames.Length; i++)
        {
            if (skinNames[i] == name) return i;
        }
        return -1;
    }
}
