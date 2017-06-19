using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
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
        if ( EnemiesAlive > 0 )
            return;

        if( countdown <= 0 )
        {
            countdown = timeBetweenWaves;
            StartCoroutine( SpawnWave() );
            return;
        }
        
        countdown -= Time.deltaTime;
        
        waveCountDownText.text = Mathf.Floor(countdown + 1).ToString();
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

		countdown = 10f;
	}

	IEnumerator SpawnWave()
    {
        Wave wave = waves[WaveIndex]; 

        waveIndexText.text = (WaveIndex+1).ToString() + " / " + waves.Length;
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemyBase( wave.EnemyBasePrefab );
            yield return new WaitForSeconds(1/wave.rate);
        }
        WaveIndex++;

        if (WaveIndex == waves.Length)
        {
            GameManager.LevelIsWon = true;
            enabled = false;
        }
    }

    void SpawnEnemyBase( GameObject EnemyBasePrefab )
    {
       m_enemiesList.Add( Instantiate( EnemyBasePrefab, spawnPoint.position, spawnPoint.rotation ).GetComponent<EnemyBase>() );
    }
}
