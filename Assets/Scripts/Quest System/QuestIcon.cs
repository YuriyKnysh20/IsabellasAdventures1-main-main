using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject requirementNotMetToStartIcon;
    [SerializeField] private GameObject canStartIcon;
    //[SerializeField] private GameObject requirementsNotMetToFinishIcon;
    //[SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requirementNotMetToStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        //requirementsNotMetToFinishIcon.SetActive(false);
        //canFinishIcon.SetActive(false);

        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { requirementNotMetToStartIcon.SetActive(true); }
                break;
            case QuestState.CAN_START:
                if (startPoint) { canStartIcon.SetActive(true); }
                break;
            case QuestState.INPROGRESS:
                Debug.Log("SetState is called, current state: QuestState.INPROGRESS");
                //  if (finishPoint) { requirementsNotMetToFinishIcon.SetActive(true); }
                break;
            case QuestState.CAN_FINISH:
                Debug.Log("SetState is called, current state: QuestState.CAN_FINISH");
                // if (finishPoint) { canFinishIcon.SetActive(true); }
                break;
            case QuestState.FINISHED:
                Debug.Log("SetState is called, current state: QuestState.FINISHED");
                // galochka v ui kvadrate kvestov setactive true;
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                break;
        }

    }
}
