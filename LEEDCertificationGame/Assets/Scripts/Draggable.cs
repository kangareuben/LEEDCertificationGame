using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

	bool decrementedText = false;
	GameManager gm;
	int index;

	public int Index
	{
		get{return index;}
		set{index = value;}
	}

	// Use this for initialization
	void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnMouseDrag()
	{
		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
		transform.position = new Vector3(pos_move.x, pos_move.y, transform.position.z);

		if(!decrementedText)
		{
			decrementedText = true;
			gm.IncrementOrDecrementText(index, -1);
		}
	}

	void OnMouseUpAsButton()
	{
		Debug.Log("mouse released");
	}
}
