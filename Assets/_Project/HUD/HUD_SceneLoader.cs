using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingWindow;
    [SerializeField] private CanvasGroup transitionScreen;
    [SerializeField] private CanvasGroup progressIcon;
    [SerializeField] private float fakeWaitingDuration;
    [SerializeField] private float transitionDuration;

    public void LoadLevel(int sceneToLoadIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoadIndex));
    }

    private void Awake()
    {
      /*  loadingWindow.SetActive(false);
        transitionScreen.gameObject.SetActive(true);
        transitionScreen.DOFade(0f, transitionDuration / 2).SetDelay(1f).OnComplete(() => transitionScreen.gameObject.SetActive(false));
        progressIcon.DOFade(0f, 0.5f);*/
    }

    private IEnumerator LoadSceneAsync(int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        yield return null;

        /* AsyncOperation operation = SceneManager.LoadSceneAsync(index);
         operation.allowSceneActivation = false;
         loadingWindow.SetActive(true);
         progressIcon.gameObject.SetActive(true);
         progressIcon.DOFade(1f, 0.5f);

         if (fakeWaitingDuration > 0)
         {
             yield return new WaitForSeconds(fakeWaitingDuration);
         }

         while (!operation.isDone)
         {
             float progress = Mathf.Clamp01(operation.progress / 0.9f);

             if (operation.progress >= 0.9f)
             {
                 transitionScreen.gameObject.SetActive(true);
                 transitionScreen.alpha = 0f;
                 transitionScreen.DOFade(1f, transitionDuration / 2);
                 yield return new WaitForSeconds(transitionDuration / 2);
                 loadingWindow.SetActive(false);
                 progressIcon?.DOFade(0f, 0.5f);
                 yield return new WaitForSeconds(0.5f);
                 operation.allowSceneActivation = true;
                 break;
             }
         }
         transitionScreen.DOFade(0f, transitionDuration / 2).OnComplete(() => transitionScreen.gameObject.SetActive(false));*/
    }
}
