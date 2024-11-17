using System.Collections;
using UnityEngine;

public class Destructible : MonoBehaviour
{
     public GameObject destroyedVersion;
     public float breakDelay = 2f; // Default starting delay
     public AudioClip breakSound; // Reference to the sound effect

     private bool isBreaking = false;
     private AudioSource audioSource;

     void Start()
     {
          // Add an AudioSource component if not already present and set the clip
          audioSource = gameObject.AddComponent<AudioSource>();
          audioSource.clip = breakSound;
          audioSource.playOnAwake = false; // Prevents the sound from playing on object start
     }

     void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.CompareTag("Player") && !isBreaking)
          {
               isBreaking = true;
               breakDelay = ScoreManager.instance.breakDelay; // Ensure the current break delay is consistent
               BreakDelayManager.instance?.StartCountdown(breakDelay);
               StartCoroutine(BreakAfterDelay());
          }
     }

     IEnumerator BreakAfterDelay()
     {
          yield return new WaitForSeconds(breakDelay);

          // Play the breaking sound
          if (audioSource != null && audioSource.clip != null)
          {
               audioSource.Play();
               Debug.Log("Playing break sound");
          }

          GameObject instance = Instantiate(destroyedVersion, transform.position, transform.rotation);
          Destroy(gameObject);

          // Apply forces to the broken pieces
          foreach (Rigidbody rb in instance.GetComponentsInChildren<Rigidbody>())
          {
               Vector3 randomDirection = new Vector3(
                   Random.Range(-1f, 1f),
                   Random.Range(-1f, -0.1f),
                   Random.Range(-1f, 1f)
               ).normalized;

               rb.AddForce(randomDirection * Random.Range(0.5f, 2f), ForceMode.Impulse);
          }

          // Add score through ScoreManager
          if (ScoreManager.instance != null)
          {
               ScoreManager.instance.AddScore(1);
          }

          isBreaking = false; // Reset breaking state for next collision
     }
}
