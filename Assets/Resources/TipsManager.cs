using UnityEngine;

public class TipsManager : MonoBehaviour
{
    // �e�I�u�W�F�N�g�̎q�I�u�W�F�N�g���i�[����z��
    private GameObject[] children;
    private GameObject currentChild;

    void Start()
    {
        // �e�I�u�W�F�N�g�̎q�I�u�W�F�N�g�����ׂĎ擾
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }

        // �ŏ��Ƀ����_���Ȏq�I�u�W�F�N�g��\��
        ShowRandomChild();
    }

    void Update()
    {
        // ���N���b�N�Ń����_���Ȏq�I�u�W�F�N�g�ɐ؂�ւ�
        if (Input.GetMouseButtonDown(0))  // 0�͍��N���b�N
        {
            ShowRandomChild();
        }
    }

    // �����_���Ȏq�I�u�W�F�N�g��\��
    void ShowRandomChild()
    {
        // ���ׂĂ̎q�I�u�W�F�N�g���\���ɂ���
        foreach (var child in children)
        {
            child.SetActive(false);
        }

        // �����_���ɃC���f�b�N�X��I��
        int randomIndex = Random.Range(0, children.Length);

        // �����_���Ȏq�I�u�W�F�N�g��\��
        currentChild = children[randomIndex];
        currentChild.SetActive(true);
    }
}
