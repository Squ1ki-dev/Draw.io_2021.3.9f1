using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenuView : View<DebugMenuView>
{
    [SerializeField] private Toggle m_SkinScreenToggle, m_PlayerCollisionToggle, m_SpeedBoosterToggle;
    [SerializeField] private GameObject BrushSelect, NewSkinButton;
    [SerializeField] private MainMenuView m_MainMenuView;
    private int currentToggleState;

    private void Start() => Init();

    private void Init()
    {
        m_SkinScreenToggle.isOn = PlayerPrefs.GetInt(Constants.c_SkinScreenToggle) == 1 ? true : false;
        m_PlayerCollisionToggle.isOn = PlayerPrefs.GetInt(Constants.c_CollisionToggle) == 1 ? true : false;
        m_SpeedBoosterToggle.isOn = PlayerPrefs.GetInt(Constants.c_BoosterToggle) == 1 ? true : false;
    }

    public void OnReturnButton()
    {
        this.Transition(false);
        this.gameObject.SetActive(false);
        m_MainMenuView.gameObject.SetActive(true);
        m_MainMenuView.Transition(true);
    }

    public void OnOldSkinScreen()
    {
        currentToggleState = m_SkinScreenToggle.isOn == true ? 1 : 0;
        
        if(currentToggleState == 1)
        {
            BrushSelect.SetActive(true);
            NewSkinButton.SetActive(false);
        }
        else
        {
            BrushSelect.SetActive(false);
            NewSkinButton.SetActive(true);
        }

        PlayerPrefs.SetInt(Constants.c_SkinScreenToggle, currentToggleState);
    }

    public void OnPlayerCollision()
    {
        currentToggleState = m_PlayerCollisionToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt(Constants.c_CollisionToggle, currentToggleState);
    }

    public void OnSpeedBooster()
    {
        currentToggleState = m_SpeedBoosterToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt(Constants.c_BoosterToggle, currentToggleState);
    }
}
