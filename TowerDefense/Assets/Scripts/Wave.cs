using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
	public List<EnemyBase.EnemyType> m_enemiesList;
    public GameObject EnemyBasePrefab;
    public int count;
    public float rate;
}
