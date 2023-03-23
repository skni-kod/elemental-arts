using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevels : MonoBehaviour
{
    public string lvlName;
    [SerializeField] private TextMeshProUGUI lvlNameText;

    private void Start()
    {
        lvlNameText.text = "Level " + lvlName;
    }

    public void ChooseLvl()
    {
        SceneManager.LoadScene("Level" + lvlName);
    }

}
