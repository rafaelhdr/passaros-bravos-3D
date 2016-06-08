using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public Rigidbody rb;
	public float speed;
	public float elasticSpeed;
//	public Transform birdCamera;

	private float forceX;
	private float forceY;
	private int status;
	private Vector3 position0;
	private bool isLaunched;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
		rb.useGravity = false;

		// Doing nothing
		status = 0;
		isLaunched = false;
    }

	void FixedUpdate()
	{
		// Doing nothing
		if (status == 0)
		{
			WaitInteraction ();
		}

		// Accumulating force
		if (status == 1)
		{
			AcumulateForce ();
		}

		// Throw
		if (status == 2)
		{
			Throw ();
		}
	}

	void WaitInteraction ()
	{
		float moveSubmit = Input.GetAxis("Fire1");

		if (moveSubmit == 1.0)
		{
			position0 = rb.position;
			status = 1;
		}
	}

	void AcumulateForce ()
	{
		float moveSubmit = Input.GetAxis("Fire1");
		float moveMouseX = Input.GetAxis("Mouse X");
		float moveMouseY = Input.GetAxis("Mouse Y");

		float yPull = moveMouseX + moveMouseY;

		forceX = forceX + moveMouseX;
		forceY = forceY + moveMouseY;

		Vector3 position;
		position = rb.position;
		rb.position = rb.position + new Vector3(moveMouseX * elasticSpeed, yPull * elasticSpeed, moveMouseY * elasticSpeed);

		if (moveSubmit == 0.0)
		{
			status = 2;
		}
	}

	void Throw ()
	{
		Vector3 movement = (position0 - rb.position) * speed;
		rb.useGravity = true;
		rb.AddForce(movement);
		status = 3;
		isLaunched = true;
	}

	void OnCollisionEnter (Collision collision)
	{
		if (isLaunched)
		{
//			Instantiate (rb, new Vector3 (0.0f, 4.15f, 0.0f), Quaternion.identity);
			Rigidbody newBird = (Rigidbody)Instantiate (rb, new Vector3 (0.0f, 4.15f, 0.0f), Quaternion.identity);
			CameraController.bird = newBird.transform;
			isLaunched = false;
		}
	}
}