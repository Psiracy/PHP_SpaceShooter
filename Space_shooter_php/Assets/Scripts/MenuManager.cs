using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    ButtonSlideEffect[] buttons;
    int donebuttoncount;
    int count;
    float timer;
    bool clicked;
    int scene;

    void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].done == true)
            {
                donebuttoncount++;
            }
        }

        if (donebuttoncount >= buttons.Length)
        {
            SceneManager.LoadScene(scene);
        }

        if (clicked == true && count < buttons.Length)
        {
            buttons[count].start = true;
            timer += Time.deltaTime;
            if (timer > .3)
            {
                count++;
                timer = 0;
            }
        }
    }

    public void SlideEffect()
    {
        clicked = true;
    }

    public void LogIn()
    {
        scene = 1;
        SlideEffect();
    }

    public void HowToPlay()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
