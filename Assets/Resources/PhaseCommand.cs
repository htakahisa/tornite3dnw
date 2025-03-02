using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseCommand : MonoBehaviourPun
{

    public static PhaseCommand pc;

    private void Awake()
    {
        pc = this;
    }


    void Start()
    {

        
    }

    public void CommandAqua(Vector3 position)
    {
        StartCoroutine(InstantiateAqua(position));
    }

    public void CommandBlueLight(Vector3 position)
    {
        StartCoroutine(InstantiateBlueLight(position));
    }
    public void CommandC4(Vector3 position)
    {
        StartCoroutine(InstantiateC4(position));
    }

    private IEnumerator InstantiateAqua(Vector3 position)
    {
        // "Buy" �t�F�[�Y�ł���Ԃ͑ҋ@
        while (PhaseManager.pm.GetPhase() == "Buy")
        {
            Debug.Log("WaitForBattle");
            yield return null; // ���̃t���[���܂ő҂�
        }

        PhotonNetwork.Instantiate("Aquaring", position, Quaternion.identity);
    }
    private IEnumerator InstantiateBlueLight(Vector3 position)
    {
        // "Buy" �t�F�[�Y�ł���Ԃ͑ҋ@
        while (PhaseManager.pm.GetPhase() == "Buy")
        {
            Debug.Log("WaitForBattle");
            yield return null; // ���̃t���[���܂ő҂�
        }

        PhotonNetwork.Instantiate("BlueLightSmoke", position, Quaternion.identity);
    }
    private IEnumerator InstantiateC4(Vector3 position)
    {
        // "Buy" �t�F�[�Y�ł���Ԃ͑ҋ@
        while (PhaseManager.pm.GetPhase() == "Buy")
        {
            Debug.Log("WaitForBattle");
            yield return null; // ���̃t���[���܂ő҂�
        }

        PhotonNetwork.Instantiate("C4", position, Quaternion.identity);
    }
}
