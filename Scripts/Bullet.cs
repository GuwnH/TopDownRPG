using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    private Vector2 startPosition;
    private float conquaredDistance = 0;
    private Rigidbody2D rb2d;
    public UnityEvent OnHit = new UnityEvent();
    public BulletData bulletData;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if (conquaredDistance >= bulletData.maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    // private void OnTriggerEnter2D(Collider2D collision) 
    // {
    //     Debug.Log("Colliderd " + collision.name);
    //     OnHit?.Invoke();
    //     var damagable = collision.GetComponent<Damagable>();
    //     if (damagable != null)
    //     {
    //         damagable.Hit(bulletData.damage);
    //     }
        
    //     DisableObject();
    // }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("Colliderd " + collision.gameObject.name);
        OnHit?.Invoke();
        var damagable = collision.gameObject.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(bulletData.damage);
        }
        
        DisableObject();
    }
}
