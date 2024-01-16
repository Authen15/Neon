using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;

	public float fireRate = 1f;
    private float fireCountdown = 0f;

	// Update is called once per frame
	private void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			if(fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}
			
		}
		fireCountdown -= Time.deltaTime;
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}