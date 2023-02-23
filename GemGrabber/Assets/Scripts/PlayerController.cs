using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // adjust as needed
    public float sideStepDistance = 2f; // adjust as needed
    public float cameraFollowSpeed = 5f; // adjust as needed
    public Transform cameraTransform; // assign in inspector
    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = cameraTransform.position - transform.position;
    }

    void Update()
    {
        // move forward at a constant speed
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // move left or right based on keyboard input
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.left * sideStepDistance);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right * sideStepDistance);
        }

        // adjust camera position to follow player horizontally
        Vector3 targetCameraPosition = transform.position + cameraOffset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
    }
}
