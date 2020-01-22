using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField]
    InputField username, password;
    [SerializeField]
    Fade fade;
    Request request;
    string json;

    // Use this for initialization
    void Start()
    {
        request = new Request();
    }

    // Update is called once per frame
    void Update()
    {
        request.username = username.text;
        request.password = password.text;
    }

    public void PlayerLogin()
    {
        json = JsonUtility.ToJson(request);
        StartCoroutine(LoginRequest());
    }

    public void PlayerSignUp()
    {
        json = JsonUtility.ToJson(request);
        StartCoroutine(SignUpRequest());
    }

    IEnumerator LoginRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("json", json);
        WWW www = new WWW("http://127.0.0.1/edsa-space/login.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Response response = JsonUtility.FromJson<Response>(www.text);
            if (response.status == "logged in")
            {
                GameObject holder = new GameObject();
                ValueHolder hold = holder.AddComponent<ValueHolder>();
                hold.credits = response.credits;
                hold.movspeedlvl = response.movementSpeed;
                hold.atkspeedlvl = response.attackSpeed;
                hold.dmg = response.damage;
                hold.hp = response.health;
                hold.username = username.text;
                holder.name = "holder";
                DontDestroyOnLoad(holder);
                SceneManager.LoadScene(2);
            }
            else
            {
                fade.StartFade(response.status);
            }
        }
    }

    IEnumerator SignUpRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("json", json);
        WWW www = new WWW("http://127.0.0.1/edsa-space/signup.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Response response = JsonUtility.FromJson<Response>(www.text);
            fade.StartFade(response.status);
        }

    }
}

[System.Serializable]
public class Request
{
    public string username;
    public string password;
}

[System.Serializable]
public class Response
{
    public string status;
    public int credits;
    public int movementSpeed;
    public int attackSpeed;
    public int damage;
    public int health;
}
