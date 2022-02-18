using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerPos;
    public Transform endZone;

    public float speed;

    public float zOffSet;
    public float yOffSet;

    public bool hasWon = false;
    public bool move = true;
    public bool door = false;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(hasWon == false && door == false && move == true)
        {
            transform.position = new Vector3(playerPos.position.x, playerPos.position.y + yOffSet, zOffSet);
        }

        if(hasWon == true)
        {
            hasWon = true;
            Vector3 endZonePos = new Vector3(endZone.position.x, endZone.position.y, zOffSet);
            transform.position = Vector3.MoveTowards(transform.position, endZonePos, speed * Time.deltaTime);
        }

        if (door == true)
        {
            GameObject doorObject = GameObject.FindGameObjectWithTag("door");

            Vector3 doorPos = new Vector3(doorObject.transform.position.x, doorObject.transform.position.y, zOffSet);

            transform.position = Vector3.MoveTowards(transform.position, doorPos, speed * Time.deltaTime);
        }
    }
}
