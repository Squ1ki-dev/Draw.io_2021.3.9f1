using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpCollision : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;

    private void OnCollisionEnter(Collision collision)
    {
        Brush brush = collision.gameObject.GetComponent<Brush>();

        if(GameManager.Instance.m_IsPlaying == true)
        {
            if (brush)
                JumpAnim();
        }
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
