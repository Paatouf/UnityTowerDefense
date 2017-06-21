using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    //Type de la vague à spawner
    public WaveType waveType = WaveType.Random;

    public enum WaveType
    {
        Random = 0,
        Define = 1,
        Count
    }

    //Ces 2 champs deviennent une classe ou une structure
    public List<EnemyBatch> m_enemiesList;
    public int count;
    public float fRate;
}
