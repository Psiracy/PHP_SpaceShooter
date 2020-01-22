using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ValueToChance
{
    movementSpeed,
    attackSpeed,
    damage,
    health
}

public class CockpitHUDManager : MonoBehaviour
{
    [SerializeField]
    Text time, day, date, location, movspeed, atkspeed, damage, health, credits, movspeedpricetxt, atkspeedpricetxt, damagepricetxt, healthpricetxt, faction;
    [SerializeField]
    string[] knownLoctions, factions;
    int year;
    ValueHolder holder;
    string json;
    int movspeedprice, atkspeedprice, damageprice, healthprice, factionNUm;
    void Start()
    {
        location.text = knownLoctions[UnityEngine.Random.Range(0, knownLoctions.Length)];
        year = DateTime.Today.Year + UnityEngine.Random.Range(140, 1240);
        holder = GameObject.Find("holder").GetComponent<ValueHolder>();
        Value values = new Value()
        {
            movementSpeed = holder.movspeedlvl,
            attackSpeed = holder.atkspeedlvl,
            damage = holder.dmg,
            health = holder.hp,
            credits = holder.credits,
            username = holder.username
        };
        json = JsonUtility.ToJson(values);
        StartCoroutine(UpdateScoresRequest());
        factionNUm = holder.faction;
        if (factionNUm == 0)
        {
            factionNUm = 1;
        }
    }

    private void Update()
    {
        movspeed.text = "Movement speed: " + holder.movspeedlvl.ToString();
        atkspeed.text = "Attack speed: " + holder.atkspeedlvl.ToString();
        damage.text = "Damage: " + holder.dmg.ToString();
        health.text = "Health: " + holder.hp.ToString();
        credits.text = "Credits: " + holder.credits.ToString();
        time.text = DateTime.Now.ToString("HH:mm:ss");
        day.text = DateTime.Now.DayOfWeek.ToString();
        date.text = string.Format("{0} {1} {2}", DateTime.Today.Day, DateTime.Today.ToString("MMM"), year);
        movspeedprice = 250 * holder.movspeedlvl;
        atkspeedprice = 250 * holder.atkspeedlvl;
        damageprice = 250 * holder.dmg;
        healthprice = 250 * holder.hp;
        movspeedpricetxt.text = "price: " + movspeedprice.ToString();
        atkspeedpricetxt.text = "price: " + atkspeedprice.ToString();
        damagepricetxt.text = "price: " + damageprice.ToString();
        healthpricetxt.text = "price: " + healthprice.ToString();
        faction.text = string.Format("Enemy faction: {0}({1})", factions[factionNUm - 1], factionNUm);
    }

    public void AddLVL(int num)
    {
        ValueToChance value = (ValueToChance)num;
        Value values = new Value();
        switch (value)
        {
            case ValueToChance.movementSpeed:
                if (holder.credits < movspeedprice)
                {
                    return;
                }
                values.movementSpeed = holder.movspeedlvl + 1;
                values.attackSpeed = holder.atkspeedlvl;
                values.damage = holder.dmg;
                values.health = holder.hp;
                values.credits = holder.credits - movspeedprice;
                break;
            case ValueToChance.attackSpeed:
                if (holder.credits < atkspeedprice)
                {
                    return;
                }
                values.movementSpeed = holder.movspeedlvl;
                values.attackSpeed = holder.atkspeedlvl + 1;
                values.damage = holder.dmg;
                values.health = holder.hp;
                values.credits = holder.credits - atkspeedprice;
                break;
            case ValueToChance.damage:
                if (holder.credits < damageprice)
                {
                    return;
                }
                values.movementSpeed = holder.movspeedlvl;
                values.attackSpeed = holder.atkspeedlvl;
                values.damage = holder.dmg + 1;
                values.health = holder.hp;
                values.credits = holder.credits - damageprice;
                break;
            case ValueToChance.health:
                if (holder.credits < healthprice)
                {
                    return;
                }
                values.movementSpeed = holder.movspeedlvl;
                values.attackSpeed = holder.atkspeedlvl;
                values.damage = holder.dmg;
                values.health = holder.hp + 1;
                values.credits = holder.credits - healthprice;
                break;
            default:
                break;
        }
        values.username = holder.username;
        json = JsonUtility.ToJson(values);
        StartCoroutine(UpdateScoresRequest());
    }

    IEnumerator UpdateScoresRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("json", json);
        WWW www = new WWW("http://127.0.0.1/edsa-space/updateScores.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Response response = JsonUtility.FromJson<Response>(www.text);
            holder.credits = response.credits;
            holder.movspeedlvl = response.movementSpeed;
            holder.atkspeedlvl = response.attackSpeed;
            holder.dmg = response.damage;
            holder.hp = response.health;
        }
    }

    public void StartGame()
    {
        holder.faction = factionNUm;
        SceneManager.LoadScene(3);
    }

    public void AddDifficulty()
    {
        if (factionNUm <= 12)
        {
            factionNUm++;
        }
    }

    public void LowerDifficulty()
    {
        if (factionNUm - 1 >= 0)
        {
            factionNUm--;
        }
    }
}


[System.Serializable]
public class Value
{
    public int credits;
    public int movementSpeed;
    public int attackSpeed;
    public int damage;
    public int health;
    public string username;
    public string status;
}
