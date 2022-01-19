using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    //private VectorExtensions vectorExtensions;

    Vector3 direction;


    public float shieldtimer;
    public ParticleSystem shield;
    public GameObject shieldrune;

    public GameObject inventory;
    public bool invOUT = false;
    //public CharacterController controller;

    public GameObject lockUI;
    public bool locked;

    public bool isdead;

    //............................

    public XPbar xpscript;
    public float xp;
    public float maxXP = 100;

    //............................
    private bool punching;

    private float flametimer;
    public GameObject flameparticle;

    //......................................
    public Rigidbody rb;
    public Transform cam;
    public float speed = 6f;
    public float turnsmoothing = 0.1f;// for movement
    float turnsmoothvelocity = 0.5f;
    public float maxVelocity = 1f;
    Vector3 rbVelocity;
    //......................................

    public Healthbar healthbar;
    public int maxHealth = 100;// health
    public int currentHealth;

    //......................................
    private float damageTimer;
    //......................................

    GameObject ground;

    Grapple grappleScript;

    //public float gravity = -9.81f;
    public float jumpheight = 5f;// for jumping

    //......................................

    public Transform groundcheck;
    public float grounddistance = 0.4f;// for is grounded
    public LayerMask groundmask;
    public Collider groundedcheck;

    //......................................

    public bool isgrounded;
    Vector3 velocity;
    public Animator anim;

    //......................................



    void Start()
    {
        lockUI.SetActive(false);

        flametimer = 0;

        GameObject.Find("hand.L").GetComponent<SphereCollider>().enabled = false;
        GameObject.Find("hand.R").GetComponent<SphereCollider>().enabled = false;

        currentHealth = maxHealth;

        grappleScript = GetComponent<Grapple>();

        healthbar.SetMaxHealth(maxHealth);

        xpscript.SetMaxXP(maxXP);

        ground = GameObject.FindGameObjectWithTag("ground");

        anim.SetBool("weapondrawn", false);

        damageTimer = 10;

        Cursor.lockState = CursorLockMode.Locked;

    }




    //......................................
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

    }
    //......................................

    void Update()
    {
        TriggerShield();


        if (Input.GetKey("e"))
        {
            invOUT = !invOUT;
            if (invOUT == true)
            {
                inventory.active = true;

                Cursor.lockState = CursorLockMode.Confined;
            }
            else if (invOUT == false)
            {
                inventory.active = false;

                Cursor.lockState = CursorLockMode.Locked;
            }
        }



        if (invOUT == false)
        {




            if (Input.GetMouseButton(0))
            {
                punching = true;

                GameObject.Find("hand.L").GetComponent<SphereCollider>().enabled = true;
                GameObject.Find("hand.R").GetComponent<SphereCollider>().enabled = true;

            }
            else
            {
                punching = false;

                GameObject.Find("hand.L").GetComponent<SphereCollider>().enabled = false;
                GameObject.Find("hand.R").GetComponent<SphereCollider>().enabled = false;

            }


            if (Input.GetKey("l"))
            {

                locked = !locked;

            }

            if (locked == true)
            {
                lockUI.SetActive(true);
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

            }
            else
            {
                lockUI.SetActive(false);
                transform.Rotate(0, Input.GetAxis("Horizontal") * 3 * Time.deltaTime, 0);
            }
            //......................................


            anim.SetLayerWeight(1, 0);
            if (punching)
            {
                anim.SetBool("punching", true);
                anim.SetLayerWeight(1, 1);
            }
            else
            {
                anim.SetBool("punching", false);
                anim.SetLayerWeight(1, 0);

            }


        }




        //......................................

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        xpscript.SetXP(xp);








        //......................................
        rbVelocity = rb.velocity;

        if (rbVelocity.sqrMagnitude > maxVelocity)// right alt and shift for||||
        {
            Vector3 endVelocity = rb.velocity;
            //limiting the velocity yes
            endVelocity.z *= 0.9f;
            endVelocity.x *= 0.9f;
            rb.velocity = endVelocity;

        }

        //......................................

        Triggerfire();

        //......................................

        anim.SetBool("walking", false);
        if ((rb.velocity.magnitude > 1) && isgrounded)
        {
            anim.SetBool("walking", true);

        }
        else
        {
            anim.SetBool("walking", false);
        }

        //......................................



        //......................................

        if (!isgrounded && velocity.y <= 0)
        {
            //velocity.y += -9f * Time.deltaTime;  // here you fall faster the longer you fall
            //rb.MovePosition(velocity * Time.deltaTime);
            velocity.y = -5f;
        }



        //......................................

        anim.SetBool("grounded", false);

        if (!isgrounded)
        {
            anim.SetBool("grounded", false);
        }
        if (isgrounded)
        {
            anim.SetBool("grounded", true);
        }


        //......................................

    }

    public void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);

        if (invOUT == false)
        {
            if (direction.magnitude >= 0.1f && grappleScript.isgrappling == false)
            {


                float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement




                if (!locked)
                {
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);// makes it so the player faces its movement direction
                }





                Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward * Time.deltaTime;// here is the movement
                rb.AddForce(movedir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
            }
            else
            {

                Vector3 resultVelocity = rb.velocity;
                resultVelocity.z = 0;
                resultVelocity.x = 0;
                rb.velocity = resultVelocity;


                //rb.velocity = VectorExtensions.XZvel;//ruining the gravity FIX IT
            }
        }


        if (Input.GetKey("space") && isgrounded)
        {
            rb.AddForce(transform.up * jumpheight, ForceMode.Impulse);// here u jump
        }






    }

    void Triggerfire()
    {

        if (flametimer > 0)
        {
            flameparticle.SetActive(true);

            flametimer -= Time.deltaTime;

        }
        else
        {
            flameparticle.SetActive(false);
        }

        if (Input.GetKey("i"))
        {
            flametimer = 6;

        }






    }

    void TriggerShield()
    {
        if (shieldtimer > 0)
        {
            shield.Play();
            shieldrune.SetActive(true);
            shieldtimer -= Time.deltaTime;

        }
        else
        {
            shield.Stop();
            shieldrune.SetActive(false);
        
        }

        if (Input.GetKey("h"))
        {
            shieldtimer = 6;

        }
    }


    void OnTriggerStay(Collider other)
    {
        


        
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
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit raycastHit))
            {
                // hit something
                debugHitPointtransform.position = raycastHit.point;
            }
        }
    }*/


}
