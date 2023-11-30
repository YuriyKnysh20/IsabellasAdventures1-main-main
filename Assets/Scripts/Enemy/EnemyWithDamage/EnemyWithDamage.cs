using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Script.Enemy.EnemyWithDamage
{
    public class EnemyWithDamage : MonoBehaviour, IEnemy
    {
        public int ID { get; set; } // for quest system
        [SerializeField] private int _health;
        [SerializeField] private Player _target;
        [SerializeField] private List<ItemsData> _itemsDatas;
        [SerializeField] private ItemsData _experience;
        [SerializeField] private AudioSource soundSource;
        [SerializeField] private AudioClip WolfDamage;
        [SerializeField] private AudioClip WolfDie;
        [SerializeField] public float _life;

        private Animator _animator;
        private int _currentHealth;
        public Player Target => _target;
        public event UnityAction<int, int> EnemyHealthCheanged;
        public event UnityAction<int, int> EnemyHealthTextChanged;
        private void Start()
        {
            ID = 0;// for QUESTSYSTEM
            EnemyHealthTextChanged?.Invoke(_currentHealth, _health);
        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _health;
        }
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            soundSource.PlayOneShot(WolfDamage);
            EnemyHealthCheanged?.Invoke(_currentHealth, _health);
            EnemyHealthTextChanged?.Invoke(_currentHealth, _health);

            if (_currentHealth <= 0)
            {
                StartCoroutine(StartDie());

            }
        }
        private IEnumerator StartDie()
        {
            soundSource.PlayOneShot(WolfDie);
            yield return new WaitForSeconds(_life);

            Experience();
            ItemDrop();

            Destroy(gameObject, 0.3f);

            QuestEvents.EnemyDied(this); // for qs
            yield break;
        }
        private GameObject ItemDrop()
        {
            int randomIndex = Random.Range(0, _itemsDatas.Count);
            ItemsData data = _itemsDatas[randomIndex];

            GameObject item = Instantiate(data.Prefab, transform.position, Quaternion.identity);
            item.GetComponent<Items>().GetItemId(data.TypeID);
            item.GetComponent<Items>().Count = data.Value;

            return item;
        }
        private GameObject Experience()
        {
            GameObject exp = Instantiate(_experience.Prefab, transform.position, Quaternion.identity);
            exp.GetComponent<Items>().GetItemId(_experience.TypeID);
            exp.GetComponent<Items>().Count = _experience.Value;
            return exp;
        }

        #region ForKnockBack with trigger if you dont want to use AttackState.cs
        // now realization in Attack state.
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        _target.KBCounter = _target.KBTotalTime;
        //        if (other.transform.position.x <= transform.position.x)// if player on the left hit from the right
        //        {
        //            _target.KnockFromRight = true;
        //        }
        //        if (other.transform.position.x > transform.position.x)// if player on the left hit from the right
        //        {
        //            _target.KnockFromRight = false;
        //        }
        //    }
        //}
        #endregion
    }
}
