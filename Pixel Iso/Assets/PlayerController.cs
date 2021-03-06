﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;
	Vector2 movement;
	Rigidbody2D playerRigidbody;
	private Animator anim;
	public bool canMove = true;
	public float H;
	public float V;
	public float HPause;
	public float VPause;
	private float HDelay;
	private float VDelay;
	private Animator LegAnim;


	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		LegAnim = gameObject.transform.GetChild (1).GetComponent<Animator> ();
	}

	void FixedUpdate () {
		if (canMove) {
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");

			if (h != 0 || v != 0) {
				anim.SetBool ("Walking", true);
				LegAnim.SetBool ("Walking", true);
				if (h > 0) {
					anim.SetFloat ("LastMoveX", 1f);
					LegAnim.SetFloat ("LastMoveX", 1f);
					H = 1f;
					HDelay = Time.time + HPause;
				} else if (h < 0) {
					anim.SetFloat ("LastMoveX", -1f);
					LegAnim.SetFloat ("LastMoveX", -1f);
					H = -1f;
					HDelay = Time.time + HPause;
				} else if (h == 0 && HDelay < Time.time) {
					anim.SetFloat ("LastMoveX", 0f);
					LegAnim.SetFloat ("LastMoveX", 0f);
					H = 0f;
				} 

				if (v > 0) {
					anim.SetFloat ("LastMoveY", 1f);
					LegAnim.SetFloat ("LastMoveY", 1f);
					V = 1f;
					VDelay = Time.time + VPause;
				} else if (v < 0) {
					anim.SetFloat ("LastMoveY", -1f);
					LegAnim.SetFloat ("LastMoveY", -1f);

					V = -1f;
					VDelay = Time.time + VPause;
				} else if (v == 0 && VDelay < Time.time) {
					anim.SetFloat ("LastMoveY", 0f);
					LegAnim.SetFloat ("LastMoveY", 0f);
					V = 0f;
				} 

			} else {
				anim.SetBool ("Walking", false);
				LegAnim.SetBool ("Walking", false);
			}
			Move (h, v);
		}
	}

	void Move (float h, float v)
	{
		movement.Set (h, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition ((Vector2)gameObject.transform.position + movement);


	}


}
