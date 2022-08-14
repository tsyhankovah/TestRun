using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour 
{
	
	public float rotationSpeed;
	public AudioClip collectSound;
	public GameObject collectEffect;
	
	void FixedUpdate () 
	{

	transform.Rotate (Vector3.up * rotationSpeed * Time.fixedDeltaTime, Space.World);
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);

		if (collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		Destroy (gameObject);
	}
}
