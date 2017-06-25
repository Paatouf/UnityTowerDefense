using UnityEngine;
using UnityEngine.UI;

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
        GameUIManager.instance.sceneFader.FadeTo();
    }

    public void Menu()
    {
        GameUIManager.instance.sceneFader.FadeTo();
        Time.timeScale = 1;
    }
        

}
