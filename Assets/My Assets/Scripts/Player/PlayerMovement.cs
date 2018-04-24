using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	public PlayerInputButtons playerInput;
	[SerializeField]
	public MovementOptions playerMovement;

	private CharacterController m_characterController;

	private Vector3 velocity;

	void Start () {

		m_characterController = GetComponent<CharacterController> ();

	}

	void Update () {
		
		Vector3 forwardDir = (Vector3.Scale(Camera.main.transform.forward, new Vector3 (1, 0, 1))).normalized;
		Vector3 rightDir = Quaternion.Euler (new Vector3 (0, 90, 0)) * forwardDir;

		Vector3 movement = new Vector3 ();

		movement += playerMovement.movementSpeed * Input.GetAxis(playerInput.forwardAxis) * forwardDir;
		movement += playerMovement.movementSpeed * Input.GetAxis(playerInput.rightAxis) * rightDir;

		float angleDiff = angle(new Vector2(forwardDir.x, forwardDir.z), new Vector2(transform.forward.x, transform.forward.z));

		angleDiff = Mathf.Clamp (angleDiff, -playerMovement.turnSpeed * Time.deltaTime, playerMovement.turnSpeed * Time.deltaTime);

		if ((movement).magnitude != 0)
			transform.Rotate (0, -angleDiff, 0);
		
		if (Input.GetButtonDown (playerInput.jumpButton))
			velocity += Vector3.up * playerMovement.jumpSpeed;
		
		velocity += Physics.gravity * Time.deltaTime;

		movement += velocity;

		m_characterController.Move (movement * Time.deltaTime);

		if (m_characterController.isGrounded)
			velocity = new Vector3 ();
		
	}

	[System.Serializable]
	public class PlayerInputButtons {

		public string forwardAxis;
		public string rightAxis;
		public string jumpButton;

	}

	[System.Serializable]
	public class MovementOptions {

		public float movementSpeed;
		public float turnSpeed;
		public float jumpSpeed;

	}

	private float angle (Vector2 a, Vector2 b) {

		Vector2 rot = new Vector2 (-b.y, b.x);
		float sign = (Vector2.Dot (a, rot) < 0) ? -1 : 1;
		return Vector2.Angle (a, b) * sign;

	}

}
