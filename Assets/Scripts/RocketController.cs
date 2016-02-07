using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	private Rigidbody m_Rigidbody;
	public float m_Acceleration = 5;
	public Vector3 m_InitialVelocity;
	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Rigidbody.velocity = m_InitialVelocity;;
	}
	
	// Update is called once per frame
	void Update () {
		
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
		m_Rigidbody.AddForce (transform.forward*m_Acceleration, ForceMode.Impulse);
	}
}
