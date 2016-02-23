using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour
{
	public float speed;
	public float stopY = 5000;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(transform.position.y < stopY)
		{
			Vector3 temp = transform.position;
			temp.y += speed;
			transform.position = temp;
		}
	}
}
