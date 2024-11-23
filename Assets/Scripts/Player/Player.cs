using System;
using DefaultNamespace;
using Health;
using UnityEngine;
using UnityEngine.UI;
using Weapon;
using Utils;

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
            try
            {
                InitializePlayerStats();
                SetupRigidBody();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during Start: {ex.Message}");
            }
        }

        private void InitializePlayerStats()
        {
            try
            {
                _health = maxHealth;
                _armor = 0f;
                healthBar.maxValue = maxHealth;
                healthBar.value = _health;
                armorBar.maxValue = maxArmor;
                armorBar.value = _armor;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error initializing player stats: {ex.Message}");
            }
        }

        private void SetupRigidBody()
        {
            try
            {
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error setting up Rigidbody: {ex.Message}");
            }
        }

        public void Update()
        {
            try
            {
                UpdateHealthBarPosition();
                UpdateBowPowerSliderPosition();
                UpdateArmorBarPosition();
                Move();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during Update: {ex.Message}");
            }
        }

        private void UpdateHealthBarPosition()
        {
            try
            {
                Vector3 screenPositionForHealthBar = Camera.main.WorldToScreenPoint(transform.position + healthSliderOffset);
                healthBar.transform.position = screenPositionForHealthBar;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating health bar position: {ex.Message}");
            }
        }

        private void UpdateBowPowerSliderPosition()
        {
            try
            {
                Vector3 screenPositionForChargeBar = Camera.main.WorldToScreenPoint(transform.position + bowSliderOffset);
                bowPowerSlider.transform.position = screenPositionForChargeBar;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating bow power slider position: {ex.Message}");
            }
        }

        private void UpdateArmorBarPosition()
        {
            try
            {
                Vector3 screenPositionForArmorBar = Camera.main.WorldToScreenPoint(transform.position + armorSliderOffset);
                armorBar.transform.position = screenPositionForArmorBar;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating armor bar position: {ex.Message}");
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            try
            {
                if (other.gameObject.CompareTag("Heart"))
                {
                    HandleHeartCollision(other);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during collision: {ex.Message}");
            }
        }

        private void HandleHeartCollision(Collision2D other)
        {
            try
            {
                Heart heart = other.gameObject.GetComponent<Heart>();
                heart.Die();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error handling heart collision: {ex.Message}");
            }
        }

        public void TakeDamage(float damage)
        {
            try
            {
                if (_armor > 0)
                {
                    _armor -= damage;
                }
                else
                {
                    _health -= damage;
                }

                UpdateBars();

                if (_health <= 0)
                {
                    Die();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error taking damage: {ex.Message}");
            }
        }

        private void UpdateBars()
        {
            try
            {
                armorBar.value = _armor;
                healthBar.value = _health;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating bars: {ex.Message}");
            }
        }

        public void AddHealth(float healthAddition)
        {
            try
            {
                _health += healthAddition;
                if (_health > maxHealth)
                {
                    _health = maxHealth;
                }

                healthBar.value = _health;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error adding health: {ex.Message}");
            }
        }

        public void Die()
        {
            try
            {
                GameManager.Instance.GameOver();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during death: {ex.Message}");
            }
        }

        public void FixedUpdate()
        {
            try
            {
                UpdateMovement();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during FixedUpdate: {ex.Message}");
            }
        }

        private void UpdateMovement()
        {
            try
            {
                rigidbody2D.velocity = _movement * moveSpeed;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating movement: {ex.Message}");
            }
        }

        public void Move()
        {
            try
            {
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");

                _movement = new Vector2(moveX, moveY).normalized;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during movement: {ex.Message}");
            }
        }

        public void AddArmor(float armorValue)
        {
            try
            {
                _armor += armorValue;
                if (_armor > maxArmor)
                {
                    _armor = maxArmor;
                }

                armorBar.value = _armor;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error adding armor: {ex.Message}");
            }
        }

        public void UpdateArmorBar(float armorValue)
        {
            try
            {
                armorBar.maxValue = maxArmor;
                armorBar.value = armorValue;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating armor bar: {ex.Message}");
            }
        }

        public void Deconstruct(out Rigidbody2D rigidbody2D, out float health, out float armor)
        {
            rigidbody2D = this.rigidbody2D;
            health = _health;
            armor = _armor;
        }
    }
}
