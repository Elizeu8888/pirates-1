using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walk,
        BackUp

    }

    public float movespeed = 25f;

    States state = States.Idle;
    float stateTimer = 2;

    Rigidbody rb;

    public GameObject check;
    Collider enemyrange;
    GameObject target;

    public float turnsmoothing = 0.1f;
    float turnsmoothvelocity = 0.5f;
    public float maxVelocity = 50f;

    public Animator anim;
    string statetext;


    void Start()
    {
        
        target = GameObject.FindWithTag("Player");
        enemyrange = check.GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {

        target = GameObject.FindWithTag("Player");

        if (rb.velocity.sqrMagnitude > maxVelocity)
        {
            //limiting the velocity yes
            rb.velocity *= 0.9f;
        }

        StateManager();

    }

    void StateManager()
    {

        stateTimer -= Time.deltaTime;

        if (state == States.Idle)
        {

            anim.SetBool("walking", false);
            Idle();


        }
        if (state ==States.Walk)
        {

            anim.SetBool("walking", true);
            Walk();
        }
        if(state == States.BackUp)
        {
            BackUp();
            stateTimer = 2;
            anim.SetBool("walking", false);

            if (stateTimer <= 0)
            {
                state = States.Idle;
            }
        }

    }

    void Walk()
    {

        Vector3 targetPos = target.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
        rb.AddForce(movedir.normalized * movespeed * Time.deltaTime, ForceMode.Impulse);
    }

    void Idle()
    {
        Vector3 resultVelocity = rb.velocity;
        resultVelocity.z = 0;
        resultVelocity.x = 0;
        rb.velocity = resultVelocity;
    }

    void BackUp()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * -Vector3.forward;
        rb.AddForce(movedir.normalized * movespeed * Time.deltaTime * 3, ForceMode.Impulse);

    }

    void NewGUI()
    {
        string state = statetext;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("ddd");
            //state = 2;


        }
    }


}
