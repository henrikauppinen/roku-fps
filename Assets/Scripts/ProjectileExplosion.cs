using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour {

	public GameObject explosion;
	
	private AudioSource audioSource;
	public AudioClip bounceSound;
	public AudioClip explosionSound;

	private bool explodeOnTouch = false;
	private bool hasExploded = false;

	//private bool fuseStarted = false;
	private WaitForSeconds fuseLength = new WaitForSeconds(3f);

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	public void armProjectile(bool instantExplode) {
		if(instantExplode) {
			explodeOnTouch = true;
		}
		else {
			StartCoroutine(fuse());
		}
	}

	void OnCollisionEnter() {
		if(explodeOnTouch && !hasExploded) {
			StartCoroutine(explode());
		}
	}

	public void setExplodeOnTouch() {
		explodeOnTouch = true;
	}

	private IEnumerator fuse() {
		yield return fuseLength;
		yield return StartCoroutine( explode() );
	}

	private IEnumerator explode() {
		hasExploded = true;

		GameObject p = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		p.GetComponent<ParticleSystem>().Play();
		audioSource.PlayOneShot(explosionSound);
		GetComponent<Renderer>().enabled = false;

		// particle effect duration is approx +10 seconds
		yield return new WaitForSeconds(11f);

		Destroy(p);
		Destroy(gameObject);
	}

}
