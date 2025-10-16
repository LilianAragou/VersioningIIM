using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSceneManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject credit;
    public Animator animatorCredit;

    public void Start()
    {
        credit.SetActive(true);
    } 

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credit()
    {
        animatorCredit.SetTrigger("selected");
    }

}