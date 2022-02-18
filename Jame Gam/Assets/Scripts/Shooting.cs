using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;

    public Transform shootPoint;

    public int shootIntervals;

    private void Start()
    {
        InvokeRepeating("Shoot", shootIntervals, shootIntervals);
    }

    void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, transform.rotation);
        shootIntervals = Random.Range(2, 5);
    }
}
