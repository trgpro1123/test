using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private float sfxMinimunDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBMG;
    private int indexBMG;
    private bool canPlaySFX;

    private void Awake() {
        if(instance!=null&&this.gameObject!=null){
            Destroy(this.gameObject);
        }
        else{
            instance=this;
        }
        if(!gameObject.transform.parent){
            DontDestroyOnLoad(gameObject);
        }
        indexBMG=-1;
        Invoke("AllowPlaySFX",.1f);
    }
    private void Update() {
        if(!playBMG){
            StopAllBMG();
        }else{
            if(!bgm[indexBMG].isPlaying){
                PlayBMG(indexBMG);
            }
        }
    }

    public void PlaySFX(int _indexSFX){

        // if(sfx[_indexSFX].isPlaying) 
        //     return;
        if(canPlaySFX==false) return;
        sfx[_indexSFX].pitch=Random.Range(.85f,1.1f);
        if(_indexSFX<sfx.Length){
            sfx[_indexSFX].Play();
        }
    }
    public void StopSFX(int _indexSFX) => sfx[_indexSFX].Stop();

    public void PlayBMG(int _indexBMG){
        if(bgm[_indexBMG].isPlaying) 
            return;
        indexBMG=_indexBMG;
        StopAllBMG();
        if(_indexBMG<bgm.Length){
            bgm[indexBMG].Play();
        }
    }
    public void StopAllBMG(){
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    private void PlayRandomBMG(){
        indexBMG=Random.Range(0,bgm.Length);
        bgm[indexBMG].Play();
    }
    private void AllowPlaySFX()=>canPlaySFX=true;
}
