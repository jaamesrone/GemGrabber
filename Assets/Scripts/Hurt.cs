using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{

    MeshRenderer meshRenderer;
    Color origColor;
    float flashtime = .25f;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree" || collision.gameObject.tag == "Spikes" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "DeathBox" || collision.gameObject.tag == "BoobyTrap")
        {
            StartCoroutine(Eflash());
        }
        else if (collision.gameObject.tag == "Life")
        {
            StartCoroutine(HealthFlash());
            GameManager.Instance.lives++;
            Destroy(collision.gameObject);
        }
    }


    IEnumerator Eflash()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashtime);
        meshRenderer.material.color = origColor;
    }

    IEnumerator HealthFlash()
    {
        meshRenderer.material.color = Color.green;
        yield return new WaitForSeconds(flashtime);
        meshRenderer.material.color = origColor;
    }
}