using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("Enemy Collided " + collision.gameObject.name);
        OnHit?.Invoke();
        var damagable = collision.gameObject.GetComponent<Damagable>();
        if (damagable != null && collision.gameObject.name == "PlayerModel")
        {
            damagable.Hit(10);
        }
        
    }
}
