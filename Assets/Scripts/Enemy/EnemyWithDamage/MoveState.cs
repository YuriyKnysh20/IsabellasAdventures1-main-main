using Enemy.EnemyWithDamage.State_Mashine;
using UnityEngine;

namespace Enemy.EnemyWithDamage
{
    [RequireComponent(typeof(Animator))]
    public class MoveState : State
    {
        [SerializeField] private float _speed;

        private int _move;

        public Transform targetPlayer;
        public bool isFlipped = true;

        private void Update()
        {
            Move();
            LookAtPlayer();
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        }

        private void LookAtPlayer()
        {
            if (transform.position.x > targetPlayer.position.x && isFlipped)
            {
                Reflect(_move);
                isFlipped = true;
            }
            else if (transform.position.x < targetPlayer.position.x && !isFlipped)
            {
                Reflect(-_move);
                isFlipped = false;
            }
        }

        private void Reflect(int move)
        {
            transform.localScale = new Vector3(move, 1, 1);
        }
    }
}
