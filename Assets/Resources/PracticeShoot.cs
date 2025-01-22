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

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // レイヤーのマスクを設定 (例: "PracticeTarget" レイヤーを指定)
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
         
            
            // Rayを飛ばす起点と方向を指定
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Rayが特定のレイヤーのオブジェクトに当たったかどうかを判定
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                // 当たったオブジェクトのタグが特定のもの (例: "PracticeTarget")
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
