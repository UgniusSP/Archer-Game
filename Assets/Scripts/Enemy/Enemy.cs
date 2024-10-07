using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IMovable, IDamagable, IAttackable, IDieable
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxHealth;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackDamage;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Vector3 sliderOffset;
        [SerializeField] private CircleCollider2D circleCollider2D;

        private float _canAttack;
        private Transform _player;
        private float _health;
        private Rigidbody2D _rigidbody2D;
    

        public void Start()
        {
            _health = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = _health;
            
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
        
            sliderOffset = new Vector3(0.04f, 1.45f, 0);
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
    
        public void Attack(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (attackSpeed <= _canAttack)
                {
                    other.gameObject.GetComponent<Player.Player>().TakeDamage(attackDamage);
                    _canAttack = 0f;
                }
                else
                {
                    _canAttack += Time.deltaTime;    
                }
            }
        }
    
        public void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, moveSpeed * Time.deltaTime);
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
    
        public void Die()
        {
            GameManager.Instance.UpdatePoints(1);
            Destroy(gameObject);
        }

    
    }
}