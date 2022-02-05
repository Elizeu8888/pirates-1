using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //
    public Healthbar healthbar;
    public int maxHealth = 100;// health
    public int currentHealth;
    //
    static public float damageTimer;

    public bool killed = false;

    EnemyMovement enemystate;

    public float respawntime;

    public GameObject plyer;

    public Playercontroller plyscript;
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        enemystate = GetComponent<EnemyMovement>();

        plyer = GameObject.Find("player");

        plyscript = plyer.GetComponent<Playercontroller>();

        healthbar.SetMaxHealth(maxHealth);

        killed = false;

        respawntime = 6;

        damageTimer = 1f;
    }

    //......................................
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (damageTimer <= 0)
        {
            currentHealth -= damage;
        }
        else
        {
            damageTimer -= Time.deltaTime;
        }
        


    }
    //......................................

    void Update()
    {
        

        if (currentHealth <= 0)
        {
            killed = true;

        }
        else
        {
            gameObject.SetActive(true);
            killed = false;
        }

        if (currentHealth <= 0)
        {
            transform.position = spawn.position;

            plyscript.xp += 20;

            currentHealth = maxHealth;
            healthbar.SetHealth(currentHealth);
            killed = false;

        }




    }


    public void Counter()
    {

    }

    void FixedUpdate()
    {
        healthbar.SetHealth(currentHealth);
    }


    void OnTriggerStay(Collider other)
    {
        



        if (other.gameObject.tag == "Player")
        {

            
            
            if (damageTimer < 0)
            {
                plyscript.currentHealth = plyscript.currentHealth - 20;
                damageTimer = 2f;
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }

        }



    }

}
