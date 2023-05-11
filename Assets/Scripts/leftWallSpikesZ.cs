using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftWallSpikesZ : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed;
    public bool goingLeft;

    // Start is called before the first grame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    private void Move()
    {
        if (goingLeft)
        {
            if (transform.position.z <= leftPos.z)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.back * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.z >= rightPos.z)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.forward * Time.deltaTime * speed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
