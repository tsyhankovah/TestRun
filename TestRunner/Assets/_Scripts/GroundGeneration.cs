using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    
    public GameObject[] ground;
    private List<GameObject> groundList = new List<GameObject>();

    private float position = 0;
    private float groundLenght = 100;

    private int startIndex = 6;

    public Transform player;


    // building first platforms 
    void Start()
    {

        for (int i = 0; i < startIndex; i++)
        {
            Spawner();
        }

    }


    // endless building platforms
    private void Update()
    {
        if (player.position.z - 120 > position - (startIndex * groundLenght))
        {
            Spawner();
            Destroyer();
        }
    }

    private void Spawner()
    {
        GameObject newGround = Instantiate(ground[Random.Range(0, ground.Length)], transform.forward * position, Quaternion.identity);
        groundList.Add(newGround);

        position += groundLenght;
    }


    private void Destroyer()
    {
        Destroy(groundList[0]);
        groundList.RemoveAt(0);
    }

}





