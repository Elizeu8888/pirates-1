using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthtransfer : MonoBehaviour
{

    EnemyController enemyscript;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        enemyscript = gameObject.GetComponentInParent<EnemyController>();
    }


    private void TakeDamage(int damage)
    {
        enemyscript.TakeDamage(damage);
        
    }

    void FixedUpdate()
    {

        TakeDamage(damage);
    }
}
