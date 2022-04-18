using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D body;
    private BoxCollider2D collider;
    private Animator anim;
    private bool grounded;
    public bool isAlive;
    public int health;
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    private void Awake()
    {
        isAlive = true;
        health = Variables.playerHealth;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        string skin = PlayerPrefs.GetString("currentSkin", "Starter");
        switch (skin)
        {
            case "Starter":
                transform.position = new Vector3(-27, -51, 0);
                SetAnimations(0);
                break;
            case "Progressive":
                transform.position = new Vector3(-31, -51, 0);
                SetAnimations(1);
                break;
            case "Hero":
                transform.position = new Vector3(-30, -48, 0);
                SetAnimations(2);
                break;
            case "Wizard":
                transform.position = new Vector3(-26, -32, 0);
                SetAnimations(3);
                break;
            case "Flamie":
                transform.position = new Vector3(-30, -51, 0);
                SetAnimations(4);
                break;
        }
        
    }
    private void SetAnimations(int ind)
    {
        anim.runtimeAnimatorController = overrideControllers[ind];
    }
    private void Update()
    {
        anim.SetBool("isAlive", isAlive);
    }
    public void attack()
    {
        anim.SetTrigger("attack");
    }
    public void takeHit()
    {
        anim.SetTrigger("takehit");
    }
}
