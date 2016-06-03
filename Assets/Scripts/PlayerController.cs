using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Rigidbody rb;

	public float speed;
	public float elasticSpeed;
	public int status;
	public float forceX;
	public float forceY;
	protected Vector3 position0;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
		rb.useGravity = false;

		// Doing nothing
		status = 0;
    }

	void FixedUpdate()
	{
		// Doing nothing
		if (status == 0)
		{
			float moveSubmit = Input.GetAxis("Fire1");

			if (moveSubmit == 1.0)
			{
				position0 = rb.position;
				status = 1;
			}
		}

		// Accumulating force
		if (status == 1)
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

		// Throw
		if (status == 2)
		{
			float yForce = forceX + forceY;
			// Vector3 movement = new Vector3 (-forceX * speed, -yForce * speed, -forceY * speed);
			Vector3 movement = (position0 - rb.position) * speed;
			rb.useGravity = true;
			rb.AddForce(movement);

			status = 3;
		}
	}
}
