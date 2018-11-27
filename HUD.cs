using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class HUD : MonoBehaviour {

	public UnityEngine.UI.Image[] hearts;
	public UnityEngine.UI.Text keyCount;

	public GameObject winLooseCanv;
	public GameObject youWinUI;
	public GameObject youLooseUI;

	private int heartsIdx = 0;
	private int numKeys = 15;

	private bool GameIsOver = false;

	// Use this for initialization
	void Start () {
		keyCount.text = numKeys.ToString();
		numKeys = 0;
		// Debug.Log(numKeys);


		// TODO set to engabled
		for (int i = 0; i < hearts.Length; i++) {
			hearts[i].enabled = true;
		}
		heartsIdx = hearts.Length + 1;
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void hit()
	{
		Debug.Log(heartsIdx  + " life left");

		if (--heartsIdx < 0)
		{
			GameIsOver = true;
			OnGameLoose();
			return;
		}

		hearts[heartsIdx].enabled = false;
	}
	
	public void addGeneratorToHUD()
	{
		// Debug.Log(numKeys);
		// numKeys++;
		// setGenerators(numKeys);
	}

	public void subGeneratorToHUD()
	{
		Debug.Log("Spawn Point Destroyed");
		int temp = Int32.Parse(keyCount.text);
		temp -= 1;
		// Debug.Log(temp);
		keyCount.text = temp.ToString();
	}

	public void setGenerators(int gens)
	{
		if (gens <= 0)
		{
			onGameWin();
		} else
		{
			keyCount.text = gens.ToString();
		}
	}

	public void onGameWin()
	{
		winLooseCanv.SetActive(true);
		// youWinUI.SetActive(true);
	}

	public void OnGameLoose()
	{
		// Debug.Log("Game is over");
		winLooseCanv.SetActive(true);
		// youLooseUI.SetActive(true);
		// Debug.Log("Game is over");
	}
}
