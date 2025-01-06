using Photon.Pun;
using UnityEngine;

public class Stradarts : MonoBehaviourPun {
    public float speed = 150f;                // �_�[�g�̈ړ����x        
    public float scanRadius = 7f;            // �X�L�����͈�
    public LayerMask hitMask;                 // �q�b�g����p�̃��C���[�}�X�N

    private Vector3 launchDirection;          // ���˕���
    private bool isScanning = false;          // �X�L���������ǂ���

    void Start() {

        if (photonView.IsMine){
            // ���˕�����ݒ�
            launchDirection = transform.forward;
            // �X�L�������J�n
            StartCoroutine(Scan());
        }
    }

    void Update() {
        if (photonView.IsMine){
            // �_�[�g���ړ�������
            if (!isScanning) {
                float distanceToMove = speed * Time.deltaTime;
                RaycastHit hit;

                // ���C���΂��ĕǂɓ����邩�m�F
                if (Physics.Raycast(transform.position, launchDirection, out hit, distanceToMove)) {

                    // �ǂɓ��������ꍇ�A�X�L�������J�n
                    isScanning = true;
                    transform.position = hit.point; // �ǂ̈ʒu�Ŏ~�܂�
                } else {
                    // �ǂɓ�����Ȃ��ꍇ�͈ړ�����
                    transform.position += launchDirection * distanceToMove;
                }
            }
        }
    }

    private System.Collections.IEnumerator Scan() {
        // �X�L�������s��
        yield return new WaitForSeconds(2f); // �X�L�����O�̑ҋ@����

        // �X�L�����͈͓��̓G�����o
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets) {
            Debug.Log("�G�����o: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");
            // �G�ɑ΂��鏈���������ɒǉ�
            ScanCamera.sc.ActiveScan();


        }

        // �X�L������̏����i�I�u�W�F�N�g��j�󂷂�Ȃǁj
        
        Invoke("DestroyPun", 2f); // �_�[�g��j��
    }

    private void DestroyPun() {
        BattleData.bd.DetectEnd();
        ScanCamera.sc.InActiveScan();
        PhotonNetwork.Destroy(gameObject);
    }

    void OnDrawGizmos() {
        // �X�L�����͈͂�����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
