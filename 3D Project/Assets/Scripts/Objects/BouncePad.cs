using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();

            playerRB.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
