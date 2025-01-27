using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAbility : MonoBehaviour
{

    [SerializeField]
    private LayerMask mapobject;

    [SerializeField]
    private GameObject mappointer;

    public float speed = 10f; // オブジェクトの移動速度


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // マウスの移動量に応じて移動
        Vector3 movement = new Vector3(mouseX, 0, mouseY);

        // オブジェクトの位置を更新（Z軸を固定）
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
