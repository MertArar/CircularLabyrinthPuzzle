using UnityEngine;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public CircleGenerator generator;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bravoText;
    public GameObject player;

    private int currentLevel = 1;

    void Start()
    {
        bravoText.gameObject.SetActive(false);
        StartLevel(currentLevel);
    }

    void StartLevel(int level)
    {
        currentLevel = level;
        generator.GenerateLevel(level);
        levelText.text = "Level " + level;
        bravoText.gameObject.SetActive(false);
        ResetPlayerPosition();
    }

    public void CompleteLevel()
    {
        StartCoroutine(NextLevelRoutine());
    }

    IEnumerator NextLevelRoutine()
    {
        bravoText.gameObject.SetActive(true);
        bravoText.text = "Bravo!";
        yield return new WaitForSeconds(2f);
        bravoText.gameObject.SetActive(false);
        StartLevel(++currentLevel);
    }

    void ResetPlayerPosition()
    {
        if (player)
            player.transform.position = Vector3.zero;
    }
}