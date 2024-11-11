using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime = 0.1f;

     private AudioSource cameraSound;

     // Start is called before the first frame update
     void Start()
     {
          cameraSound = GetComponent<AudioSource>();

          if (cameraSound == null)
          {
               Debug.LogError("AudioSource component not found on this object. Camera sound will not play.");
          }
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
          // Play the camera shutter sound if the AudioSource is available
          if (cameraSound != null)
          {
               cameraSound.Play();
          }
          else
          {
               Debug.LogWarning("Attempted to play sound, but no AudioSource is available.");
          }

          // Enable the flash effect
          cameraFlash.SetActive(true);
        
        // Wait for the specified flash time
        yield return new WaitForSeconds(flashTime);
        
        // Disable the flash effect
        cameraFlash.SetActive(false);
    }
}
