using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Rigidbody rb;

	public float speed;
	public int status;
	public float forceX;
	public float forceY;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();

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
				status = 1;
			}
		}

		// Accumulating force
		if (status == 1)
		{
			float moveSubmit = Input.GetAxis("Fire1");
			float moveMouseX = Input.GetAxis("Mouse X");
			float moveMouseY = Input.GetAxis("Mouse Y");

			forceX = forceX + moveMouseX;
			forceY = forceY + moveMouseY;

			if (moveSubmit == 0.0)
			{
				status = 2;
			}
		}

		// Throw
		if (status == 2)
		{
			Vector3 movement = new Vector3 (-forceX * speed, 100.0f, -forceY * speed);
			rb.AddForce(movement);

			status = 3;
		}
	}
}
