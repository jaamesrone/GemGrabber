using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float sideStepDistance = 2f; // adjust as needed
    public float rotationAngle = 90f; // adjust as needed
    public float rotationSpeed = 10f; // adjust as needed
    public float cameraFollowSpeed = 5f; // adjust as needed
    public float jumpHeight = 2f; // adjust as needed
    public float jumpTime = 0.5f; // adjust as needed
    private float crouchTime = 2f;
    private float crouchTimer = 0f;
    public float respawnTime = 3f; // The amount of time it takes for the object to respawn

    private float standingHeight;
    private float crouchingHeight = 0.5f;
    private bool isCrouching = false;

    private Transform cameraTransform; // assign in inspector
    public Rigidbody rb;

    private Vector3 cameraOffset;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    public float speed;
    public float boostTimer;
    public float decreaseTime;
    public float countDownTimer = 5.0f; // initial countdown time

    public float gravityTimer;
    public bool boosting = false;
    public bool gravityOn = false;
    public bool decreaseBoosting = false;
    bool countdownFinished = false;



    void Start()
    {
        cameraTransform = GameObject.Find("Camera").transform;
        gravityTimer = 0f;
        cameraOffset = cameraTransform.position - transform.position;
        speed = 10;
        boostTimer = 0;
        boosting = false;
        rb = GetComponent<Rigidbody>();
        standingHeight = transform.localScale.y;
        StartCoroutine(CountdownCoroutine());
    }


    void Update()
    {
        
        if (countdownFinished)
        {
            Run();
        }
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
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * sideStepDistance * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * sideStepDistance * Time.deltaTime, Space.World);
        }
        // strafe left or right based on arrow keys
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-transform.right * sideStepDistance * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right * sideStepDistance * Time.deltaTime, Space.World);
        }

        if (isCrouching)
        {
            crouchTimer += Time.deltaTime;
            if (crouchTimer >= crouchTime)
            {
                // stand up
                transform.localScale = new Vector3(transform.localScale.x, standingHeight, transform.localScale.z);
                isCrouching = false;
                crouchTimer = 0f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                // crouch
                transform.localScale = new Vector3(transform.localScale.x, crouchingHeight, transform.localScale.z);
                isCrouching = true;
            }
        }
        PowerEffects();


        // adjust camera position to follow player
        Vector3 targetCameraPosition = transform.position + cameraOffset;
        if (cameraTransform != null)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
            cameraTransform.LookAt(transform.position, Vector3.up);
        }

        // jump
        if (!isJumping && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
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

    IEnumerator CountdownCoroutine()
    {
        while (countDownTimer > 0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            countDownTimer--;
            Debug.Log("Countdown: " + countDownTimer.ToString());
        }
        countdownFinished = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Gem"))
        {
            GameManager.Instance.score++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == ("BadGem"))
        {
            GameManager.Instance.score--;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == ("RareGem"))
        {
            GameManager.Instance.score += 10;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == ("Goal"))
        {
            SceneManager.LoadScene(0);
        }
        else if (collision.gameObject.tag == ("SpeedBoost"))
        {
            boosting = true;
            speed = 20;
            GameManager.Instance.score++;
            collision.gameObject.SetActive(false);
            StartCoroutine(Respawn(collision.gameObject));
        }
        else if (collision.gameObject.tag == "Gravity")
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(Respawn(collision.gameObject));
            rb.useGravity = false;
            gravityOn = true;
            gravityTimer = 0;
        }
        else if (collision.gameObject.tag == "Time")
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(Respawn(collision.gameObject));
            decreaseBoosting = true;
            speed = 5;
        }
    }


    public void Run()
    {
        // move forward at the current speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private IEnumerator Respawn(GameObject obj)
    {
        yield return new WaitForSeconds(respawnTime);
        obj.SetActive(true);
    }
    public void PowerEffects()
    {
        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)
            {
                speed = 10;
                boostTimer = 0;
                boosting = false;
            }
        }
        else if (decreaseBoosting)
        {
            decreaseTime += Time.deltaTime;
            if (decreaseTime >= 5)
            {
                speed = 10;
                decreaseTime = 0;
                decreaseBoosting = false;
            }
        }
        else if (gravityOn)
        {
            gravityTimer += Time.deltaTime;
            if (gravityTimer >= 10f)
            {
                rb.useGravity = true;
                gravityOn = false;

            }
        }
    }
}
