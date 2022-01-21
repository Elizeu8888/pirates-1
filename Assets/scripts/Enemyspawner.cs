using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{

    public Healthbar healthbar;

    public GameObject enemyPrefab;

    GameObject newEnemy;

    EnemyController enemyscript;

    public GameObject player;

    Playercontroller playerscript;

    void Start()
    {



        

        newEnemy = Instantiate(enemyPrefab, new Vector3(-12, 2, 22), Quaternion.identity);
        //Instantiate(enemyPrefab, new Vector3(-12, 2, 22), Quaternion.identity);

        enemyscript = newEnemy.GetComponent<EnemyController>();
        enemyscript.killed = false;

        playerscript = player.GetComponent<Playercontroller>();


    }


    void Update()
    {

        if(enemyscript.currentHealth <= 0)
        {
            newEnemy.transform.position = new Vector3(-12, 2, 22);

            playerscript.xp += 20;

            enemyscript.currentHealth = enemyscript.maxHealth;
            healthbar.SetHealth(enemyscript.currentHealth);
            enemyscript.killed = true;

        }


        
        if(enemyscript.killed == true)
        {
            
            //enemyscript.currentHealth = enemyscript.maxHealth;
            enemyscript.killed = false;

            
            

        }



    }
}
