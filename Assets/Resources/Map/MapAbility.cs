using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAbility : MonoBehaviour
{

    [SerializeField]
    private LayerMask mapobject;

    [SerializeField]
    private GameObject mappointer;

    public float speed = 10f; // �I�u�W�F�N�g�̈ړ����x


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // �}�E�X�̈ړ��ʂɉ����Ĉړ�
        Vector3 movement = new Vector3(mouseX, 0, mouseY);

        // �I�u�W�F�N�g�̈ʒu���X�V�iZ�����Œ�j
        transform.position += movement * speed * Time.deltaTime;

    }

    public Vector3 Position()
    {

        Vector3 down = new Vector3(transform.position.x, 15, transform.position.z);

        RaycastHit hit;
        Physics.Raycast(down, transform.forward, out hit, mapobject);
        mappointer.SetActive(true);
        mappointer.transform.position = hit.point += new Vector3 (0,1,0);
        return hit.point;
    }

    private void OnDrawGizmos()
    {

        Vector3 down = new Vector3(transform.position.x, 15, transform.position.z);

        Gizmos.color = Color.red;

        Gizmos.DrawRay(down, transform.forward * 10);

    }

    public void EndAbility()
    {
        mappointer.SetActive(false);
    }

}
