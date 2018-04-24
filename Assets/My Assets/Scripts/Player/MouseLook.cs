using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

	[SerializeField]
	private string verticalLookAxis;
	[SerializeField]
	private string horizontalLookAxis;

	void Update () {

		float vert = 0;
		if (!verticalLookAxis.Equals (""))
			vert = Input.GetAxis (verticalLookAxis);
		
		float hori = 0;
		if (!horizontalLookAxis.Equals (""))
			vert = Input.GetAxis (horizontalLookAxis);

		Debug.Log (hori + ", " + vert);

		transform.eulerAngles = transform.eulerAngles + new Vector3 (vert, hori, 0);

	}

}
