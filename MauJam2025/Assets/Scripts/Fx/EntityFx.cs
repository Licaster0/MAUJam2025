using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    [SerializeField] protected Player player;
    protected SpriteRenderer sr;

    [Header("Pop Up Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;



    [Header("Hit FX")]
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject criticalHitFx;

    void Awake()
    {
    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;
        originalMat = sr.material;
    }

    // public void CreatePopUpText(string _text)
    // {
    //     float randomX = Random.Range(-1, 1);
    //     float randomY = Random.Range(1.5f, 3);

    //     Vector3 positionOffset = new Vector3(randomX, randomY, 0);

    //     GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);

    //     newText.GetComponent<TextMeshPro>().text = _text;
    // }


    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;

        else
            sr.color = Color.red;
    }

    public void CreateHitFX(Transform _target, bool _critical)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);

        Vector3 hitFxRotation = new Vector3(0, 0, zRotation);

        GameObject hitPrefab = hitFx;

        if (_critical)
        {
            hitPrefab = criticalHitFx;

            float yRotation = 0;
            zRotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().facingDir == -1)
                yRotation = -180;

            hitFxRotation = new Vector3(0, yRotation, zRotation);

            CriticalHitStopEffect(.2f, .2f);
        }

        LowHitStopEffect();
        GameObject newHitFx = Instantiate(hitPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity, _target);
        newHitFx.transform.Rotate(hitFxRotation);
        Destroy(newHitFx, .5F);
    }

    private void LowHitStopEffect()
    {
        player.fx.ScreenShake(player.fx.shakeLowDamage);
        StartCoroutine(HitStop(.05f, .1f));
    }

    public void CriticalHitStopEffect(float _duration, float _slowdownFactor)
    {
        player.fx.ScreenShake(player.fx.shakeHighDamage);
        StartCoroutine(HitStop(_duration, _slowdownFactor));
    }

    IEnumerator HitStop(float duration, float slowdownFactor)
    {
        Time.timeScale = slowdownFactor;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
