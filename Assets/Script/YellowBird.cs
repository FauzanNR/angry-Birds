using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird: Birds {
	[SerializeField]
	public float _boostForce = 100;
	public bool _hasBoost = false;

	public void Boost() {
		if(state == BirdState.Thrown && !_hasBoost) {
			RigidBody.AddForce( RigidBody.velocity * _boostForce );
			_hasBoost = true;
		}
	}

	public override void onTap() {
		Boost();
	}
}
