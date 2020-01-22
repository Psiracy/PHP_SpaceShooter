using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    ObjectPool pool;
    [SerializeField]
    Transform projectileSpawnPoint;
    float timer;
    float attackSpeed = 1;
    float attackPower = 10;
    float speedMultiplier;
    float attackMultiplier;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            timer += Time.deltaTime * (attackSpeed + speedMultiplier);
            // Debug.Log(timer);
            if (timer >= 1)
            {
                pool.InitItem(projectileSpawnPoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), (attackPower + attackMultiplier));
                timer = 0;
            }
        }
    }

    public void SetAtkSpeed(int attackspeedlevel)
    {
        speedMultiplier = (attackspeedlevel * .7f);
    }

    public void SetDamage(int damagelevel)
    {
        attackMultiplier = (damagelevel * 1.2f);
    }
}
