using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniScript : MonoBehaviour
{
	public NavMeshAgent mini;
	
	public Transform player;

	public LayerMask whatIsGround, whatisPlayer;

	public Vector3 walkPoint;
	bool walkPointSet;
	public float walkPointRange;

	public float sightRange, runRange;
	public bool playerInSightRange, playerInRunRange;


	private void Awake()
	{
		player = GameObject.Find("Player").transform;
		mini = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
		playerInRunRange = Physics.CheckSphere(transform.position, runRange, whatisPlayer);

		if(!playerInSightRange && !playerInRunRange) Patroling();
		if(playerInSightRange && !playerInRunRange) RunFromPlayer();
	}

	private void Patroling()
	{
		if(!walkPointSet) SearchWalkPoint();
		if(walkPointSet)
			mini.SetDestination(walkPoint);
		Vector3 distanceToWalkPoint = transform.position - walkPoint;
		if(distanceToWalkPoint.magnitude < 1f)
			walkPointSet = false;
	}
	private void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange,walkPointRange);
		float randomX = Random.Range(-walkPointRange,walkPointRange);

		walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		if(Physics.Raycast(walkPoint, -transform.up,2f,whatIsGround))
			walkPointSet = true;
	}
	private void RunFromPlayer()
	{
		mini.SetDestination(player.position);
	}
}