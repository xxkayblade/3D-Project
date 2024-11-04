using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 direction;

    private void Start()
    {
        startPos = transform.position;

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
