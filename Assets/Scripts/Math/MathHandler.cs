using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathHandler : MonoBehaviour
{
    private TMP_Text mathText;
    private TMP_Text mathTextMinor;
    private Buttons btn;
   
    public int sign;
    public float timeToSolve = 5.0f;

    private GameManager manager;
    private int hide;

    private void Awake()
    {
        btn = GetComponent<Buttons>();
        manager = GetComponent<GameManager>();
        mathText = manager.popupManager.mathText;
        mathTextMinor = manager.popupManager.mathTextMinor;
        hide = PlayerPrefs.GetInt("hide");
    }
    public (string, float) genEquation()
    {
        sign = 1;
        Variables.currentAnswer = 0;
        Variables.rightAnswer = -1;

        string type = Variables.gameType;
        int[] equation = Generation.gen_equation(Variables.book, type);
        timeToSolve = equation[4];
        string eq = "";
        eq += equation[0].ToString();
        switch (equation[2])
        {
            case 1:
                eq += " + ";
                break;
            case 2:
                eq += " - ";
                break;
            case 3:
                eq += " * ";
                break;
            case 4:
                eq += " / ";
                break;
        }
        eq += equation[1].ToString() + " = ";
        Variables.rightAnswer = equation[3];
        var res = (eq, timeToSolve);
        return res;
    }
    private void backspace()
    {
        string new_text = "";
        for (int i = 0; i < mathText.text.Length - 1; i++)
        {
            new_text += mathText.text[i];
        }
        mathText.text = new_text;
        new_text = "";
        for (int i = 0; i < mathTextMinor.text.Length - 1; i++)
        {
            new_text += mathTextMinor.text[i];
        }
        mathTextMinor.text = new_text; 
    }
    public void mathInput()
    {
        if (!btn.clicked || !Variables.canClick) return;
        switch (btn.numberInput)
        {
            case 10:
                if (Variables.currentAnswer > 0)
                {
                    backspace();
                    Variables.currentAnswer = (int)Mathf.Floor((float)Variables.currentAnswer / 10);
                }
                if (sign == -1)
                {
                    backspace();
                    sign = 1;
                }
                break;

            case 11:
                if (sign == 1 && Variables.currentAnswer == 0)
                {
                    mathText.text = mathText.text + "-";
                    mathTextMinor.text = mathTextMinor.text + "-";
                    sign = -1;

                }
                break;
        }
        if (btn.numberInput <= 9 && !(Variables.currentAnswer == 0 && btn.numberInput == 0) && Variables.currentAnswer < 1e7)
        {
            Variables.currentAnswer *= 10;
            Variables.currentAnswer += sign * btn.numberInput;
            if (Variables.hideMathInput != 1)
                mathText.text = mathText.text + btn.numberInput;
            mathTextMinor.text = mathTextMinor.text + btn.numberInput;
        }
        btn.clicked = false;
    }
}
