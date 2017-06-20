using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject target;
    private EnemyBase targetEnemyBase;


    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public float damageOverTime = 4f;
    public float slowPercent = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;


    [Header("Unity Setup Fields")]
    public string EnemyBaseTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform partToRotate;

    
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating( "UpdateTarget", 0f, 0.5f );
    }

    void UpdateTarget()
    {
		if ( GameManager.instance.bGameIsStarted )
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag( EnemyBaseTag );
			float shotestDistance = Mathf.Infinity;
			GameObject nearestEnemyBase = null;

			foreach ( GameObject EnemyBase in enemies )
			{
				float distanceToEnemyBase = Vector3.Distance( transform.position, EnemyBase.transform.position );
				if ( distanceToEnemyBase < shotestDistance )
				{
					shotestDistance = distanceToEnemyBase;
					nearestEnemyBase = EnemyBase.gameObject;
				}
			}

			if ( nearestEnemyBase != null && shotestDistance <= range )
			{
				target = nearestEnemyBase;
				targetEnemyBase = nearestEnemyBase.GetComponent<EnemyBase>();
			}
			else
			{
				target = null;
			}
		}
    }

    void Update()
    {
        if (target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

                return;
        }

        LockOnTarget();
        
        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        } 
    }

    void Laser()
    {
		if ( targetEnemyBase != null )
		{
			targetEnemyBase.TakeDamage( damageOverTime * Time.deltaTime );
			targetEnemyBase.Slow( slowPercent );
		}

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
            

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 dir = firePoint.position - target.transform.position;
        impactEffect.transform.position = target.transform.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation( dir );

        
    }
    void LockOnTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation( dir );
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    void Shoot()
    {
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target.transform);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
