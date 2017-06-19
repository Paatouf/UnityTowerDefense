using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public Text moneyText;

	public List<Node> m_turretList;
	// Use this for initialization
	void Start ()
	{
		RefreshMoney();

		m_turretList = new List<Node>();
		GameObject[] goNode = GameObject.FindGameObjectsWithTag( "Node" );
		for ( int i = 0 ; i < goNode.Length ; ++i )
			m_turretList.Add( goNode[ i ].GetComponent<Node>() );
	}
	
	public void Reset()
	{
		for( int i = 0 ; i < m_turretList.Count ; ++i )
			m_turretList[ i ].Reset();
		RefreshMoney();
	}

	public void AddMoney( int value )
	{
		GameManager.instance.playerStats.Money += value;
		RefreshMoney();
	}

	public void RemoveMoney( int value )
	{
		GameManager.instance.playerStats.Money -= value;
		RefreshMoney();
	}

	public void RefreshMoney()
	{
		moneyText.text = "$ " + GameManager.instance.playerStats.Money;
	}
}
