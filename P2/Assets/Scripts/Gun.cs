using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject muzzle;
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;

    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float fireDistance = 10f;

    Transform player;
    Vector2 offset;

    private float timeSinceLastFire = 0f;
    Transform closestEnemy;
    Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastFire = fireRate;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = (Vector2)player.position + offset;
        FindClosestEnemy();
        AimAtEnemy();
        Shooting();
    }

    void FindClosestEnemy()
    { 
        closestEnemy = null;
        float closetDistance = Mathf.Infinity;
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closetDistance && distance < fireDistance)
            {   
                closetDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
    }
    void AimAtEnemy(){
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = (Vector2)player.position + offset;
        }
    }
    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }

    void Shooting(){
        if (closestEnemy != null)
        {
            timeSinceLastFire += Time.deltaTime;
            if (timeSinceLastFire >= fireRate)
            {
                Shoot();
                timeSinceLastFire = 0;
            }
        } else
        {
            return;
        }
    }

    void Shoot() {
        Debug.Log("Shoot");
        var muzzleGo = Instantiate(muzzle, muzzlePosition.position, transform.rotation);
        muzzleGo.transform.SetParent(transform);
        Destroy(muzzleGo, 0.1f);

        var projectileGo = Instantiate(projectile, muzzlePosition.position, transform.rotation);
        Destroy(projectileGo, 3f);
    }
}
