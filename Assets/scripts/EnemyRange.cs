using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{

    EnemyMovement enemyscript;
    public GameObject enemy;
    float timer;
    public bool enemyinrange;
    public float targetrange;
    public LayerMask Playermask;

    // Start is called before the first frame update
    void Start()
    {
        enemyscript = enemy.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position,1);


        enemyinrange = Physics.CheckSphere(enemy.transform.position, targetrange, Playermask);

        if(enemyinrange && enemyscript.state != 2)
        {
            enemyscript.state = 1;
        }
        else
        {
            enemyscript.state = 0;
        }

    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyscript.state != 2)
        {
            timer -= Time.deltaTime;

            enemyscript.state = 1;


        }
    }

    void OnTriggerExit(Collider other)
    {
        enemyscript.state = 0;
    }*/
}
