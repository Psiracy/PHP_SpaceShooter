using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    float fadetime = 1.75f;
   // float timer;
   // float timeend = 5;

    private void Start()
    {
        gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }
    public void Update()
    {
        Color tempcolor = gameObject.GetComponent<Text>().color;
        tempcolor.a = Mathf.Lerp(tempcolor.a, 0, Time.deltaTime * fadetime);
        gameObject.GetComponent<Text>().color = tempcolor;

    }

    public void StartFade(string errormessage)
    {
        gameObject.GetComponent<Text>().color = Color.red;
        gameObject.GetComponent<Text>().text = errormessage;
    }
}
