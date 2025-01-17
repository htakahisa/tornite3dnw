using UnityEngine;

public class PinManager : MonoBehaviour {


    public static PinManager pm = null;

    private GameObject Map;
    Transform map;



    private void Start() {

        map = gameObject.transform.GetChild(0); // 0は子オブジェクトのインデックス
        Map = map.gameObject;
        Map.SetActive(false);

    }

    private void Awake() {

        map = gameObject.transform.GetChild(0); // 0は子オブジェクトのインデックス
        Map = map.gameObject;
        Map.SetActive(false);
        pm = this;
    }

    void Update() {


        if (Input.GetKeyDown(KeyCode.Tab)) {
            Map.SetActive(!Map.activeSelf);
        }

       


       
    }

   

 

    public void BlueLight() {

        Map.SetActive(true);
        Map.GetComponent<MapPin>().BlueLight();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void Aqua()
    {

        Map.SetActive(true);
        Map.GetComponent<MapPin>().Aqua();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }


    public void Hide() {

        Map.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
