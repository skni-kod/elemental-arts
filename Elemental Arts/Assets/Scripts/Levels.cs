using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] GameObject levelPrefab;
    private List<GameObject> levels;
    void Start()
    {
        levels = new List<GameObject>();
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            GameObject newLvl = Instantiate(levelPrefab, transform);
            levels.Add(newLvl);
            newLvl.GetComponentInChildren<SelectLevels>().lvlName = i.ToString();
        }
    }
}
