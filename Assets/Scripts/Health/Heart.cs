using DefaultNamespace;
using UnityEngine;
using Utils;

namespace Health
{
    public class Heart : MonoBehaviour, IDieable
    {
        [SerializeField] private float heartValue;
    
        private float _timeUntilSpawn;
        private Rigidbody2D _rigidbody2D;
    
        void Start()
        {
            InitializeRigidbody();
            ScheduleAutoDestruction();
        }

        private void InitializeRigidbody()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
        }

        private void ScheduleAutoDestruction()
        {
            Destroy(gameObject, 3f);
        }
        
        public void Die()
        {
            RestorePlayerHealth();
            DestroyHeartObject();
        }

        private void RestorePlayerHealth()
        {
            Player.Player player = GetPlayer();
            if (player != null)
            {
                player.AddHealth(heartValue);
            }
        }

        private Player.Player GetPlayer()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            return playerObject != null ? playerObject.GetComponent<Player.Player>() : null;
        }

        private void DestroyHeartObject()
        {
            Destroy(gameObject);
        }
    }
}