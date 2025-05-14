using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    private int currentLevelIndex = 0;

    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int index)
    {
    

        // Enable selected level
        if (index >= 0 && index < levels.Length)
        {
            levels[index].SetActive(true);
            currentLevelIndex = index;
        }
    }

    public void NextLevel()
    {
        int nextLevel = currentLevelIndex + 1;
        if (nextLevel < levels.Length)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            Debug.Log("All levels completed!");

        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }
  
}
