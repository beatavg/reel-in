using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveGrabObject : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
 
    public GameObject collidingObject;//To keep track of what objects have rigidbodies
    public GameObject objectInHand;//To track the object you're holding

    void OnTriggerEnter(Collider other)//Activate function in trigger zone, checking rigidbodies and ignoring if no rigidbodies 
    {
        if (!other.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = other.gameObject;//If rigidbody, then assign object to collidingObject variable
  }
 
    void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    void Update ()
    {
        if (Controller.GetPressDown (SteamVR_Controller.ButtonMask.Grip))// Push grip buttons and touching object, set GrabObject function
        {
            if (collidingObject)
            {
                GrabObject ();
            }
        }  
        if (Controller.GetPressUp (SteamVR_Controller.ButtonMask.Grip))// If release grip buttons and holding object, set to release
        {
            if (objectInHand)
            {
                ReleaseObject ();
            }
        }
    }

    private void GrabObject() // Picking up object and assigning objectInHand variable
    {
        objectInHand = collidingObject;
        objectInHand.transform.SetParent (this.transform);
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
    }
// Releasing object and disabling kinematic functionality so other forces can affect object
    private void ReleaseObject()
    {
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand.transform.SetParent (null);
    }
}