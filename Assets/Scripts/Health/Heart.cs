using DefaultNamespace;
using UnityEngine;
using Utils;

namespace Health
{
    public class Heart : MonoBehaviour, IDieable
    {
    
        [SerializeField] private float heartValue;
        [SerializeField] private float moveSpeed;
    
        private float _timeUntilSpawn;
        private Rigidbody2D _rigidbody2D;
    
        void Start()
        {
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
            
            Destroy(gameObject, 3f);
        }
        
        public void Die()
        {
            GameManager.Instance.UpdatePoints(1);
            Player.Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
            
            player.AddHealth(heartValue);
            Destroy(gameObject);
        }
        
    }
}
