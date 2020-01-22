using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    ObjectPool pool, bulletPool;

    [SerializeField]
    float offsetHorizontal, offsetVertical;
    float timer = 2;
    float timerEnd = 4;
    ValueHolder holder;
    // Start is called before the first frame update
    void Start()
    {
        holder = GameObject.Find("holder").GetComponent<ValueHolder>();
        switch (holder.faction)
        {
            case 1:
                timerEnd = 4;
                break;
            case 2:
                timerEnd = 3;
                break;
            case 3:
                timerEnd = 2.5f;
                break;
            case 11:
                timerEnd = 1;
                break;
            case 12:
                timerEnd = .5f;
                break;
            default:
                timerEnd = 2;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log(timer);
        if (timer >= timerEnd)
        {
            pool.InitItem(new Vector3(Random.Range(-32, 29), Random.Range(-18, 22), 0), Quaternion.identity, bulletPool);
            timer = 0;
        }
    }
}
