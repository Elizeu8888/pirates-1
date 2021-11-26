using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] float pullspeed = 0.5f;
    [SerializeField] float stopDistance = 4f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;


    public Animator anim;

    public bool isgrappling;



    Hook hook;
    bool pulling;
    Rigidbody rigid;

    public Playercontroller playerscript;




    // Start is called before the first frame update
    void Start()
    {

        rigid = GetComponent<Rigidbody>();
        pulling = false;

    }

    public void StartPull()
    {
        pulling = true;
    }

    void DestroyHook()
    {
        if (hook == null) return;

        pulling = false;
        Destroy(hook.gameObject);
        hook = null;


    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (hook == null && Input.GetMouseButtonDown(1))
        {
            StopAllCoroutines();
            pulling = false;
            hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Initialize(this, shootTransform);
            StartCoroutine(DestroyHookAfterLifetime());
        }
        else if (hook != null && Input.GetMouseButtonDown(1))
        {
            DestroyHook();
        }

        if (!pulling || hook == null) return;

        if (Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            DestroyHook();
            isgrappling = false;
        }
        else
        {
            rigid.AddForce((hook.transform.position - transform.position).normalized * pullspeed, ForceMode.VelocityChange);// here it pulls you
            anim.Play("jumpfalling");
            playerscript.isgrounded = false;
            isgrappling = true;
        }

    }

    private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(8f);

        DestroyHook();

    }


}
