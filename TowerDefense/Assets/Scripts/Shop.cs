using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public TurretBlueprint standardTurret;
    public TurretBlueprint longRangeTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserBeamerTurret;

    public Text standardTurretCostText;
    public Text longRangeTurretCostText;
    public Text missileTurretCostText;
    public Text laserBeamerTurretCostText;

    BuildManager buildManager;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Shop in the scene!");
        }
        instance = this;
    }


    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectLongRangeTurret()
    {
        buildManager.SelectTurretToBuild(longRangeTurret);
    }
    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(missileTurret);
    }
    public void SelectLaserBeamerTurret()
    {
        buildManager.SelectTurretToBuild(laserBeamerTurret);
    }

    public void UpdateCost(TurretBlueprint turret)
    {
        turret.cost = System.Convert.ToInt32(System.Math.Floor(turret.cost + turret.cost * 0.2));
        if(turret.prefab == standardTurret.prefab)
        {
            standardTurretCostText.text = "$" + turret.cost.ToString();
        }
        else if(turret.prefab == longRangeTurret.prefab)
        {
            longRangeTurretCostText.text = "$" + turret.cost.ToString();
        }
        else if(turret.prefab == missileTurret.prefab)
        {
            missileTurretCostText.text = "$"+turret.cost.ToString();
        }
        else if(turret.prefab == laserBeamerTurret.prefab)
        {
            laserBeamerTurretCostText.text = "$" + turret.cost.ToString();
        }
    }

}
