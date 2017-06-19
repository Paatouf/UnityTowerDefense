using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int startMoney = 20;
    [SerializeField]
    private int startLives = 10;

    public Text livesText;

    public int Money
    {
        get { return startMoney; }
        set { startMoney = value; }
    }

    public int Lives
    {
        get { return startLives; }
        set
        {
            startLives = value;
            UpdateLives();
        }
    }
    
    void UpdateLives()
    {
        livesText.text = "Lives: "+ startLives.ToString();
    }
}
