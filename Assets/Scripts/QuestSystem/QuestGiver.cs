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
    public bool AssignedQuest { get; set; }//����������� �����
    public bool Helped { get; set; }
    private Quest Quest { get; set; }// ����� ������� ���� ����� ����� � ������ �������

    [SerializeField] private GameObject quests; // in this GO we will created a Quest
    [SerializeField] private string questType;// ��� ������ �������� "KillWolfes" KillWolfes.cs
   
    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));// � ������ ����� �������� ������, �������� ������.
        _questPanel.ShowPanel(!Quest.Completed);
    }
    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Debug.Log("Quest.Completed======" + Quest.Completed);

            _questPanel.ShowPanel(!Quest.Completed);
            _finishQuest.SetActive(true);
            Quest.GiveReward();// ���� ������� ������
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
            if (!AssignedQuest && !Helped)// ���� ��� ������ � ��� �� ��������
            {
                _takeQuest.SetActive(true);
            }
            else if (AssignedQuest && !Helped)// ���� ���� ����� � ��� �� ��������
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
            if (!AssignedQuest && !Helped)// ���� ��� ������ � ��� �� ��������
            {
                AssignQuest();//��������� ����� ��� ������ � �������
            }
        }
    }
}
