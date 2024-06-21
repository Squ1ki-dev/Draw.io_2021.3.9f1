using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrushRotation : MonoBehaviour
{
	private Transform m_Transform;
	[SerializeField] private float m_rotationDuration;

	private void Awake()
	{
		// Cache
		m_Transform = transform;
	}

	private void Start()
	{
		Rotation();
	}

	private void Rotation()
	{
		m_Transform.DORotate(new Vector3(0, 360f, 0), m_rotationDuration, RotateMode.LocalAxisAdd)
        	.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear);
	}
}
