using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotSpeed;

    public bool isRecharged = true;
    public bool onPressedComma = false;
    public bool onPressedPeriod = false;

    public Transform FirePoint;
    public GameObject Bullet;

    private Rigidbody2D rb;
    private Transform tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash)) Shoot();
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            onPressedComma = true;
        }
        if (Input.GetKeyUp(KeyCode.Comma))
        {
            onPressedComma = false;
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            onPressedPeriod = true;
        }
        if (Input.GetKeyUp(KeyCode.Period))
        {
            onPressedPeriod = false;
        }
    }
    void FixedUpdate()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        Vector2 moveDerection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = (moveDerection * speed);
    }

    private void Rotate()
    {
        if (onPressedComma) tr.rotation *= Quaternion.Euler(0f, 0f, rotSpeed * Time.fixedDeltaTime);
        if (onPressedPeriod) tr.rotation *= Quaternion.Euler(0f, 0f, -rotSpeed * Time.fixedDeltaTime);
    }
    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(1f);
        isRecharged = true;

    }
    private void Shoot()
    {
        if (isRecharged)
        {
            StartCoroutine(ShootCooldown());
            Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            isRecharged = false;
        }
    }
}
