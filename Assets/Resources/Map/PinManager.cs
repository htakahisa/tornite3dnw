using UnityEngine;

public class PinManager : MonoBehaviour {
    public GameObject pinPrefab;

    public static PinManager pm = null;

    private GameObject Map;
    Transform map;

    private void Start() {

        map = gameObject.transform.GetChild(0); // 0�͎q�I�u�W�F�N�g�̃C���f�b�N�X
        Map = map.gameObject;
        Map.SetActive(false);

    }

    private void Awake() {

        map = gameObject.transform.GetChild(0); // 0�͎q�I�u�W�F�N�g�̃C���f�b�N�X
        Map = map.gameObject;
        Map.SetActive(false);
        pm = this;
    }

    void Update() {


        if (Input.GetKeyDown(KeyCode.Tab)) {
            Map.SetActive(!Map.activeSelf);
        }

       


        if (Input.GetMouseButtonDown(2)) // �}�E�X��������
        {
            PlacePinAtClickPosition();
        }
    }

    void PlacePinAtClickPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Instantiate(pinPrefab, hit.point, Quaternion.identity);
        }
    }

    public void StrayChild() {

        Map.SetActive(true);
        Map.GetComponent<MapPin>().Stray();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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
