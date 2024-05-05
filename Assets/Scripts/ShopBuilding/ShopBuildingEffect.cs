using UnityEngine;
using System.Collections;

public class ShopBuildingEffect : MonoBehaviour
{
    public GameObject player;
    private Light shopBuildingLight;
    private Rigidbody shopBuildingRigidbody;
    private Vector3 currPosition;
    public bool isPlayerInShopRange;
    public bool showLight;

    public float transitionDuration = 0.5f;
    public float targetIntensityWhenInRange = 4f;
    public float targetIntensityWhenNotInRange = 1f;

    private Coroutine intensityTransitionCoroutine;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopBuildingLight = GetComponent<Light>();
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
        if (other.gameObject != player) return;

        isPlayerInShopRange = true;

        if (intensityTransitionCoroutine != null)
            StopCoroutine(intensityTransitionCoroutine);

        intensityTransitionCoroutine = StartCoroutine(ChangeIntensitySmoothly(targetIntensityWhenInRange));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != player) return;

        isPlayerInShopRange = false;

        if (intensityTransitionCoroutine != null)
            StopCoroutine(intensityTransitionCoroutine);

        intensityTransitionCoroutine = StartCoroutine(ChangeIntensitySmoothly(targetIntensityWhenNotInRange));
    }

    private IEnumerator ChangeIntensitySmoothly(float targetIntensity)
    {
        float startIntensity = shopBuildingLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            shopBuildingLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shopBuildingLight.intensity = targetIntensity;
    }
}
