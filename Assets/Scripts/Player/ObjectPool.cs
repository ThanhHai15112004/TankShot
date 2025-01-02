using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> poolQueue;
    private GameObject objectPrefab;

    public void Initialize(GameObject prefab, int poolSize)
    {
        poolQueue = new Queue<GameObject>();
        objectPrefab = prefab;

        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject CreateObject()
    {
        if (poolQueue.Count == 0)
        {
            Debug.LogWarning("Object Pool is empty!");
            return null;
        }

        var obj = poolQueue.Dequeue();
        obj.SetActive(true);
        poolQueue.Enqueue(obj);
        return obj;
    }
}
