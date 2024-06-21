using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinView : View<SelectSkinView>
{
    [SerializeField] private List<Image> m_ColoredImages;
    [SerializeField] private Transform m_prefabContainer;
    [SerializeField] private SkinUIElement m_skinUIPrefab;
    [SerializeField] private MainMenuView m_MainMenuView;
    [SerializeField] private SkinUIElement m_SelectedBrush;
    private List<SkinUIElement> skinUIElements;
    private StatsManager m_StatsManager;
    private int m_IdSkin = 0;

    private void Awake()
    {
        base.Awake();

        m_StatsManager = StatsManager.Instance;
        m_IdSkin = m_StatsManager.FavoriteSkin;
    }

    private void Start() => TryInitialize();
    public void SetTitleColor(Color _Color) => UpdateColors(_Color);

    public void OnReturnButton()
    {
        this.Transition(false);
        this.gameObject.SetActive(false);
        m_MainMenuView.gameObject.SetActive(true);
        m_MainMenuView.Transition(true);
    }

    private void TryInitialize()
    {
        if (skinUIElements != null)
            return;

        skinUIElements = new List<SkinUIElement>();

        var existingSkins = GameManager.Instance.m_Skins;

        foreach (var skinData in existingSkins)
        {
            var prefab = Instantiate(m_skinUIPrefab, m_prefabContainer);
            prefab.Setup(skinData, OnClickSkin);

            skinUIElements.Add(prefab);
        }

        var favoriteSkin = GameManager.Instance.m_Skins[m_IdSkin];
        GameManager.Instance.SetSkin(favoriteSkin);
        m_SelectedBrush.Setup(favoriteSkin);
    }

    private void UpdateColors(Color _Color)
    {
        TryInitialize();

        foreach (var image in m_ColoredImages)
        {
            image.color = _Color;

            foreach (var skin in skinUIElements)
                skin.UpdateColor(_Color);
        }
    }

    private void OnClickSkin(SkinData selectedSkin)
    {
        GameManager.Instance.SetSkin(selectedSkin);
        m_SelectedBrush.Setup(selectedSkin);
        m_StatsManager.FavoriteSkin = GameManager.Instance.GetSkinIndex(selectedSkin);
    }
}
