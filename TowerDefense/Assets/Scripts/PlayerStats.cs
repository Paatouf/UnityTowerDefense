using UnityEngine;


public class PlayerStats : MonoBehaviour
{
	private int m_nCurrentMoney = 20;
    private int m_nCurrentLives = 10;

	public int nBaseMoney = 20;
	public int nBaseLives = 10;

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
			if ( m_nCurrentLives <= 0 )
				GameManager.GameState = GameManager.LevelState.Lose;
			UpdateLives();
        }
    }
    
    void UpdateLives()
    {
        GameUIManager.instance.livesText.text = "Lives: "+ m_nCurrentLives.ToString();
    }

	public void Reset()
	{
		Money = nBaseMoney;
		Lives = nBaseLives;
	}
}
