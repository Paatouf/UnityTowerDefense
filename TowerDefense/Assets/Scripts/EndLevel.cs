using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public Text roundsText;
	public Text stateText;
	public Image background;
    public SceneFader sceneFader;
	public Animator animator;

    void OnEnable()
	{
		animator.SetInteger( "nGameIsWon", (int)GameManager.GameState );

		if ( GameManager.GameState == GameManager.LevelState.Win )
		{
			background.color = new Color( 0, 1, 0, 0.75f );
			stateText.text = "CONGRATULATIONS!";
		}
		else if ( GameManager.GameState == GameManager.LevelState.Lose )
		{
			background.color = new Color( 1, 0, 0, 0.75f );
			stateText.text = "GAME OVER! LOOSER!";
		}

		roundsText.text = WaveSpawner.WaveIndex.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo();
    }

    public void Menu()
    {
        sceneFader.FadeTo();
        Time.timeScale = 1;
    }
        

}
