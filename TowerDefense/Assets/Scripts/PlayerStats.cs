using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
	private int m_nCurrentMoney = 20;
    private int m_nCurrentLives = 10;

	public int nBaseMoney = 20;
	public int nBaseLives = 10;

    public Text livesText;

	public void Start()
	{
		Reset();
	}

	public int Money
    {
        get { return m_nCurrentMoney; }
        set { m_nCurrentMoney = value; }
    }

    public int Lives
    {
        get { return m_nCurrentLives; }
        set
        {
			m_nCurrentLives = value;
            UpdateLives();
        }
    }
    
    void UpdateLives()
    {
        livesText.text = "Lives: "+ m_nCurrentLives.ToString();
    }

	public void Reset()
	{
		Money = nBaseMoney;
		Lives = nBaseLives;
	}
}
