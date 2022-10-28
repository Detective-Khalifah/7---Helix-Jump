using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRb;
    private AudioManager audioManager;
    public float bounceForce = 6;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
        audioManager.Play("Bounce");
        string materialType = collision.gameObject.GetComponent<MeshRenderer>().material.name;
        switch (materialType)
        {
            case "Safe (Instance)":
                break;
            case "Unsafe (Instance)":
                GameManager.gameOver = true;
                audioManager.Play("Game Over");
                break;
            case "Last Ring (Instance)":
                if (!GameManager.levelCompleted)
                    audioManager.Play("Win Level");
                GameManager.levelCompleted = true;
                break;
            default:
                break;
        }
    }

}
