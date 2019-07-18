using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _enemy;
	[SerializeField]
	private GameObject _powerUp;
	[SerializeField]
	private GameObject _speedUp;
	[SerializeField]
	private GameObject Spawn_Coroutine;

	[SerializeField]
	private GameObject[] _powerUps;

	[HideInInspector]
	public static SpawnManager instance = null;
	private bool _needSpawn = true;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		StartCoroutine(SpawnEnemyCoroutine());
		StartCoroutine(SpawnPowerUpCoroutine());
		//StartCoroutine(SpawnSpeedUpCoroutine());
	}

	IEnumerator SpawnSpeedUpCoroutine()
	{
		while (_needSpawn)
		{
			GameObject powerUp = Instantiate(_speedUp, new Vector3(Random.Range(-10f, 10f), 7, 0), Quaternion.identity);
			if (_speedUp != null)
			{
				powerUp.transform.parent = Spawn_Coroutine.transform;
			}

			yield return new WaitForSeconds(Random.Range(10f, 15f));
		}
	}

	IEnumerator SpawnPowerUpCoroutine()
	{
		while (_needSpawn)
		{
			int randomBoostUp = Random.Range(0,2);
			GameObject powerUp = Instantiate(_powerUps[randomBoostUp], new Vector3(Random.Range(-10f, 10f), 7, 0) , Quaternion.identity);
			if (powerUp != null)
			{
				powerUp.transform.parent = Spawn_Coroutine.transform;
			}

			yield return new WaitForSeconds(Random.Range(7f, 12f));
		}
	}

	IEnumerator SpawnEnemyCoroutine()
	{
		Debug.Log(_needSpawn);
		while (_needSpawn)
		{
			 GameObject newEnemy =  Instantiate(_enemy);
			if(newEnemy != null)
			{
				newEnemy.transform.parent = Spawn_Coroutine.transform;
			}
			//Debug.Log(Player.instance.IsDead);
			//if (Player.instance.IsDead == true)
			//	StopCoroutine(SpawnCoroutine());

			yield return new WaitForSeconds(5f);
		}
	}

	public void StopSpawn()
	{
		Debug.Log("StopSpawn");
		_needSpawn = false;
	}
}
