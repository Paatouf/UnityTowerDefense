using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    private string levelToLoad = "";

    [Header("LevelTab")]
    public GameObject LevelTab;
    private bool LevelASelected = false;
    private bool LevelBSelected = false;
    private bool LevelCSelected = false;
    public Text ConfirmText;
    public GameObject GoButton;

    [Header("StatsTab")]
    public GameObject StatsTab;



    private void Start()
    {
        ResetLevelTab();
    }

    public void Play ()
    {
        StatsTab.SetActive(false);

        if (LevelTab.activeInHierarchy)
        {
            ResetLevelTab();
            LevelTab.SetActive(false);
        }
        else
        {
            LevelTab.SetActive(true);
        }
	}

    public void Stats()
    {
        LevelTab.SetActive(false);

        if (StatsTab.activeInHierarchy)
        {
            StatsTab.SetActive(false);
        }
        else
        {
            StatsTab.SetActive(true);
        }
    }
    public void SelectLevelA()
    {
        LevelASelected = true;
        LevelBSelected = false;
        LevelCSelected = false;

        ConfirmText.text = "ARE YOU SURE YOU WANT TO PLAY THE LEVEL A";
        GoButton.SetActive(true);
    }
    public void SelectLevelB()
    {
        LevelASelected = false;
        LevelBSelected = true;
        LevelCSelected = false;

        ConfirmText.text = "ARE YOU SURE YOU WANT TO PLAY THE LEVEL B";
        GoButton.SetActive(true);
    }
    public void SelectLevelC()
    {
        LevelASelected = false;
        LevelBSelected = false;
        LevelCSelected = true;

        ConfirmText.text = "ARE YOU SURE YOU WANT TO PLAY THE LEVEL C";
        GoButton.SetActive(true);
    }

    public void Go()
    {
        if(LevelASelected)
        {
            levelToLoad = "LevelA";
        }
        else if(LevelBSelected)
        {
            levelToLoad = "LevelB";
        }
        else if(LevelCSelected)
        {
            levelToLoad = "LevelC";
        }

        SceneManager.LoadScene(levelToLoad);
    }

    public void ResetLevelTab()
    {
        GoButton.SetActive(false);
        ConfirmText.text = "";
        LevelASelected = false;
        LevelBSelected = false;
        LevelCSelected = false;
    }
    public void Quit ()
    {
        Application.Quit();
	}
}
