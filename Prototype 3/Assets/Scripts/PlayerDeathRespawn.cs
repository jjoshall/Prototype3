using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathRespawn : MonoBehaviour
{
     // If player triggers death zone, restart scene
     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player"))
          {
               // Restart the scene
               Scene scene = SceneManager.GetActiveScene();
               SceneManager.LoadScene(scene.name);
          }
     }
}
