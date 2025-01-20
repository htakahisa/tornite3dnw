using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class Arte : MonoBehaviourPun
{
    public float[] radii = { 3f, 4f, 5f }; // �����̔��a
    public int[] damages = { 30, 20, 10 }; // �����̃_���[�W
    public float explosionDelay = 0.5f; // �����Ԃ̊Ԋu

    private float throwForce = 8f; // �������

    Rigidbody rb;

    private SoundManager sm;

    private bool HasExplode = false;

    private HashSet<GameObject> damagedParents = new HashSet<GameObject>(); // �_���[�W��^�����e�I�u�W�F�N�g��ǐ�

    private bool showExplosionRange = false; // �����͈͂�\������t���O
    private int currentExplosionIndex = -1; // ���݂̔����C���f�b�N�X

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        if (photonView.IsMine)
        {
            // �O���l�[�h�ɗ͂�������
            Vector3 throwDirection = transform.forward * 2;
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (HasExplode)
        {
            return;
        }

        HasExplode = true;
        showExplosionRange = true; // �����͈͂̕\�����J�n
        Invoke("StartExplosionAttack", 1f);
    }

    // �����U�����J�n���郁�\�b�h
    public void StartExplosionAttack()
    {
        StartCoroutine(ExplosionSequence());
    }


    // ���������Ԃɏ�������R���[�`��
    private IEnumerator ExplosionSequence()
    {
        for (int i = 0; i < radii.Length; i++)
        {
            currentExplosionIndex = i; // ���݂̔����C���f�b�N�X��ݒ�
            PerformExplosion(radii[i], damages[i]);
            yield return new WaitForSeconds(explosionDelay);
        }
        showExplosionRange = false; // �����͈͂̕\�����I��
        Destroy(gameObject);
    }

    // �������������郁�\�b�h
    private void PerformExplosion(float radius, int damage)
    {
        DamageManager damagemanager = new DamageManager();

        // ���݂̔����̏����f�o�b�O���O�ɕ\��
        Debug.Log($"Explosion at radius {radius}m with {damage} damage.");

        // �����͈͓��̃R���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            GameObject parentObject = hitCollider.transform.root.gameObject; // �e�I�u�W�F�N�g���擾

            if ((hitCollider.gameObject.tag == "Body" || hitCollider.gameObject.tag == "Head") && !damagedParents.Contains(parentObject))
            {
                damagemanager.causeDamage(hitCollider.gameObject, damage);
                damagedParents.Add(parentObject); // �_���[�W��^�����e�I�u�W�F�N�g���L�^
                Debug.Log(damage);
            }
        }

        // �����G�t�F�N�g�Ȃǂ�ǉ��\
        // Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // �����͈͂������^�C�����ɕ\��
        if (showExplosionRange && currentExplosionIndex >= 0 && currentExplosionIndex < radii.Length)
        {
            DrawExplosionRange(radii[currentExplosionIndex]);
        }
    }

    private void DrawExplosionRange(float radius)
    {
        // �����͈͂����Ƃ��ĕ`��
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = new Color(1f, 0f, 0f, 0.3f); // �������̐ԐF

        // �X�P�[���𔽉f�����s����쐬
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * radius * 2);
        Graphics.DrawMesh(CreateSphereMesh(), matrix, mat, 0);
    }

    private Mesh CreateSphereMesh()
    {
        GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Mesh sphereMesh = tempSphere.GetComponent<MeshFilter>().sharedMesh;
        Destroy(tempSphere);
        return sphereMesh;
    }

    private void OnDrawGizmos()
    {
        // �V�[���r���[�Ŕ����͈̔͂����o�I�Ɋm�F
        Gizmos.color = Color.red;
        foreach (float radius in radii)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
