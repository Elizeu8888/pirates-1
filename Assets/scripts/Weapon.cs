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

    public Playercontroller plyscript;

    // Start is called before the first frame update
    void Start()
    {
        weaponout = false;

        anim.SetLayerWeight(1, 0);

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("f") && weaponout == false)
        {
            anim.SetLayerWeight(2, 1);
            anim.SetBool("Sworddraw", true);
            
            print("ddd");

            plyscript.weaponDrawn = true;


            weaponout = true;
        }

        if (weaponout == true)
        {
            anim.SetLayerWeight(2, 1);

        }



        if (Input.GetKeyDown("g") && weaponout == true)
        {
            plyscript.weaponDrawn = false;
            print("ddd");
            anim.SetBool("Sworddraw", false);



            
        }

        
        if (weaponout == false)
        {
            anim.SetLayerWeight(2, 0);
        }


    }


    public void Draw()
    {
        print("hello");
        transform.SetParent(hand);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

    }
    public void ReDraw()
    {
        transform.SetParent(back);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0,0,0);

        weaponout = false;
    }


    public void Damage()
    {
        plyscript.AttackWeapon();
    }

}
