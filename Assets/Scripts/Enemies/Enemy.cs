using System;
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

    protected float Health;
    protected Transform Player;
    protected Rigidbody2D Rigidbody2D;

    public void Start()
    {
        Health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = Health;
        
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.isKinematic = true;
    }

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