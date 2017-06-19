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

		GameManager.instance.levelMgr.RemoveMoney(buildManager.GetTurretToBuild().cost);


        GameObject _turret = Instantiate(blueprint.prefab, GetBuiltPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuiltPosition(), Quaternion.identity);
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
            Debug.Log("Not enough money to upgrade that! - TODO Display on UI");
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

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

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

    void SetTurretRadius(Color color)
    {
        Color radiusColor = BuildManager.instance.turretRadiusPrefab.GetComponent<Renderer>().material.color = color;
        radiusColor = new Color(color.r, color.g, color.b, 0.1f);
        BuildManager.instance.turretRadiusPrefab.transform.position = transform.position;
    }

    void OnMouseExit()
    {
        rend.material.color = baseColor;
        BuildManager.instance.turretRadiusPrefab.SetActive(false);
    }

	public void Reset()
	{
		Destroy( turret );
		turretBlueprint = null;
		isUpgraded = false;
	}
}
