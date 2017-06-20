using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    //Ces 2 champs deviennent une classe ou une structure
    public List<EnemyBase.EnemyType> m_enemiesList;
    public float rate;

    //Nombre total d'ennemis sur la vague
    public int count;

    //Type de la vague à spawner


    public GameObject EnemyBasePrefab;  
}
