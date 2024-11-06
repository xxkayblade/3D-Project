using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    // values for movement speed
    public float sideSpeed = 7f; 
    public float continuousSpeed = 20f;
    // values for jumping
    public float jumpHeight = 15f;
    public float gravityScale = 3.5f;

    [Header("Player Attack")]
    // values for enemy detection and radius
    public LayerMask enemyLayer;
    public float enemyDetectRadius = 10f;
    // values for tongue attack/line renderer 
    public float tongueSpeed = 10f;
    public float tongueTimer = 0f; 
    public float tongueDuration = 1f; // how long the tongue will be active 

    [Header("Other")]
    // transforms and objects needed
    public Transform tongueStart;
    public Transform ResapawnPoint;
    public GameObject winScreen;
    // components needed from player and enemy
    private Rigidbody playerRB;
    private LineRenderer tongue;
    private Transform enemy;

    [Header("Checks")]
    public bool grounded = true;
    public bool isGrappling = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        tongue = GetComponent<LineRenderer>();
        tongue.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            // adds force upwards when player presses jump 
            grounded = false;
            playerRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        
        if (!grounded)
        {
            // drags player down through gravity scale to create less floaty jump
            playerRB.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
        }

        DetectEnemies();

        if (tongue.enabled)
        {
            // determines how long line renderer should be enabled
            tongueTimer += Time.deltaTime;

            if (tongueTimer >= tongueDuration)
            {
                tongueTimer = 0f;
                tongue.enabled = false;
            }
        }
    }

    // gets called every physics update
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

    void DetectEnemies()
    {
        // creates sphere around player object
        // objects within enemyLayer that collide with sphere will be detected
        Collider[] enemies = Physics.OverlapSphere(transform.position, enemyDetectRadius, enemyLayer);

        if (enemies.Length > 0)
        {
            // Debug.Log("enemy detected");

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Debug.Log("attack!");
                enemy = enemies[0].transform; // attacks first enemy object that collided with sphere
                TongueAttack(); // function that enables line renderer
            }
        }
    }

    // what occurs when player presses E and attack
    void TongueAttack()
    {
        // line renderer is enabled and is positioned between the player and the target enemy
        tongue.enabled = true;
        tongue.SetPosition(0, tongueStart.position);
        tongue.SetPosition(1, enemy.position);

        Destroy(enemy.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        // depending if player collides with enemy or goes out of bound, position will change to the respawn point pos
        if (other.gameObject.tag == "Death" || other.gameObject.tag == "EnemyRight" || other.gameObject.tag == "EnemyLeft")
        {
           // Debug.Log("death!");
           transform.position = ResapawnPoint.position;
        }

        // when player collides with the end goal, enables win screen 
        if (other.gameObject.tag == "Finish")
        {
            // Debug.Log("win!");
            Destroy(gameObject);
            winScreen.SetActive(true);
        }

        // depending on the platform the player is on, their jumpHeight and/or speed will alternate
        switch (other.gameObject.tag)
        {
            case "Ground":
                grounded = true;
                jumpHeight = 15;
                continuousSpeed = 20;
                break;
            case "BouncePadOne":
                grounded = true;
                jumpHeight = 30;
                break;
            case "BouncePadTwo":
                grounded = true;
                jumpHeight = 40;
                break;
            case "BouncePadThree":
                grounded = true;
                jumpHeight = 45;
                continuousSpeed = 25f;
                break;
            case "BouncePadFour":
                grounded = true;
                jumpHeight = 50;
                break;
        }
    }

    // visualize the enemy detection radius around the player
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius);
    }*/

}