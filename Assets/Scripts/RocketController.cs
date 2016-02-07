using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	private Rigidbody m_Rigidbody;
	private string m_TimeStopAxisName = "TimeStop";
	private float m_TimeStopAxisValue;
	public float m_Acceleration = 5f;
	public float m_InitialSpeed = 30f; //m/s //TODO: How to adjust variable to correspond to player movement
	public float m_GravityForce = 30f;

	float m_Timer;
	float m_PrevTime = 0f;
	float m_DeltaTime;
	bool m_StopTime = false;
	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody> ();
		//m_Rigidbody.velocity = transform.forward * m_InitialVelocity;
	}
	
	// Update is called once per frame
	void Update () {

		m_PrevTime = m_Timer;
		if (!m_StopTime) {
			m_Timer += Time.deltaTime;
		}
		m_DeltaTime = m_Timer - m_PrevTime;

		//m_TimeStopAxisValue = Input.GetAxis (m_TimeStopAxisName);
		//if (m_TimeStopAxisValue != 0f) {
			//ToggleStopTime ();
		//}

		if(Input.GetKeyDown("left shift")){
			ToggleTimeStop ();
		}
	}
	void OnMouseDown () {
		ReverseDirection ();
	}
	void FixedUpdate() {
		Move ();
	}

	private void ReverseDirection() {
		m_Rigidbody.transform.forward = -m_Rigidbody.transform.forward;
	}
	private void Move() {
		//m_Rigidbody.MovePosition (m_Rigidbody.transform.position + m_Rigidbody.transform.forward*m_Speed*m_DeltaTime);
		Vector3 movement = m_Rigidbody.transform.forward*m_InitialSpeed*m_DeltaTime + new Vector3(0f,-1f,0)*m_GravityForce*m_DeltaTime; //TODO: Should we use gravity?
		m_Rigidbody.velocity = movement;
		m_Rigidbody.AddForce (transform.forward*m_Acceleration*m_DeltaTime, ForceMode.Impulse);
	}
	private void ToggleTimeStop() {
		if (m_StopTime) {
			m_StopTime = false;
		} else {
			m_StopTime = true;
		}
	}
}
