using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (PlayerMovement))]
public class PlayerMovementState : MonoBehaviour {
	
	[SerializeField]
	private float transitionLength = .2f;

	[SerializeField]
	private MovementState[] moveStates;

	private MovementState currentState;

	private CharacterController charCon;
	private PlayerMovement movement;

	void Start () {

		charCon = GetComponent<CharacterController> ();
		movement = GetComponent<PlayerMovement> ();

	}

	void Update () {

		foreach (MovementState ms in moveStates) {

			foreach (string s in ms.enablers) {
				if (Input.GetButtonDown (s))
					goToState (ms);
			}

			foreach (string s in ms.disablers) {
				if (Input.GetButtonDown (s))
					goToState (moveStates[0]);
			}

			foreach (string s in ms.toggles) {
				if (Input.GetButtonDown (s)) {
					if (currentState.Equals (ms))
						goToState (moveStates [0]);
					else
						goToState (ms);
				}
			}

		}

		charCon.radius = currentState.radius;
		charCon.height = currentState.height;
		movement.playerMovement.movementSpeed = currentState.speed;

	}

	[System.Serializable]
	private class MovementState {

		public float height;
		public float radius;
		public float speed;
		public string[] enablers;
		public string[] disablers;
		public string[] toggles;

		public MovementState(float height, float radius, float speed) {

			this.height = height;
			this.radius = radius;
			this.speed = speed;

		}

		public MovementState(float height, float radius, float speed, string[] enablers, string[] disablers, string[] toggles) {

			this.height = height;
			this.radius = radius;
			this.speed = speed;
			this.enablers = enablers;
			this.disablers = disablers;
			this.toggles = toggles;

		}

		public static MovementState lerp(MovementState start, MovementState end, float t) {

			t = Mathf.Clamp (t, 0, 1);

			return new MovementState(
				start.height * (1-t) + end.height * t,
				start.radius * (1-t) + end.radius * t,
				start.speed * (1-t) + end.speed * t);

		}

		public override bool Equals (object obj)
		{

			MovementState other = (MovementState)obj;

			return other.height == height && other.radius == radius && other.speed == speed;

		}

	}

	IEnumerator goToState(MovementState target) {

		float t = 0;

		while (t < transitionLength) {

			currentState = MovementState.lerp (currentState, target, t/transitionLength);

			t += Time.deltaTime;

		}

		return null;

	}

}
