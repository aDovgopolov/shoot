using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text _scoreText;
	[SerializeField]
	private Text _gameOverText;
	[SerializeField]
	private Text _gameOverRestartText;
	[SerializeField]
	private Image _livesImage;
	[SerializeField]
	private Sprite[] _liveSprites;
	[HideInInspector]
	public static UIManager instance = null;

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
		_gameOverText.gameObject.SetActive(false);
		_gameOverRestartText.gameObject.SetActive(false);
		_scoreText.text = "Score : " + 0;
	}

	public void SetScoreText(int score)
	{
		_scoreText.text = "Score : " + score;
	}

	public void UpdateLives(int lives)
	{
		_livesImage.sprite = _liveSprites[lives];
	}

	public void SetGameOverText()
	{
		_gameOverText.gameObject.SetActive(true);
		_gameOverRestartText.gameObject.SetActive(true);
	}
}
