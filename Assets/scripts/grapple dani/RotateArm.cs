using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    public GrappleV2 grapplescript;

    void Update()
    {
        if (!grapplescript.IsGrappling()) return;
        transform.LookAt(grapplescript.GetGrapplePoint());
    }
}
