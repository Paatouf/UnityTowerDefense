using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Color baseColor;
    public Material groundMat;

    public Vector3 positionOffset;

    public Renderer rend;

    public bool isActive = true;
    

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager;

    void Awake()
    {
        if(!isActive)
        {
            this.GetComponent<Renderer>().material = groundMat;
            this.enabled = false;
        }
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
        if (enabled)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (turret != null)
            {
                buildManager.SelectNode(this);
                return;

            }
            if (!buildManager.CanBuild)
                return;

            BuildTurret(buildManager.GetTurretToBuild());
        }
    }

    void OnMouseEnter()
    {
        if (enabled)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!buildManager.CanBuild)
                return;

            if (Input.GetMouseButton(0))
            {
                if (turret != null)
                {
                    BuildManager.instance.turretRadiusPrefab.SetActive(false);
                    return;
                }
                else
                {
                    BuildTurret(buildManager.GetTurretToBuild());
                    BuildManager.instance.turretRadiusPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    BuildManager.instance.turretRadiusPrefab.SetActive(true);


                }


            }
            else
            {
                if (turret == null)
                {
                    BuildManager.instance.turretRadiusPrefab.SetActive(true);

                    if (buildManager.HasMoney)
                    {
                        rend.material.color = hoverColor;
                        SetTurretRadius(hoverColor);
                    }
                    else
                    {
                        rend.material.color = notEnoughMoneyColor;
                        SetTurretRadius(notEnoughMoneyColor);
                    }
                }
                else
                {
                    BuildManager.instance.turretRadiusPrefab.SetActive(false);
                }
            }
        }
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (GameManager.instance.playerStats.Money < blueprint.cost)
        {
            GameUIManager.instance.DisplayInfo("Sorry, you need more money to build that!");
            return;
        }

		GameManager.instance.levelMgr.RemoveMoney(buildManager.GetTurretToBuild().cost);


        GameObject _turret = Instantiate(blueprint.prefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuiltPosition(), Quaternion.identity);
        effect.GetComponent<Renderer>().material.color = turret.GetComponentInChildren<MeshRenderer>().material.color;
        Destroy(effect, 3f);

        Shop.instance.UpdateCost(blueprint);
        BuildManager.instance.turretRadiusPrefab.SetActive(false);

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
		GameManager.instance.levelMgr.RefreshMoney();

        GameObject effect = Instantiate(buildManager.sellEffect, GetBuiltPosition(), Quaternion.identity);
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
            GameUIManager.instance.DisplayInfo("Not enough money to upgrade this turret!");
            return;
        }

		GameManager.instance.levelMgr.RemoveMoney(turretBlueprint.upgradedCost);

        
        //destroy the old one
        Destroy(turret);

        //build a new one
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        // à changer pour un effet d'upgrade dédié
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuiltPosition(), Quaternion.identity); 
        effect.GetComponent<Renderer>().material.color = turret.GetComponentInChildren<MeshRenderer>().material.color;
        Destroy(effect, 3f);
		
        isUpgraded = true;
    }

    void SetTurretRadius(Color color)
    {
        color.a = 0.3f;
        BuildManager.instance.turretRadiusPrefab.GetComponent<Renderer>().material.color= color;
        BuildManager.instance.turretRadiusPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    void OnMouseExit()
    {
        if (enabled)
        {
            ResetBaseColor();
            BuildManager.instance.turretRadiusPrefab.SetActive(false);
        }
    }

	public void Reset()
	{
		Destroy( turret );
		turretBlueprint = null;
		isUpgraded = false;
	}

    public void ResetBaseColor()
    {
        rend.material.color = baseColor;
    }
}
