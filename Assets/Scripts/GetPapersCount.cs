using UnityEngine;

public class GetPapersCount : MonoBehaviour
{
    private static int collectedPapers = 0;
    private static int totalPapers = 0;

    private void Start()
    {
        CountPapers();
    }

    private void CountPapers()
    {
        GameObject[] papersObjects = GameObject.FindGameObjectsWithTag("Papers");
        totalPapers = papersObjects.Length;

        Debug.Log($"Found {totalPapers} papers in the scene.");
    }

    public static void IncreaseCollectedPapers()
    {
        collectedPapers++;
    }

    public static bool AreAllPapersCollected()
    {
        return collectedPapers == totalPapers;
    }
}
