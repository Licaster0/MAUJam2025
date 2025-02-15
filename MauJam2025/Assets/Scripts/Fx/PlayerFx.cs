using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerFx : EntityFx
{
    [Header("Screen Shake FX")]
    private CinemachineImpulseSource screenShake;
    [SerializeField] private float shakeMultiplier;
    public Vector3 shakeHighDamage;


    [Space]
    [SerializeField] private ParticleSystem dustFx;
    [SerializeField] private ParticleSystem coinFx;
    protected override void Start()
    {
        base.Start();
        screenShake = GetComponent<CinemachineImpulseSource>();
    }


    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse(_shakePower);
    }


    public void PlayDustFX()
    {
        if (dustFx != null)
            dustFx.Play();
    }

    public void PlayCoinFX()
    {
        if (coinFx != null)
            coinFx.Play();
    }
}
