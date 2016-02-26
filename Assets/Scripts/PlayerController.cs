using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float m_Speed = 5f;
	public float m_JumpSpeed = 12f;
	public int m_PlayerNumber = 1;

	private CapsuleCollider m_CapsuleCollider;
	private Animator m_Animator;
	private Rigidbody m_RigidBody;

	private string m_MovementAxisName; 
	private string m_JumpButtonName;
	private float m_MovementInputValue;



	private float m_jumpTimer;
	// Use this for initialization

	void Awake() {
		m_RigidBody = GetComponent<Rigidbody> ();
		m_Animator = GetComponent<Animator> ();
		m_CapsuleCollider = GetComponent<CapsuleCollider> ();
		m_RigidBody.AddForce(new Vector3(0,0,0));
	}
	void Start() {
		m_MovementAxisName = "Horizontal" + m_PlayerNumber;
		m_JumpButtonName = "Jump" + m_PlayerNumber;
		
	}
	void Update () {
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		Animate ();
	}
	void FixedUpdate() {
		Move ();
		//m_MovementAxisName ();
			
	}

	void Animate(){
		m_Animator.speed = 1 / Time.timeScale;
		if (Input.GetButton(m_MovementAxisName)) {
			if (m_MovementInputValue < 0f) {
				//transform.forward = new Vector3 (0f, 0f, -1f);
                transform.right = new Vector3(0f, 0f, 1f);
			} else {
				//transform.forward = new Vector3 (0f, 0f, 1f);
                transform.right = new Vector3(0f, 0f, -1f);
			}
			m_Animator.SetInteger("Speed", 1);

			m_MovementInputValue = Mathf.Abs (m_MovementInputValue);
		} else if (Input.GetButtonUp(m_MovementAxisName)) {
			m_Animator.SetInteger("Speed", 0);
		}

		if (Input.GetButton(m_JumpButtonName)) {

			m_jumpTimer = 1f;
			m_Animator.SetBool ("Jumping", true);

		}

		if (m_jumpTimer > 0.5f) {
			m_jumpTimer -= Time.deltaTime;
		} else if (m_Animator.GetBool ("Jumping") == true) {
			m_Animator.SetBool ("Jumping", false);
		}
	}


	void Move() {
		float deltaTime = Time.deltaTime;
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime * (1/Time.timeScale); //TODO: SEE_LEDIO: multiplying by 1/TimeScale makes it ignore the scale Factor. Same applied in Camera Control
		m_RigidBody.MovePosition (m_RigidBody.position + movement);
	}




}
