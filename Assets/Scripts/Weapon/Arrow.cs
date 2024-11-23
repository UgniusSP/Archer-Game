using System;
using DefaultNamespace;
using Health;
using UnityEngine;

namespace Weapon
{
    public class Arrow : MonoBehaviour, IAttackable
    {
        private Rigidbody2D _rigidbody2D;
        private float _damage;

        void Start()
        {
            InitializeRigidbody();
            DestroyArrowAfterTime(4f); // Automatically destroy the arrow after 4 seconds if not already destroyed
        }

        void Update()
        {
            TryRotateArrowBasedOnVelocity();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Attack(other);
        }

        private void InitializeRigidbody()
        {
            try
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error initializing Rigidbody2D: {ex.Message}");
            }
        }

        private void DestroyArrowAfterTime(float time)
        {
            try
            {
                Destroy(gameObject, time);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error scheduling arrow destruction: {ex.Message}");
            }
        }

        private void TryRotateArrowBasedOnVelocity()
        {
            try
            {
                if (_rigidbody2D.velocity != Vector2.zero)
                {
                    float angle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error rotating the arrow: {ex.Message}");
            }
        }

        public void Attack(Collision2D other)
        {
            try
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    TryAttackEnemy(other);
                }
                else if (other.gameObject.CompareTag("Heart"))
                {
                    TryAttackHeart(other);
                }
                else if (other.gameObject.CompareTag("Wall"))
                {
                    StopArrow();
                }
                else if (other.gameObject.CompareTag("Armor"))
                {
                    TryAttackArmor(other);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error handling collision: {ex.Message}");
            }
        }

        private void TryAttackEnemy(Collision2D other)
        {
            try
            {
                if (other.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Enemy component not found on the collided object.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error attacking enemy: {ex.Message}");
            }
        }

        private void TryAttackHeart(Collision2D other)
        {
            try
            {
                if (other.gameObject.TryGetComponent(out Heart heart))
                {
                    heart.Die();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Heart component not found on the collided object.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error attacking heart: {ex.Message}");
            }
        }

        private void TryAttackArmor(Collision2D other)
        {
            try
            {
                if (other.gameObject.TryGetComponent(out Armor armor))
                {
                    armor.Die();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Armor component not found on the collided object.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error attacking armor: {ex.Message}");
            }
        }

        private void StopArrow()
        {
            try
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.isKinematic = true; 
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error stopping the arrow: {ex.Message}");
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
