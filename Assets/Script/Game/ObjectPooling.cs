using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private int startCount;

    private List<GameObject> objects;

    private void Start()
    {
        for(int i = 0; i < startCount; i++)
        {
            CreateNew(false);
        }
    }

    public void CreateNew(bool active)
    {
        var go = Instantiate(obj);
        go.SetActive(active);
        objects.Add(go);
    }

    public GameObject GetObject()
    {
        foreach(var go in objects)
        {
            if(!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }
        CreateNew(true);
        return objects[objects.Count - 1];
    }
}