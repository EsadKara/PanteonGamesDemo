using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingPlatform : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    Quaternion newRot;

    private void Start()
    {
        newRot = Quaternion.Euler(0, 0, 0);
    }
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = this.transform;
            collision.gameObject.transform.rotation = newRot;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = this.transform;
            collision.gameObject.transform.rotation = newRot;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
