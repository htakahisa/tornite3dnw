using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviourPun {

    public AudioSource audioSource;  // ���ʉ���Đ�����AudioSource
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip shoot;
    public AudioClip reload;
    public AudioClip noarmo;
    public AudioClip phantom;
    public AudioClip stray;
    public AudioClip wolf;
    public AudioClip coward;
    public AudioClip flash;
    public AudioClip boostio;
    public AudioClip beep;
    public AudioClip clapping;
    public AudioClip selfwalk;
    public AudioClip warp;
    public AudioClip defuse;
    public AudioClip wallclose;
    public AudioClip barClimb;
    public AudioClip detect;
    public AudioClip detectstart;
    public AudioClip arte;
    public AudioClip shoot_mischief;
    public AudioClip shoot_noel;
    public AudioClip wolfbark;
    public AudioClip cat1;
    public AudioClip cat2;
    public AudioClip cat3;
    public AudioClip cat4;
    public AudioClip collect;
    public AudioClip blackBell;
    public AudioClip landing;

    void Awake() {
        
        // AudioSource�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�͎����擾
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
          //  audioSource.PlayOneShot(walk);
        }
    }


    public void PlaySound(string type) {
        if (SceneManager.GetActiveScene().name == "Practice") {
            return;
        }
        photonView.RPC("PlaySoundRPC", RpcTarget.Others, type);       
        PlayMySound(type);
    }

    [PunRPC]
    public void PlayMySound(string type) {

       
        

        if (type.Equals("walk")) {
            audioSource.PlayOneShot(selfwalk);
        }
        if (type.Equals("jump")) {
            audioSource.PlayOneShot(jump);
        }
        if (type.Equals("shoot")) {
            audioSource.PlayOneShot(shoot);
        }
        if (type.Equals("shoot_mischief"))
        {
            audioSource.PlayOneShot(shoot_mischief);
        }
        if (type.Equals("shoot_noel"))
        {
            audioSource.PlayOneShot(shoot_noel);
        }
        if (type.Equals("reload")) {
            audioSource.PlayOneShot(reload);
        }
        if (type.Equals("noarmo")) {
            audioSource.PlayOneShot(noarmo);
        }
        if (type.Equals("phantom")) {
            audioSource.PlayOneShot(phantom);
        }
        if (type.Equals("stray")) {
            audioSource.PlayOneShot(stray);
        }
        if (type.Equals("wolf")) {
            audioSource.PlayOneShot(wolf);
        }
        if (type.Equals("cat1"))
        {
            audioSource.PlayOneShot(cat1);
        }
        if (type.Equals("cat2"))
        {
            audioSource.PlayOneShot(cat2);
        }
        if (type.Equals("cat3"))
        {
            audioSource.PlayOneShot(cat3);
        }
        if (type.Equals("cat4"))
        {
            audioSource.PlayOneShot(cat4);
        }
        if (type.Equals("coward")) {
            audioSource.PlayOneShot(coward);
        }
        if (type.Equals("flash")) {
            audioSource.PlayOneShot(flash);
        }
        if (type.Equals("boostio")) {
            audioSource.PlayOneShot(boostio);
        }
        if (type.Equals("beep")) {
            audioSource.PlayOneShot(beep);
        }

        if (type.Equals("clapping"))
        {
            audioSource.PlayOneShot(clapping);
        }
        if (type.Equals("warp"))
        {
            audioSource.PlayOneShot(warp);
        }
        if (type.Equals("defuse"))
        {
            audioSource.PlayOneShot(defuse);
        }
        if (type.Equals("wallclose"))
        {
            audioSource.PlayOneShot(wallclose);
        }
        if (type.Equals("barClimb"))
        {
            audioSource.PlayOneShot(barClimb);
        }
        if (type.Equals("detect"))
        {
            audioSource.PlayOneShot(detect);
        }
        if (type.Equals("detectstart"))
        {
            audioSource.PlayOneShot(detectstart);
        }
        if (type.Equals("arte"))
        {
            audioSource.PlayOneShot(arte);
        }
        if (type.Equals("wolfbark"))
        {
            audioSource.PlayOneShot(wolfbark);
        }
        if (type.Equals("collect"))
        {
            audioSource.PlayOneShot(collect);
        }
        if (type.Equals("blackBell"))
        {
            audioSource.PlayOneShot(blackBell);
        }
        if (type.Equals("landing"))
        {
            audioSource.PlayOneShot(landing);
        }
    }

    


        // RPC�ő��̃N���C�A���g�ł���ʉ���Đ�������
    [PunRPC]
    void PlaySoundRPC(string type) {
        if (PhaseManager.pm.GetPhase().Equals("Battle"))
        {
            PlayMySound(type);
            
        }

    }
}
