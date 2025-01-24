using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviourPun {
    private Transform target;  // �v���C���[�Ȃǂ̃^�[�Q�b�g
    private NavMeshAgent agent;
    public float attackDistance = 0.1f; // �U�����s�������i�����ł�0�ɋ߂������j
    private bool antirugcheck = false;
    private float stepTimer = 0f;

    private SoundManager sm;

    void Start() {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        if (enemy != null) {
            // NavMeshAgent�R���|�[�l���g�̎擾
            agent = GetComponent<NavMeshAgent>();
            target = enemy.transform;  // �^�[�Q�b�g��ݒ�
        }

        Invoke("Destroy", 10f);

    }

    void Update() {
        // �^�[�Q�b�g�̈ʒu�Ɍ������Ĉړ�
        if (target != null) {
            stepTimer += Time.deltaTime;
            // ������0.5�b���ɍĐ�
            if (stepTimer >= 0.5f) {
                sm.PlayMySound("wolf");
                stepTimer = 0f; // �^�C�}�[�����Z�b�g
            }
            agent.SetDestination(target.position);
            // �^�[�Q�b�g�Ƃ̋������v�Z
            float distance = Vector3.Distance(transform.position, target.position);

            // �������w�肵���U�������ȓ��ɂȂ����ꍇ
            if (distance <= attackDistance) {
                Damage(); // Damage���\�b�h���Ăяo��
            }
        }
    }

    private void Destroy() {
        PhotonNetwork.Destroy(gameObject);
    }


    private void Damage() {
        if (!antirugcheck) {
            // �w�肵�����O�̎q�I�u�W�F�N�g���擾        
            DamageCounter dc = target.GetComponentInChildren<DamageCounter>();
            GameObject body = dc.gameObject;
            if (dc != null) {
                dc.DamageCount(50, body);
                body.GetComponentInParent<CameraController>().Stuned(1f,1);
                Destroy();
                antirugcheck = true;
            }
        }
    }
}
