using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static LevelState GameState = LevelState.NotStarted;

    public GameObject endGameUI;

    public WaveSpawner waveSpawner;
    public PlayerStats playerStats;
	public LevelManager levelMgr;
    public NodeUI nodeUI;

	public enum LevelState
	{
		NotStarted = 0,
		InProgress = 1,
		Win = 2,
		Lose = 3,
		Count
	}

	public static GameManager instance
    {
        get
        {
            if (SearchInstance() == null)
            {
                CreateInstance();
            }
            return s_instance;
        }
    }

    private static GameManager s_instance;

    void Start()
    {
		SetInstance(GetComponent<GameManager>());
        playerStats = GetComponent<PlayerStats>();
        ResetGame();
	}

    private static void SetInstance(GameManager newInstance)
    {
        if (newInstance != null)
        {
            s_instance = newInstance;
            DontDestroyOnLoad(s_instance.gameObject);
        }
    }

    public static GameManager SearchInstance()
    {
        if (s_instance == null)
        {
            SetInstance(FindObjectOfType(typeof(GameManager)) as GameManager);
        }
        return s_instance;
    }

    public static GameManager CreateInstance()
    {
        if (s_instance == null)
        {
            GameObject go = new GameObject(typeof(GameManager).ToString());
            SetInstance(go.AddComponent<GameManager>());
        }
        return s_instance;
    }

    public static void DestroyInstance()
    {
        if (IsInstanceValid())
        {
            Destroy(s_instance.gameObject);
            FreeInstance();
        }
    }

    public static void FreeInstance()
    {
        s_instance = null;
    }

    public static bool IsInstanceValid()
    {
        return (s_instance != null);
    }

    public static bool IsInstanceEqualTo(GameManager otherInstance)
    {
        return (s_instance == otherInstance);
    }

    void Update ()
    {
        if ( GameState == LevelState.NotStarted )
            return;

		if( GameState == LevelState.Win || GameState == LevelState.Lose )
        {
            EndGame();
        }
	}

    void EndGame()
    {
		endGameUI.SetActive( true );
	}

	public void Launch()
	{
		GameState = LevelState.InProgress;
	}


	public void ResetGame()
    {
		GameState = LevelState.NotStarted;

        nodeUI.Hide();
		endGameUI.SetActive( false );
        waveSpawner.Reset();
		levelMgr.Reset();
		playerStats.Reset();
	}

}
