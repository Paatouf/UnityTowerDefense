using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public Node target;
    public GameObject ui;
    public Text upgradeCost;
    public Text sellCost;

	public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuiltPosition();

        upgradeCost.text = "$"+target.turretBlueprint.upgradedCost.ToString();
        sellCost.text = "$" + target.turretBlueprint.GetSellAmount().ToString();
        ui.SetActive(true);

        GameObject parent = upgradeCost.gameObject.transform.parent.gameObject;

        if (target.isUpgraded)
        {
            parent.SetActive(false);
        }
        else
        {
            parent.SetActive(true);
        }
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        if(!target.isUpgraded)
        {
            target.UpgradeTurret();
            BuildManager.instance.DeselectNode();
        }
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();


    }
}
