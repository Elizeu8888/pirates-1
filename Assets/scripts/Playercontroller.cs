using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    //private VectorExtensions vectorExtensions;

    //public CharacterController controller;


    public float xp;
    public bool isead;


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

        flametimer = 0;

        GameObject.Find("hand.L").GetComponent<SphereCollider>().enabled = false;
        GameObject.Find("hand.R").GetComponent<SphereCollider>().enabled = false;

        currentHealth = maxHealth;

        grappleScript = GetComponent<Grapple>();

        healthbar.SetMaxHealth(maxHealth);

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

        //......................................

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);


            GameObject.Find("hand.L").GetComponent<SphereCollider>().enabled = true;
            GameObject.Find("hand.R").GetComponent<SphereCollider>().enabled = true;

        }
        else
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * 3 * Time.deltaTime, 0);


            GameObject.Find("hand.L").GetComponent<SphereCollider>().enabled = false;
            GameObject.Find("hand.R").GetComponent<SphereCollider>().enabled = false;

        }

        //......................................

        if (direction.magnitude >= 0.1f && grappleScript.isgrappling == false)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement




            if (!punching)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                transform.rotation = Quaternion.Euler(0f, angle, 0f);// makes it so the player faces its movement direction
            }





            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
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
        if (GetComponent<Rigidbody>().velocity.sqrMagnitude > maxVelocity)
        {
            //limiting the velocity yes
            GetComponent<Rigidbody>().velocity *= 0.9f;
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

        if (Input.GetKeyDown("space") && isgrounded)
        {
            rb.AddForce(transform.up * jumpheight, ForceMode.Impulse);// here u jump
        }

        //......................................

        if (!isgrounded && velocity.y <= 0)
        {
            //velocity.y += -9f * Time.deltaTime;  // here you fall faster the longer you fall
            //rb.MovePosition(velocity * Time.deltaTime);
            velocity.y = -5f;
        }

        //......................................


        anim.SetLayerWeight(1, 0);

        if (Input.GetMouseButton(0))
        {
            punching = true;
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            punching = false;
            anim.SetLayerWeight(1, 0);
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

    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);




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
            flametimer = 3;

        }

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
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit raycastHit))
            {
                // hit something
                debugHitPointtransform.position = raycastHit.point;
            }
        }
    }*/


}
