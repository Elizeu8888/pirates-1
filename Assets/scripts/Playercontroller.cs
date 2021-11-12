using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnsmoothing = 0.1f;
    float turnsmoothvelocity = 0.5f;


    public static int playerscore = 0;

    GameObject coin;
    GameObject ground;

    public float gravity = -9.81f;
    public float jumpheight = 5f;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;

    public bool isgrounded;
    Vector3 velocity;
    public Animator anim;

    void start()
    {
        ground = GameObject.FindGameObjectWithTag("ground");
        coin = GameObject.FindGameObjectWithTag("Coin");
        anim.SetBool("weapondrawn", false);
        //anim = GetComponent<Animator>();
    }


    void Update()
    {


        anim.SetBool("jump", false);

        if (Input.GetKey("v"))
        {
            anim.SetBool("weapondrawn", true);
        }
        if (Input.GetKey("c"))
        {
            anim.SetBool("weapondrawn", false);
        }




        anim.SetBool("grounded", false);

        //isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);





        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -13f;
        }



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;




        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);



        if (direction.magnitude >= 0.1f)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            controller.Move(movedir.normalized * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


        if (Input.GetKey("space") && isgrounded)
        {
            //anim.SetBool("jump", true);
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        if (!isgrounded)
        {
            anim.SetBool("grounded", false);
        }
        if (isgrounded)
        {
            anim.SetBool("grounded", true);
        }


        anim.SetBool("walking", false);




        if ((GetComponent<Rigidbody>().velocity.magnitude > 0.1) && isgrounded)
        {
            anim.SetBool("walking", true);

        }
        else
        {
            anim.SetBool("walking", false);
        }











    }

    void OnTriggerEnter(Collider other)
    {
        print("ontrigger=" + other.gameObject.tag);

        if (other.gameObject == coin)
        {
            playerscore = playerscore + 420;
        }

        if (other.gameObject.tag == "ground")
        {
            isgrounded = true;
        }
        else
        {
            isgrounded = false;
        }

    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            isgrounded = false;
        }
        else
        {
            isgrounded = true;
        }
    }

    void FixedUpdate()
    {


    }
}
