using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAIScript : MonoBehaviour
{
    public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private float vertSpeed;
	private Animator animator;
	private CharacterController charController;
	void Start(){
		animator = GetComponent<Animator>();

	}

	void Update() {
		Vector3 movement = Vector3.zero;
		transform.Translate(0, 0, speed * Time.deltaTime);	
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.SphereCast(ray, 0.75f, out hit)) {
			if (hit.distance < obstacleRange) {
				float angle = Random.Range(-110, 110);
				transform.Rotate(0, angle, 0);
			}
		}
		animator.SetFloat("Speed", movement.sqrMagnitude);
		bool hitGround = false;
		if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
			float check = (charController.height + charController.radius) / 1.9f;
			hitGround = hit.distance <= check;	// to be sure check slightly beyond bottom of capsule
		}
		movement *= Time.deltaTime;
		charController.Move(movement);
	}

}