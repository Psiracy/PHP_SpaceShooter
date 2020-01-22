using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : Poolitem
{
    float movementSpeed = 4;
    float angle;
    float radius = 18;
    public float health = 50;
    float fullhealth = 50;
    Vector2 curentMoveTarget;
    ScreenShake screenShake;
    ValueHolder holder;
    public GameObject explosion;
    float vieuw = 12;
    public ObjectPool bulletPool;
    GameObject player;
    float timer;
    int attackSpeed = 1;
    float speedMultiplier;
    int attackPower = 1;
    int attackMultiplier = 1;
    protected override void Reset()
    {
        health = fullhealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0, 360);
        screenShake = Camera.main.GetComponent<ScreenShake>();
        holder = GameObject.Find("holder").GetComponent<ValueHolder>();
        player = GameObject.FindGameObjectWithTag("Player");
        health += holder.faction * 10;
        fullhealth = health;
        attackMultiplier = holder.faction;
        speedMultiplier = holder.faction / 5;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if (Vector3.Distance(transform.position, player.transform.position) > 3)
        {
            transform.position += (transform.right * movementSpeed) * Time.deltaTime;
        }

        //rotation
        if (Vector3.Distance(transform.position, player.transform.position) > 8)
        {
            if (Random.Range(0, 100) <= 5)
            {
                angle = Random.Range(0, 360);
            }

            if (Vector3.Distance(transform.position, Vector3.zero) >= radius)
            {
                Vector3 distance = Vector3.zero - transform.position;
                float z = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, z), Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime);
            }
        }
        else
        {
            Vector3 newDir = player.transform.position - transform.position;
            newDir.Normalize();
            float angle = Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            timer += Time.deltaTime * (attackSpeed + speedMultiplier);
            // Debug.Log(timer);
            if (timer >= 1)
            {
                bulletPool.InitItem(transform.position + (transform.forward * 4), Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), (attackPower + attackMultiplier));
                timer = 0;
            }
        }

        //death
        if (health <= 0)
        {
            holder.credits += 25 * holder.faction;
            Instantiate(explosion, transform.position, Quaternion.identity);
            screenShake.Shake(transform.position);
            Return();
        }
    }
}
