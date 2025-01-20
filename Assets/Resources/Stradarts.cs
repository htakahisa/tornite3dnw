using Photon.Pun;
using UnityEngine;

public class Stradarts : MonoBehaviourPun
{
    private float speed = 12f;                // �_�[�g�̈ړ����x        
    private float scanRadius = 8f;           // �X�L�����͈�
    public LayerMask hitMask;                // �q�b�g����p�̃��C���[�}�X�N

    private Vector3 launchDirection;         // ���˕���
    private bool isScanning = false;         // �X�L���������ǂ���
    private bool showSphere = false;         // ���̕\���t���O
    private Material sphereMaterial;         // ���̃}�e���A��
    private GameObject materialmanager;

    void Start()
    {
        if (photonView.IsMine)
        {
            if(materialmanager == null)
            {
                materialmanager = GameObject.Find("materialmanager");
            }
            // ���˕�����ݒ�
            launchDirection = transform.forward;

            // �}�e���A���̃Z�b�g�A�b�v (�J�X�^���_�u���T�C�h�V�F�[�_�[���g�p)
            sphereMaterial = new Material(materialmanager.GetComponent<MeshRenderer>().material);

            
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // �_�[�g���ړ�������
            if (!isScanning)
            {
                float distanceToMove = speed * Time.deltaTime;
                RaycastHit hit;

                // ���C���΂��ĕǂɓ����邩�m�F
                if (Physics.Raycast(transform.position, launchDirection, out hit, distanceToMove))
                {
                    // �ǂɓ��������ꍇ�A�X�L�������J�n
                    isScanning = true;
                    transform.position = hit.point; // �ǂ̈ʒu�Ŏ~�܂�
                    showSphere = true; // ����\��
                    // �X�L�������J�n
                    StartCoroutine(Scan());
                }
                else
                {
                    // �ǂɓ�����Ȃ��ꍇ�͈ړ�����
                    transform.position += launchDirection * distanceToMove;
                }
            }
        }
    }

    private System.Collections.IEnumerator Scan()
    {
        // �X�L�������s��
        yield return new WaitForSeconds(2f); // �X�L�����O�̑ҋ@����
                                             // ���̔�\���t���O���Z�b�g
        showSphere = false;
        // �X�L�����͈͓��̓G�����o
        Collider[] targets = Physics.OverlapSphere(transform.position, scanRadius, hitMask);
        foreach (Collider target in targets)
        {
            Debug.Log("�G�����o: " + target.name);
            BattleData.bd.Detect("enemy\ndetected");
            // �G�ɑ΂��鏈���������ɒǉ�
            ScanCamera.sc.ActiveScan();
        }

       

        // �X�L������̏����i�I�u�W�F�N�g��j�󂷂�Ȃǁj
        Invoke("DestroyPun", 2f); // �_�[�g��j��
    }

    private void DestroyPun()
    {
      
        BattleData.bd.DetectEnd();
        ScanCamera.sc.InActiveScan();
        PhotonNetwork.Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        // �f�o�b�O�p�̃X�L�����͈͂�����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }

    void OnRenderObject()
    {
        // �Q�[�����ɋ���`��
        if (showSphere && sphereMaterial != null)
        {
            Graphics.DrawMesh(
                MeshBuilder.CreateSphere(20, 20), // �����b�V���̍쐬
                Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * scanRadius), // ���m�ȃX�P�[��
                sphereMaterial,
                0
            );
        }
    }
}

// �w���p�[�N���X�F�����b�V�����쐬
public static class MeshBuilder
{
    public static Mesh CreateSphere(int longitude, int latitude)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(longitude + 1) * (latitude + 1)];
        int[] triangles = new int[longitude * latitude * 6];
        float pi = Mathf.PI;
        float twoPi = pi * 2;

        for (int lat = 0; lat <= latitude; lat++)
        {
            float a1 = pi * lat / latitude;
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= longitude; lon++)
            {
                float a2 = twoPi * lon / longitude;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                vertices[lat * (longitude + 1) + lon] = new Vector3(sin1 * cos2, cos1, sin1 * sin2);
            }
        }

        int index = 0;
        for (int lat = 0; lat < latitude; lat++)
        {
            for (int lon = 0; lon < longitude; lon++)
            {
                int current = lat * (longitude + 1) + lon;
                int next = current + longitude + 1;

                triangles[index++] = current;
                triangles[index++] = next;
                triangles[index++] = current + 1;

                triangles[index++] = current + 1;
                triangles[index++] = next;
                triangles[index++] = next + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
