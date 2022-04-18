using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public int numberInput;
    public bool clicked = false;
    public void buttonClick(int value)
    {
        numberInput = value;
        clicked = true;
    }
}
