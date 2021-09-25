using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController: MonoBehaviour {
	// Start is called before the first frame update
	public Text text;
	public TrailController trail;
	public BoxCollider2D tapCollider;
	public SlingShooter SlingShooter;
	public List<Birds> birds;
	public List<Enemy> enemies;
	private bool isEnd = false;
	private Birds _shotBird;
	void Start() {

		tapCollider.enabled = false;
		foreach(Birds bird in birds) {
			bird.onBirdDestroyed += changeBird;
			bird.onBirdShot += assignTrail;
		}

		foreach(Enemy enemy in enemies) {
			enemy.onEnemyDestroyed += checkNoEnemy;
		}
		SlingShooter.InitiateBird( birds[0] );
	}

	private void assignTrail(Birds arg0) {
		trail.setBird( arg0 );
		StartCoroutine( trail.spawnTrail() );
		tapCollider.enabled = true;
	}

	private void checkNoEnemy(GameObject arg0) {

		for(int i = 0; i < enemies.Count; i++) {
			if(enemies[i].gameObject != null)
				if(enemies[i].gameObject == arg0) {
					enemies.RemoveAt( i );
					break;
				}
		}

		if(enemies.Count == 0) {
			isEnd = true;
			text.enabled = true;
			text.transform.localPosition = new Vector2( 0f, 1f ) * Time.deltaTime * -3f;
			if(SceneManager.GetActiveScene().buildIndex == 0) StartCoroutine( loadNewScene() );
		}
	}

	IEnumerator loadNewScene() {
		yield return new WaitForSeconds( 3 );
		SceneManager.LoadScene( 1 );
	}

	public void changeBird() {
		tapCollider.enabled = false;
		if(isEnd) return;

		birds.RemoveAt( 0 );
		if(birds.Count > 0) {
			SlingShooter.InitiateBird( birds[0] );
			_shotBird = birds[0];
		}
	}

	void OnMouseUp() {
		if(_shotBird != null) {
			_shotBird.onTap();
		}
	}
}
