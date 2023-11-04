using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }
    public MiscEvents miscEvents;
    public QuestEvents questEvents;

    private void Awake()
    {
        if (Instance != null)
        {
        }
        Instance = this;

        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
    }
}
