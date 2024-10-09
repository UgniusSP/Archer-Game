using System;
using DefaultNamespace;
using Health;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Weapon;
using Slider = UnityEngine.UI.Slider;

namespace Player
{
    public class Player : MonoBehaviour, IMovable, IDamagable, IDieable
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider bowPowerSlider;
        [SerializeField] private Vector3 healthSliderOffset;
        [SerializeField] private Vector3 bowSliderOffset;
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
            
        }
    
        public void Update()
        {
            Vector3 screenPositionForHealthBar = Camera.main.WorldToScreenPoint(transform.position + healthSliderOffset);
            healthBar.transform.position = screenPositionForHealthBar;
            
            Vector3 screenPositionForChargeBar = Camera.main.WorldToScreenPoint(transform.position + bowSliderOffset);
            bowPowerSlider.transform.position = screenPositionForChargeBar;
        
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
            GameManager.Instance.GameOver();
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
