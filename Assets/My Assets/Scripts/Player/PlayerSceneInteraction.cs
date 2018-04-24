using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneInteraction : MonoBehaviour {

	[SerializeField]
	private float interactDistance = 3;						//max distance from player to interactable object

	private InteractableObject[] m_interactableObjects;	//list of all interactable objects in the scene
	private Vector3 m_prevPos;							//previous player position
	private int m_prevObj = -1;								//previously selected object id

	void Start() {

		//load all interactable objects in the scene
		m_interactableObjects = GameObject.FindObjectsOfType<InteractableObject> ();
		//set previous position to something not equal to the current position to trigger an update on the first cycle
		m_prevPos = transform.position + Vector3.down;

	}

	void Update() {

		//only update notifications when the player moves
		if (transform.position != m_prevPos) {

			//initialize object detection to detect nothing
			float closestDist = interactDistance;
			int closestObj = -1;

			//find the closest object in the scene
			//switch to prevent array errors
			switch (m_interactableObjects.Length) {

			case 0:		//if there are no interactable objects, leave closestObj at -1 to find nothing
				break;

			case 1:		//if there is one interactable object, find it's distance and select it
				closestDist = Vector3.Distance (transform.position, m_interactableObjects [0].transform.position);
				if (closestDist < interactDistance)
					closestObj = 0;
				break;

			default:	//if there are more than one interactable objects, find the closest one that is closer than the maximum player reach
				for (int i = 0; i < m_interactableObjects.Length; i++) {

					float dist = Vector3.Distance (transform.position, m_interactableObjects [i].transform.position);

					if (dist < closestDist) {
						closestDist = dist;
						closestObj = i;
					}

				}
				break;
			}

			//if the object selection has changed since the last update, update the active object
			if (closestObj != m_prevObj) {
				
				//if there was an object selected, deselect it
				if (m_prevObj != -1)
					m_interactableObjects [m_prevObj].setActive (false);
				//if there is now an object selected, select it
				if (closestObj != -1)
					m_interactableObjects [closestObj].setActive (true);

				m_prevObj = closestObj;

			}

			//reset player position for next update
			m_prevPos = transform.position;

		}

	}

}
