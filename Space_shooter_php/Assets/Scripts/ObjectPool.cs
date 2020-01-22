using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject PooledItem;
    public int PoolSize;

    List<Poolitem> PooledItems;

    [SerializeField]
    GameObject explosion;

    void Start()
    {
        PooledItems = new List<Poolitem>();

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject item = Instantiate(PooledItem);
            AddItem(item.GetComponent<Poolitem>());
            PooledItems[i].ObjectPool = this;
            if (item.tag == "enemy")
            {
                item.GetComponent<Ai>().explosion = explosion;
            }
        }
    }

    public void AddItem(Poolitem item)
    {
        PooledItems.Add(item);
        item.transform.parent = transform;
        item.gameObject.SetActive(false);
    }

    public GameObject InitItem(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (PooledItems.Count <= 0)
        {
            Debug.LogError("No items in pool");
            return null;
        }

        PooledItems[0].Spawn(position, rotation, parent);
        GameObject item = PooledItems[0].gameObject;
        PooledItems.RemoveAt(0);
        return item;
    }

    public GameObject InitItem(Vector3 position, Quaternion rotation, float damage, Transform parent = null)
    {
        if (PooledItems.Count <= 0)
        {
            Debug.LogError("No items in pool");
            return null;
        }

        PooledItems[0].Spawn(position, rotation, parent);
        GameObject item = PooledItems[0].gameObject;
        if (item.GetComponent<Projectile>())
        {
            item.GetComponent<Projectile>().projectileDamage = damage;
        }
        else
        {
            item.GetComponent<EnemyBullet>().projectileDamage = damage;
        }
        PooledItems.RemoveAt(0);
        return item;
    }

    public GameObject InitItem(Vector3 position, Quaternion rotation, ObjectPool pool, Transform parent = null)
    {
        if (PooledItems.Count <= 0)
        {
            Debug.LogError("No items in pool");
            return null;
        }

        PooledItems[0].Spawn(position, rotation, parent);
        GameObject item = PooledItems[0].gameObject;
        item.GetComponent<Ai>().bulletPool = pool;
        PooledItems.RemoveAt(0);
        return item;
    }
}
