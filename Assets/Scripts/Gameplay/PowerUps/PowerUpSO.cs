using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUpBaseSO", menuName = "Data/PowerUpBaseSO")]
public class PowerUpSO : ScriptableObject
{
    public float 			m_Duration;
	public ParticleSystem   m_ParticleSystem;
	public ParticleSystem   m_IdleParticleSystem;
	public GameObject       m_Shadow;
}
