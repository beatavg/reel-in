using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBoat : MonoBehaviour
{   
    //Vector3 spawnPosition;
    //private float movementSpeed = 5f;

    // float smooth = 5.0f;
    //float tiltAngle = 60.0f;

    [SerializeField] protected Vector3 m_from = new Vector3(5.0F, 0.0F, 0.0F);
    [SerializeField] protected Vector3 m_to = new Vector3(5.0F, 0.0F, 0.0F);
    [SerializeField] protected float m_frequency = 2.0F;

    void Start()
    {
        InvokeRepeating("ChangePosition", 1, 0.001f);
    }

    void ChangePosition () {

        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        // float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        Quaternion from = Quaternion.Euler(this.m_from);
        Quaternion to = Quaternion.Euler(this.m_to);

        float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * this.m_frequency));
        this.transform.localRotation = Quaternion.Lerp(from, to, lerp);
        

        // spawnPosition = new Vector3(horizontalInput, verticalInput,0);
        
        
        // spawnPosition = new Vector3 (Random.Range(-402,-402), Random.Range(-203.92f,-204.2f), Random.Range(0,0));
        // transform.position = spawnPosition;

        // Quaternion target = Quaternion.Euler(0, tiltAroundX, 0);
        // transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);

        //transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
        //Debug.Log(transform.position);
        }
}
