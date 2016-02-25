using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	private Rigidbody m_Rigidbody;
	//private string m_TimeStopAxisName = "TimeStop";
	//private float m_TimeStopAxisValue;
	public float m_Acceleration = 5f;
	public float m_InitialSpeed = 30f; //m/s //TODO: How to adjust variable to correspond to player movement
	public float m_TimeScale = 0.00000001f;

	bool m_TimeIsScaled = false;

	Vector3 m_NotScaledVelocity;
	// Use this for initialization
	void Start () {
		
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Rigidbody.velocity = transform.forward * m_InitialSpeed;
		m_NotScaledVelocity = m_Rigidbody.velocity ;
	}
	
	// Update is called once per frame
	void Update () {

		//m_TimeStopAxisValue = Input.GetAxis (m_TimeStopAxisName);
		//if (m_TimeStopAxisValue != 0f) {
			//ToggleStopTime ();
		//}

		if(Input.GetKeyDown("left shift")){ 
			ToggleTimeScale ();
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
		if (m_TimeIsScaled) {
			m_Rigidbody.velocity = m_Rigidbody.velocity * m_TimeScale; //TODO: thi works but we don't know why!!! but works for now. We should NOT scale velocity every update
		}
			
		m_Rigidbody.AddForce (transform.forward*m_Acceleration*Time.deltaTime, ForceMode.Impulse);
	}

	private void ToggleTimeScale() {
		if (Time.timeScale == 1f) {
			Time.timeScale = m_TimeScale;
			m_TimeIsScaled = true;
			m_NotScaledVelocity = m_Rigidbody.velocity;
		} else {
			Time.timeScale = 1f;
			m_TimeIsScaled = false;
			m_Rigidbody.velocity = m_NotScaledVelocity;
		}
	}
}
