using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject goodEnding;
    public GameObject badEnding;
    public GameObject transition;

    private void Start()
    {
        if(PlayerController.goldAmount > 0)
        {
            goodEnding.SetActive(true);
            badEnding.SetActive(false);
        }

        if(PlayerController.goldAmount <= 0)
        {
            goodEnding.SetActive(false);
            badEnding.SetActive(true);
        }
    }

    public void Transition()
    {
        FindObjectOfType<AudioManager>().Play("blip");
        transition.GetComponent<Animator>().SetTrigger("to");
        Invoke("MainMenu", 2f);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
