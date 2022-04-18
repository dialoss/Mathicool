using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator anim;
    public bool isAlive;
    public int health;
    private void Awake()
    {
        isAlive = true;
        health = Variables.monsterHealth;
    }
    public void attack()
    {
        anim.SetTrigger("attack");
    }
    public void takeHit()
    {
        anim.SetTrigger("takehit");
    }
    public void dead()
    {
        isAlive = false;
        anim.SetBool("isAlive", false);
    }
}
