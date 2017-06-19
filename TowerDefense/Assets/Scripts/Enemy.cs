using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float baseHealth = 10f;


    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float health;

    private bool bIsDead = false; 

    public int enemyMoney = 1;
    public GameObject DeathEffectPrefab;
    public GameObject BaseDamagedEffectPrefab;

    public Image healthBar;

    void Start()
    {
        health = baseHealth;
        speed = baseSpeed;

        SetHealth();
        SetSpeed();
    }



    public void TakeDamage(float amount)
    {
        healthBar.transform.gameObject.transform.parent.parent.gameObject.SetActive(true);

        health -= amount;
        healthBar.fillAmount = health / baseHealth;
   
        if (health <= 0 && !bIsDead)
        {
            bIsDead = true;
            Die();
        }
    }

    public void Die()
    {
        GameObject DeathEffect = (GameObject)Instantiate(DeathEffectPrefab, transform.position, transform.rotation);
        DeathEffect.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
        Destroy(DeathEffect, 3f);

        WaveSpawner.EnemiesAlive--;

        BuildManager.instance.AddMoney(enemyMoney);
        Destroy(gameObject);
    }

    public void Slow(float value)
    {
        speed = baseSpeed * (1f - value);
    }

    public void SetSpeed()
    {
        speed = System.Convert.ToInt32(System.Math.Floor(baseSpeed + (WaveSpawner.WaveIndex /2)));
        Debug.Log("speed" + speed);
    }

    public void SetHealth()
    {

        health = System.Convert.ToInt32(System.Math.Floor(baseHealth + (WaveSpawner.WaveIndex * 4)));
        Debug.Log("health" + health);
    }
   
}
