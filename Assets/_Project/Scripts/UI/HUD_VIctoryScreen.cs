using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUD_VIctoryScreen : MonoBehaviour
{
    public GameObject VictoryScreen;
    public GameObject m_firstButtonInOptionMenu;


    public GameObject P1Won;
    public GameObject P2Won;


    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_firstButtonInOptionMenu);
        
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_firstButtonInOptionMenu);
    }
}
