using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    float duration = .5f;
    float power = 2.4f;
    float magnitude = 0;
    float timer;
    int constant = 1;
    bool shake;

    Vector3 origin;

    private void Update()
    {
        if (shake == true)
        {
            timer += Time.deltaTime;
            if (timer <= duration)
            {
                transform.localPosition = transform.localPosition + Random.insideUnitSphere * magnitude;
            }
            else
            {
                timer = 0;
                transform.localPosition = origin;
                shake = false;
            }
        }
    }

    public void Shake(Vector3 otherpos)
    {
        magnitude = (constant / Vector3.Distance(otherpos, transform.position)) * power;
        origin = transform.localPosition;
        shake = true;
    }
}
