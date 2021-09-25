using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController: MonoBehaviour {

	public GameObject trail;
	public Birds targetObject;

	private List<GameObject> trailList;

	void Start() {
		trailList = new List<GameObject>();
	}

	public void setBird(Birds bird) {
		targetObject = bird;
		foreach(GameObject i in trailList) {
			Destroy( i.gameObject );
		}
		trailList.Clear();
	}

	public IEnumerator spawnTrail() {
		trailList.Add( Instantiate( trail, targetObject.transform.position, Quaternion.identity ) );
		yield return new WaitForSeconds( 0.06f );
		if(targetObject != null && targetObject.state != Birds.BirdState.HitSomething) {
			StartCoroutine( spawnTrail() );
		}
	}
}
