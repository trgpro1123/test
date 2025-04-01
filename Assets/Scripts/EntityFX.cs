// using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    

    [Header("Aliment Particle")]
    [SerializeField] protected ParticleSystem bleedParticle;
    [SerializeField] protected ParticleSystem coldParticle;
    [SerializeField] protected ParticleSystem fastParticle;
    [SerializeField] protected ParticleSystem slowParticle;
    [SerializeField] protected ParticleSystem reverseControlsParticle;


    protected Coroutine slowEffectCoroutine;
    protected Coroutine fastEffectCoroutine;
    protected SpriteRenderer spriteRenderer;

    public void Awake() {
        spriteRenderer=transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    
    public void BleedEffect(bool _isBleeding){
        if(_isBleeding){
            bleedParticle.Play();
        }
        else{
            bleedParticle.Stop();
            bleedParticle.Clear();
        }
    }
    public void ColdEffect(bool _coldStuned){
        if(_coldStuned){
            coldParticle.Play();
        }
        else{
            coldParticle.Stop();
        }
    }
    public void FastEffect(bool _isFast){
        if(_isFast){
            fastParticle.Play();
        }
        else{
            fastParticle.Stop();
        }
    }
    public void SlowEffect(bool _isSlow){
        if(_isSlow){
            slowParticle.Play();
        }
        else{
            slowParticle.Stop();
        }
    }
    public void ReverseControlsEffect(bool _isReverseControls){
        if(_isReverseControls){
            reverseControlsParticle.Play();
        }
        else{
            reverseControlsParticle.Stop();
        }
    }
    public void FastEffectForTime(float _duration){
        if (fastEffectCoroutine != null){
            StopCoroutine(fastEffectCoroutine);
        }
        fastEffectCoroutine = StartCoroutine(FastEffectForTimeCoroutine(_duration));
    }

    public IEnumerator FastEffectForTimeCoroutine(float duration)
    {
        fastParticle.Play();
        yield return new WaitForSeconds(duration);
        fastParticle.Stop();
        fastEffectCoroutine = null;
    }
    public void SlowEffectForTime(float _duration){
        if (slowEffectCoroutine != null){
            StopCoroutine(slowEffectCoroutine);
        }
        slowEffectCoroutine = StartCoroutine(SlowEffectForTimeCoroutine(_duration));
    }

    public IEnumerator SlowEffectForTimeCoroutine(float duration)
    {
        slowParticle.Play();
        yield return new WaitForSeconds(duration);
        slowParticle.Stop();
        slowEffectCoroutine = null;
    }
    public void StopAllEffect(){
        bleedParticle.Stop();
        coldParticle.Stop();
        fastParticle.Stop();
        slowParticle.Stop();
        reverseControlsParticle.Stop();
    }
    
    
}
