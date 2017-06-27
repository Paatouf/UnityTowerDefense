using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public SceneFader sceneFader;
    public GameObject InfoMessage;

    [Header("End Level")]
    public Text roundsText;
    public Text stateText;
    public Image background;
    public Animator animator;

    [Header("Level Manager")]
    public Text moneyText;

    [Header("Node")]
    public Text infoMessageText;

    [Header("Node UI")]
    public Text upgradeCost;
    public Text sellCost;

    [Header("Player Stats")]
    public Text livesText;

    [Header("Shop")]
    public Text standardTurretCostText;
    public Text cannonTurretTurretCostText;
    public Text missileTurretCostText;
    public Text laserBeamerTurretCostText;

    [Header("Wave Spawner")]
    public Text waveCountDownText;
    public Text waveIndexText;


    void Awake()
    {
        instance = this;
    }

	public void DisplayInfo( string sText )
	{
		StartCoroutine( DisplayInfoRoutine( sText ) );
	}


	private IEnumerator DisplayInfoRoutine(string text)
    {
		InfoMessage.SetActive(true);
		infoMessageText.text = text;
        yield return new WaitForSeconds(3);
        InfoMessage.SetActive(false);
	}

}
