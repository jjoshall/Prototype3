using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
     public GameObject PlayerPOV;
     public MonoBehaviour PlayerController; // Reference to the player's controller script

     private AnimalController activeAnimalController = null; // Currently active animal controller

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
          Camera activeCamera = PlayerPOV.activeSelf ? PlayerPOV.GetComponent<Camera>() : activeAnimalController?.animalCamera;

          Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if (Physics.Raycast(ray, out hit))
          {
               if (hit.collider.CompareTag("Animal"))
               {
                    AnimalController animalController = hit.collider.GetComponent<AnimalController>();
                    if (animalController != null)
                    {
                         StartCoroutine(SwitchToAnimalCamera(animalController));
                    }
               }
               else if (hit.collider.CompareTag("Player"))
               {
                    StartCoroutine(SwitchToPlayerCamera());
               }
          }
     }

     private IEnumerator SwitchToAnimalCamera(AnimalController animalController)
     {
          // Trigger the flash effect before switching cameras
          yield return StartCoroutine(pictureScript.FlashEffect());

          // Switch to animal camera after flash effect
          ActivateAnimalCamera(animalController);
     }

     private IEnumerator SwitchToPlayerCamera()
     {
          // Trigger the flash effect before switching cameras
          yield return StartCoroutine(pictureScript.FlashEffect());

          // Switch to player camera after flash effect
          ActivatePlayerCamera();
     }

     void ActivatePlayerCamera()
     {
          // Deactivate any active animal controller and camera
          if (activeAnimalController != null)
          {
               activeAnimalController.isActive = false;
               activeAnimalController.animalCamera.gameObject.SetActive(false);
               activeAnimalController = null;
          }

          PlayerPOV.SetActive(true);
          PlayerController.enabled = true;
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
     }

     void ActivateAnimalCamera(AnimalController animalController)
     {
          // Deactivate the player controller and camera
          PlayerPOV.SetActive(false);
          PlayerController.enabled = false;

          // Deactivate any previously active animal
          if (activeAnimalController != null)
          {
               activeAnimalController.isActive = false;
               activeAnimalController.animalCamera.gameObject.SetActive(false);
          }

          // Activate the new animal
          activeAnimalController = animalController;
          activeAnimalController.isActive = true;
          activeAnimalController.animalCamera.gameObject.SetActive(true);
     }
}
