using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[HideInInspector]
	public static GameManager instance = null;

	private bool _isGameOver = false;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
        
    }

    void Update()
    {
		if (_isGameOver && Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}
	}

	private void RestartLevel()
	{
		_isGameOver = false;
		SceneManager.LoadScene(0);
	}

	public void SetGameOver()
	{
		_isGameOver = true;
	}
}
