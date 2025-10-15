using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject loadingBar;
    public Image loadingBarFill;

    public void Start()
    {
        loadingBar.SetActive(false);
        loadingScreen.SetActive(false);
        loadingBarFill.fillAmount = 0f;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        loadingBar.SetActive(true);
        loadingScreen.SetActive(true);

        float fakeProgress = 0f;

        while (fakeProgress < 0.9f)
        {
            float step = Random.Range(0.02f, 0.2f);
            fakeProgress += step;
            fakeProgress = Mathf.Min(fakeProgress, 0.9f);

            float startFill = loadingBarFill.fillAmount;
            float t = 0f;
            float speed = Random.Range(5f, 8f);

            while (t < 1f)
            {
                t += Time.deltaTime * speed;
                loadingBarFill.fillAmount = Mathf.Lerp(startFill, fakeProgress, t);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f)); 
        }

        while (loadingBarFill.fillAmount < 1f)
        {
            loadingBarFill.fillAmount = Mathf.MoveTowards(loadingBarFill.fillAmount, 1f, Time.deltaTime * 2f);
            yield return null;
        }

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}