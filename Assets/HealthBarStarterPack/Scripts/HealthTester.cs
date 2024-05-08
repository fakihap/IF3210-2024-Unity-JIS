using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTester : MonoBehaviour {
	private Health health;
	[SerializeField]
	private GameObject healthBarPrefab;
	void Start () {
		health = transform.GetComponent<Health> ();
		health.Initialize (100f);
		BarUI barUi = GameObject.Instantiate(healthBarPrefab, transform).GetComponent<BarUI> ();
		barUi.transform.localPosition = new Vector3 (0f, .4f, 0f);
		barUi.Initialize (health);
	}
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			float mult = 10f;
			float dmg = (Time.deltaTime * mult);
			health.Damage (dmg);
		}
	}
}
