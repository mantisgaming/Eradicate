using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour {

	[SerializeField]
	private UnityEvent onInteract;
	[SerializeField]
	private string interactButton = "Interact";
	[SerializeField]
	private GameObject interactionSprite;

	private Animator spriteAnimator;

	private bool m_isActive = false;

	void Update() {
		if (m_isActive) {
			if (Input.GetButtonDown (interactButton)) {
				spriteAnimator.SetTrigger ("Press");
				onInteract.Invoke ();
			}
		}
	}

	public void setActive (bool active) {
		m_isActive = active;

		if (!spriteAnimator)
			spriteAnimator = Instantiate (interactionSprite, transform).GetComponent<Animator> ();
		spriteAnimator.SetBool ("Visible", active);
	}

	public void winLevel () {
		Debug.Log ("Win: " + name);
	}

	public void activateObject (Activatable target) {
		target.activate ();
	}

	public void loadScene (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void damagePlayer (float damage) {
		Player.activePlayer.health.changeHealth (-damage);
	}
}
