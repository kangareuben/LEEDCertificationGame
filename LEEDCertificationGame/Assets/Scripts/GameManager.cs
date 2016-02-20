using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject bikeRackPrefab;
	public GameObject ventilationPrefab;
	public GameObject wasteManagementPrefab;
	public GameObject appliancesPrefab;
	public GameObject insulationPrefab;
	public GameObject lightingPrefab;
	public GameObject windowsPrefab;
	public GameObject geothermalPrefab;
	public GameObject showerPrefab;
	public GameObject solarPanelPrefab;
	public GameObject drainagePrefab;
	public GameObject irrigationPrefab;
	public GameObject landscapingPrefab;
	public GameObject windTurbinePrefab;

	public GameObject bikeRackText;
	public GameObject ventilationText;
	public GameObject wasteManagementText;
	public GameObject appliancesText;
	public GameObject insulationText;
	public GameObject lightingText;
	public GameObject windowsText;
	public GameObject geothermalText;
	public GameObject showerText;
	public GameObject solarPanelText;
	public GameObject drainageText;
	public GameObject irrigationText;
	public GameObject landscapingText;
	public GameObject windTurbineText;

	public GameObject building1Text;
	public GameObject building2Text;
	public GameObject building3Text;

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
	void Start () {
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
