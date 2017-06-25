using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class EnemyBaseMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private EnemyBase EnemyBase;

    void Start()
    {
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
		
		Destroy( gameObject );

		GameObject effet = Instantiate(EnemyBase.BaseDamagedEffectPrefab, transform.position, transform.rotation);
        Destroy(effet, 5f);
    }
}
