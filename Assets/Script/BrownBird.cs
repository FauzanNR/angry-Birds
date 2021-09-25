using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird: Birds {
	[SerializeField]
	public float _force = 10f;
	public float raduis = 5f;
	public bool _hasBoost = false;
	private Collider2D[] inRadius;

	public void explode() {
		inRadius = Physics2D.OverlapCircleAll( transform.position, raduis );

		if((state == BirdState.Thrown || state == BirdState.HitSomething) && !_hasBoost) {

			Collider.transform.localScale = new Vector3( 1.1f, 1.1f );
			foreach(var item in inRadius) {
				Rigidbody2D rigidbody = item.GetComponent<Rigidbody2D>();
				if(rigidbody != null) {
					Vector2 distance = item.transform.position - transform.position;
					if(distance.magnitude > 0) {
						rigidbody.AddForce( distance * _force );
					}
				}
			}
			_hasBoost = true;
		}
	}

	public override void onTap() {
		explode();
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere( transform.position, raduis );
	}
}
