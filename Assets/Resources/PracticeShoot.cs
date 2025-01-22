using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeShoot : MonoBehaviour
{
    public LayerMask hitMask;
    public int Point = 0;
    static float NormalSensityvity = 1f;
    float Xsensitivity = 1.5f, Ysensitivity = 1.5f;

    [SerializeField]
    private Camera camera;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -90f, maxX = 90f;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // ���C���[�̃}�X�N��ݒ� (��: "PracticeTarget" ���C���[���w��)
        this.layerMask = LayerMask.GetMask("PracticeTarget");
        
    }

    // Update is called once per frame
    void Update() {

       
            float mouseX = Input.GetAxis("Mouse X") * Xsensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * Ysensitivity;
            Vector3 currentRotation = transform.localEulerAngles;



       

            currentRotation.x -= mouseY;
            currentRotation.y += mouseX;




            gameObject.transform.localEulerAngles = currentRotation;

        if (Input.GetMouseButtonDown(1))
        {
            camera.fieldOfView = 50;
        } if(Input.GetMouseButtonUp(1)){
            camera.fieldOfView = 80;
        }



        if (Input.GetKeyDown(KeyCode.Mouse0)) {
         
            
            // Ray���΂��N�_�ƕ������w��
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Ray������̃��C���[�̃I�u�W�F�N�g�ɓ����������ǂ����𔻒�
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                // ���������I�u�W�F�N�g�̃^�O������̂��� (��: "PracticeTarget")
                if (hit.collider.CompareTag("PracticeTarget")) {
                    Point++;
                    Next();
                }
            }

              
            
        }
    }



    private void Next() {
        TargetManager.tm.ActivateRandomChildObject();
    }
}
