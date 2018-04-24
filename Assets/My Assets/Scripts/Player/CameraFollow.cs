using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform target;
	[SerializeField]
	private float distance;
	[SerializeField]
	private MouseLook mouseLook;

	void Start () {
		
		mouseLook.Init (transform, transform);

	}

	void Update () {

		mouseLook.LookRotation (transform, transform.GetChild(0));

		RaycastHit hit;
		Physics.SphereCast (target.position, .1f, -transform.GetChild(0).forward, out hit, distance);

		if (!hit.collider)
			transform.position = target.position - (transform.GetChild(0).forward * distance);
		else
			transform.position = hit.point;
		
	}

}
