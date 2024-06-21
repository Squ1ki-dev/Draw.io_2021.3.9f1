using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinModelPreview : MonoBehaviour, IBrushElement
{
    public delegate void HandleOnClickSkin(SkinData skinData);
    public event HandleOnClickSkin OnClickSkin;
    [SerializeField] BrushMainMenu m_Brush;

    private SkinData m_skinData;
    public void Setup(SkinData _SkinData, HandleOnClickSkin _OnClickSkin = null)
    {
        this.OnClickSkin = _OnClickSkin;
        this.m_skinData = _SkinData;

        SetupSkin();
    }

    public void OnClick() => OnClickSkin?.Invoke(m_skinData);

    public void SetupSkin() => m_Brush.Set(m_skinData);
}
