using System;
using DefaultNamespace;
using Health;
using UnityEngine;
using Weapon;
using Slider = UnityEngine.UI.Slider;

namespace Player
{
    public class Player : MonoBehaviour, IMovable, IDamagable, IDieable
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Vector3 sliderOffset;
        [SerializeField] private float moveSpeed;
        [SerializeField] private new Rigidbody2D rigidbody2D;

        private Vector2 _movement;
        private float _health;
    
        public void Start()
        {
            _health = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = _health;
        
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        
            sliderOffset = new Vector3(0.04f, 1.45f, 0);
        }
    
        public void Update()
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
        
            Move();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Heart"))
            {
                Heart heart = other.gameObject.GetComponent<Heart>();
                heart.Die();
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            healthBar.value = _health;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void AddHealth(float healthAddition)
        {
            _health += healthAddition;
            healthBar.value = _health;
        
            if(_health > maxHealth)
            {
                _health = maxHealth;
            } 
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    
        public void FixedUpdate()
        {
            rigidbody2D.velocity = _movement * moveSpeed;
        }
    
        public void Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            _movement = new Vector2(moveX, moveY).normalized;
        }
    }
}
