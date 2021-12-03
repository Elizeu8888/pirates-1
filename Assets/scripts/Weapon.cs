using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform hand;
    public Transform back;

    bool weaponout;

    // Start is called before the first frame update
    void Start()
    {
        weaponout = false;
        


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey("f"))
        {
            print("ddd");
            transform.SetParent(hand);


            weaponout = true;
        }
        else
        {
            weaponout = false;
        }
        if (weaponout == true)
        {
            
        }



        if (Input.GetKey("g"))
        {

            print("ddd");

            transform.SetParent(back);

            weaponout = false;
        }
        else
        {
            weaponout = true;
        }
        
        if (weaponout == false)
        {
            transform.SetParent(back);
        }


    }





}
