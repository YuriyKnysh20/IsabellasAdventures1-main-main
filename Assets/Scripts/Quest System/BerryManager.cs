using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BerryManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private int startingGold = 5;

        public int currentGold { get; private set; }

        private void Awake()
        {
            currentGold = startingGold;
        }

        private void OnEnable()
        {
          //  GameEventsManager.Instance.goldEvents.onGoldGained += GoldGained;
        }

        private void OnDisable()
        {
          //  GameEventsManager.Instance.goldEvents.onGoldGained -= GoldGained;
        }

        private void Start()
        {
           // GameEventsManager.Instance.goldEvents.GoldChange(currentGold);
        }

        private void GoldGained(int gold)
        {
            currentGold += gold;
           // GameEventsManager.Instance.goldEvents.GoldChange(currentGold);
        }
    }
