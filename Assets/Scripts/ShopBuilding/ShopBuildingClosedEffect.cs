using UnityEngine;
using System.Collections;
using Nightmare;

public class ShopBuildingClosedEffect : MonoBehaviour
{
    public GameObject player;
    private Rigidbody shopBuildingRigidbody;
    private Vector3 currPosition;
    public bool isPlayerInShopRange;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopBuildingRigidbody = GetComponent<Rigidbody>();
        var position = transform.position;
        currPosition.Set(position.x, position.y, position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInShopRange = true;
            Debug.Log("Player entered shop range");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInShopRange = false;
            Debug.Log("Player exited shop range");
        }
    }
}
