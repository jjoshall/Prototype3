using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FlashEffect());
        }
    }

    public IEnumerator FlashEffect()
    {
        // Enable the flash effect
        cameraFlash.SetActive(true);
        
        // Wait for the specified flash time
        yield return new WaitForSeconds(flashTime);
        
        // Disable the flash effect
        cameraFlash.SetActive(false);
    }
}
