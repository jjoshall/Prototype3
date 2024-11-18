using UnityEngine;

public class AudioTest : MonoBehaviour
{
     private AudioSource audioSource;

     void Start()
     {
          audioSource = GetComponent<AudioSource>();
          if (audioSource != null)
          {
               audioSource.Play();
               Debug.Log("Test sound played on Start.");
          }
     }
}
