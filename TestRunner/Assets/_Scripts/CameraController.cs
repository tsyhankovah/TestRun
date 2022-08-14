using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;

    // player tracking
    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
    }



    public void onRestart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

}
