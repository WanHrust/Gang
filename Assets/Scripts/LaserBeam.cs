
//This is free to use and no attribution is required
//No warranty is implied or given
using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	LineRenderer m_Line;
	public float m_Force = 500.0f; // Force with which the laser beam hits the target
	public float m_LaserDuration= 1.0f; // How fast beam will reach its max lenght
	void Start() {
		m_Line = gameObject.GetComponent<LineRenderer> ();
		m_Line.enabled = false; //turn of line that it doesnot show from start
	}

	void Update() {
		if (Input.GetButtonDown ("Fire1")) {
			StopCoroutine ("FireLaser"); //TODO: read about coroutines
			StartCoroutine ("FireLaser");
		}
	}

	IEnumerator FireLaser () {
		m_Line.enabled = true;
		float counter = 0.0f;
		while (Input.GetButton ("Fire1")) {
			Ray ray = new Ray (transform.position,transform.forward);
			RaycastHit hit;
			counter += Time.deltaTime / m_LaserDuration; //Counter that counts till LaserDuration 
			m_Line.SetPosition (0, ray.origin); // where our linerenderer starts
			float length = Mathf.Lerp (0, 100, counter); 

			if (Physics.Raycast (ray, out hit,length)) { // check if the ray has hit someone
				m_Line.SetPosition (1, hit.point);
				if (hit.rigidbody) { // check if the hitted object has a rigid body.
					hit.rigidbody.AddForceAtPosition (transform.forward * m_Force * Time.timeScale, hit.point);
				}
			} else {
				m_Line.SetPosition (1, ray.GetPoint(length));
			}

			yield return null; // TODO: Some magic happens have to read about Corutines
		}
		counter = 0.0f;
		m_Line.enabled = false;
	}
}

