using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Script.Enemy.EnemyWithDamage
{
    public class EnemyWithDamage : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private Player _target;
        [SerializeField] private List<ItemsData> _itemsDatas;
        [SerializeField] private ItemsData _experience;

        private Animator _animator;
        private int _currentHealth;
        public Player Target => _target;
        public event UnityAction<int, int> EnemyHealthCheanged;
        public event UnityAction<int, int> EnemyHealthTextChanged;

        private void Start()
        {
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

            EnemyHealthCheanged?.Invoke(_currentHealth, _health);
            EnemyHealthTextChanged?.Invoke(_currentHealth, _health);

            if (_currentHealth <= 0)
            {
                Experience();
                ItemDrop();

                Destroy(gameObject, 0.3f);
            }
        }

        private GameObject ItemDrop()
        {
            int randomIndex = Random.Range(0, _itemsDatas.Count);
            ItemsData data = _itemsDatas[randomIndex];
        
            GameObject item= Instantiate(data.Prefab, transform.position, Quaternion.identity);
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
    }
}
