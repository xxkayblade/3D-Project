using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // values for enemy movement
    public float speed = 5f;
    public float distance = 5f;

    // vectors needed enemy movement direction
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 direction;

    void Start()
    {
        // start pos initialized to current object position
        startPos = transform.position;

        // depending on tag of enemy object will determine which direction it will start moving in 
        if (CompareTag("EnemyLeft"))
        {
            endPos = startPos + new Vector3(distance, 0, 0);
            direction = Vector3.right;
        }
        else if (CompareTag("EnemyRight"))
        {
            endPos = startPos - new Vector3(distance, 0, 0);
            direction = Vector3.left; 
        }
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // enemy objects moves side-to-side between start and end position
        if (CompareTag("EnemyLeft"))
        {
            if (transform.position.x >= endPos.x)
            {
                direction = Vector3.left;
                endPos = startPos;
            }
            else if (transform.position.x <= startPos.x)
            {
                direction = Vector3.right;
                endPos = startPos + new Vector3(distance, 0, 0);
            }
        }

        if (CompareTag("EnemyRight"))
        {
            if (transform.position.x <= endPos.x)
            {
                direction = Vector3.right;
                endPos = startPos;
            }
            else if (transform.position.x >= startPos.x)
            {
                direction = Vector3.left;
                endPos = startPos - new Vector3(distance, 0, 0);
            }
        }
    }
}
