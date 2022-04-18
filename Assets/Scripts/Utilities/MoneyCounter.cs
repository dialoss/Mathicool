using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyCounter : MonoBehaviour
{
    private int moneyToAdd = 0;
    private int currentMoney;
    private TMP_Text moneyText;
    public float countTime = 2f;
    private int step;
    public Animator popupAnim;
    public void StartCount()
    {
        currentMoney = 0;
        moneyToAdd = Mathf.Max(50, Generation.random(400, 500, 1) - 20 * Variables.allAnswers);
        moneyText = GetComponent<TextMeshProUGUI>();
        step = Mathf.FloorToInt(moneyToAdd / (countTime * 20));
        StartCoroutine(Counter());
        int playerMoney = PlayerPrefs.GetInt("playerMoney", 0);
        PlayerPrefs.SetInt("playerMoney", playerMoney + moneyToAdd);
    }
    IEnumerator Counter()
    {
        yield return new WaitForSeconds(0.5f);
        while (currentMoney < moneyToAdd)
        {
            currentMoney += step;
            moneyText.text = currentMoney.ToString();
            yield return null;
        }   
    }

}
