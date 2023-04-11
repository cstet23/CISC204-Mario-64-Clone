using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private float vertSpeed;
	private Animator animator;
	private CharacterController charController;
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

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

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        animator = GetComponent<Animator>();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);


        
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}