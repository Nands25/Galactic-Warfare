using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    // dictionary keyed by prefab name
    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null) return null;
        string key = prefab.name;
        if (!pool.ContainsKey(key)) pool[key] = new Queue<GameObject>();

        Queue<GameObject> q = pool[key];
        GameObject instance;
        if (q.Count > 0)
        {
            instance = q.Dequeue();
            instance.SetActive(true);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
        }
        else
        {
            instance = GameObject.Instantiate(prefab, position, rotation);
            // if prefab has PoolItem, ensure it knows its prefab ref
            PoolItem pi = instance.GetComponent<PoolItem>();
            if (pi == null)
                instance.AddComponent<PoolItem>().prefab = prefab;
            else if (pi.prefab == null)
                pi.prefab = prefab;
        }
        return instance;
    }

    public void Despawn(GameObject instance)
    {
        if (instance == null) return;
        PoolItem pi = instance.GetComponent<PoolItem>();
        if (pi == null || pi.prefab == null)
        {
            // can't pool without prefab reference; destroy
            Destroy(instance);
            return;
        }
        string key = pi.prefab.name;
        if (!pool.ContainsKey(key)) pool[key] = new Queue<GameObject>();
        instance.SetActive(false);
        pool[key].Enqueue(instance);
    }
}