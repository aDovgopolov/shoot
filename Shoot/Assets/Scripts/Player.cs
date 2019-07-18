using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour
{
	[SerializeField]
	private float _speed = 5f;
	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1f;
	[SerializeField]
	private int _lives = 3;
	private bool isDead = false;

	[HideInInspector]
	public static Player instance = null;

	public bool IsDead{
		get {return isDead;}
		private set{}
	}

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
		transform.position = new Vector3(0, 0, 0);
    }
	
    void Update()
    {
		CalculateMovement();

		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
		{
			FireLaser();
		}
	}

	private void FireLaser()
	{
		_canFire = Time.time + _fireRate;
		Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
	}

	private void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
		
		transform.Translate(direction * _speed * Time.deltaTime);

		//if (transform.position.y >= 0)
		//{
		//	transform.position = new Vector3(transform.position.x, 0, 0);
		//}
		//else if (transform.position.y <= -3.5f)
		//{
		//	transform.position = new Vector3(transform.position.x, -3.5f, 0);
		//}

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0), 0);

		if (transform.position.x > 12f)
		{
			transform.position = new Vector3(-12f, transform.position.y, 0);
		}
		else if (transform.position.x < -12f)
		{
			transform.position = new Vector3(12f, transform.position.y, 0);
		}
	}

	public void DamagePlayer()
	{
		_lives--;
		if ( _lives <= 0)
		{
			Debug.Log("GameOver");
			Destroy(this.gameObject);
			SpawnManager.instance.StopSpawn();
			//isDead = true;
		}
	}
}
