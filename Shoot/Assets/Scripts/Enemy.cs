using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{	
	[SerializeField]
	private float _speed = 5f;
	[SerializeField]
	private int enemyScore;
	private Animator _anim;
	[SerializeField]
	private AudioClip _enemyLaserClip;
	private AudioSource _enemyAudioSource;
	[SerializeField]
	private GameObject laserPrefab;
	private float _fireRate = 3.0f;
	private float _canFire = -1f;

	void Start()
	{
		//StartCoroutine(EnemyShot());

		transform.position = new Vector3(0, 6, 0);
		_anim = GetComponent<Animator>();

		_enemyAudioSource = GetComponent<AudioSource>();
		_enemyAudioSource.clip = _enemyLaserClip;

	}
	
    void Update()
	{
		if (Time.time > _canFire)
		{
			_fireRate = Random.Range(3f, 7f);

			_canFire = Time.time + _fireRate;
			Instantiate(laserPrefab, transform.position + new Vector3(0, -3, 0), Quaternion.identity); //+ new Vector3(0, -9, 0)
			//Debug.Break();
		}


		Movement();
	}

	private void Movement()
	{
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -4f)
		{
			transform.position = new Vector3(Random.Range(-10f, 10f), 6, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.tag == "Laser")
		{
			Destroy(other.gameObject);
			_speed = 0;
			_anim.SetTrigger("OnEnemyDeath");
			Destroy(GetComponent<BoxCollider2D>());
			//StopCoroutine(EnemyShot());
			Destroy(this.gameObject, 2.5f);
			//GetComponent<BoxCollider2D>().gameObject.SetActive(false);
			Player.instance.AddScore(10); // enemyScore
			_enemyAudioSource.Play();
		}

		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			if (player != null) {
				player.DamagePlayer();
			}
			_speed = 0;
			_anim.SetTrigger("OnEnemyDeath");
			//StopCoroutine(EnemyShot());
			Destroy(this.gameObject, 2.5f);
			Destroy(GetComponent<BoxCollider2D>());
			//GetComponent<BoxCollider2D>().gameObject.SetActive(false);
			_enemyAudioSource.Play();
			StopAllCoroutines();
		}
	}

	IEnumerator EnemyShot()
	{
		yield return new WaitForSeconds(Random.Range(1f, 2f));

		Debug.Log("EnemyShot()");
		//Instantiate(laserPrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
		GameObject bullet = Instantiate(laserPrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);

		Debug.Log($"{bullet.GetComponent<BoxCollider2D>()} - {GetComponent<BoxCollider2D>()}");
		Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
	}
}
