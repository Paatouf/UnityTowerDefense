using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public string levelToLoad = "LevelA";

	public void Play ()
    {
        levelToLoad = "LevelA";
        SceneManager.LoadScene(levelToLoad);
	}

    public void Play2()
    {
        levelToLoad = "LevelB";
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit ()
    {
        Application.Quit();
	}
}
