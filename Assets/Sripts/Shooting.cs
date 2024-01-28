using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Shooting : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;

    private float fireCountdown = 0f;

	private PlayerCreature _playerCreature;

	void Awake()
	{
		_playerCreature = gameObject.GetComponentInParent<PlayerCreature>();
	}

	// Update is called once per frame
	private void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			if(fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / _playerCreature.fireRate;
			}
			
		}
		fireCountdown -= Time.deltaTime;
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}