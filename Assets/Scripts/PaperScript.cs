using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class PaperScript : MonoBehaviour
{
    [SerializeField] private LevelGoals levelGoals;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetPapersCount.IncreaseCollectedPapers();//++ zapizki                    
            CheckAllPapersCollected();
            SetCollected(true);// Помечаем записку как собранную (необязательно, но может быть полезно в будущих версиях)
            Destroy(gameObject);// destroy paper
        }
    }
    private void SetCollected(bool value)
    {
    }
    private void CheckAllPapersCollected()
    {
        if (GetPapersCount.AreAllPapersCollected())
        {
            levelGoals.SetPapersCompleted(true);// Если все записки собраны, устанавливаем _isPapersCompleted в true
        }
    }
}
