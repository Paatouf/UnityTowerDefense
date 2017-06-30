using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    void OnEnable()
	{
		GameUIManager.instance.animator.SetInteger( "nGameIsWon", (int)GameManager.GameState );

		if ( GameManager.GameState == GameManager.LevelState.Win )
		{
            GameUIManager.instance.background.color = new Color( 0, 1, 0, 0.75f );
			GameUIManager.instance.stateText.text = "CONGRATULATIONS!";
		}
		else if ( GameManager.GameState == GameManager.LevelState.Lose )
		{
            GameUIManager.instance.background.color = new Color( 1, 0, 0, 0.75f );
            GameUIManager.instance.stateText.text = "GAME OVER! LOOSER!";
		}

        GameUIManager.instance.roundsText.text = WaveSpawner.WaveIndex.ToString();
    }

    public void Retry()
    {
        GameUIManager.instance.sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        GameUIManager.instance.sceneFader.FadeTo("MainMenu");
        Time.timeScale = 1;
    }
        

}
