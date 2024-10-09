using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public abstract class Enemy : MonoBehaviour, IAttackable, IDamagable, IDieable, IMovable
{
    
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Slider healthBar;
    [SerializeField] protected Vector3 sliderOffset;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected CircleCollider2D circleCollider2D;

    protected float Health;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.value = Health;
        if (Health <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
    
    public abstract void Attack(Collision2D other);
    
    public abstract void Move();
    
}