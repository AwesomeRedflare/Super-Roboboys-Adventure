using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject transition;
    public GameObject levelSelectPanel;

    private string level;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transition();
            FindObjectOfType<AudioManager>().Play("blip");
            Invoke("GameStart", 1f);
        }
    }

    public void Transition()
    {
        transition.GetComponent<Animator>().SetTrigger("to");
    }

    void GameStart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenLevelSelect()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        levelSelectPanel.SetActive(true);
        levelSelectPanel.GetComponent<Animator>().SetTrigger("open");
    }

    public void CloseLevelSelect()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        levelSelectPanel.GetComponent<Animator>().SetTrigger("close");
    }

    public void SelectLevelOne()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        Invoke("LoadLevel", 1f);
        level = "Level 1";
    }

    public void SelectLevelTwo()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        Invoke("LoadLevel", 1f);
        level = "Level 2";
    }

    public void SelectLevelThree()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        Invoke("LoadLevel", 1f);
        level = "Level 3";
    }

    public void SelectLevelFour()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        Invoke("LoadLevel", 1f);
        level = "Level 4";
    }

    public void SelectLevelFive()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        Invoke("LoadLevel", 1f);
        level = "Level 5";
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(level);
    }
}
