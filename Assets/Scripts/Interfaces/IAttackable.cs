using UnityEngine;

namespace DefaultNamespace
{
    public interface IAttackable
    {
        public void Attack(Collision2D other);
    }
}