using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveGrabShoot : MonoBehaviour
{

    //==Required code to use vive controllers==
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
    //=========================================

    public float shootForce;

    public GameObject collidingObject;
    public GameObject objectHeld;

    public GameObject prefabBullet;

    public void OnTriggerEnter(Collider other)
    {
        if (collidingObject || !other.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = other.gameObject;
    }
    public void OnTriggerStay(Collider other)
    {
        if (collidingObject || !other.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = other.gameObject;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject != collidingObject)
        {
            return;
        }

        collidingObject = null;
    }


    //==========
    void Update()
    {
        //detecting input
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (objectHeld)
            {
                ReleaseObject();
            }
        }

        //======part 2========
        if (Controller.GetHairTriggerDown() && objectHeld)
        {
            if (objectHeld.name == "Gun")
            {
                Shoot();
            }
        }
        //====================
    }
    //grab and releasing objects
    private void GrabObject()
    {
        objectHeld = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectHeld.GetComponent<Rigidbody>();
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<Rigidbody>())
        {
            // 2
            GetComponent().connectedBody = null;
            Destroy(GetComponent<Rigidbody>());
            // 3
            objectHeld.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectHeld.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4
        objectHeld = null;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<UnityEngine.FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    //shoot!
    private void Shoot()
    {
        GameObject bullet = Instantiate(prefabBullet, objectHeld.transform.position + objectHeld.transform.forward * 0.3f, objectHeld.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(objectHeld.transform.forward * shootForce);
    }
}

