using UnityEngine;
using UnityEngine.UI;

public class SoundDetectorController : MonoBehaviour
{



    private Transform player;       // �v���C���[��Transform
    private Transform opponent;  // ����(����)��Transform


    [SerializeField] private RectTransform compassCircle; // UI�̉~�`�̔w�i
    [SerializeField] private RectTransform opponentIcon;  // ����̈ʒu�������A�C�R��


    private void Start()
    {
    }

    void Update()
    {
        if (opponent == null)
        {
            GameObject players = EnemyTag.enemytag.gameObject;
            if (players != null)
            {
                opponent = players.transform;
            }
        }
        if (player == null)
        {
            GameObject p = MyTag.mytag.gameObject;
            if (p != null)
            {
                player = p.transform;
            }
        }
        if (opponent == null || player == null)
        {
            // ���Ղ͔�\��
            opponentIcon.gameObject.SetActive(false);
            return;
        }


        
        if (opponent.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            // ���Ղ͕\��
            opponentIcon.gameObject.SetActive(true);
            // ����ƃv���C���[�̈ʒu��������x�N�g�����v�Z
            Vector3 direction = opponent.position - player.position;
            direction.y = 0; // �����𖳎����Đ����ʂ̕����̂݌v�Z
            direction.Normalize();

            // �v���C���[�̑O������ɑ���̕������p�x�Ōv�Z
            float angle = Vector3.SignedAngle(player.forward, direction, Vector3.up);

            // �~����̍��W���v�Z
            UpdateOpponentIconPosition(angle);

        }
        else
        {
            // ���Ղ͔�\��
            opponentIcon.gameObject.SetActive(false);
            return;
        }
    }

    private void UpdateOpponentIconPosition(float angle)
    {
        // �~�̔��a���v�Z
        float radius = compassCircle.rect.width / 2;

        // �p�x����~����̈ʒu���v�Z
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Sin(radians) * radius;
        float y = Mathf.Cos(radians) * radius;

        // �A�C�R���̈ʒu���X�V
        opponentIcon.anchoredPosition = new Vector2(x, y);
    }
}
