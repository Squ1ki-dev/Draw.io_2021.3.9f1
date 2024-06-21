using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpCollision : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;

    private void Start() => ApplyCollisionSettings();

    private void ApplyCollisionSettings()
    {
        if(PlayerPrefs.GetInt(Constants.c_CollisionToggle) == 1)
            jumpHeight = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(GameManager.Instance.m_IsPlaying && collision.gameObject.TryGetComponent(out Brush brush))
            JumpAnim();
    }

    private void JumpAnim()
    {
        Vector3 initialPosition = transform.localPosition;

        Sequence jumpSequence = DOTween.Sequence();

        jumpSequence.Append(transform.DOMoveY(initialPosition.y + jumpHeight, jumpDuration / 2).SetEase(Ease.OutQuad));
        jumpSequence.Append(transform.DOMoveY(initialPosition.y, jumpDuration / 2).SetEase(Ease.InQuad));
        jumpSequence.Play();
    }
}
