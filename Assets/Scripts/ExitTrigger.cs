using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            FindObjectOfType<LevelManager>().CompleteLevel();
        }
    }
}