using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    private Vector3[] originalPositions;
    private Transform[] objectsToReset;

    void Start()
    {

        GameObject[] resettableObjects = GameObject.FindGameObjectsWithTag("Resettable");
        objectsToReset = new Transform[resettableObjects.Length];
        originalPositions = new Vector3[resettableObjects.Length];

        for (int i = 0; i < resettableObjects.Length; i++)
        {
            objectsToReset[i] = resettableObjects[i].transform;
            originalPositions[i] = resettableObjects[i].transform.position;
        }
    }

    public void ResetObjects()
    {

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].position = originalPositions[i];
        }
    }
}
