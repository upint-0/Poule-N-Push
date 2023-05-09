using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class HUD_MainMenu : MonoBehaviour
{

    public CanvasGroup GOPanel;
    public GameObject m_firstButtonInOptionMenu;
    public Canvas HUDINGAME;
    public AudioSource audiso;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    private void Start()
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_firstButtonInOptionMenu);
    }

    public void LaunchTheGame()
    {
        GameManager.Instance.StartNewGame();
        Time.timeScale = 1;
        HUDINGAME.gameObject.SetActive(true);
        audiso.GetComponent<AudioLowPassFilter>().enabled = false;
        audiso.GetComponent<AudioReverbFilter>().enabled = false;
        audiso.pitch = 1f;
        
        GOPanel.DOFade(0f, 1f).SetEase(Ease.OutQuint).OnComplete(() => GOPanel.gameObject.SetActive(false));
    }
    public void LeaveTheGame()
    {
        Application.Quit();
    }



    private void Update()
    {
       
    }
}
