using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{

    public GameObject enemyPrefab;

    Enemymove enemyscript;

    // Start is called before the first frame update
    void Start()
    {

        enemyscript = enemyPrefab.GetComponent<Enemymove>();
        enemyscript.killed = false;

        Instantiate(enemyPrefab, new Vector3(-12, 2, 22), Quaternion.identity);

        

    }

    // Update is called once per frame
    void Update()
    {

        if(enemyscript.currentHealth <= 0)
        {
            enemyscript.killed = true;
        }


        
        if(enemyscript.killed == true)
        {
            enemyscript.killed = false;
            Instantiate(enemyPrefab, new Vector3(-12, 2, 22), Quaternion.identity);
            
        }



    }
}
