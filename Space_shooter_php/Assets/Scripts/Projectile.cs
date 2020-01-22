using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Poolitem
{
    float projectileSpeed = 25;
    public float projectileDamage;
    float timer;

    protected override void Reset()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.right * projectileSpeed) * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > 1f)
        {
            Return();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            collision.GetComponent<Ai>().health -= projectileDamage;
            Return();
        }
    }
}
