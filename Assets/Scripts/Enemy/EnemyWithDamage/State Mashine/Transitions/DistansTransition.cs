using UnityEngine;

namespace Enemy.EnemyWithDamage.State_Mashine.Transitions
{
    public class DistansTransition : Transition
    {
        [SerializeField] private float _transitionRange;
        [SerializeField] private float _rangetSpread;

        private void Start()
        {
            _transitionRange += Random.Range(-_rangetSpread, _rangetSpread);
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
            {
                NeedTransit = true;
            }
        }
    }
}
