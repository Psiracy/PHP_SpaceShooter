using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int movspdlvl, atkspdlvl, dmglvl, hplvl;
    [SerializeField]
    PlayerAttack playerAttack;
    [SerializeField]
    PlayerMovement playerMovement;
    ValueHolder holder;
    // Start is called before the first frame update
    void Start()
    {
        holder = GameObject.Find("holder").GetComponent<ValueHolder>();
        movspdlvl = holder.movspeedlvl;
        atkspdlvl = holder.atkspeedlvl;
        dmglvl = holder.dmg;
        hplvl = holder.hp;
        playerAttack.SetAtkSpeed(atkspdlvl);
        playerAttack.SetDamage(dmglvl);
        playerMovement.SetSpeedLvl(movspdlvl);
        playerMovement.SetHealthlvl(hplvl);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
