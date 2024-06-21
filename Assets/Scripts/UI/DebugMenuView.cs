using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenuView : View<DebugMenuView>
{
    [SerializeField] private Toggle m_SkinSelectorToggle, m_PlayerCollisionToggle, m_SpeedBoosterToggle;
    [SerializeField] private GameObject BrushSelect, NewSkinButton;
    [SerializeField] private MainMenuView m_MainMenuView;
    private int currentToggleState;

    private void Start() => Init();

    private void Init()
    {
        m_SkinSelectorToggle.isOn = PlayerPrefs.GetInt(Constants.c_SkinSelectorToggle) == 1;
        m_PlayerCollisionToggle.isOn = PlayerPrefs.GetInt(Constants.c_CollisionToggle) == 1;
        m_SpeedBoosterToggle.isOn = PlayerPrefs.GetInt(Constants.c_BoosterToggle) == 1;

        m_SkinSelectorToggle.onValueChanged.AddListener(OnSkinSelectorToggleChanged);
        m_PlayerCollisionToggle.onValueChanged.AddListener(OnPlayerCollisionToggleChanged);
        m_SpeedBoosterToggle.onValueChanged.AddListener(OnSpeedBoosterToggleChanged);
    }

    public void OnReturnButton()
    {
        Transition(false);
        gameObject.SetActive(false);
        m_MainMenuView.gameObject.SetActive(true);
        m_MainMenuView.Transition(true);
    }

    private void OnSkinSelectorToggleChanged(bool isOn)
    {
        BrushSelect.SetActive(isOn);
        NewSkinButton.SetActive(!isOn);
        PlayerPrefs.SetInt(Constants.c_SkinSelectorToggle, isOn ? 1 : 0);
    }

    private void OnPlayerCollisionToggleChanged(bool isOn) => PlayerPrefs.SetInt(Constants.c_CollisionToggle, isOn ? 1 : 0);

    private void OnSpeedBoosterToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(Constants.c_BoosterToggle, isOn ? 1 : 0);
        GameManager.Instance.InitPowerUps();
    }
}
