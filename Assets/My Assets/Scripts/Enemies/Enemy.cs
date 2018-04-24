using UnityEngine;

public abstract class Enemy {

	protected float detection;
	protected GameObject objectOfInterest;

	protected abstract void InitializeAI();
	protected abstract void UpdateAI();

}
