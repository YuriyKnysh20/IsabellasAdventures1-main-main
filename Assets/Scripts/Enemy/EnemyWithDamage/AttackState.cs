using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private float _lastAttackTime;
    private Animator _animator;
    private float distance;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _animator.SetTrigger("Attack");
        target.ApplyDamage(_damage);

        #region For Knockback
        target.KBCounter = target.KBTotalTime;
        if (target.transform.position.x <= transform.position.x)// if player on the left hit from the right
        {
            target.KnockFromRight = true;
        }
        if (target.transform.position.x > transform.position.x)// if player on the left hit from the right
        {
            target.KnockFromRight = false;
        }
        #endregion
    }
}