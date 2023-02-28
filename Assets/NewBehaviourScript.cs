using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        int a;
        a = 5;

        a = 7;
        a = 5 + 7;

        int b = 2;
        int c = 8;

        a = b + c; // 10

        b = 7; // 7
        a = a + c; // 18
        c = 4; // 4

        b = a + b + c;  // 29

        // a = 18
        // b = 29
        // c = 4

        int i = 100;
        while (i >= 0)
        {
            Debug.Log(i);
            i = i - 2;
        }

        for (int j = 100; j >= 0; j = j - 2)
        {
            Debug.Log(i);
        }
    }


}
