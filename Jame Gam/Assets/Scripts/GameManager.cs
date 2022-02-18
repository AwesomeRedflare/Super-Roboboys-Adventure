using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text goldAmountText;
    public Text levelCompleteText;

    public GameObject levelCompletePanel;
    public GameObject transition;
    public GameObject decisionPanel;

    public PlayerController playerController;
    public CameraFollow cameraFollow;

    private void Start()
    {
        levelCompleteText.text = SceneManager.GetActiveScene().name + " Complete";

        if (SceneManager.GetActiveScene().name == "Boss")
        {
            playerController.canMove = false;
            cameraFollow.move = false;
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Theme");
        }
    }

    private void Update()
    {
        goldAmountText.text = "  x " + PlayerController.goldAmount.ToString();
    }

    public void LevelComplete()
    {
        FindObjectOfType<AudioManager>().Mute("Theme");
        FindObjectOfType<AudioManager>().Play("LevelEnd");
        cameraFollow.hasWon = true;
        levelCompletePanel.SetActive(true);
        Invoke("Transition", 3f);
        Invoke("LoadNextScene", 4.2f);
    }

    public void Door()
    {
        cameraFollow.move = false;
        cameraFollow.door = true;
        Invoke("Transition", 1f);
        Invoke("LoadNextScene", 2f);
    }

    public void Transition()
    {
        transition.GetComponent<Animator>().SetTrigger("to");
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DecisionFight()
    {
        FindObjectOfType<AudioManager>().Play("Roboss");
        //decisionPanel.SetActive(false);
        decisionPanel.GetComponent<Animator>().SetTrigger("close");
        playerController.canMove = true;
        GameObject.FindGameObjectWithTag("boss").GetComponent<Boss>().fight = true;
        GameObject.FindGameObjectWithTag("boss").GetComponent<Animator>().SetTrigger("start");
    }

    public void Win()
    {
        //decisionPanel.GetComponent<Animator>().SetTrigger("close");
        playerController.canMove = false;
        FindObjectOfType<AudioManager>().Play("LevelEnd");
        Invoke("Transition", 3f);
        Invoke("EndGame", 4f);
    }

    public void DecisionGiveGold()
    {
        PlayerController.goldAmount = 0;
        //decisionPanel.SetActive(false);
        decisionPanel.GetComponent<Animator>().SetTrigger("close");
        playerController.canMove = false;
        FindObjectOfType<AudioManager>().Play("LevelEnd");
        Invoke("Transition", 3f);
        Invoke("EndGame", 4f);
    }

    void EndGame()
    {
        SceneManager.LoadScene("Win");
    }
}
