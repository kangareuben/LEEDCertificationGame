using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour
{
	bool decrementedText = false;
	GameManager gm;
	int index;
	int onBuildingNumber = -1;

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
		if(gm.IconsDraggable)
		{
			float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
			transform.position = new Vector3(pos_move.x, pos_move.y, transform.position.z);

			if(!decrementedText)
			{
				decrementedText = true;
				gm.IncrementOrDecrementText(index, -1);
			}

			if(onBuildingNumber != -1)
			{
				CheckAllTheCasesBackwards();
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -gm.IconValues[index]);
				onBuildingNumber = -1;
			}
		}
	}

	void OnMouseUpAsButton()
	{
		if(gameObject.transform.position.y > -1.5f)
		{
			if(gameObject.transform.position.x < -4f)
			{
				onBuildingNumber = 0;
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, gm.IconValues[index]);

				CheckAllTheCases();
			}
			else if(gameObject.transform.position.x > -2.5f && gameObject.transform.position.x < 2.5f)
			{
				onBuildingNumber = 1;
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, gm.IconValues[index]);

				CheckAllTheCases();
			}
			else if(gameObject.transform.position.x > 4f)
			{
				onBuildingNumber = 2;
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, gm.IconValues[index]);

				CheckAllTheCases();
			}
			else
			{
				ReturnToOriginalPosition();
			}
		}
		else
		{
			ReturnToOriginalPosition();
		}
	}

	void ReturnToOriginalPosition()
	{
		gameObject.transform.position = gm.iconPrefabs[index].transform.position;
		decrementedText = false;
		gm.IncrementOrDecrementText(index, 1);
	}

	void CheckAllTheCases()
	{
		if(name.Contains("Shower"))
		{
			int showerCount = 0;
			int bikeRackCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Shower") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					showerCount++;
				}
				else if(g.name.Contains("Bike") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					bikeRackCount++;
				}
			}
			
			if(bikeRackCount >= showerCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, 2);
			}
		}
		else if(name.Contains("Bike"))
		{
			int showerCount = 0;
			int bikeRackCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Shower") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					showerCount++;
				}
				else if(g.name.Contains("Bike") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					bikeRackCount++;
				}
			}
			
			if(bikeRackCount <= showerCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, 2);
			}
		}
	}

	void CheckAllTheCasesBackwards()
	{
		if(name.Contains("Shower"))
		{
			int showerCount = 0;
			int bikeRackCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Shower") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					showerCount++;
				}
				else if(g.name.Contains("Bike") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					bikeRackCount++;
				}
			}
			
			if(bikeRackCount >= showerCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -2);
			}
		}
		else if(name.Contains("Bike"))
		{
			int showerCount = 0;
			int bikeRackCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Shower") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					showerCount++;
				}
				else if(g.name.Contains("Bike") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					bikeRackCount++;
				}
			}
			
			if(bikeRackCount <= showerCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -2);
			}
		}
	}
}
