using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _enemy;
	[SerializeField]
	private GameObject Spawn_Coroutine;

	[HideInInspector]
	public static SpawnManager instance = null;
	private bool _needSpawn = true;

	void Awake()
	{
		//singleton starts
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		//singleton ends
	}

	void Start()
    {
		StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator SpawnCoroutine()
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
