using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviourPun
{
    private Transform target;
    private NavMeshAgent agent;
    private float stepTimer = 1.1f;

    public float attackDistance = 0.3f; // �U�����s�������i�����ł�0�ɋ߂������j
    private bool antirugcheck = false;
    //private CatEffect catEffect;

    private float flashDuration = 2.5f; // �t���b�V���̎�������

    private SoundManager sm;
    private int randomValue = 0;

    private bool HasDetected = false;

    private Vector3 targetpos;

    private GameObject enemy;

    void Start()
    {
        enemy = EnemyTag.enemytag.gameObject;
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();

        
        if (enemy != null)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(targetpos);
        }

        Invoke("Destroy", 20f);
    }

    void Update()
    {
        
        // �������Đ�
        if (stepTimer >= 1.0f + randomValue * 2.5f)
        {
            randomValue = Random.Range(1, 5);
            sm.PlayMySound("cat" + randomValue);
            stepTimer = 0f;

            agent.speed = randomValue * 2;
        }

        if (agent != null)
        {
            stepTimer += Time.deltaTime;

            target = enemy.transform;

            // �^�[�Q�b�g�Ƃ̋������v�Z
            float distance = Vector3.Distance(transform.position, target.position);
            Debug.Log(distance);
            if (distance <= 3)
            {
                HasDetected = true;
            }

            if (HasDetected)
            {
                agent.SetDestination(target.position);
            }

            // �U�������ȓ��̏ꍇ
            if (distance <= attackDistance)
            {
                Hug();
            }
        }

    }

    public void SetTarget(Vector3 position)
    {
        targetpos = position;
    }

    private void Hug()
    {

        if (antirugcheck)
        {
            return;
        }

        antirugcheck = true;

        CatEffect targetCatEffect = target.GetComponentInChildren<CatEffect>();

        if (targetCatEffect != null)
        {
            // ����� CatEffect �ɑ΂��ăG�t�F�N�g��L����
            targetCatEffect.ApplyFlash(flashDuration);
            Destroy();
        }
    }


    private void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        PhotonNetwork.Instantiate("CatCollect", transform.position, Quaternion.identity);
    }

}
