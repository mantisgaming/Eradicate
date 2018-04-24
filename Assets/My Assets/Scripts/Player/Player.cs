using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (PlayerHealth))]
public class Player : MonoBehaviour {

	public static Player activePlayer;
	public PlayerHealth health;

	public Player () {

		if (activePlayer == null)
			activePlayer = this;
		else
			Debug.LogWarning ("Multiple players created");

	}

	void Start() {
		health = GetComponent<PlayerHealth> ();
	}

	void OnDestroy() {

		if (activePlayer == this)
			activePlayer = null;

	}

}
