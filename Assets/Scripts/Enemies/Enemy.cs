using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public abstract class Enemy : MonoBehaviour, IAttackable, IDamagable, IDieable, IMovable, ICloneable
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
        try
        {
            InitializeHealth();
            InitializeHealthBar();
            LocatePlayer();
            InitializeRigidBody();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error initializing enemy: {ex.Message}");
        }
    }

    private void InitializeHealth()
    {
        Health = maxHealth;
    }

    private void InitializeHealthBar()
    {
        try
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = Health;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error initializing health bar: {ex.Message}");
        }
    }

    private void LocatePlayer()
    {
        try
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error locating player: {ex.Message}");
        }
    }

    private void InitializeRigidBody()
    {
        try
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            SetRigidBodyToKinematic();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error initializing rigidbody: {ex.Message}");
        }
    }

    private void SetRigidBodyToKinematic()
    {
        Rigidbody2D.isKinematic = true;
    }

    public void TakeDamage(float damage)
    {
        try
        {
            ReduceHealth(damage);
            UpdateHealthBar();
            CheckHealth();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error taking damage: {ex.Message}");
        }
    }

    private void ReduceHealth(float damage)
    {
        Health -= damage;
    }

    private void UpdateHealthBar()
    {
        try
        {
            healthBar.value = Health;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error updating health bar: {ex.Message}");
        }
    }

    private void CheckHealth()
    {
        try
        {
            if (IsHealthDepleted())
            {
                Die();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error checking health: {ex.Message}");
        }
    }

    private bool IsHealthDepleted()
    {
        return Health <= 0;
    }

    public abstract void Die();

    public abstract void Attack(Collision2D other);

    public abstract void Move();

    public void Deconstruct(out float currentHealth, out float maxHealth, out float attackDamage)
    {
        currentHealth = Health;
        maxHealth = this.maxHealth;
        attackDamage = this.attackDamage;
    }

    public object Clone()
    {
        try
        {
            return this.MemberwiseClone();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error cloning enemy: {ex.Message}");
            return null;
        }
    }
}
