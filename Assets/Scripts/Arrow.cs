using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;
    private float _damage;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        float angle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Heart"))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Heart heart = other.gameObject.GetComponent<Heart>();
            player.UpdateHealth(heart.GetHeartValue());
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    
}
