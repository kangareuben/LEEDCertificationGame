using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void Credits()
	{
		Application.LoadLevel("Credits");
	}

	public void MainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void StartGame()
	{
		Application.LoadLevel("GameScene");
	}
}
