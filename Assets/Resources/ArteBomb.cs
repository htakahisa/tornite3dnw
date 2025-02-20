using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArteBomb : MonoBehaviourPun
{

    private Material sphereMaterial;
    private GameObject materialmanager;

    private HashSet<GameObject> damagedParents = new HashSet<GameObject>(); // �_���[�W��^�����e�I�u�W�F�N�g��ǐ�

    public float radii = 1f; // �����̔��a
    public int damages = 10; // �����̃_���[�W
    private bool drawingradii = false;

    private bool HasExplode = false;



    // Start is called before the first frame update
    void Awake()
    {
     

        if (materialmanager == null)
        {
            materialmanager = MaterialManager.mm.gameObject;
        }
        // �}�e���A���̃Z�b�g�A�b�v (�J�X�^���_�u���T�C�h�V�F�[�_�[���g�p)
        sphereMaterial = new Material(materialmanager.GetComponent<MeshRenderer>().materials[1]);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (HasExplode)
        {
            return;
        }
        if (collision.gameObject.tag == "Arte")
        {
            return;
        }
        if (HasExplode)
        {
            return;
        }

        HasExplode = true;
        StartCoroutine(PerformExplosion(radii, damages));
    }


    // �������������郁�\�b�h
    private IEnumerator PerformExplosion(float radius, int damage)
    {

        yield return new WaitForSeconds(0.5f);
        drawingradii = true;
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
        
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (drawingradii)
        {
            DrawExplosionRange(radii);
        }

    }

        private void DrawExplosionRange(float radius)
    {


        // �X�P�[���𔽉f�����s����쐬
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * radius * 2);
        Graphics.DrawMesh(CreateSphereMesh(), matrix, sphereMaterial, 0);
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
      
            Gizmos.DrawWireSphere(transform.position, radii);
        
    }
}
