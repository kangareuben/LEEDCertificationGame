using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
	bool decrementedText = false;
	GameManager gm;
	int index;
	int onBuildingNumber = -1;

	Color negativeHighlightColor;
	Color positiveHighlightColor;

	public int Index
	{
		get{return index;}
		set{index = value;}
	}

	// Use this for initialization
	void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		negativeHighlightColor = new Color(1f, 0.75f, 0.75f);
		positiveHighlightColor = new Color(0.75f, 1f, 0.75f);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnMouseDrag()
	{
		if(gm.IconsDraggable)
		{
			if(gm.instructionQueued)
			{
				gm.instructionQueued = false;
				gm.StopCoroutine("ShowInstructionText");
				gm.instructionText.GetComponent<Text>().enabled = false;
				gm.StartCoroutine("ShowCombinationHelpText");
			}

			float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
			transform.position = new Vector3(pos_move.x, pos_move.y, transform.position.z);

			if(gameObject.transform.position.y > -1.5f &&
			  ((gameObject.transform.position.x > -9f && gameObject.transform.position.x < -4f) ||
			  (gameObject.transform.position.x > -2.5f && gameObject.transform.position.x < 2.5f) ||
			  (gameObject.transform.position.x > 4f && gameObject.transform.position.x < 9f)))
			{
				GetComponent<SpriteRenderer>().color = positiveHighlightColor;

				if(gameObject.transform.position.x < -4f)
				{
					gm.buildings[0].GetComponent<SpriteRenderer>().color = positiveHighlightColor;
				}
				else if(gameObject.transform.position.x > -2.5f && gameObject.transform.position.x < 2.5f)
				{
					gm.buildings[1].GetComponent<SpriteRenderer>().color = positiveHighlightColor;
				}
				else if(gameObject.transform.position.x > 4f)
				{
					gm.buildings[2].GetComponent<SpriteRenderer>().color = positiveHighlightColor;
				}
			}
			else
			{
				GetComponent<SpriteRenderer>().color = negativeHighlightColor;

				foreach(GameObject g in gm.buildings)
				{
					g.GetComponent<SpriteRenderer>().color = Color.white;
				}
			}

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
		GetComponent<SpriteRenderer>().color = Color.white;

		foreach(GameObject g in gm.buildings)
		{
			g.GetComponent<SpriteRenderer>().color = Color.white;
		}

		if(gameObject.transform.position.y > -1.5f)
		{
			if(gameObject.transform.position.x > -9f && gameObject.transform.position.x < -4f)
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
			else if(gameObject.transform.position.x > 4f && gameObject.transform.position.x < 9f)
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
					/*gm.IncrementOrDecrementBuildingText(onBuildingNumber, -gm.IconValues[index]);
					onBuildingNumber = -1;
					ReturnToOriginalPosition();
					StartCoroutine("DisplayFeedbackText");
					break;*/
				}
			}
			
			if(/*onBuildingNumber != -1 &&*/ bikeRackCount <= showerCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, 2);
			}
		}
		else if(name.Contains("Drainage"))
		{
			int drainageCount = 0;
			int irrigationCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Drainage") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					drainageCount++;
				}
				else if(g.name.Contains("Irrigation") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					irrigationCount++;
				}
			}
			
			if(irrigationCount >= drainageCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, 3);
			}
		}
		else if(name.Contains("Irrigation"))
		{
			int drainageCount = 0;
			int irrigationCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Drainage") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					drainageCount++;
				}
				else if(g.name.Contains("Irrigation") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					irrigationCount++;
					/*gm.IncrementOrDecrementBuildingText(onBuildingNumber, -gm.IconValues[index]);
					onBuildingNumber = -1;
					ReturnToOriginalPosition();
					StartCoroutine("DisplayFeedbackText");
					break;*/
				}
			}
			
			if(/*onBuildingNumber != -1 &&*/ drainageCount >= irrigationCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, 3);
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
		else if(name.Contains("Drainage"))
		{
			int drainageCount = 0;
			int irrigationCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Drainage") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					drainageCount++;
				}
				else if(g.name.Contains("Irrigation") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					irrigationCount++;
				}
			}
			
			if(irrigationCount >= drainageCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -3);
			}
		}
		else if(name.Contains("Irrigation"))
		{
			int drainageCount = 0;
			int irrigationCount = 0;
			
			foreach(GameObject g in gm.Icons)
			{
				if(g.name.Contains("Drainage") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					drainageCount++;
				}
				else if(g.name.Contains("Irrigation") && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
				{
					irrigationCount++;
				}
			}
			
			if(irrigationCount <= drainageCount)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -3);
			}
		}
	}

	IEnumerator DisplayFeedbackText()
	{
		gm.feedbackText.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(3f);
		gm.feedbackText.GetComponent<Text>().enabled = false;
	}
}
