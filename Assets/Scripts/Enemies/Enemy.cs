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
        InitializeHealth();
        InitializeHealthBar();
        LocatePlayer();
        InitializeRigidBody();
    }

    private void InitializeHealth()
    {
        Health = maxHealth;
    }

    private void InitializeHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = Health;
    }

    private void LocatePlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void InitializeRigidBody()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SetRigidBodyToKinematic();
    }

    private void SetRigidBodyToKinematic()
    {
        Rigidbody2D.isKinematic = true;
    }

    public void TakeDamage(float damage)
    {
        ReduceHealth(damage);
        UpdateHealthBar();
        CheckHealth();
    }

    private void ReduceHealth(float damage)
    {
        Health -= damage;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = Health;
    }

    private void CheckHealth()
    {
        if (IsHealthDepleted())
        {
            Die();
        }
    }

    private bool IsHealthDepleted()
    {
        return Health <= 0;
    }

    public abstract void Die();
    
    public abstract void Attack(Collision2D other);
    
    public abstract void Move();
}