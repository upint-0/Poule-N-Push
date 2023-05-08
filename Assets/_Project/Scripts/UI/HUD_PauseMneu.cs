using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HUD_PauseMneu : MonoBehaviour
{

    public GameObject m_pauseMenu;
    public GameObject m_firstButtonInOptionMenu;
    private bool m_activeManette;
    private bool m_isPaused;
    public AudioSource audiso;
    // Start is called before the first frame update
    void Start()
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_firstButtonInOptionMenu);
        m_isPaused = false;
    }
    private void OnEnable()
    {
        /*EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_firstButtonInOptionMenu);
        m_isPaused = false;*/
    }
    public void MenuOnPause()
    {
        if (m_isPaused == true)
        {

            if (m_activeManette)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(m_firstButtonInOptionMenu);
                m_activeManette = false;
               
            }
            Time.timeScale = 0;
            m_pauseMenu.SetActive(true);
            audiso.GetComponent<AudioLowPassFilter>().enabled = true;
            audiso.GetComponent<AudioReverbFilter>().enabled = true;
            audiso.pitch = 0.5f;

            return;
        }
        if (m_isPaused == false)
        {
            Time.timeScale = 1;
            m_pauseMenu.SetActive(false);

         //   m_pauseMenu.SetActive(true);
            audiso.GetComponent<AudioLowPassFilter>().enabled = false;
            audiso.GetComponent<AudioReverbFilter>().enabled = false;
            audiso.pitch = 1f;
            return;
        }
    }

    public void DisablePause()
    {
        m_isPaused = !m_isPaused;
        m_activeManette = true;
        MenuOnPause();
       
    }



}
