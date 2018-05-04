using System.Collections;
using UnityEngine;

public class SwitchEnabled : MonoBehaviour
{

	public GameObject callingObject;

	public GameObject calledObject;
	// Use this for initialization
	
	void MenuTransition ()
	{
		callingObject.SetActive(false);
		calledObject.SetActive(true);
	}
	

}
