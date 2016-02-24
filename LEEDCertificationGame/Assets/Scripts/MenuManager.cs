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
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void Credits()
	{
		GetComponents<AudioSource>()[1].Play();
		Application.LoadLevel("Credits");
	}

	public void MainMenu()
	{
		GetComponents<AudioSource>()[1].Play();
		Application.LoadLevel("MainMenu");
	}

	public void StartGame()
	{
		GetComponents<AudioSource>()[1].Play();
		Application.LoadLevel("GameScene");
	}
}
