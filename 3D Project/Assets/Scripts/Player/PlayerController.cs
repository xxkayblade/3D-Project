using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float sideSpeed = 7f;
    [SerializeField] float continuousSpeed = 7f;
    [SerializeField] float jumpHeight = 15f;
    [SerializeField] float gravityScale = 4f;

    public Transform ResapawnPoint;
    Rigidbody playerRB;

    [Header ("Checks")]
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            playerRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        if (!grounded)
        {
            playerRB.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = Movement();
        playerRB.MovePosition(playerRB.position + movement * Time.fixedDeltaTime);

        
    }

    Vector3 Movement()
    {
        float horiInput = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(horiInput * sideSpeed, 0, continuousSpeed);
        return move; 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (other.gameObject.tag == "Death")
        {
            Debug.Log("death");
           transform.position = ResapawnPoint.position;
        }
    }
}