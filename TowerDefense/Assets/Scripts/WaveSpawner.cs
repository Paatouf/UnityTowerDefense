﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_prefabList;

    public WaveSpawnerType waveSpawnerType = WaveSpawnerType.WaveCount;

    public enum WaveSpawnerType
    {
        Endless = 0,
        WaveCount = 1
    }

    public static int EnemiesAlive = 0;

    public List<Wave> waves;
	private List<EnemyBase> m_enemiesList;
    public Transform spawnPoint;
    private GameObject waveCountPanel;

    public Text waveCountDownText;
    public Text waveIndexText;

    public float timeBetweenWaves = 5f;
    public float spawnSpeedInsideWave = 0.3f;
    private float countdown = 10f;

    public static int WaveIndex = 0;

    void Start()
    {
        Reset();
	}

    void Update()
    {
            if (GameManager.GameState == GameManager.LevelState.InProgress)
            {
                if (EnemiesAlive > 0)
                    return;

                if (countdown <= 0)
                {
                    countdown = timeBetweenWaves;
                    StartCoroutine(SpawnWave());
                    return;
                }

                countdown -= Time.deltaTime;
                waveCountDownText.text = Mathf.Floor(countdown + 1).ToString();
            }

            if (GameManager.GameState == GameManager.LevelState.InProgress && WaveIndex == waves.Count && EnemiesAlive == 0 && GameManager.instance.playerStats.Lives > 0)
            {
                GameManager.GameState = GameManager.LevelState.Win;
                //enabled = false;
                return;
            }
	}

	public void Reset()
	{
		if ( m_enemiesList != null && m_enemiesList.Count > 0 )
		{
			for ( int i = 0 ; i < m_enemiesList.Count ; ++i )
				if( m_enemiesList[ i ] != null )
					Destroy( m_enemiesList[ i ].gameObject );
			m_enemiesList.Clear();
		}

		m_enemiesList = new List<EnemyBase>();
		WaveIndex = 0;
		EnemiesAlive = 0;
		waveCountPanel = waveCountDownText.transform.parent.gameObject;
		waveCountPanel.SetActive( true );

        waveCountDownText.text = "";
        waveIndexText.text = "";

        countdown = 10f;
	}

	IEnumerator SpawnWave()
    {
		if ( WaveIndex < waves.Count)
		{
			Wave wave = waves[ WaveIndex ];

			waveIndexText.text = ( WaveIndex + 1 ).ToString() + " / " + waves.Count;
			EnemiesAlive = wave.count;

            for (int i = 0; i < wave.m_enemiesList.Count; ++i)
            {
                for (int j = 0; j < wave.m_enemiesList[i].size; ++j)
                {
                    SpawnEnemyBase(m_prefabList[(int)wave.m_enemiesList[i].type]);
                    yield return new WaitForSeconds(1 / wave.fRate);
                }
                //yield return new WaitForSeconds(1 / wave.fRate);
            }
			WaveIndex++;
		}
    }

    void SpawnEnemyBase( GameObject EnemyBasePrefab )
    {
       m_enemiesList.Add( Instantiate( EnemyBasePrefab, spawnPoint.position, spawnPoint.rotation ).GetComponent<EnemyBase>() );
    }
}
