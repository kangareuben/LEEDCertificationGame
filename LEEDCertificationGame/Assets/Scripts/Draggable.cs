using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
	bool audioPlayed = false;
	bool decrementedText = false;
	TextMesh descriptionText;
	GameManager gm;
	int index;
	int onBuildingNumber = -1;

	Color negativeHighlightColor;
	Color positiveHighlightColor;

	public TextMesh DescriptionText
	{
		get{return descriptionText;}
		set{descriptionText = value;}
	}

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

	void OnMouseEnter()
	{
		if(onBuildingNumber != -1)
		{
			descriptionText.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	void OnMouseExit()
	{
		if(onBuildingNumber != -1)
		{
			descriptionText.GetComponent<MeshRenderer>().enabled = false;
		}
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

			if(!audioPlayed)
			{
				audioPlayed = true;
				gm.GetComponents<AudioSource>()[1].Play();
			}

			descriptionText.GetComponent<MeshRenderer>().enabled = false;

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

		gm.GetComponents<AudioSource>()[2].Play();
		audioPlayed = false;

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
		CheckSpecificCase("Shower", "Bike", 2);
		CheckSpecificCase("Drainage", "Irrigation", 3);
		CheckSpecificCase("Insulation", "Windows", 1);
		CheckSpecificCase("Geothermal", "Solar", "Wind", 5);
		CheckSpecificCase("Appliance", "Light", 1);
	}

	void CheckSpecificCase(string item1, string item2, int pointsToAdd)
	{
		int item1Count = GetIconCount(item1, onBuildingNumber);
		int item2Count = GetIconCount(item2, onBuildingNumber);

		if(name.Contains(item1))
		{
			if(item1Count <= item2Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, pointsToAdd);

				StartCoroutine("FlashGreen");

				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item2) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						return;
					}
				}

			}
		}
		else if(name.Contains(item2))
		{
			if(item1Count >= item2Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, pointsToAdd);

				StartCoroutine("FlashGreen");

				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item1) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						return;
					}
				}
			}
		}
	}

	void CheckSpecificCase(string item1, string item2, string item3, int pointsToAdd)
	{
		int item1Count = GetIconCount(item1, onBuildingNumber);
		int item2Count = GetIconCount(item2, onBuildingNumber);
		int item3Count = GetIconCount(item3, onBuildingNumber);

		if(name.Contains(item1))
		{
			if(item1Count <= item2Count && item1Count <= item3Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, pointsToAdd);

				StartCoroutine("FlashGreen");

				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item2) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						break;
					}
				}
				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item3) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						return;
					}
				}
				
			}
		}
		else if(name.Contains(item2))
		{
			if(item1Count >= item2Count && item3Count >= item2Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, pointsToAdd);

				StartCoroutine("FlashGreen");

				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item1) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						break;
					}
				}

				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item3) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						return;
					}
				}
			}
		}
		else if(name.Contains(item3))
		{
			if(item1Count >= item3Count && item3Count <= item2Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, pointsToAdd);
				
				StartCoroutine("FlashGreen");
				
				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item1) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						break;
					}
				}
				
				foreach(GameObject g in gm.Icons)
				{
					if(g.name.Contains(item2) && g.GetComponent<Draggable>().onBuildingNumber == onBuildingNumber)
					{
						g.GetComponent<Draggable>().StartCoroutine("FlashGreen");
						return;
					}
				}
			}
		}
	}


	int GetIconCount(string iconName, int buildingNumber)
	{
		int iconCount = 0;

		foreach(GameObject g in gm.Icons)
		{
			if(g.name.Contains(iconName) && g.GetComponent<Draggable>().onBuildingNumber == buildingNumber)
			{
				iconCount++;
			}
		}

		return iconCount;
	}

	void CheckAllTheCasesBackwards()
	{
		CheckSpecificCaseBackwards("Shower", "Bike", 2);
		CheckSpecificCaseBackwards("Drainage", "Irrigation", 3);
		CheckSpecificCaseBackwards("Insulation", "Windows", 1);
		CheckSpecificCaseBackwards("Geothermal", "Solar", "Wind", 5);
		CheckSpecificCaseBackwards("Appliance", "Light", 1);
	}

	void CheckSpecificCaseBackwards(string item1, string item2, int pointsToSubtract)
	{
		int item1Count = GetIconCount(item1, onBuildingNumber);
		int item2Count = GetIconCount(item2, onBuildingNumber);

		if(name.Contains(item1))
		{
			if(item1Count <= item2Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -pointsToSubtract);
			}
		}
		else if(name.Contains(item2))
		{
			if(item1Count >= item2Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -pointsToSubtract);
			}
		}
	}

	void CheckSpecificCaseBackwards(string item1, string item2, string item3, int pointsToSubtract)
	{
		int item1Count = GetIconCount(item1, onBuildingNumber);
		int item2Count = GetIconCount(item2, onBuildingNumber);
		int item3Count = GetIconCount(item3, onBuildingNumber);
		
		if(name.Contains(item1))
		{
			if(item1Count <= item2Count && item1Count <= item3Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -pointsToSubtract);
			}
		}
		else if(name.Contains(item2))
		{
			if(item1Count >= item2Count && item2Count <= item3Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -pointsToSubtract);
			}
		}
		else if(name.Contains(item3))
		{
			if(item1Count >= item3Count && item2Count >= item3Count)
			{
				gm.IncrementOrDecrementBuildingText(onBuildingNumber, -pointsToSubtract);
			}
		}
	}

	public IEnumerator FlashGreen()
	{
		for(int i = 0; i < 3; i++)
		{
			GetComponent<SpriteRenderer>().color = positiveHighlightColor;
			yield return new WaitForSeconds(.25f);
			GetComponent<SpriteRenderer>().color = Color.white;
			yield return new WaitForSeconds(.25f);
		}
	}

	IEnumerator DisplayFeedbackText()
	{
		gm.feedbackText.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(3f);
		gm.feedbackText.GetComponent<Text>().enabled = false;
	}
}
