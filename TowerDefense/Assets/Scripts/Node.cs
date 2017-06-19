using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color baseColor;
    public Vector3 positionOffset;

    private Renderer rend;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager;

    void Awake()
    {
        baseColor = GetComponent<Renderer>().material.color;
    }
    void Start ()
    {
        rend = GetComponent<Renderer>();

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuiltPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;

        }
        if (!buildManager.CanBuild)
            return;


        if(buildManager.CanBuild)
            BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (GameManager.instance.playerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that! - TODO Display on UI");
            return;
        }
        buildManager.RemoveMoney(buildManager.GetTurretToBuild().cost);


        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuiltPosition(), Quaternion.identity);
        effect.GetComponent<Renderer>().material.color = turret.GetComponentInChildren<MeshRenderer>().material.color;
        Destroy(effect, 3f);

        Shop.instance.UpdateCost(blueprint);

        
    }
    public void SellTurret()
    {
        if(!isUpgraded)
        {
            GameManager.instance.playerStats.Money += turretBlueprint.GetSellAmount();
        }
        else
        {
            GameManager.instance.playerStats.Money += turretBlueprint.GetSellAmount()*2;
        }
        BuildManager.instance.RefreshMoney();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuiltPosition(), Quaternion.identity);
        effect.GetComponent<Renderer>().material.color = turret.GetComponentInChildren<MeshRenderer>().material.color;
        Destroy(effect, 3f);
        
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }

    public void UpgradeTurret()
    {
        if (GameManager.instance.playerStats.Money < turretBlueprint.upgradedCost)
        {
            Debug.Log("Not enough money to upgrade that! - TODO Display on UI");
            return;
        }
        buildManager.RemoveMoney(turretBlueprint.upgradedCost);

        
        //destroy the old one
        Destroy(turret);

        //build a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        

        // à changer pour un effet d'upgrade dédié
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuiltPosition(), Quaternion.identity); 
        effect.GetComponent<Renderer>().material.color = turret.GetComponentInChildren<MeshRenderer>().material.color;
        Destroy(effect, 3f);


        isUpgraded = true;
      
    }


    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = baseColor;    
    }
}
