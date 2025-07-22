using UnityEngine;

public class ExitDetector : MonoBehaviour
{
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.CompleteLevel();
            //gameObject.SetActive(false); // Bir daha tetiklenmesin
        }
    }
}