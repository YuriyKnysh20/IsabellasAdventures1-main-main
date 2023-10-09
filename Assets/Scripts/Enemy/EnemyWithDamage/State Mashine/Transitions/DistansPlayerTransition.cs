using UnityEngine;

namespace Enemy.EnemyWithDamage.State_Mashine.Transitions
{
    public class DistansPlayerTransition : Transition
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Health player))
            {
                Debug.Log("player " + player);
                NeedTransit = true;
            }
        }
    }
}
