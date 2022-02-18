using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;

    public GameObject platform;

    public Transform pointOne;
    public Transform pointTwo;

    public bool destinationOne;

    private void Update()
    {
        if(destinationOne == true)
        {
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, pointOne.position, speed * Time.deltaTime);
        }
        else
        {
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, pointTwo.position, speed * Time.deltaTime);
        }

        if (platform.transform.position == pointOne.position)
        {
            destinationOne = false;
        }

        if (platform.transform.position == pointTwo.position)
        {
            destinationOne = true;
        }
    }
}
