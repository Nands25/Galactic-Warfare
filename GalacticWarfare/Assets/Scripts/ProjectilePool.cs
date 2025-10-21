using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!pools.ContainsKey(prefab)) pools[prefab] = new Queue<GameObject>();

        Queue<GameObject> q = pools[prefab];
        GameObject obj;
        if (q.Count > 0)
        {
            obj = q.Dequeue();
            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.rotation = rot;
        }
        else
        {
            obj = Instantiate(prefab, pos, rot);
        }
        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        var prefabRef = obj; // requires a way to map to prefab; simple approach: attach PoolItem component
        var poolItem = obj.GetComponent<PoolItem>();
        if (poolItem == null) Destroy(obj);
        else
        {
            if (!pools.ContainsKey(poolItem.prefab)) pools[poolItem.prefab] = new Queue<GameObject>();
            pools[poolItem.prefab].Enqueue(obj);
        }
    }
}