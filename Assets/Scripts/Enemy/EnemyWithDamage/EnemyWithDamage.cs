using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.EnemyWithDamage
{
    public class EnemyWithDamage : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private Player _target;
        [SerializeField] private List<ItemsData> _itemsDatas;
        [SerializeField] private ItemsData _experience;

        private Animator _animator;
        private int _currentHealth;

        public GameObject coin;
        public Player Target => _target;
        public event UnityAction<int, int> EnemyHealthCheanged;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            EnemyHealthCheanged?.Invoke(_currentHealth, _health);

            if (_currentHealth <= 0)
            {
                //_animator.SetTrigger("Die");
                ItemDrop();
                Experiens();

                Destroy(gameObject, 0.8f);
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

        private GameObject Experiens()
        {
            GameObject exp = Instantiate(_experience.Prefab, transform.position, Quaternion.identity);
            exp.GetComponent<Items>().GetItemId(_experience.TypeID);
            exp.GetComponent<Items>().Count = _experience.Value;
            return exp;
        }
    }
}
