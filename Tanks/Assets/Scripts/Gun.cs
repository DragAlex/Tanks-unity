using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Bullet;

    public int reflections;
    
    public bool isRecharged = true;

    private RaycastHit2D hit;
    private Ray2D ray;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        

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
                    Debug.Log("Did Hit Enemy");
                    break;
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                    Debug.Log("Reflect " + (i+2));
                    ray = new Ray2D(hit.point + hit.normal * 0.001f, Vector2.Reflect(ray.direction, hit.normal));

                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.white);
                Debug.Log("Did not Hit");
                break;
            }
        }
    }


    void FixedUpdate()
    {
        Ray();
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(1f);
        isRecharged = true;

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
