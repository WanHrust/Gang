using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	private Rigidbody m_Rigidbody;
	public TimeController m_TimeController;
	//private string m_TimeStopAxisName = "TimeStop";
	//private float m_TimeStopAxisValue;
	public float m_Acceleration = 5f;
	public float m_InitialSpeed = 30f; //m/s //TODO: How to adjust variable to correspond to player movement

	private float m_PrevTimeScale; //store the value of time scale of the previous frame. Required to check if the scale has changed

	Vector3 m_NotScaledVelocity;
	// Use this for initialization
	void Start () {
		
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Rigidbody.velocity = transform.forward * m_InitialSpeed;
		m_NotScaledVelocity = m_Rigidbody.velocity ;
		m_PrevTimeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateOnTimeScale();
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

		//TODO: SEE_LEDIO: Gor rid of this. it was wrong. It worked fine with the one time scale. the problem is when you change the time scale, velocity is always very small.
		//Now I change velocity once in UpdateOnTimeScale();
//		if (m_TimeController.IsTimeScaled()) {
//			//m_Rigidbody.velocity = m_Rigidbody.velocity * Time.timeScale; //TODO: thi works but we don't know why!!! but works for now. We should NOT scale velocity every update
//		}
			
		m_Rigidbody.AddForce (transform.forward*m_Acceleration*Time.deltaTime, ForceMode.Impulse);
	}

	
	//TODO: SEE_LEDIO: This function will handle everthing that has to be done with object after time scale has changed.
	// Will need similar funciton for each class we want to be affected by time scale
	private void UpdateOnTimeScale(){
		if (m_PrevTimeScale != Time.timeScale) { 
			if (m_TimeController.IsTimeScaled ()) {
				m_NotScaledVelocity = m_Rigidbody.velocity;
				m_Rigidbody.velocity = m_Rigidbody.velocity * Time.timeScale; // Here is the proper place to update velocity. however we get now jiggling
			} else {
				m_Rigidbody.velocity = m_NotScaledVelocity;
			}
		}
		m_PrevTimeScale = Time.timeScale; //update m_PrevTimeScale to the current time scale after we done cheking if it changed.

	}
}
