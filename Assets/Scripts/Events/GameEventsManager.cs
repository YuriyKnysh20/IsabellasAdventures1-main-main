using UnityEngine;
using UnityEngine.UIElements;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;

    private void Awake()
    {
        if (Instance != null)
        {
        }
        Instance = this;

        inputEvents = new InputEvents();
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
    }
}
