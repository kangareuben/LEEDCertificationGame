using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public List<GameObject> iconPrefabs = new List<GameObject>();

	public List<GameObject> iconTexts = new List<GameObject>();

	public List<GameObject> buildingTexts = new List<GameObject>();

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

		CreatePuzzle();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(iconsDraggable){
			CheckForWin();
		}
	}

	void CheckForWin()
	{
		int num0;
		int num1;
		int num2;
		
		if(buildingTexts[0].GetComponent<Text>().text.Substring(buildingTexts[0].GetComponent<Text>().text.Length - 2, 1) == " ")
		{
			num0 = int.Parse(buildingTexts[0].GetComponent<Text>().text.Substring(buildingTexts[0].GetComponent<Text>().text.Length - 1));
		}
		else
		{
			num0 = int.Parse(buildingTexts[0].GetComponent<Text>().text.Substring(buildingTexts[0].GetComponent<Text>().text.Length - 2, 2));
		}

		if(buildingTexts[1].GetComponent<Text>().text.Substring(buildingTexts[1].GetComponent<Text>().text.Length - 2, 1) == " ")
		{
			num1 = int.Parse(buildingTexts[1].GetComponent<Text>().text.Substring(buildingTexts[1].GetComponent<Text>().text.Length - 1));
		}
		else
		{
			num1 = int.Parse(buildingTexts[1].GetComponent<Text>().text.Substring(buildingTexts[1].GetComponent<Text>().text.Length - 2, 2));
		}

		if(buildingTexts[2].GetComponent<Text>().text.Substring(buildingTexts[2].GetComponent<Text>().text.Length - 2, 1) == " ")
		{
			num2 = int.Parse(buildingTexts[2].GetComponent<Text>().text.Substring(buildingTexts[2].GetComponent<Text>().text.Length - 1));
		}
		else
		{
			num2 = int.Parse(buildingTexts[2].GetComponent<Text>().text.Substring(buildingTexts[2].GetComponent<Text>().text.Length - 2, 2));
		}

		if(num0 >= 36 && num1 >= 36 && num2 >= 20)
		{
			StartCoroutine("EndLevel");
		}

		if(num0 >= 36)
		{
			buildingTexts[0].GetComponent<Text>().color = Color.green;
		}
		else
		{
			buildingTexts[0].GetComponent<Text>().color = Color.white;
		}

		if(num1 >= 36)
		{
			buildingTexts[1].GetComponent<Text>().color = Color.green;
		}
		else
		{
			buildingTexts[1].GetComponent<Text>().color = Color.white;
		}

		if(num2 >= 20)
		{
			buildingTexts[2].GetComponent<Text>().color = Color.green;
		}
		else
		{
			buildingTexts[2].GetComponent<Text>().color = Color.white;
		}
	}

	void CreatePuzzle()
	{
		int totalPoints = 0;

		while(totalPoints < 93)
		{
			int index = Random.Range(0, iconPrefabs.Count);

			GameObject g = Object.Instantiate(iconPrefabs[index], iconPrefabs[index].transform.position, iconPrefabs[index].transform.rotation) as GameObject;
			g.AddComponent<Draggable>();
			g.GetComponent<Draggable>().Index = index;
			g.AddComponent<BoxCollider2D>();
			g.tag = "Draggable";

			IncrementOrDecrementText(index, 1);

			totalPoints += iconValues[index];
		}

		Debug.Log(totalPoints);
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

	void RestartPuzzle()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
