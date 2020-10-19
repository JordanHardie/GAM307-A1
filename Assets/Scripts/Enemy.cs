﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float rotationSpeed = 180; // In degrees per second

	[SerializeField]
	private float movementSpeed = 1f; // In units per second

	[SerializeField]
	private float meshRadius = 1f; // In units

	[SerializeField]
	private Animator animatorComponent;

	private IEnumerator turnTowardsPlayerCoroutine;
	private IEnumerator moveTowardsPlayerCoroutine;

	public bool isDead = false;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player" && !isDead)
		{
			float playerDistance = Vector3.Distance(collider.transform.position, transform.position);

			// Ignore trigger events from the inner colliders
			if (playerDistance >= 2f * meshRadius)
			{
				turnTowardsPlayerCoroutine = TurnTowardsPlayer(collider.transform);
				moveTowardsPlayerCoroutine = MoveTowardsPlayer(collider.transform);
				StartCoroutine(turnTowardsPlayerCoroutine);
				StartCoroutine(moveTowardsPlayerCoroutine);
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Player")
		{
			float playerDistance = Vector3.Distance(collider.transform.position, transform.position);
			
			// Ignore trigger events from the inner colliders
			if (playerDistance >= 2f * meshRadius)
			{
				StopCoroutine(turnTowardsPlayerCoroutine);
				StopCoroutine(moveTowardsPlayerCoroutine);
				animatorComponent.SetTrigger("Idle");
			}
		}
	}

	void OnDeath()
	{
		if (isDead)
		{
			return;
		}

		isDead = true;

		StopCoroutine (turnTowardsPlayerCoroutine);
		StopCoroutine (moveTowardsPlayerCoroutine);

		Destroy (gameObject, 3f);
	}

	private IEnumerator TurnTowardsPlayer(Transform player)
	{
		while (true)
		{
			Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);
			targetRotation.x = 0f;
			targetRotation.z = 0f;

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			yield return 0;
		}
	}

	private IEnumerator MoveTowardsPlayer(Transform player)
	{
		animatorComponent.SetTrigger("Walk");
		while (true)
		{
			Vector3 playerDirection = transform.position - player.position;
			playerDirection.y = 0;
			playerDirection = playerDirection.normalized;

			Vector3 deltaMovement = playerDirection * movementSpeed * Time.deltaTime;

			int layermask = LayerMask.GetMask("Environment");
			Vector3 movingTowards = transform.position - playerDirection*meshRadius + (new Vector3(0f, 0.1f, 0f));

			transform.position -= deltaMovement;

			yield return 0;
		}
	}
}
