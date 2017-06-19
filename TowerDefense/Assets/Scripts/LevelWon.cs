using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelWon : MonoBehaviour
{
    public Text roundsText;
    public SceneFader sceneFader;

    void OnEnable()
    {
        roundsText.text = WaveSpawner.WaveIndex.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
        Time.timeScale = 1;
    }


}
