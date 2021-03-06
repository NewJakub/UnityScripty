﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappeable;
    // it's gunTip but it was erroring
    public Transform gunTyp;
    public Transform grappleCamera;
    public Transform player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update() {
         if (Input.GetMouseButtonDown(1)) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1)) {
            StopGrapple();
        }

    }
    void LateUpdate()  {
        DrawRope();

    }
    void StartGrapple() {
        RaycastHit hit;
       if (Physics.Raycast(grappleCamera.position, grappleCamera.forward, out hit, maxDistance)){
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            }
        }


        void DrawRope(){
        if (!joint) return;

        lr.SetPosition(0, gunTyp.position);
        lr.SetPosition(1, grapplePoint);
    }

        void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;

    }

}
