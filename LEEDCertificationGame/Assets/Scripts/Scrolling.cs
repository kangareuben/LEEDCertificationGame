using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour
{
	public float speed;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		Vector3 temp = transform.position;
		temp.y += speed;
		transform.position = temp;
	}
}
