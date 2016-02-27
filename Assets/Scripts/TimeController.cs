using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {

	public float m_TimeScale = 0.00000001f;
	private bool m_TimeIsScaled = false;
	// Use this for initialization
	void Start () {
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

	private void ToggleTimeScale() {
		if (Time.timeScale == 1f) {
			Time.timeScale = m_TimeScale;
			m_TimeIsScaled = true;
		} else {
			Time.timeScale = 1f;
			m_TimeIsScaled = false;
		}
		Time.fixedDeltaTime = 0.02F * Time.timeScale; //TODO: SEE_LEDIO: this line actually solves the problem with jiggling 
	}

	//getter for m_TimeIsScaled. Protecting the variable from changing from outside the class.
	public bool IsTimeScaled() {
		return m_TimeIsScaled;
	}
}
