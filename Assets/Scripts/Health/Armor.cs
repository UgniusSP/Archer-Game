using DefaultNamespace;
using UnityEngine;
using Weapon;

namespace Health
{
    public class Armor : MonoBehaviour, IDieable
    {
        [SerializeField] private float armorValue;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision);
        }

        private void HandleCollision(Collision2D collision)
        {
            if (IsArrowCollision(collision))
            {
                HandleArrowCollision(collision);
            }
            else if (IsEnemyCollision(collision))
            {
                HandleEnemyCollision();
            }
            else if (IsPlayerCollision(collision))
            {
                HandlePlayerCollision(collision);
            }
        }

        private bool IsArrowCollision(Collision2D collision)
        {
            return collision.collider.CompareTag("Arrow");
        }

        private bool IsEnemyCollision(Collision2D collision)
        {
            return collision.collider.CompareTag("Enemy");
        }

        private bool IsPlayerCollision(Collision2D collision)
        {
            return collision.collider.CompareTag("Player");
        }

        private void HandleArrowCollision(Collision2D collision)
        {
            Arrow arrow = collision.collider.GetComponent<Arrow>();
            if (arrow != null)
            {
                ApplyArmorToPlayer();
                Die();
            }
        }

        private void HandleEnemyCollision()
        {
            Die();
        }

        private void HandlePlayerCollision(Collision2D collision)
        {
            Player.Player player = collision.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                ApplyArmorToPlayer();
                Die();
            }
        }

        private void ApplyArmorToPlayer()
        {
            Player.Player player = GetPlayer();
            if (player != null)
            {
                player.AddArmor(armorValue);
            }
        }
        
        private Player.Player GetPlayer()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            return playerObject != null ? playerObject.GetComponent<Player.Player>() : null;
        }

        public void Die()
        {
            DestroyArmorObject();
        }

        private void DestroyArmorObject()
        {
            Destroy(gameObject);
        }
    }
}
