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

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;

        }
        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void OnMouseEnter()
    {
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

    void SetTurretRadius(Color color)
    {
        color.a = 0.3f;
        BuildManager.instance.turretRadiusPrefab.GetComponent<Renderer>().material.color= color;
        BuildManager.instance.turretRadiusPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    void OnMouseExit()
    {
        ResetBaseColor();
        BuildManager.instance.turretRadiusPrefab.SetActive(false);
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
