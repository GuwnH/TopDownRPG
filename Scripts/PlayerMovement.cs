using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public List<Transform> firePoints;
    public BulletData bulletData;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;

    private bool canShoot = true;
    private Collider2D[] colliders;
    private float currentDelay = 0;
    public float reloadDelay = 1;

    public UnityEvent OnShoot, OnReload, OnCantShoot;
    public UnityEvent<float> OnReloading;

    private Vector2 movement;
    private Vector2 shootDirection;

    private void Awake()
    {
        colliders = GetComponentsInParent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        shootDirection.x = Input.GetAxis("ShootHorizontal");
        
        shootDirection.y = Input.GetAxis("ShootVertical");
        
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay / reloadDelay);
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }

        if (canShoot && (shootDirection.x != 0 || shootDirection.y != 0))
        {
            Shoot(shootDirection.x,shootDirection.y);
        }    
    }

    void Shoot(float x, float y)
    {
        
        if(canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            foreach (var point in firePoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, point.position, point.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed * Time.fixedDeltaTime : Mathf.Ceil(x) * bulletSpeed * Time.fixedDeltaTime,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed * Time.fixedDeltaTime : Mathf.Ceil(y) * bulletSpeed * Time.fixedDeltaTime
                );

                foreach (var collider in colliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
        }
        }

        OnShoot?.Invoke();
        OnReloading?.Invoke(currentDelay);
        
        if(canShoot == false)
        {
            OnCantShoot?.Invoke();
        }
    }
}