using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform hand;
    public Transform back;

    bool weaponout;

    public Animator anim;

    public GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        weaponout = false;

        anim.SetLayerWeight(1, 0);

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey("f") && weaponout == false)
        {

            anim.SetTrigger("sworddraw");
            
            print("ddd");
            transform.SetParent(hand);

            transform.localPosition = new Vector3(0,0,0);
            transform.localRotation = Quaternion.Euler(0,0,90);

            weaponout = true;
        }

        if (weaponout == true)
        {
            anim.SetLayerWeight(1, 1);
        }



        if (Input.GetKey("g") && weaponout == true)
        {
            anim.SetLayerWeight(1, 0);
            print("ddd");

            transform.SetParent(back);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;

            weaponout = false;
        }

        
        if (weaponout == false)
        {
            transform.SetParent(back);
        }


    }


    public GameObject Draw(GameObject sword)
    {
        print("hello");
        return sword;
    }


}
