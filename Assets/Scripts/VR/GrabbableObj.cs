using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObj : MonoBehaviour
{
    public Transform RootObj;
    public Transform RestingPlace;

    public List<Collider> ColsToDisableOnGrab = new List<Collider>();

    public void Grabbed()
    {
        for(int i = 0; i < ColsToDisableOnGrab.Count; ++i)
        {
            ColsToDisableOnGrab[i].enabled = false;
        }
        if (RestingPlace == null)
        {
            Rigidbody myRigid = RootObj.GetComponent<Rigidbody>();
            myRigid.useGravity = false;
            myRigid.isKinematic = true;
        }
    }

    public void UnGrabbed()
    {
        for (int i = 0; i < ColsToDisableOnGrab.Count; ++i)
        {
            ColsToDisableOnGrab[i].enabled = true;
        }
        if (RestingPlace == null)
        {
            Rigidbody myRigid = RootObj.GetComponent<Rigidbody>();
            myRigid.useGravity = true;
            myRigid.isKinematic = false;
        }
        else
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

}
