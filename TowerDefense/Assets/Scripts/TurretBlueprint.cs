using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    public int baseCost;

    public GameObject upgradedPrefab;
    public int upgradedCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
