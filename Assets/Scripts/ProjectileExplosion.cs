using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour {

	public GameObject explosion;
	
	private AudioSource audioSource;
	public AudioClip bounceSound;
	public AudioClip explosionSound;

	private bool explodeOnTouch = true;

	//private bool fuseStarted = false;
	private WaitForSeconds fuseLength = new WaitForSeconds(3f);

	void Start() {
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(fuse());
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

		if(explodeOnTouch) {
			explode();
		}
		else {
			audioSource.PlayOneShot(bounceSound, 0.5f);	
		}
	}

	public void setExplodeOnTouch() {
		explodeOnTouch = true;
	}

	private IEnumerator fuse() {
		
		yield return fuseLength;

		GameObject p = explode();
				
		// particle effect duration is approx +10 seconds
		yield return new WaitForSeconds(11f);
		Destroy(p);
		Destroy(gameObject);
	}

	private GameObject explode() {
		GameObject p = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		p.GetComponent<ParticleSystem>().Play();
		audioSource.PlayOneShot(explosionSound);
		GetComponent<Renderer>().enabled = false;

		return p;
	}

}
