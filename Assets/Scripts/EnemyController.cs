using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // This is the speed at which the enemy will move
    public float followRadius = 10f; // This is the maximum distance at which the enemy will follow the player
    public string playerTag = "Player"; // This is the tag of the player object

    private Transform player; // This will hold a reference to the player's transform

    void Start()
    {
        // Find the player object using its tag
        GameObject playerObject = GameObject.FindWithTag(playerTag);

        if (playerObject != null)
        {
            // Get the player's transform component
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Could not find object with tag '" + playerTag + "'!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = player.position - transform.position;

            // Check if the player is within the follow radius
            if (direction.magnitude <= followRadius)
            {
                // Normalize the direction vector
                direction.Normalize();

                // Move towards the player
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
