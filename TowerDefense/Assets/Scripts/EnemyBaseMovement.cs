using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBase))]
public class EnemyBaseMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private EnemyBase EnemyBase;
    private HierarchyManager hierarchyManager;

    void Start()
    {
        hierarchyManager = GameManager.instance.GetComponent<HierarchyManager>();
        EnemyBase = GetComponent<EnemyBase>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //direction
        transform.Translate(dir.normalized * EnemyBase.fSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            GetNextWaypoint();
        }

        EnemyBase.fSpeed = EnemyBase.baseSpeed;

        if(waypointIndex >= 1)
        {
            if (target.transform.position.y > Waypoints.points[waypointIndex - 1].transform.position.y)
            {
                EnemyBase.Up();
            }

            if (target.transform.position.y < Waypoints.points[waypointIndex - 1].transform.position.y)
            {
                EnemyBase.Down();
            }

        }
        
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndPath()
    {
        GameManager.instance.playerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
        
        GameUIManager.instance.DisplayInfo("An enemy has reached you base! " + GameManager.instance.playerStats.Lives+ " lives remaining");
        GameUIManager.instance.livesText.GetComponent<Animator>().SetTrigger("Refresh");

        GameObject effet = Instantiate(EnemyBase.BaseDamagedEffectPrefab, transform.position, transform.rotation);
        effet.transform.parent = hierarchyManager.EffectsParent;
        Destroy(effet, 5f);

        Destroy(gameObject);
    }
}