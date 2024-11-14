using UnityEngine;

public class BranchDestruction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillTrigger"))
        {
            Destroy(gameObject); // Destroys the branch piece when it enters the KillTrigger
        }
    }
}
