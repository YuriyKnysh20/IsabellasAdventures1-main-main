using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestTest : MonoBehaviour
{
    [SerializeField] private GameObject QuestSystem;
    private QuestSystem _questSystem;
    public event UnityAction<int, int> TabletChanged;
    private void Start()
    {
        _questSystem = QuestSystem.GetComponent<QuestSystem>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _questSystem.TabletLeftToCollect--;

            TabletChanged?.Invoke(_questSystem.TabletLeftToCollect, _questSystem.TabletCount);
            Debug.Log("sobrano aptechek:");
        }
        Destroy(gameObject);
    }
}
