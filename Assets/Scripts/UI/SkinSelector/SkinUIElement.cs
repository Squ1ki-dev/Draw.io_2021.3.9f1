using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;

public class SkinUIElement : MonoBehaviour, IBrushElement
{
    public delegate void HandleOnClickSkin(SkinData skinData);
    public event HandleOnClickSkin OnClickSkin;

    [SerializeField] Camera m_Camera;
    [SerializeField] RawImage m_RawImage;
    [SerializeField] Image m_ButtonImage;
    [SerializeField] BrushMainMenu m_Brush;
    
    private SkinData m_skinData;
    private RenderTexture m_renderTexture;

    private void Awake()
    {
        InitRenderTexture();
    }

    private void InitRenderTexture()
    {
        m_renderTexture = new RenderTexture(256, 256, 16,  RenderTextureFormat.ARGB32);
        m_renderTexture.Create();

        m_Camera.targetTexture = m_renderTexture;

        m_RawImage.texture = m_renderTexture;
    }

    public void Setup(SkinData _SkinData, HandleOnClickSkin _OnClickSkin = null)
    {
        this.OnClickSkin = _OnClickSkin;
        this.m_skinData = _SkinData;

        SetupSkin();
    }

    public void UpdateColor(Color _UpdateColor) => m_ButtonImage.color = _UpdateColor;

    public void OnClick() => OnClickSkin?.Invoke(m_skinData);

    public void SetupSkin() => m_Brush.Set(m_skinData);
}