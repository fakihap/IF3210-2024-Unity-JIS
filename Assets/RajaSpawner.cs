using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rajaPrefab;
    void OnTriggerEnter(Collider col) {
        if (!GameDirector.Instance.isBossReady) {
            return;
        }

		Instantiate (rajaPrefab, transform.position, transform.rotation);

        // WARNING : this also may causes problem
        GameDirector.Instance.isBossReady = false;
	}
}
