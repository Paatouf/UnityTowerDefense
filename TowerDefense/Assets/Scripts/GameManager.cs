﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public static bool LevelIsWon;

    public GameObject gameOverUI;
    public GameObject levelWonUI;

    public WaveSpawner waveSpawner;

    public PlayerStats playerStats;

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
        s_instance = GetComponent<GameManager>();
        playerStats = GetComponent<PlayerStats>();
        ResetGame();
    }

    private static void SetInstance(GameManager newInstance)
    {
        if (newInstance != null)
        {
            s_instance = newInstance;
            GameObject.DontDestroyOnLoad(s_instance.gameObject);
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
            GameObject.Destroy(s_instance.gameObject);
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
        if (GameIsOver)
            return;

        if (LevelIsWon)
        {
            levelWonUI.SetActive(true);
            return;
        }

		if( playerStats.Lives <= 0 )
        {
            EndGame();
        }
	}

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    void ResetGame()
    {
        GameIsOver = false;
    }

}
