using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public TurretBlueprint standardTurret;
    public TurretBlueprint longRangeTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserBeamerTurret;

    public GameObject GOstandardTurret;
    public GameObject GOlongRangeTurret;
    public GameObject GOmissileTurret;
    public GameObject GOlaserBeamerTurret;

    public GameObject TurretSelectedSquare;

    BuildManager buildManager;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        buildManager = BuildManager.instance;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectStandardTurret();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectLongRangeTurret();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectMissileTurret();
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectLaserBeamerTurret();
        }
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
        SetSelectedTurretSquare(GOstandardTurret.transform);
    }
    public void SelectLongRangeTurret()
    {
        buildManager.SelectTurretToBuild(longRangeTurret);
        SetSelectedTurretSquare(GOlongRangeTurret.transform);
    }
    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(missileTurret);
        SetSelectedTurretSquare(GOmissileTurret.transform);
    }
    public void SelectLaserBeamerTurret()
    {
        buildManager.SelectTurretToBuild(laserBeamerTurret);
        SetSelectedTurretSquare(GOlaserBeamerTurret.transform);
    }


    void SetSelectedTurretSquare(Transform pos)
    {
        TurretSelectedSquare.transform.position = pos.transform.position;
    }

    public void UpdateCost(TurretBlueprint turret)
    {
        turret.cost = System.Convert.ToInt32(System.Math.Floor(turret.cost + turret.cost * 0.35));
        if(turret.prefab == standardTurret.prefab)
        {
            GameUIManager.instance.standardTurretCostText.text = "$" + turret.cost.ToString();
        }
        else if(turret.prefab == longRangeTurret.prefab)
        {
            GameUIManager.instance.longRangeTurretCostText.text = "$" + turret.cost.ToString();
        }
        else if(turret.prefab == missileTurret.prefab)
        {
            GameUIManager.instance.missileTurretCostText.text = "$"+turret.cost.ToString();
        }
        else if(turret.prefab == laserBeamerTurret.prefab)
        {
            GameUIManager.instance.laserBeamerTurretCostText.text = "$" + turret.cost.ToString();
        }
    }

    public void Reset()
    {
        standardTurret.cost = standardTurret.baseCost;
        longRangeTurret.cost = longRangeTurret.baseCost;
        missileTurret.cost = missileTurret.baseCost;
        laserBeamerTurret.cost = laserBeamerTurret.baseCost;

        GameUIManager.instance.standardTurretCostText.text = "$" + standardTurret.baseCost.ToString();
        GameUIManager.instance.longRangeTurretCostText.text = "$" + longRangeTurret.baseCost.ToString();
        GameUIManager.instance.missileTurretCostText.text = "$" + missileTurret.baseCost.ToString();
        GameUIManager.instance.laserBeamerTurretCostText.text = "$" + laserBeamerTurret.baseCost.ToString();
    }
}
