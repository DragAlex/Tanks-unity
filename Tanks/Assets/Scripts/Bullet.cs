using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float Speed = 4.0f;
    private Rigidbody2D rb;
    private Vector2 v;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * Speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int score2 = PlayerPrefs.GetInt("score2");
            PlayerPrefs.SetInt("score2", score2 + 1);
            SceneManager.LoadScene(0);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int score1 = PlayerPrefs.GetInt("score1");
            PlayerPrefs.SetInt("score1", score1 + 1);
            SceneManager.LoadScene(0);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log(collision.contacts[0].normal);
            v = Vector2.Reflect(transform.up, collision.contacts[0].normal).normalized;
            rb.velocity = v * Speed;
            transform.up = rb.velocity.normalized;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

