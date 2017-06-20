using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Time.timeScale = 0;

        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Time.timeScale = 1;

        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Time.timeScale = 2;

        }

        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            Time.timeScale = 3;

        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Time.timeScale = 4;

        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Time.timeScale = 5;

        }
    }

}
