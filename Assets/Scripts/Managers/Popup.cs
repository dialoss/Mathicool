using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    private Animator calculatorAnim;
    public Animator inputAnim;
    public GameObject input;
    public GameObject inputMinor;
    public TMP_Text mathText;
    public TMP_Text mathTextMinor;
    public Text counter;
    public Slider progressBar;
    public Slider healthBarPlayer;
    public Slider healthBarMonster;
    public Text healthBarPlayerCounter;
    public Text healthBarMonsterCounter;
    public bool opened = false;
    public bool canSkip = true;
    private float changePopupDelay = 0.5f;
    private float curTime = 0;
    private bool eqHided = false;
    private int hideval;
    private bool canShowMinor = true;
    Coroutine progressBarIncrease;
  
    private void Awake()
    {
        calculatorAnim = GetComponent<Animator>();
        int ph = Variables.playerHealth;
        int mh = Variables.monsterHealth;
        setHealthBar(true, ph);
        setHealthBar(false, mh);
    }
    private float timeElapsed = 0;
    private float timeToShow = 0;
    private bool count = false;
    IEnumerator showProgressBar(float timeToSolve)
    {
        while (opened)
        {
            if (progressBar.value <= 1)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed > 1)
                {
                    timeElapsed = 0;
                    int cur = Convert.ToInt32(counter.text);
                    if (cur > 0)
                    {
                        counter.text = (cur - 1).ToString();
                    }
                }
                if (timeElapsed > Variables.timeToHide)
                {
                    if (count) timeToShow += Time.deltaTime;
                    if (hideval == 1)
                    {
                        if (!eqHided && Variables.prevScene != "Book")
                        {
                            inputAnim.SetBool("void", false);
                            inputAnim.SetTrigger("close");
                            count = true;
                            eqHided = true;
                        }
                        if (eqHided && timeToShow > 0.15f)
                        {
                            timeToShow = 0;
                            inputMinor.SetActive(true);
                            count = false;
                        }
                    }
                }
                progressBar.value += 1 / timeToSolve * Time.deltaTime;
            }
            if (curTime < changePopupDelay) curTime += Time.deltaTime;
            else canSkip = true;
            yield return null;
        }
    }
    IEnumerator Waiter(float time, string eq)
    {
        yield return new WaitForSeconds(time);
        mathTextMinor.text = "";
        mathText.text = eq;
        inputMinor.SetActive(false);
        Variables.canClick = true;
    }
    public void changePopUp(string eq, float timeToSolve, bool right)
    {
        StopCoroutine(progressBarIncrease);
        eqHided = false;
        Variables.canClick = false;
        if (right) inputAnim.SetTrigger("right");
        else inputAnim.SetTrigger("wrong");
        if (hideval == 1)
        {
            mathText.text = "";
            StartCoroutine(Waiter(0.3f, eq));
        }
        else
        {
            StartCoroutine(Waiter(0.4f, eq));
        }
        
        canSkip = false;
        curTime = 0;
        progressBar.value = 0;
        timeElapsed = 0;
        timeToSolve = (int)timeToSolve;
        counter.text = (timeToSolve).ToString();
        
        progressBarIncrease = StartCoroutine(showProgressBar(timeToSolve));
        inputAnim.SetBool("void", true);
        Variables.allAnswers += 1;
    }
    public void showPopUp(string eq, float timeToSolve)
    {
        Variables.allAnswers = 0;
        hideval = PlayerPrefs.GetInt("hide", 0);
        inputAnim.SetBool("hideval", (hideval == 1));
        Variables.hideMathInput = hideval;
        opened = true;
        progressBar.value = 0;
        timeElapsed = 0;
        timeToSolve = (int)timeToSolve;
        counter.text = timeToSolve.ToString();
        progressBarIncrease = StartCoroutine(showProgressBar(timeToSolve));
        mathText.text = eq;
        calculatorAnim.SetTrigger("pop");
    }
    public void closePopUp()
    {
        StopCoroutine(progressBarIncrease);
        calculatorAnim.SetTrigger("close");
        opened = false;
    }
    public bool progreesBarEnd()
    {
        return Mathf.Abs(progressBar.value - 1) < 1e-7;
    }
    public void setHealthBar(bool player, int health)
    {
        if (player)
        {
            healthBarPlayer.maxValue = health;
            healthBarPlayer.value = health;
            healthBarPlayerCounter.text = health.ToString();
        }
        else
        {
            healthBarMonster.maxValue = health;
            healthBarMonster.value = health;
            healthBarMonsterCounter.text = health.ToString();
        }
    }
    public (int, int) decreaseHealth(bool player)
    {
        if (player)
        {
            healthBarPlayer.value -= 1;
            healthBarPlayerCounter.text = healthBarPlayer.value.ToString();
        }
        else
        {
            healthBarMonster.value -= 1;
            healthBarMonsterCounter.text = healthBarMonster.value.ToString();
        }
        return ((int)healthBarPlayer.value, (int)healthBarMonster.value);
    }
}
