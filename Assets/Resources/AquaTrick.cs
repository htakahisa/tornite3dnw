using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaTrick : MonoBehaviourPunCallbacks
{

    private bool instantiatedScan; // ���������v���t�@�u��ێ�����ϐ�
    private GameObject instantiatedPrefab; // ���������v���t�@�u��ێ�����ϐ�
                    
    public float scanRadius = 3f;            // �X�L�����͈�
    public LayerMask hitMask;                 // �q�b�g����p�̃��C���[�}�X�N
    private Ability ability;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private System.Collections.IEnumerator Scan()
    {
        // �X�L�������s��
        yield return new WaitForSeconds(1f); // �X�L�����O�̑ҋ@����

        // �X�L�����͈͓��̓G�����o
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets)
        {
            Debug.Log("�G�����o: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");

            target.GetComponentInParent<CameraController>().Stuned(1.5f, 0.1f);

        }

        // �X�L������̏����i�I�u�W�F�N�g��j�󂷂�Ȃǁj
        Invoke("End", 2f); // �X�L�����@�\��j��
    }
    private void End()
    {
        BattleData.bd.DetectEnd();
    }

     private void OnActive() {
        ability = Camera.main.transform.parent.GetComponent<Ability>();

        if (ability.number2 >= 1) { 
        

            // ���������v���t�@�u�����݂��Ȃ��ꍇ�ɐ���
            if (instantiatedPrefab == null)
            {
                ability.Spend(2,1);
                Debug.Log($"{gameObject.name} �����N���b�N����܂���");
                instantiatedPrefab = PhotonNetwork.Instantiate("AquaSmoke", transform.position, Quaternion.identity);

            }
            else if (!instantiatedScan)
            {


                ability.Spend(2, 1);
                instantiatedScan = true;
                StartCoroutine(Scan());


            }
        }



     }



        // �E�N���b�N���ꂽ�Ƃ��̏���
        private void OnCancel()
    {
        // ���������v���t�@�u�����݂���ꍇ�ɍ폜
        if (instantiatedPrefab != null)
        {

        Debug.Log($"{gameObject.name} ���E�N���b�N����܂���");
       
        DestroyPrefab();
        ability.Collect(2, 1);

        } else { 
               
            Debug.Log("Prefab already destroyed or not instantiated");

        }

    }

    public void DestroyPrefab()
    {
        PhotonNetwork.Destroy(gameObject);
        instantiatedPrefab.GetComponent<SmokeManager>().PhotonDestroy();
            Debug.Log("Prefab destroyed");
        
    }


        void OnMouseOver() {

        // �E�N���b�N�ɂ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnActive();
        }

        // �E�N���b�N�ɂ����
        if (Input.GetMouseButtonDown(1))
        {
            OnCancel();
        }
    }
}
