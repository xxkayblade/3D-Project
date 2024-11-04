using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float sideSpeed = 7f;
    [SerializeField] float continuousSpeed = 20f;
    [SerializeField] float jumpHeight = 15f;
    [SerializeField] float gravityScale = 3.5f;

    [Header("Player Attack")]
    [SerializeField] float enemyDetectRadius = 10f; // detection sphere around player
    [SerializeField] LayerMask enemyLayer;
    public float tongueSpeed = 10f;
    public float tongueTimer = 0f; 
    public float tongueDuration = 1f; // how long the tongue will be active 

    private Transform enemy;
    public Transform tongueStart;
    private LineRenderer tongue;

    [Header("Checks")]
    public bool grounded = true;
    public bool isGrappling = false;

    public Transform ResapawnPoint;
    public GameObject winScreen;
    Rigidbody playerRB;

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
            grounded = false;
            playerRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        
        if (!grounded)
        {
            playerRB.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
        }

        DetectEnemies();

        if (tongue.enabled)
        {
            tongueTimer += Time.deltaTime;

            if (tongueTimer >= tongueDuration)
            {
                tongueTimer = 0f;
                tongue.enabled = false;
            }
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

    void DetectEnemies()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, enemyDetectRadius, enemyLayer);

        if (enemies.Length > 0)
        {
            Debug.Log("enemy detected");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("attack!");
                enemy = enemies[0].transform;
                TongueAttack();
            }
        }
    }

    void TongueAttack()
    {
        tongue.enabled = true;
        tongue.SetPosition(0, tongueStart.position);
        tongue.SetPosition(1, enemy.position);

        Destroy(enemy.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Death" || other.gameObject.tag == "EnemyRight" || other.gameObject.tag == "EnemyLeft")
        {
            Debug.Log("death!");
           transform.position = ResapawnPoint.position;
        }

        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("win!");
            Destroy(gameObject);
            winScreen.SetActive(true);
        }


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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius);
    }

}