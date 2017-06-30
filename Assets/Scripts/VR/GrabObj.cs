using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabObj : MonoBehaviour
{
    public bool ClickToHold = true;

    GrabbableObj myObj;
    GrabbableObj currentObj;

    // Use this for initialization
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        currentObj = other.GetComponent<GrabbableObj>();
        print("Entry");
    }

    public void GrabAction()
    {
        print("Unclicked");
        if(myObj != null)
        {
            // drop
            myObj.RootObj.SetParent(null);
            myObj.UnGrabbed();
            myObj = null;
        }
        else
        {
            //pickup
            if(currentObj != null)
            {
                myObj = currentObj;
                currentObj.RootObj.SetParent(transform, true);
                currentObj.Grabbed();              
            }
        }
    }

}
