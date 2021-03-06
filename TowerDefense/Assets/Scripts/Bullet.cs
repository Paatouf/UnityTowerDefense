﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public int bulletDamage = 3;
    public GameObject impactEffect;

    private HierarchyManager hierarchyManager;

    public void Start()
    {
        hierarchyManager = GameManager.instance.GetComponent<HierarchyManager>();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        
    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        effectIns.transform.parent = hierarchyManager.EffectsParent;
        Destroy(effectIns,3f);

        if(explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage( target );
        }

        Destroy(gameObject);
    }

    void Damage(Transform EnemyBase)
    {
        EnemyBase e = EnemyBase.GetComponent<EnemyBase>();
        
        if(e != null)
        {
            e.TakeDamage(bulletDamage);
        }

    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if( collider.tag == "Enemy" )
            {
                Damage(collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
    }
}
