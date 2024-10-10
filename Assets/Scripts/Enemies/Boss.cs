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
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
            Move();
        }
    
        private void OnCollisionStay2D(Collision2D other)
        {
            Attack(other);
        }
    
        public override void Attack(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (attackSpeed <= _canAttack)
                {
                    other.gameObject.GetComponent<Player.Player>().TakeDamage(damage: attackDamage);
                    _canAttack = 0f;
                }
                else
                {
                    _canAttack += Time.deltaTime;    
                }
            }
        }
    
        public override void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
        }

        public override void Die()
        {
            Destroy(gameObject);
            GameManager.Instance.LevelComplete();
        }
    }
}
