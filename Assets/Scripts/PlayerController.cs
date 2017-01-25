using UnityEngine;

namespace Roku.Player {

	public class PlayerController : MonoBehaviour {

		private CharacterController characterController;
		private Camera cam;
		
		[SerializeField] private PlayerMouseLook playerMouseLook;

		private float speed = 10f;


		// Use this for initialization
		void Start () {
			characterController = GetComponent<CharacterController>();
			cam = Camera.main;

			playerMouseLook.Init(transform, cam.transform);
		}
		
		// Update is called once per frame
		void Update () {
			
			playerMouseLook.LookRotation(transform, cam.transform);

			Vector3 moveDirection = Vector3.zero;

			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			characterController.Move(moveDirection * Time.deltaTime);
		}
	}
}