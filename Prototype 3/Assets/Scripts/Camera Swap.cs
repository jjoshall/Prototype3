using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public GameObject PlayerPOV;
    public GameObject AnimalPOV;
    public MonoBehaviour PlayerController; // Reference to the player's controller script
    public MonoBehaviour AnimalController; // Reference to the animal's controller script

    private bool isPlayerCameraActive = true;

    public Picture pictureScript;

    void Start()
    {
        ActivatePlayerCamera(); // Start with player camera active
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            RaycastToSwitchCamera();
        }
    }

    void RaycastToSwitchCamera()
    {

        Camera activeCamera = PlayerPOV.activeSelf ? PlayerPOV.GetComponent<Camera>() : AnimalPOV.GetComponent<Camera>();

        Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            if (hit.collider.CompareTag("Animal"))
            {
                ActivateAnimalCamera();
            }
            else if (hit.collider.CompareTag("Player"))
            {
                ActivatePlayerCamera();
            }
        }
    }
    private IEnumerator SwitchToAnimalCamera()
    {
        // Trigger the flash effect before switching cameras
        yield return StartCoroutine(pictureScript.FlashEffect());

        // Switch to animal camera after flash effect
        ActivateAnimalCamera();
    }

    private IEnumerator SwitchToPlayerCamera()
    {
        // Trigger the flash effect before switching cameras
        yield return StartCoroutine(pictureScript.FlashEffect());

        // Switch to player camera after flash effect
        ActivatePlayerCamera();
    }
    void SwitchCamera()
    {
        if (isPlayerCameraActive)
        {
            ActivateAnimalCamera();
        }
        else
        {
            ActivatePlayerCamera();
        }
    }

    void ActivatePlayerCamera()
    {
        PlayerPOV.SetActive(true);
        AnimalPOV.SetActive(false);
        PlayerController.enabled = true;
        AnimalController.enabled = false;
        isPlayerCameraActive = true;
    }

    void ActivateAnimalCamera()
    {
        AnimalPOV.SetActive(true);
        PlayerPOV.SetActive(false);
        PlayerController.enabled = false;
        AnimalController.enabled = true;
        isPlayerCameraActive = false;
    }
}