﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    public GameObject longRangeTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject laserBeamerTurretPrefab;

    public GameObject buildEffect;
    public GameObject sellEffect;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public Text moneyText;

    //public bool canBuild = false;
    //ublic bool hasMoney = true;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return GameManager.instance.playerStats.Money >= turretToBuild.cost; } }

    Shop shop;

    void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in the scene!");
        }
        instance = this;

       
    }

    void Start()
    {
        RefreshMoney();
        //CanBuild();
        //HasMoney();
        shop = Shop.instance;
    }

    /*public void CanBuild()
    {
        if (turretToBuild != null)
        {
            canBuild = true;
        }
        else
        {
            canBuild = false;
        }
    }

    public void HasMoney()
    {
        if(turretToBuild !=null)
        {
            if (PlayerStats.Money >= turretToBuild.cost)
            {
                hasMoney = true;
            }
            else
            {
                hasMoney = false;
            }
        }
        

    }*/


    public void SelectTurretToBuild(TurretBlueprint turret)
    {  
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    
    public void AddMoney(int value)
    {
        GameManager.instance.playerStats.Money += value;
        RefreshMoney();
    }
    public void RemoveMoney(int value)
    {
        GameManager.instance.playerStats.Money -= value;
        RefreshMoney();
    }

    public void RefreshMoney()
    {
        moneyText.text = "$ "+ GameManager.instance.playerStats.Money;
    }
}
