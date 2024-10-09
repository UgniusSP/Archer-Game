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
        private Transform _player;
        private Rigidbody2D _rigidbody2D;

        public void Start()
        {
            Health = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = Health;
            
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
            
        }

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
            transform.position = Vector2.MoveTowards(transform.position, _player.position, moveSpeed * Time.deltaTime);
        }

        public override void Die()
        {
            GameManager.Instance.UpdatePoints(1);
            Destroy(gameObject);
        }
    }
}
