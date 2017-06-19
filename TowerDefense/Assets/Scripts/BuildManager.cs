using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    public GameObject longRangeTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject laserBeamerTurretPrefab;

    public GameObject turretRadiusPrefab;

    public GameObject buildEffect;
    public GameObject sellEffect;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

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
        shop = Shop.instance;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        turretRadiusPrefab.transform.localScale = new Vector3(turret.prefab.GetComponent<Turret>().range, turret.prefab.GetComponent<Turret>().range, turret.prefab.GetComponent<Turret>().range);
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
}
