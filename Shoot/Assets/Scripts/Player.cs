using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour
{
	[SerializeField]
	private float _speed = 7f;
	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	private GameObject tripleShotPrefab;
	[SerializeField]
	private GameObject shield;
	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1f;
	private float _canFireTripleShot = -1f;
	[SerializeField]
	private float _tripleShotFireRate = 5f;

	[SerializeField]
	private int _lives = 3;

	[HideInInspector]
	public static Player instance = null;
	[SerializeField]
	private bool _isTripleShotAvailable = false;
	[SerializeField]
	private bool _isShieldActive = false;


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

		//if (Time.time > _canFireTripleShot)
		//{
		//	_isTripleShotAvailable = true;
		//	_canFireTripleShot = Time.time + _tripleShotFireRate;
		//}

		if (_isTripleShotAvailable)
		{
			//_isTripleShotAvailable = false;
			Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
		}
		else
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
		if (_isShieldActive)
		{
			_isShieldActive = false;
			shield.gameObject.SetActive(false);
			// stop anim;
			return;
		}

		_lives--;
		if ( _lives <= 0)
		{
			Debug.Log("GameOver");
			Destroy(this.gameObject);
			SpawnManager.instance.StopSpawn();
			//isDead = true;
		}
	}

	public void ActivateTripleShop()
	{
		_isTripleShotAvailable = true;
		StartCoroutine(DisableTripleShot());
	}

	public void ActivateSpeed()
	{

		Debug.Log("ActivateSpeed method");
		_speed = _speed * 2;
		StartCoroutine(DisableSpeed());
	}

	public void ActivateShield()
	{
		Debug.Log("ActivateShield");
		_isShieldActive = true;
		shield.gameObject.SetActive(true);
		//StartCoroutine(DisableSheild());
	}

	IEnumerator DisableTripleShot()
	{
		yield return new WaitForSeconds(5f);
		_isTripleShotAvailable = false;
		yield return null;
	}

	IEnumerator DisableSpeed()
	{
		yield return new WaitForSeconds(5f);
		_speed = 7f;
		yield return null;
	}

	//IEnumerator DisableSheild()
	//{
	//	yield return new WaitForSeconds(5f);
	//	yield return null;
	//}
}
