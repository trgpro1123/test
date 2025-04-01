using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float duration;

    private Material material;
    private int dissolveAmountID=Shader.PropertyToID("_DissolveAmount");

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    public void StartDissolve()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(Appear());
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(Vanish());
        }
    }
    private void Update() {
        StartDissolve();
    }

    public IEnumerator Vanish()
    {
        float time = 0;
        while (time < duration)
        {

            time += Time.deltaTime;
            // Debug.Log(time);
            float lerpedValue = Mathf.Lerp(0, 1.1f, time / duration);
            material.SetFloat(dissolveAmountID, lerpedValue);
            yield return null;
        }
        Destroy(transform.parent.gameObject);

    }
    public IEnumerator Appear()
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float lerpedValue = Mathf.Lerp(1.1f, 0, time / duration);
            material.SetFloat(dissolveAmountID, lerpedValue);
            yield return null;
        }
    }
}
