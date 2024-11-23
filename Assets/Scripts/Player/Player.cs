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
        [SerializeField] private float maxArmor = 50f;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider armorBar;
        [SerializeField] private Slider bowPowerSlider;
        [SerializeField] private Vector3 healthSliderOffset;
        [SerializeField] private Vector3 armorSliderOffset;
        [SerializeField] private Vector3 bowSliderOffset;
        [SerializeField] private float moveSpeed;
        [SerializeField] private new Rigidbody2D rigidbody2D;

        private Vector2 _movement;
        private float _health;
        private float _armor;

        public void Start()
        {
            _health = maxHealth;
            _armor = 0f;
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

            Vector3 screenPositionForArmorBar = Camera.main.WorldToScreenPoint(transform.position + armorSliderOffset);
            armorBar.transform.position = screenPositionForArmorBar;
            
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
            if (_armor > 0)
            {
                _armor -= damage;
            }
            else 
            {
                _health -= damage;
            }

            armorBar.value = _armor;
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

        public void AddArmor(float armorValue)
        {
            _armor += armorValue;
            if (_armor > maxArmor)
            {
                _armor = maxArmor;
            }
        }

        public void UpdateArmorBar(float armorValue)
        {
            armorBar.maxValue = maxArmor;
            armorBar.value = armorValue;
        }

        public void Deconstruct(out Rigidbody2D rigidbody2D, out float health, out float armor)
        {
            rigidbody2D = this.rigidbody2D;
            health = _health;
            armor = _armor;
        }

    }
}