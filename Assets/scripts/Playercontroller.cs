using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    //private VectorExtensions vectorExtensions;

    Vector3 direction;


    public float shieldtimer;
    public ParticleSystem shield, lightning;
    public GameObject shieldrune;

    public GameObject inventory;
    public bool invOUT = false;
    //public CharacterController controller;

    public GameObject lockUI;
    public bool locked;

    private bool isdead;

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
    public bool sprinting;
    public float sprintBonus;
    public float walkspeed;
    public float airVelocity = 750;
    public float lockedSpeed;
    //......................................

    public Healthbar healthbar;
    public int maxHealth = 100;// health
    public int currentHealth;

    //......................................
    private float damageTimer;
    //......................................

    Grapple grappleScript;
    //public float gravity = -9.81f;
    public float jumpheight = 5f;// for jumping

    //......................................

    public Transform groundcheck;
    public float grounddistance = 0.4f;// for is grounded
    public LayerMask groundmask;

    //......................................

    public bool isgrounded;
    Vector3 velocity;
    public Animator anim,hitanim;


    //......................................

    public EnemyController enemyhealth;

    //public GameObject lefthand;
    //public GameObject righthand;

    public Collider[] hitboxes;

    public bool weaponDrawn;
    public Weapon weaponscript;
    bool shieldOut = false;



    void Start()
    {
        if(PlayerPrefs.HasKey("xp") == true)
        {
            xp = PlayerPrefs.GetFloat("xp");
        }
        else
        {
            xp = 0f;
        }

        lockUI.SetActive(false);

        flametimer = 0;

        //lefthand.GetComponent<BoxCollider>().enabled = false;
        //righthand.GetComponent<BoxCollider>().enabled = false;

        currentHealth = maxHealth;

        grappleScript = GetComponent<Grapple>();

        healthbar.SetMaxHealth(maxHealth);

        xpscript.SetMaxXP(maxXP);

        anim.SetBool("weapondrawn", false);

        damageTimer = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        

    }




    //......................................
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        


    }
    //......................................

    void Update()
    {
        //hitanim.SetBool("hit", false);
        TriggerShield();
        rbVelocity = rb.velocity;

        if (Input.GetKeyDown("e"))
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

        if(Input.GetKeyDown("r"))
        {
            LaunchAttack(hitboxes[0], 100);
        }
        if(Input.GetKey("q") && locked == false)
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
        if(sprinting == true)
        {
            speed = sprintBonus;
            maxVelocity = sprintBonus;
            anim.SetBool("sprinting", true);
        }
        else
        {
            anim.SetBool("sprinting", false);
            speed = walkspeed;
            maxVelocity = walkspeed;
        }


        if (invOUT == false)
        {

            if (Input.GetMouseButtonDown(0) && weaponDrawn == true)
            {
                anim.SetBool("swordAttack", true);
                
            }
            else
            {
                anim.SetBool("swordAttack", false);
            }




            if (Input.GetMouseButton(0) && weaponDrawn == false)
            {
                punching = true;

                //lefthand.GetComponent<BoxCollider>().enabled = true;
                //righthand.GetComponent<BoxCollider>().enabled = true;

                
            }
            else
            {
                punching = false;

                //lefthand.GetComponent<BoxCollider>().enabled = false;
                //righthand.GetComponent<BoxCollider>().enabled = false;

            }


            if (Input.GetKeyDown("l"))
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


        PlayerPrefs.SetFloat("xp", xp);
        xpscript.SetXP(xp);

        






        //......................................


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

        if(locked == true)
        {
            anim.SetLayerWeight(5, 1);
            anim.SetLayerWeight(2, 1);
            anim.SetBool("Sworddraw", true);
            weaponscript.weaponout = true;
            weaponDrawn = true;
            speed = lockedSpeed;
            shieldOut = true;
        }
        else
        {
            
            anim.SetLayerWeight(5, 0);
        }
        anim.SetFloat("Z", direction.z);
        anim.SetFloat("X", direction.x);
        //......................................

    }

    public void FixedUpdate()
    {

        healthbar.SetHealth(currentHealth);

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





                Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
                rb.AddForce(movedir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
            }
            else
            {
                if(isgrounded==true)
                {
                    Vector3 resultVelocity = rb.velocity;
                    resultVelocity.z = 0;
                    resultVelocity.x = 0;
                    rb.velocity = resultVelocity;
                }

                
            }
        }

        if(isgrounded == false)
        {
            //maxVelocity = airVelocity;
            
        }


        if (Input.GetKey("space") && isgrounded)
        {
            rb.AddForce(transform.up * jumpheight, ForceMode.Impulse);// here u jump
            isgrounded = false;
        }


    }

    void Triggerfire()
    {

        



        if (shieldOut == true)
        {
            flameparticle.SetActive(true);
            anim.SetLayerWeight(4, 1);

            

        }
        else
        {
            anim.SetLayerWeight(4, 0);
            flameparticle.SetActive(false);
        }

        if (Input.GetKeyDown("i"))
        {
            shieldOut = !shieldOut;
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



    public void LaunchAttack(Collider col, float damage)
    {

        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBoxes"));
        foreach (Collider c in cols)
        {


            if (c.transform.parent == transform)
            {
                continue;
            }

            Debug.Log(c.tag);

            //enemyhealth = c.gameObject.GetComponent<EnemyController>();
            
            //enemyhealth.currentHealth -= 15;

            switch (c.tag)
            {
                case "enemy":                
                hitanim.SetTrigger("hit");
                break;
                default:
                Debug.Log("nopedidntwork");
                break;

            }
            float timer = 1;

            c.SendMessageUpwards("TakeDamage", damage);

             


        }



    }

    public void AttackWeapon()
    {
        LaunchAttack(hitboxes[1], 15);
    }


    public void Lightning()
    {
        LaunchAttack(hitboxes[2],2);
        lightning.Play();

    }















}
