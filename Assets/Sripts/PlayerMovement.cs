using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;

	public Rigidbody2D rb;
	public Camera cam;

	private Vector2 movement;
	private Vector2 mousePos;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;

		rb.freezeRotation = true;
		rb.gravityScale = 0f;
	}

	// Update is called once per frame
	private void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}
}