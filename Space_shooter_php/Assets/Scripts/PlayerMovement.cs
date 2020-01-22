using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    [SerializeField]
    Image healthBar;
    float verticalMovement;
    float horizontalMovement;
    float speedMultiplier;
    float movementSpeed = 4;
    public float health = 100;
    float totalhealth;
    void Update()
    {
        //movement
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(verticalMovement, horizontalMovement, 0);
        if (movement != Vector3.zero)
        {
            transform.position += (transform.TransformDirection(movement) * (movementSpeed + speedMultiplier)) * Time.deltaTime;
        }

        //rotation
        Vector2 target = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 deltaPosition = target - (Vector2)transform.position;
        float distance = deltaPosition.magnitude;
        if (distance > .1f)
        {
            transform.eulerAngles = Vector3.forward * Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
        }

        //healthbar
        healthBar.fillAmount = health / totalhealth;

        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void SetSpeedLvl(int speedlvl)
    {
        speedMultiplier = (speedlvl * .3f);
    }

    public void SetHealthlvl(int healthlvl)
    {
        health = (100 + (25 * healthlvl)) - 25;
        totalhealth = health;
    }
}
