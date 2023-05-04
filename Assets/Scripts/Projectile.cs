using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float timer = 1;
    public float bulletForce = 15f; // New variable to determine bullet force

    public Transform spawnPoint;
    public GameObject bulletProjectile;

    private void Update()
    {
        ShootDart();
    }

    void ShootDart()
    {
        timer -= Time.deltaTime;
        if (timer > 0) return;

        timer = 1f;

        Vector3 bulletDirection = spawnPoint.forward;
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, bulletDirection, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                {
                    GameManager.Instance.Respawn();
                } //when the bullet hits the player
            }
        }

        // Spawn bullet
        GameObject bulletObj = Instantiate(bulletProjectile, spawnPoint.position, spawnPoint.rotation);
        bulletObj.GetComponent<Rigidbody>().velocity = bulletDirection * bulletForce;
        Destroy(bulletObj, 1f);
    }
}

