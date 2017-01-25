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
			Fire();
		}
		
		// firemode 2, bouncy grenade
		if(Input.GetButtonDown("Fire2") && Time.time > nextFireTime) {
			Fire();
		}
	}

	private void Fire() {
		Rigidbody cloneRb = Instantiate(projectile, spawnPoint.position, Quaternion.identity) as Rigidbody;
		cloneRb.GetComponent<ProjectileExplosion>().armProjectile(true);
		
		cloneRb.AddForce(spawnPoint.forward * projectileForce);
		
		nextFireTime = Time.time + fireRate;
		sound.Play();
	}
}
