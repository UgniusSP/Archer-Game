using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Enemies
{
    public class EyeEnemy : Enemy
    {
        private float _canAttack;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private GameObject armorPrefab;

        public void Update()
        {
            UpdateHealthBarPosition();
            MoveTowardsPlayer();
        }

        private void UpdateHealthBarPosition()
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            AttemptAttack(other);
        }

        private void AttemptAttack(Collision2D other)
        {
            Attack(other);
        }

        public override void Attack(Collision2D other)
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

        private bool IsPlayer(Collision2D other)
        {
            return other.gameObject.CompareTag("Player");
        }

        private bool CanAttack()
        {
            return attackSpeed <= _canAttack;
        }

        private void DealDamageToPlayer(Collision2D other)
        {
            other.gameObject.GetComponent<Player.Player>().TakeDamage(damage: attackDamage);
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
            MoveTowardsPlayer();
        }

        private void MoveTowardsPlayer()
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
        }

        public override void Die()
        {
            GrantPlayerReward();
            DropArmor();
            DestroyEnemy();
        }

        private void GrantPlayerReward()
        {
            GameManager.Instance.UpdatePoints(1);
        }

        private void DropArmor()
        {
            Instantiate(armorPrefab, transform.position, Quaternion.identity);
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}
