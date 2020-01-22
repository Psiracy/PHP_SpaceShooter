using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSlideEffect : MonoBehaviour
{
    public bool start = false;
    public bool done = false;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (start == true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x - 600, startPos.y, startPos.z), Time.deltaTime * 2.8f);
            if (Vector3.Distance(transform.position, new Vector3(startPos.x - 600, startPos.y, startPos.z)) < 12f)
            {
                done = true;
            }
        }
    }
}
