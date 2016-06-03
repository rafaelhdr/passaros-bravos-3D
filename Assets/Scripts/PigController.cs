using UnityEngine;
using System.Collections;

public class PigController : MonoBehaviour {

	public GameObject deathText;
	public float force_limit;

	// Use this for initialization
	void Start ()
	{
		deathText.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
	}

	void OnCollisionEnter (Collision collision)
	{
		// Porco atingido por pássaro
		if (collision.gameObject.CompareTag ("Bird"))
		{
			gameObject.SetActive (false);
		}

		// Porco sendo submetido a uma força
		Vector3 force = collision.impulse / Time.fixedDeltaTime;
		if (force.magnitude > force_limit)
		{
			gameObject.SetActive (false);
		}

		// Texto indicando que o porco morreu (debug)
		if (gameObject.activeSelf == false)
		{
			deathText.SetActive (true);
		}
	}
}
