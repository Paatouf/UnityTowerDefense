using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.Standard;

    public enum EnemyType
	{
		Standard,
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

    private HierarchyManager hierarchyManager;

    void Start()
    {
        hierarchyManager = GameManager.instance.GetComponent<HierarchyManager>();

        fHealth = baseHealth;
		fSpeed = baseSpeed;

        SetHealth();
        SetSpeed();
    }
	
    public void TakeDamage(float amount)
    {
        healthBar.transform.parent.parent.gameObject.SetActive(true);

		fHealth -= amount;
        healthBar.fillAmount = fHealth / Mathf.Floor(baseHealth + (WaveSpawner.WaveIndex * 4)); ;

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
        DeathEffect.transform.parent = hierarchyManager.EffectsParent;
        Destroy(DeathEffect, 3f);

        WaveSpawner.EnemiesAlive--;

        GameManager.instance.levelMgr.AddMoney(EnemyBaseMoney);
        GameUIManager.instance.moneyText.GetComponent<Animator>().SetTrigger("Refresh");
        Destroy( gameObject );
    }

    public void Slow( float value )
    {
		fSpeed = fSpeed * (1f - value);
    }

    public void Up()
    {
        fSpeed = fSpeed * (1f - 0.3f);
    }

    public void Down()
    {
        fSpeed = fSpeed * (1f + 0.3f);
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
