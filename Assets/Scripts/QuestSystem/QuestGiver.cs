using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    #region UIELEMENTS
    [SerializeField] private GameObject _takeQuest;
    [SerializeField] private GameObject _inProcessQuest;
    [SerializeField] private GameObject _finishQuest;
    [SerializeField] private GameObject _completedQuest;
    [SerializeField] private GameObject _QuestRewardPanel;
    [SerializeField] private QuestSystemUI _questPanel;
    #endregion
    public bool AssignedQuest { get; set; }//Назначенный квест
    public bool Helped { get; set; }
    private Quest Quest { get; set; }// квест который дает квест гивер в журнал квестов

    [SerializeField] private GameObject quests; // in this GO we will created a Quest
    [SerializeField] private string questType;// имя квеста например "KillWolfes" KillWolfes.cs
   
    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));// в скобки нужно передать строку, название квеста.
        _questPanel.ShowPanel(!Quest.Completed);
    }
    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Debug.Log("Quest.Completed======" + Quest.Completed);

            _questPanel.ShowPanel(!Quest.Completed);
            _finishQuest.SetActive(true);
            Quest.GiveReward();// даем награду игроку
            _QuestRewardPanel.SetActive(true);
            Helped = true;
            AssignedQuest = false;
        }
        else
        {
            _inProcessQuest.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AssignedQuest && !Helped)// если нет квеста и еще не выполнен
            {
                _takeQuest.SetActive(true);
            }
            else if (AssignedQuest && !Helped)// если есть квест и еще не выполнен
            {
                _takeQuest.SetActive(false);
                CheckQuest();
            }
            else
            {
                _completedQuest.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AssignedQuest && !Helped)// если нет квеста и еще не выполнен
            {
                AssignQuest();//назначаем квест при выходе с колизии
            }
        }
    }
}
