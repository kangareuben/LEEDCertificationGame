using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public List<GameObject> iconPrefabs = new List<GameObject>();

	List<GameObject> icons = new List<GameObject>();
	int[] numberOfIconsOfEachType = new int[14];

	public List<GameObject> iconTexts = new List<GameObject>();

	public List<GameObject> buildings = new List<GameObject>();

	public List<GameObject> buildingTexts = new List<GameObject>();

	int[] buildingPointsGoals = new int[3];
	int totalGoalPoints = 0;

	public GameObject instructionText;
	public bool instructionQueued = true;

	public GameObject combinationHelpText;

	public GameObject feedbackText;
	public GameObject winText;

	List<int> iconValues = new List<int>();
	int bikeRackValue = 1;
	int ventilationValue = 8;
	int wasteManagementValue = 3;
	int appliancesValue = 3;
	int insulationValue = 3;
	int lightingValue = 3;
	int windowsValue = 3;
	int geothermalValue = 5;
	int showerValue = 0;
	int solarPanelValue = 3;
	int drainageValue = 6;
	int irrigationValue = 4;
	int landscapingValue = 7;
	int windTurbineValue = 3;

	bool iconsDraggable = true;

	public List<GameObject> Icons
	{
		get{return icons;}
	}

	public List<int> IconValues
	{
		get{return iconValues;}
	}

	public bool IconsDraggable
	{
		get{return iconsDraggable;}
	}

	// Use this for initialization
	void Start()
	{
		RandomizeIconValues();

		CreatePuzzle();

		StartCoroutine("ShowInstructionText");
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if(iconsDraggable){
			CheckForWin();
		}
	}

	void CheckForWin()
	{
		int building0Points = GetCurrentBuildingPoints(0);
		int building1Points = GetCurrentBuildingPoints(1);
		int building2Points = GetCurrentBuildingPoints(2);

		if(building0Points >= buildingPointsGoals[0] && building1Points >= buildingPointsGoals[1] && building2Points >= buildingPointsGoals[2])
		{
			StartCoroutine("EndLevel");
		}

		GreenBuildingTextIfAppropriate(0, building0Points);
		GreenBuildingTextIfAppropriate(1, building1Points);
		GreenBuildingTextIfAppropriate(2, building2Points);
	}

	int GetCurrentBuildingPoints(int buildingNumber)
	{
		int currentPoints;

		if(buildingTexts[buildingNumber].GetComponent<Text>().text.Substring(buildingTexts[buildingNumber].GetComponent<Text>().text.Length - 2, 1) == " ")
		{
			currentPoints = int.Parse(buildingTexts[buildingNumber].GetComponent<Text>().text.Substring(buildingTexts[buildingNumber].GetComponent<Text>().text.Length - 1));
		}
		else
		{
			currentPoints = int.Parse(buildingTexts[buildingNumber].GetComponent<Text>().text.Substring(buildingTexts[buildingNumber].GetComponent<Text>().text.Length - 2, 2));
		}

		return currentPoints;
	}

	void GreenBuildingTextIfAppropriate(int index, int buildingPoints)
	{
		if(buildingPoints >= buildingPointsGoals[index])
		{
			buildingTexts[index].GetComponent<Text>().color = Color.green;
		}
		else
		{
			buildingTexts[index].GetComponent<Text>().color = Color.white;
		}
	}

	void CreatePuzzle()
	{
		RandomizeBuildingPointsGoals();
		SetTotalGoalPoints();
		CreateIcons();
	}

	void RandomizeBuildingPointsGoals()
	{
		buildingPointsGoals[0] = 36;
		buildingPointsGoals[1] = 36;
		buildingPointsGoals[2] = 20;
	}

	void SetTotalGoalPoints()
	{
		foreach(int i in buildingPointsGoals)
		{
			totalGoalPoints += i;
		}
	}

	void CreateIcons()
	{
		int totalPoints = 0;

		int bikeRacksCreated = 0;
		bool bikeRacksAllowed = true;
		int cleanVentilationCreated = 0;
		bool cleanVentilationAllowed = true;
		int cleanWasteManagementCreated = 0;
		bool cleanWasteManagementAllowed = true;
		int sustainableDrainageCreated = 0;
		bool sustainableDrainageAllowed = true;
		int sustainableIrrigationCreated = 0;
		bool sustainableIrrigationAllowed = true;
		int sustainableLandscapingCreated = 0;
		bool sustainableLandscapingAllowed = true;

		while(totalPoints < totalGoalPoints)
		{
			//Create shower if all else fails
			int index = 8;
			bool createdAcceptableIcon = false;
			
			while(!createdAcceptableIcon)
			{
				index = Random.Range(0, iconPrefabs.Count);
				
				switch(index)
				{
				case 0:
					bikeRacksCreated++;
					if(bikeRacksCreated >= 4)
					{
						bikeRacksAllowed = false;
					}
					break;
				case 1:
					cleanVentilationCreated++;
					if(cleanVentilationCreated >= 4)
					{
						cleanVentilationAllowed = false;
					}
					break;
				case 2:
					cleanWasteManagementCreated++;
					if(cleanWasteManagementCreated >= 4)
					{
						cleanWasteManagementAllowed = false;
					}
					break;
				case 10:
					sustainableDrainageCreated++;
					if(sustainableDrainageCreated >= 4)
					{
						sustainableDrainageAllowed = false;
					}
					break;
				case 11:
					sustainableIrrigationCreated++;
					if(sustainableIrrigationCreated >= 4)
					{
						sustainableIrrigationAllowed = false;
					}
					break;
				case 12:
					sustainableLandscapingCreated++;
					if(sustainableLandscapingCreated >= 4)
					{
						sustainableLandscapingAllowed = false;
					}
					break;
				default:
					break;
				}
				
				if(!((index == 0 && !bikeRacksAllowed) ||
				     (index == 1 && !cleanVentilationAllowed) ||
				     (index == 2 && !cleanWasteManagementAllowed) ||
				     (index == 10 && !sustainableDrainageAllowed) ||
				     (index == 11 && !sustainableIrrigationAllowed) ||
				     (index == 12 && !sustainableLandscapingAllowed)))
				{
					createdAcceptableIcon = true;
				}
				else
				{
					//Debug.Log("Creation of " + index + " not allowed");
				}
			}
			
			CreateIcon(index);

			totalPoints += AddForSpecialCases();
			
			IncrementOrDecrementText(index, 1);
			
			totalPoints += iconValues[index];
		}
		
		Debug.Log(totalPoints);
	}

	void CreateIcon(int index)
	{
		GameObject g = Object.Instantiate(iconPrefabs[index], iconPrefabs[index].transform.position, iconPrefabs[index].transform.rotation) as GameObject;
		g.AddComponent<Draggable>();
		g.GetComponent<Draggable>().Index = index;
		g.AddComponent<BoxCollider2D>();
		g.tag = "Draggable";
		
		icons.Add(g);
		
		numberOfIconsOfEachType[index]++;
	}

	int AddForSpecialCases()
	{
		int toAdd = 0;

		if(numberOfIconsOfEachType[0] > 0 && numberOfIconsOfEachType[8] > 0)
		{
			numberOfIconsOfEachType[0]--;
			numberOfIconsOfEachType[8]--;
			toAdd += 2;
		}
		if(numberOfIconsOfEachType[10] > 0 && numberOfIconsOfEachType[11] > 0)
		{
			numberOfIconsOfEachType[10]--;
			numberOfIconsOfEachType[11]--;
			toAdd += 3;
		}

		return toAdd;
	}

	IEnumerator EndLevel()
	{
		winText.GetComponent<Text>().enabled = true;
		iconsDraggable = false;
		yield return new WaitForSeconds(3);
		RestartPuzzle();
	}

	public void IncrementOrDecrementText(int index, int value)
	{
		string s = iconTexts[index].GetComponent<Text>().text.Substring(0, iconTexts[index].GetComponent<Text>().text.Length - 1);
		int num = int.Parse(iconTexts[index].GetComponent<Text>().text.Substring(iconTexts[index].GetComponent<Text>().text.Length - 1));
		num += value;
		iconTexts[index].GetComponent<Text>().text = s + num.ToString();
	}

	public void IncrementOrDecrementBuildingText(int index, int value)
	{
		string s;
		int num;

		if(buildingTexts[index].GetComponent<Text>().text.Substring(buildingTexts[index].GetComponent<Text>().text.Length - 2, 1) == " ")
		{
			s = buildingTexts[index].GetComponent<Text>().text.Substring(0, buildingTexts[index].GetComponent<Text>().text.Length - 1);
			num = int.Parse(buildingTexts[index].GetComponent<Text>().text.Substring(buildingTexts[index].GetComponent<Text>().text.Length - 1));
		}
		else
		{
			s = buildingTexts[index].GetComponent<Text>().text.Substring(0, buildingTexts[index].GetComponent<Text>().text.Length - 2);
			num = int.Parse(buildingTexts[index].GetComponent<Text>().text.Substring(buildingTexts[index].GetComponent<Text>().text.Length - 2, 2));
		}

		num += value;
		buildingTexts[index].GetComponent<Text>().text = s + num.ToString();
	}

	void RandomizeIconValues()
	{
		ventilationValue = Random.Range(1, ventilationValue + 1);
		wasteManagementValue = Random.Range(1, wasteManagementValue + 1);
		appliancesValue = Random.Range(1, appliancesValue + 1);
		insulationValue = Random.Range(1, insulationValue + 1);
		lightingValue = Random.Range(1, lightingValue + 1);
		windowsValue = Random.Range(1, windowsValue + 1);
		geothermalValue = Random.Range(3, geothermalValue + 1);
		drainageValue = Random.Range(1, drainageValue + 1);
		irrigationValue = Random.Range(1, irrigationValue + 1);
		landscapingValue = Random.Range(1, landscapingValue + 1);
		
		iconValues.Add(bikeRackValue);
		iconValues.Add(ventilationValue);
		iconValues.Add(wasteManagementValue);
		iconValues.Add(appliancesValue);
		iconValues.Add(insulationValue);
		iconValues.Add(lightingValue);
		iconValues.Add(windowsValue);
		iconValues.Add(geothermalValue);
		iconValues.Add(showerValue);
		iconValues.Add(solarPanelValue);
		iconValues.Add(drainageValue);
		iconValues.Add(irrigationValue);
		iconValues.Add(landscapingValue);
		iconValues.Add(windTurbineValue);
	}

	void RestartPuzzle()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator ShowInstructionText()
	{
		yield return new WaitForSeconds(10f);
		instructionText.GetComponent<Text>().enabled = true;
	}

	IEnumerator ShowCombinationHelpText()
	{
		yield return new WaitForSeconds(20f);
		combinationHelpText.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(10f);
		combinationHelpText.GetComponent<Text>().enabled = false;
	}
}
