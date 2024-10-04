using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Vector3 sliderOffset;
    [SerializeField] private float moveSpeed;
    [SerializeField] private new Rigidbody2D rigidbody2D;

    private Vector2 _movement;
    private float _health = 0f;
    
    public void Start()
    {
        _health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = _health;
        
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        sliderOffset = new Vector3(0.04f, 1.45f, 0);
    }
    
    public void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
        
        MovementInput();
    }

    public void UpdateHealth(float healthAddition)
    {
        _health += healthAddition;
        healthBar.value = _health;
        
        if(_health > maxHealth)
        {
            _health = maxHealth;
        } else if(_health <= 0f)
        {
            _health = 0f;
            PlayerDie();
            Debug.Log("Player died");
        }
    }

    private void PlayerDie()
    {
        
    }
    
    public void FixedUpdate()
    {
        rigidbody2D.velocity = _movement * moveSpeed;
    }

    private void MovementInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        _movement = new Vector2(moveX, moveY).normalized;

    }
}
