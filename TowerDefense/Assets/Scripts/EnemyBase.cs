using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
	public enum EnemyType
	{
		Normal,
		Fast,
		Boss,
		Count
	}

    public float baseSpeed = 10f;
    public float baseHealth = 10f;
	
    [HideInInspector]
    public float fSpeed;
    [HideInInspector]
    public float fHealth;

    private bool bIsDead = false; 

    public int EnemyBaseMoney = 1;
    public GameObject DeathEffectPrefab;
    public GameObject BaseDamagedEffectPrefab;

    public Image healthBar;

    void Start()
    {
		fHealth = baseHealth;
		fSpeed = baseSpeed;

        SetHealth();
        SetSpeed();
    }
	
    public void TakeDamage(float amount)
    {
        healthBar.transform.parent.parent.gameObject.SetActive(true);

		fHealth -= amount;
        healthBar.fillAmount = fHealth / baseHealth;
   
        if ( fHealth <= 0 && !bIsDead)
        {
            bIsDead = true;
            Die();
        }
    }

    public void Die()
    {
        GameObject DeathEffect = Instantiate(DeathEffectPrefab, transform.position, transform.rotation);
        DeathEffect.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(DeathEffect, 3f);

        WaveSpawner.EnemiesAlive--;

        GameManager.instance.levelMgr.AddMoney(EnemyBaseMoney);
        Destroy( gameObject );
    }

    public void Slow( float value )
    {
		fSpeed = baseSpeed * (1f - value);
    }

    public void SetSpeed()
    {
		fSpeed = Mathf.Floor( baseSpeed + ( WaveSpawner.WaveIndex * 0.5f ));
    }

    public void SetHealth()
    {
        fHealth = Mathf.Floor(baseHealth + ( WaveSpawner.WaveIndex * 4 ));
    }
   
}
