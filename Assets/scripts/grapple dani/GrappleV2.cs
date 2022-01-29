using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleV2 : MonoBehaviour
{
    LineRenderer lr;
    Vector3 grapplePoint;
    public LayerMask whatsIsGrappleable;
    public Transform grappleTip, camera, player;
    public float maxDistance;
    SpringJoint joint;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        
        if(Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatsIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distancefrompoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distancefrompoint * 0.4f;
            joint.minDistance = distancefrompoint * 0.15f;

            joint.spring = 12f;
            joint.damper = 10f;
            joint.massScale = 7f;

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, grappleTip.position);
        lr.SetPosition(1, grapplePoint);
    }



    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }


}
