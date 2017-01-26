using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour {

	public Rigidbody projectile;
	public Transform spawnPoint;
	
	[SerializeField] private float projectileForce = 200f;
	[SerializeField] private float fireRate = 0.25f;

	private AudioSource sound;
	private float nextFireTime;
	
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 0.3f);
    }


	void Start() {
		sound = GetComponent<AudioSource>();
	}

	void Update () {
		if(Input.GetButtonDown("Fire1") && Time.time > nextFireTime) {
			Fire(true);
		}
		
		// firemode 2, bouncy grenade
		if(Input.GetButtonDown("Fire2") && Time.time > nextFireTime) {
			Fire(false);
		}
	}

	private void Fire(bool explodeOnTouch) {
		Rigidbody cloneRb = Instantiate(projectile, spawnPoint.position, Quaternion.identity) as Rigidbody;
		cloneRb.GetComponent<ProjectileExplosion>().armProjectile(explodeOnTouch);
		
		cloneRb.AddForce(spawnPoint.forward * projectileForce);
		
		nextFireTime = Time.time + fireRate;
		sound.Play();
	}
}
