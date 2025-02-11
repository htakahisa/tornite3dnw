using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaTrick : MonoBehaviourPunCallbacks
{
    private bool instantiatedScan; // �X�L���������̃t���O
    private GameObject instantiatedPrefab; // ���������v���t�@�u��ێ�����ϐ�

    public float scanRadius = 3f;           // �X�L�����͈�
    public LayerMask hitMask;               // �q�b�g����p�̃��C���[�}�X�N
    private Ability ability;

    // �X�L�����͈͂̎��o����
    private GameObject scanVisual;          // �X�L�����͈͂�`�悷��I�u�W�F�N�g
    [SerializeField] private Material scanMaterial; // �������̐Ԃ��}�e���A��

    void Start()
    {
    }

    void Update()
    {
     
    }

    private IEnumerator Scan()
    {
        // �X�L�������s��
        yield return new WaitForSeconds(1f); // �X�L�����O�̑ҋ@����

        // �X�L�����͈͓��̓G�����o
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets)
        {
            Debug.Log("�G�����o: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");

            if (target.GetComponentInParent<CameraController>() != null)
            {
                target.GetComponentInParent<CameraController>().Stuned(1.5f, 0.1f);
            }
        }

        // �X�L������̏����i�I�u�W�F�N�g��j�󂷂�Ȃǁj
        Invoke("End", 2f);
    }

    private void End()
    {
        BattleData.bd.DetectEnd();
        DestroyScanVisual(); // �X�L�����͈͂̎��o���ʂ��폜
        DestroyThis();
    }

    private void OnActive()
    {
        ability = Camera.main.transform.parent.GetComponent<Ability>();

        if (ability.number2 >= 1)
        {
            // ���������v���t�@�u�����݂��Ȃ��ꍇ�ɐ���
            if (instantiatedPrefab == null)
            {
                ability.Spend(2, 1);
                Debug.Log($"{gameObject.name} �����N���b�N����܂���");
                instantiatedPrefab = PhotonNetwork.Instantiate("AquaSmoke", transform.position, Quaternion.identity);
            }
            else
            {
                ability.Spend(2, 1);
                PhotonNetwork.Instantiate("AquaArmer", transform.position, Quaternion.identity);
                
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
            DestroySmoke();
            ability.Collect(2, 1);
        }
        else
        {
            instantiatedScan = true;
            CreateScanVisual(); // �X�L�����͈͂̎��o���ʂ��쐬
            StartCoroutine(Scan());
        }
    }

    public void DestroySmoke()
    {
        instantiatedPrefab.GetComponent<SmokeManager>().PhotonDestroy();
        Debug.Log("Prefab destroyed");
    }

    public void DestroyThis()
    {
        PhotonNetwork.Destroy(gameObject);
        Debug.Log("Prefab destroyed");
    }

    void OnMouseOver()
    {

        if (!photonView.IsMine)
        {
            return;
        }

        // ���N���b�N���ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnActive();
        }

        // �E�N���b�N���ꂽ�Ƃ�
        if (Input.GetMouseButtonDown(1))
        {
            OnCancel();
        }
    }

    // �X�L�����͈͂̎��o���ʂ��쐬
    private void CreateScanVisual()
    {
        if (scanVisual != null) return; // ���ɑ��݂���ꍇ�͍쐬���Ȃ�

        scanVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        scanVisual.transform.position = transform.position;
        scanVisual.transform.localScale = new Vector3(scanRadius * 2, scanRadius * 2, scanRadius * 2); // ���a�ɍ��킹�Ċg��
        scanVisual.GetComponent<Renderer>().material = scanMaterial; // �������̐Ԃ��}�e���A����ݒ�
        scanVisual.GetComponent<Collider>().enabled = false;         // �R���C�_�[�𖳌���
    }

    // �X�L�����͈͂̎��o���ʂ��폜
    private void DestroyScanVisual()
    {
        if (scanVisual != null)
        {
            Destroy(scanVisual);
            scanVisual = null;
        }
    }
}
