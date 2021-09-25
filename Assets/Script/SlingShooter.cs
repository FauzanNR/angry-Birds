using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter: MonoBehaviour {

	public CircleCollider2D colliderBody;
	private Vector2 _startPos;
	private Birds _bird;
	public LineRenderer trajectory;

	[SerializeField]
	private float _radius = 0.75f;

	[SerializeField]
	private float _throwSpeed = 30f;

	void Start() {
		_startPos = transform.position;
	}

	void OnMouseUp() {
		colliderBody.enabled = false;
		Vector2 velocity = _startPos - ( Vector2 )transform.position;
		float distance = Vector2.Distance( _startPos, transform.position );
		_bird.Shoot( velocity, distance, _throwSpeed );
		gameObject.transform.position = _startPos;
		trajectory.enabled = false;
	}

	void OnMouseDrag() {
		Vector2 p = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		Vector2 dir = p - _startPos;
		if(dir.sqrMagnitude > _radius) dir = dir.normalized * _radius;
		transform.position = _startPos + dir;
		float distance = Vector2.Distance( _startPos, transform.position );

		if(!trajectory.enabled) trajectory.enabled = true;
		DrawTrajectory( distance );


	}

	private void DrawTrajectory(float distance) {
		if(_bird == null) return;
		Vector2 velocity = _startPos - ( Vector2 )transform.position;
		int segmentSize = 5;
		Vector2[] segments = new Vector2[segmentSize];

		segments[0] = transform.position;
		Vector2 segmentVelocity = velocity * _throwSpeed * distance;
		for(int i = 1; i < segmentSize; i++) {
			float elapsTime = i * Time.fixedDeltaTime * segmentSize;
			segments[i] = segments[0] + segmentVelocity * elapsTime + 0.5f * Physics2D.gravity * Mathf.Pow( elapsTime, 2 );
		}

		trajectory.positionCount = segmentSize;
		for(int i = 0; i < segmentSize; i++) {
			trajectory.SetPosition( i, segments[i] );
		}
	}

	public void InitiateBird(Birds birds) {
		_bird = birds;
		_bird.MoveTo( gameObject.transform.position, gameObject );
		colliderBody.enabled = true;

	}
}
