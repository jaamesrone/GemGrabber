using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // adjust as needed
    public float sideStepDistance = 2f; // adjust as needed
    public float rotationAngle = 90f; // adjust as needed
    public float rotationSpeed = 10f; // adjust as needed
    public float cameraFollowSpeed = 5f; // adjust as needed
    public float jumpHeight = 2f; // adjust as needed
    public float jumpTime = 0.5f; // adjust as needed
    public Transform cameraTransform; // assign in inspector
    private Vector3 cameraOffset;
    private bool isJumping = false;
    private float jumpTimer = 0f;
    int score;

    void Start()
    {
        cameraOffset = cameraTransform.position - transform.position;
    }

    void Update()
    {
        // move forward at a constant speed
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // rotate left or right based on keyboard input
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateCharacter(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RotateCharacter(1);
        }

        // move left or right based on current rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * sideStepDistance * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * sideStepDistance * Time.deltaTime, Space.World);
        }

        // adjust camera position to follow player
        Vector3 targetCameraPosition = transform.position + cameraOffset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
        cameraTransform.LookAt(transform.position, Vector3.up);

        // jump
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimer = 0f;
        }

        if (isJumping)
        {
            float normalizedTime = jumpTimer / jumpTime;

            // apply jump force using a parabolic curve
            float jumpForce = jumpHeight * 4f * normalizedTime * (1f - normalizedTime);
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);

            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpTime)
            {
                isJumping = false;
            }
        }
    }

    void RotateCharacter(int direction)
    {
        transform.Rotate(Vector3.up, direction * rotationAngle);
        cameraOffset = Quaternion.Euler(0, direction * rotationAngle, 0) * cameraOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            GameManager.Instance.score++;
            Destroy(other.gameObject);
        }
    }

}
