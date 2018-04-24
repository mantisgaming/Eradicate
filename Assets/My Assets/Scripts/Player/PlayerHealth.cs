using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	
	[SerializeField]
	private float maxHealth = 100;

	private float health;

	void Awake() {
		health = maxHealth;
	}

	void Update() {
		
	}

	public void heal() {
		health = maxHealth;
	}

	public void changeHealth(float diff) {
		health += diff;
	}

}
