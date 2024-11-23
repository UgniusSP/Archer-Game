using DefaultNamespace;
using Health;
using UnityEngine;

namespace Weapon
{
    public class Arrow : MonoBehaviour, IAttackable
    {
    
        private Rigidbody2D _rigidbody2D;
        private float _damage;
        private Collider2D _collider;
    
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        
            Destroy(gameObject, 4f);
        }

        void Update()
        {
            if (_rigidbody2D.velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    
        public void OnCollisionEnter2D(Collision2D other)
        {
            Attack(other);
        }
        
        public void Attack(Collision2D other)
        {
            
            switch (other.gameObject.tag)
            {
                case "Enemy" when other.gameObject.GetComponent<Enemy>() != null:
                    other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
                    Destroy(gameObject);
                    break;

                case "Heart" when other.gameObject.GetComponent<Heart>() != null:
                    other.gameObject.GetComponent<Heart>().Die();
                    Destroy(gameObject);
                    break;

                case "Wall":
                    _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                    _rigidbody2D.velocity = Vector2.zero;
                    _rigidbody2D.isKinematic = true;
                    break;
                
                case "Armor" when other.gameObject.GetComponent<Armor>() != null:
                    other.gameObject.GetComponent<Armor>().Die();
                    Destroy(gameObject);
                    break;
            }
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }
        
        public void Deconstruct(out Rigidbody2D rigidbody2D, out float damage)
        {
            rigidbody2D = _rigidbody2D;
            damage = _damage;
        }
    }
}
