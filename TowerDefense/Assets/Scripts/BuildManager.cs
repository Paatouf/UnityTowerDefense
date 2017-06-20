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

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            nodeUI.Hide();
            SelectTurretToBuild(null);
            turretRadiusPrefab.SetActive(false);

            //la couleur node sur laquelle se trouve la souris au moment du clic ne se reset pas
        }      
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        if (turretToBuild != null)
        {
            turretRadiusPrefab.transform.localScale = new Vector3(turret.prefab.GetComponent<Turret>().range * 2, 1, turret.prefab.GetComponent<Turret>().range * 2);
            DeselectNode();
        }
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
        nodeUI.SetTarget(node);
        turretToBuild = null;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
