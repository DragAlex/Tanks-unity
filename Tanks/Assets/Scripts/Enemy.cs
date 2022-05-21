using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform FirePoint;
    public Transform Player;
    public Transform[] Detectors;
    public GameObject Bullet;


    public int reflections;
    public int ang = 0;

    public bool isRecharged = true;
    public bool isRight = true;

    public float speed;
    public float rotSpeed;
    public float distanceCheck;

    private RaycastHit2D hit;
    private Ray2D ray;
    private Rigidbody2D rb;
    private Transform tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        StartCoroutine(MoveTimer());
    }

    void Update()
    {

    }

    private void Ray()
    {
        ray = new Ray2D(FirePoint.position, FirePoint.up);
        for (int i = -1; i < reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                    Shoot();
                    break;
                }
                else if (!hit.transform.CompareTag("Bullet") && !hit.transform.CompareTag("Enemy"))
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                    ray = new Ray2D(hit.point + hit.normal * 0.001f, Vector2.Reflect(ray.direction, hit.normal));

                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.white);
                break;
            }
        }
    }


    void FixedUpdate()
    {
        Ray();
        Rotate();
    }

    private void Rotate()
    {
        if (isRight)
        {
            ang++;
        }
        else
        {
            ang--;
        }

        if (ang > 45) isRight = false;
        if (ang < -45) isRight = true;
        Vector3 dir = (Player.position - tr.position);
        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.Euler(0f, 0f, rot - 90 + ang);

    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }
    private IEnumerator MoveTimer()
    {
        Move();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveTimer());
    }

    private void Move()
    {
        int i = Random.Range(0, 4);
        rb.velocity = (Detectors[i].position - transform.position).normalized * speed;
    }

    void Shoot()
    {
        if (isRecharged)
        {
            StartCoroutine(AttackCoolDown());
            Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            isRecharged = false;
        }
    }
}
