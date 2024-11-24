using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Enemies
{
    public class Boss : Enemy
    {
        private float _canAttack;

        public void Update()
        {
            try
            {
                UpdateHealthBarPosition();
                MoveTowardsPlayer();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating boss state: {ex.Message}");
            }
        }

        private void UpdateHealthBarPosition()
        {
            try
            {
                healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating health bar position: {ex.Message}");
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            try
            {
                Attack(other);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during collision attack: {ex.Message}");
            }
        }

        public override void Attack(Collision2D other)
        {
            try
            {
                if (IsPlayer(other) && CanAttack())
                {
                    DealDamageToPlayer(other);
                    ResetAttackCooldown();
                }
                else
                {
                    IncreaseAttackCooldown();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error executing attack: {ex.Message}");
            }
        }

        private bool IsPlayer(Collision2D other)
        {
            try
            {
                return other.gameObject.CompareTag("Player");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error checking if collider is player: {ex.Message}");
                return false;
            }
        }

        private bool CanAttack()
        {
            return attackSpeed <= _canAttack;
        }

        private void DealDamageToPlayer(Collision2D other)
        {
            try
            {
                other.gameObject.GetComponent<Player.Player>().TakeDamage(damage: attackDamage);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error dealing damage to player: {ex.Message}");
            }
        }

        private void ResetAttackCooldown()
        {
            _canAttack = 0f;
        }

        private void IncreaseAttackCooldown()
        {
            _canAttack += Time.deltaTime;
        }

        public override void Move()
        {
            try
            {
                MoveTowardsPlayer();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error moving boss: {ex.Message}");
            }
        }

        private void MoveTowardsPlayer()
        {
            try
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error moving towards player: {ex.Message}");
            }
        }

        public override void Die()
        {
            try
            {
                DestroyBoss();
                NotifyLevelComplete();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error on boss death: {ex.Message}");
            }
        }

        private void DestroyBoss()
        {
            try
            {
                Destroy(gameObject);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error destroying boss object: {ex.Message}");
            }
        }

        private void NotifyLevelComplete()
        {
            try
            {
                GameManager.Instance.LevelComplete();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error notifying level complete: {ex.Message}");
            }
        }
    }
}
