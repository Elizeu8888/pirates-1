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

        damageTimer = 0f;
    }

    //......................................
    void TakeDamage(int damage)
    {
        



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

    void FixedUpdate()
    {
        healthbar.SetHealth(currentHealth);
    }


    void OnTriggerStay(Collider other)
    {
        print("ontrigger=" + other.gameObject.tag);



        if (other.gameObject.tag == "Player")
        {

            print("yeeeee");
            
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
