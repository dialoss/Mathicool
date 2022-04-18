using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Generation
{
    private static int[] usedNums;
    private static int[] adds = { };

    private static int[,,,] ranges = { 
        { { { 2, 9 }, { 2, 9 } }, { { 11, 99 }, { 11, 99 } }, { { 43, 99 }, { 100, 999 } }, { { 100, 999 }, { 100, 999 } } },
        { { {41,99}, {7,19} }, { {64,99}, {13,57} }, { {167,999}, {31,99} }, { {501,999}, {112,478} } },
        { { {2,9}, {4,9} }, { {11,99}, {3,9} }, { {109,999}, {2,9} }, { {11,99}, {11,99} } },
        { { {3,9}, {4,9} }, { {21,99}, {5,9} }, { {334,999}, {3,9} }, { {32,99}, {32,99} } },
    };
    private static int[,,] ranges_fbk =
    {  { {0,0,1}, {10,50,10}, {60,90,10}, {100,300,100} },
       { {2,9,1}, {11,54,1}, {55,99,1}, {101,721,1} },
       { {11,31,2}, {34,99,1}, {101,345,1}, {345,891,1} },
       { {11,71,2}, {80,200,1}, {201,566,1}, {600,999,1} },
       { {10,50,10}, {60,120,10}, {200,560,10}, {60,990,10} },
       { {11,20,1 }, {21,66,3}, {75,105,3}, {115,300,3}, }
};
    public static int random(int a, int b, int step)
    {
        int x = UnityEngine.Random.Range(a, b + 1);
        int steps = x / step;
        return steps * step;
    }
    public static void clear()
    {
        usedNums = new int[(int)1e6];
        usedNums[0] = 1;
    }
    public static int[] gen_equation(bool book, string type)
    {
        type = type.ToLower();
        int[] equation = { 0, 0, 0, 0, 0 };
        int a = 0, b = 0, op = 0;

        int d = PlayerPrefs.GetInt(type) - 1;
        int p = PlayerPrefs.GetInt(type);

        switch (type)
        {
            case "add":
                op = 1;
                a = random(ranges[0,d, 0,0], ranges[0, d, 0,1], 1);
                b = random(ranges[0,d, 1,0], ranges[0, d, 1, 1], 1);
                equation[3] = a + b;
                equation[4] = 13 - 2 * PlayerPrefs.GetInt(type + "_speed");
                break;

            case "div":
                op = 4;
                a = random(ranges[3, d, 0, 0], ranges[3, d, 0, 1], 1);
                b = random(ranges[3, d, 1, 0], ranges[3, d, 1, 1], 1);
                equation[3] = a;
                a = a * b;
                equation[4] = 13 - 2 * PlayerPrefs.GetInt(type + "_speed");
                break;
               
            case "mult":
                op = 3;
                a = random(ranges[2, d, 0, 0], ranges[2, d, 0, 1], 1);
                b = random(ranges[2, d, 1, 0], ranges[2, d, 1, 1], 1);
                equation[3] = a * b;
                equation[4] = 13 - 2 * PlayerPrefs.GetInt(type + "_speed");
                break;

            case "sub":
                op = 2;
                a = random(ranges[1, d, 0, 0], ranges[1, d, 0, 1], 1);
                b = random(ranges[1, d, 1, 0], ranges[1, d, 1, 1], 1);
                equation[3] = a - b;
                equation[4] = 13 - 2 * PlayerPrefs.GetInt(type + "_speed");
                break;

            case "square":
                op = 3;
                a = random(ranges_fbk[5, p, 0], ranges_fbk[5, p, 1], ranges_fbk[5, p, 2]);
                b = a;
                equation[3] = a * b;
                equation[4] = Mathf.Max(8-p, (int)(a / 90));
                break;

            case "square5":
                op = 3;
                
                a = random(ranges_fbk[4, p, 0], ranges_fbk[4, p, 1], ranges_fbk[4, p, 2])+5;
                b = a;
                equation[3] = a * b;
                equation[4] = Mathf.Max(10 - p, (int)(a / 90));
                break;

            case "789":
                
                op = 2;
                b = random(7, 9, 1);
                b += random(ranges_fbk[0,p, 0],ranges_fbk[0,p,1],ranges_fbk[0,p,2]);
                a = random(20, 900, 1);
                equation[3] = a - b;
                equation[4] = 7 - p;
                break;

            case "11mult":
                op = 3;
                a = random(ranges_fbk[1, p, 0], ranges_fbk[1, p, 1], ranges_fbk[1, p, 2]);
                b = 11;
                equation[3] = a * b;
                equation[4] = 7 - p;
                break;

            case "9mult":
                op = 3;
                a = random(ranges_fbk[2, p, 0], ranges_fbk[2, p, 1], ranges_fbk[2, p, 2]);
                b = 9;
                equation[3] = a * b;
                equation[4] = Mathf.Max(8-p, (int)(a / 100));
                break;

            case "1mult":
                op = 3;
                a = random(ranges_fbk[3, p, 0], ranges_fbk[3, p, 1], ranges_fbk[3, p, 2]);
                b = random(2, 9, 1);
                equation[3] = a * b;
                equation[4] =  Mathf.Max(9 - p, (int)(a / 90));
                break;
        }
        equation[0] = a;
        equation[1] = b;
        equation[2] = op;
        return equation;
    }
}
