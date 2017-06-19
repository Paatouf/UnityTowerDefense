using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
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
        WaveIndex = 0;
        EnemiesAlive = 0;
        waveCountPanel = waveCountDownText.transform.parent.gameObject;
        waveCountPanel.SetActive(true);

        countdown = 10f;

    }

    void Update()
    {
        Debug.Log("EnemiesAlive=> " + EnemiesAlive);
        if ( EnemiesAlive > 0 )
        {
            return;
        }

        if( countdown <= 0 )
        {
            countdown = timeBetweenWaves;
            StartCoroutine(SpawnWave());
            return;
        }
        
        countdown -= Time.deltaTime;
        
        waveCountDownText.text = Mathf.Floor(countdown + 1).ToString();
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[WaveIndex]; 

        waveIndexText.text = (WaveIndex+1).ToString()+"/"+ waves.Length;
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1/wave.rate);
        }
        WaveIndex++;

        if (WaveIndex == waves.Length)
        {
            GameManager.LevelIsWon = true;
            
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
