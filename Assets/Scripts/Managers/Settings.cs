using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Color defaultText;
    public Color highLightedText;
    public Color normalHideButton;
    public Color pressedHideButton;
    public Animator hidebtnAnim;
    private void Awake()
    {
        updateValues("add");
        updateValues("div");
        updateValues("sub");
        updateValues("mult");
        updateValues("hiding");
        if (PlayerPrefs.GetInt("hide", 0) == 1)
        {
            hidebtnAnim.SetTrigger("open");
        }
    }
    public void updateValues(string type)
    {
        GameObject cont = GameObject.Find(type);
        if (type == "hiding")
        {
            if (PlayerPrefs.GetInt("hide", 0) == 1)
            {
                cont.GetComponent<Image>().color = pressedHideButton;
                GameObject slider = GameObject.FindGameObjectWithTag("hideslider");
                float val = PlayerPrefs.GetFloat("hiding_speed", 0.2f);
                slider.GetComponent<Slider>().value = val;
            }
            else
            {
                cont.GetComponent<Image>().color = normalHideButton;
            }
            return;
        }
        
        int level = PlayerPrefs.GetInt(type, 1);
        int k = 0;
        foreach (Transform child in cont.transform)
        {
            if (child.gameObject.name == "operation")
            {
                k += 1;
                if (k == level) child.gameObject.GetComponent<TextMeshProUGUI>().color = highLightedText;
                else child.gameObject.GetComponent<TextMeshProUGUI>().color = defaultText;
            }
            if (child.gameObject.name == "slide")
            {
                child.gameObject.transform.GetChild(0).GetComponent<Slider>().value = PlayerPrefs.GetInt(type + "_speed", 1);
            }
        }

    }
    public void hideEquation()
    {
        int cur = PlayerPrefs.GetInt("hide", 0);
        if (cur == 0)
        {
            hidebtnAnim.SetTrigger("open");
            PlayerPrefs.SetInt("hide", 1);
        }
        else
        {
            hidebtnAnim.SetTrigger("close");
            PlayerPrefs.SetInt("hide", 0);
        }
        updateValues("hiding");
    }
    public void setType(string type) 
    {
        string[] tmp = type.Split(' ');
        int level = Convert.ToInt32(tmp[1]);
        PlayerPrefs.SetInt(tmp[0], level);
        updateValues(tmp[0]);
    }
    public void sliderChanged(Slider slider)
    {
        string type = slider.transform.parent.gameObject.transform.parent.name;
        if (type == "hiding") PlayerPrefs.SetFloat(type + "_speed", slider.value);
        else PlayerPrefs.SetInt(type + "_speed", (int)slider.value);
        updateValues("hiding");
    }
}
