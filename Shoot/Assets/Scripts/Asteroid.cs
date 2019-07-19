using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{	
	[SerializeField]
	private int _speed = 19;
	[SerializeField]
	private GameObject _explotion;

	private void Start()
	{
	}

	void Update()
    {
		//transform.Translate(Vector3.down * _speed * Time.deltaTime);
		transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
		//transform.rotation = 5f;
		//transform.rotation.z  = transform.rotation.z + 0.5f;
		//GetComponent<Transform>().rotation.z += 0.5f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser")
		{
			
			Destroy(other.gameObject);
			Destroy(this.gameObject);
			GameObject explotion =  Instantiate(_explotion, transform.position, Quaternion.identity);
		}
	}
}
