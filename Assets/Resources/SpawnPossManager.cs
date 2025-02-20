using UnityEngine;

public class SpawnPossManager : MonoBehaviour
{

    public static SpawnPossManager spm;
    private int lastIndex = -1;

    private void Awake()
    {
        spm = this;
    }

    public Vector3 GetSpawnPos()
    {
        if (transform.childCount == 0)
        {
            Debug.LogWarning("No spawn positions available!");
            return Vector3.zero;
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, transform.childCount);
        } while (randomIndex == lastIndex);

        lastIndex = randomIndex;
        return transform.GetChild(randomIndex).position;
    }
}