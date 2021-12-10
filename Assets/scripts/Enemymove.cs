using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymove : MonoBehaviour
{
    //
    public Healthbar healthbar;
    public int maxHealth = 100;// health
    public int currentHealth;
    //
    private int damageTimer;

    public bool killed = false;

    EnemyMovement enemystate;

    public float respawntime;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        enemystate = GetComponent<EnemyMovement>();

        healthbar.SetMaxHealth(maxHealth);

        killed = false;

        respawntime = 6;

        damageTimer = 10;
    }

    //......................................
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

    }
    //......................................

    // Update is called once per frame
    void Update()
    {
        

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            killed = true;




        }
        else
        {
            gameObject.SetActive(true);
            killed = false;
        }



        /*if (killed == true)
        {

            respawntime -= Time.deltaTime;
            if(respawntime <= 0)
            {
                gameObject.SetActive(true);
                currentHealth = maxHealth;
                respawntime = 6;
                killed = false;
            }

        }*/


    }


    public void Counter()
    {

    }




    void OnTriggerStay(Collider other)
    {
        print("ontrigger=" + other.gameObject.tag);



        if (other.gameObject.tag == "weapon")
        {

            enemystate.state = 2;


            if (damageTimer < 0)
            {
                TakeDamage(10);
                damageTimer = 10;
            }
            else
            {
                damageTimer--;
            }

        }



    }

}
