using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingStick : MonoBehaviour
{
    Rigidbody stickRb;
    [SerializeField] float force;
    void Start()
    {
        stickRb = GetComponent<Rigidbody>();
        stickRb.velocity = Vector3.forward * force;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            stickRb.velocity = collision.gameObject.transform.forward * (force);
        }
    }
}
