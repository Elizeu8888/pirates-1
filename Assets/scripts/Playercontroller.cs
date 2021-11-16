using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnsmoothing = 0.1f;// for movement
    float turnsmoothvelocity = 0.5f;

    public Healthbar healthbar;
    public int maxHealth = 100;// health
    public int currentHealth;

    private int damageTimer;

    public static int playerscore = 0;

    GameObject coin;
    GameObject ground;

    public float gravity = -9.81f;
    public float jumpheight = 5f;// for jumping

    public Transform groundcheck;
    public float grounddistance = 0.4f;// for is grounded
    public LayerMask groundmask;
    public Collider groundedcheck;



    public bool isgrounded;
    Vector3 velocity;

    public Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        ground = GameObject.FindGameObjectWithTag("ground");
        coin = GameObject.FindGameObjectWithTag("Coin");
        anim.SetBool("weapondrawn", false);
        damageTimer = 10;

        Cursor.lockState = CursorLockMode.Locked;

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

    }


    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;




        velocity.y += gravity * Time.deltaTime;// here you fall faster the longer you fall
        controller.Move(velocity * Time.deltaTime);



        if (direction.magnitude >= 0.1f)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement


            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
            transform.rotation = Quaternion.Euler(0f, angle, 0f);// makes it so the player faces its movement direction


            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
            controller.Move(movedir.normalized * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
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



        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -13f;
        }






        if (Input.GetKey("space") && isgrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);// here u jump
        }



        anim.SetLayerWeight(1, 0);

        anim.SetBool("jump", false);

        if (Input.GetMouseButton(0))
        {





            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }




        anim.SetBool("grounded", false);



        if (!isgrounded)
        {
            anim.SetBool("grounded", false);
        }
        if (isgrounded)
        {
            anim.SetBool("grounded", true);
        }





    }

    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);




    }




    void OnTriggerStay(Collider other)
    {
        print("ontrigger=" + other.gameObject.tag);


        
        if (other.gameObject.tag == "enemy")
        {
            if (damageTimer < 0)
            {
                TakeDamage(5);
                damageTimer = 10;
            }
            else
            {
                damageTimer--;
            }

        }

        /*if (groundedcheck.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isgrounded = true;
        }
        else
        {
            isgrounded = false;
        }*/


    }

    void OnTriggerEnter(Collider col)
    {


    }







    void OnTriggerExit(Collider other)
    {


        /*if (groundedcheck.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isgrounded = false;
        }
        else
        {
            isgrounded = true;
        }*/


    }

    /*private void HandleHookShotStart()
    {
        if (Input.GetKey("e"))
        {
            if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out RaycastHit raycastHit))
            {
                // hit something
                debugHitPointtransform.position = raycastHit.point;
            }
        }
    }*/


}
