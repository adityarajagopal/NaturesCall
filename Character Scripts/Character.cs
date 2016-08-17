using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	protected Rigidbody2D myBody;
	protected Animator anim;
	protected float health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeDamage(){

	}

	public void jump(){}

	public void die(){

	}
}