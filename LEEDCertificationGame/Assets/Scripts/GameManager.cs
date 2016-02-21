using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List<GameObject> iconPrefabs = new List<GameObject>();

	public List<GameObject> iconTexts = new List<GameObject>();

	public List<GameObject> buildingTexts = new List<GameObject>();

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

	// Use this for initialization
	void Start()
	{
		ventilationValue = Random.Range(1, ventilationValue + 1);
		wasteManagementValue = Random.Range(1, wasteManagementValue + 1);
		appliancesValue = Random.Range(1, appliancesValue + 1);
		insulationValue = Random.Range(1, insulationValue + 1);
		lightingValue = Random.Range(1, lightingValue + 1);
		windowsValue = Random.Range(1, windowsValue + 1);
		geothermalValue = Random.Range(1, geothermalValue + 1);
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
	
	}

	void CreatePuzzle()
	{
		int totalPoints = 0;

		for(int i = 0; i < 30; i++)
		{
			int index = Random.Range(0, iconPrefabs.Count);

			GameObject g = Object.Instantiate(iconPrefabs[index], iconPrefabs[index].transform.position, iconPrefabs[index].transform.rotation) as GameObject;
			g.AddComponent<Draggable>();
			g.GetComponent<Draggable>().Index = index;
			g.AddComponent<BoxCollider2D>();

			IncrementOrDecrementText(index, 1);

			totalPoints += iconValues[index];
		}

		Debug.Log(totalPoints);
	}

	public void IncrementOrDecrementText(int index, int value)
	{
		string s = iconTexts[index].GetComponent<Text>().text.Substring(0, iconTexts[index].GetComponent<Text>().text.Length - 1);
		int num = int.Parse(iconTexts[index].GetComponent<Text>().text.Substring(iconTexts[index].GetComponent<Text>().text.Length - 1));
		num += value;
		iconTexts[index].GetComponent<Text>().text = s + num.ToString();
	}
}
