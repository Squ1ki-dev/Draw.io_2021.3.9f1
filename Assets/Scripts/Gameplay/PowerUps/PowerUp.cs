using UnityEngine;
using System.Collections;

public abstract class PowerUp : MappedObject
{
	public bool             ready { get { return (Time.time > Constants.c_PowerUpPreWarm + m_SpawnTime); } }
	public float            availabilityTime { get { return (Mathf.Max(Constants.c_PowerUpPreWarm + m_SpawnTime - Time.time, 0f)); } }
	public MeshRenderer     m_Model;
    
	protected float         m_SpawnTime;
	private bool            m_Prewarm;
	private Coroutine       m_PrewarmCoroutine;
	private bool            m_Alive;
	private float           m_ScaleFactor;
	private Vector3			m_BasePosition;

	public PowerUpSO powerUpSO;

	protected override void Awake ()
	{
		base.Awake ();

		m_ScaleFactor = m_Model.transform.localScale.x;
		m_Model.transform.localScale = Vector3.zero;
		m_Prewarm = true;
		m_PrewarmCoroutine = StartCoroutine(VisualPrewarm());
		m_SpawnTime = Time.time;
		m_BasePosition = m_Transform.position;
		
		m_Alive = true;
	}

	void Start()
	{
		RegisterMap();
	}

	void Update()
	{
		if (m_Alive == true)
		{
			if (m_Prewarm && ready)
				SetReady();

			m_Transform.RotateAround(m_Transform.position, Vector3.up, 150.0f * Time.deltaTime);
			m_Transform.position = m_BasePosition + Vector3.up * Mathf.Sin (Time.time * 5.0f) * 3.0f;
		}
		else if (powerUpSO.m_ParticleSystem.IsAlive(true) == false)
			Destroy(gameObject);
	}

	public virtual void OnPlayerTouched(Player _Player)
	{
		UnregisterMap();

        m_Model.enabled = false;
		powerUpSO.m_ParticleSystem.Play(true);
		powerUpSO.m_IdleParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		powerUpSO.m_Shadow.SetActive(false);
		m_Alive = false;      
	}

	protected virtual void SetReady()
	{
		m_Prewarm = false;

		if (m_PrewarmCoroutine != null)
			StopCoroutine(m_PrewarmCoroutine);
	}

	protected IEnumerator VisualPrewarm()
	{
		float time = 0;

		while (time < Constants.c_PowerUpPreWarm)
		{
			float value = Mathf.Lerp(0f, 1f, time / Constants.c_PowerUpPreWarm) * m_ScaleFactor;
			m_Model.transform.localScale = new Vector3(value, value, value);
			yield return (null);
			time += Time.deltaTime;
		}

		m_Model.transform.localScale = Vector3.one * m_ScaleFactor;
	}
}