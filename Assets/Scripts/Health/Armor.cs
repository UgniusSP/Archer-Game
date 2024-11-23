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
            if (collision.collider.CompareTag("Arrow"))
            {
                Arrow arrow = collision.collider.GetComponent<Arrow>();
                if (arrow != null)
                {
                    Player.Player player = FindObjectOfType<Player.Player>();
                    if (player != null)
                    {
                        player.AddArmor(armorValue);
                        player.UpdateArmorBar(armorValue);
                        Die();
                    }
                }
            } 
            else if(collision.collider.CompareTag("Enemy"))
            {
                Die();
            }
            else if(collision.collider.CompareTag("Player"))
            {
                Player.Player player = collision.collider.GetComponent<Player.Player>();
                if (player != null)
                {
                    player.AddArmor(armorValue);
                    player.UpdateArmorBar(armorValue);
                    Die();
                }
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}