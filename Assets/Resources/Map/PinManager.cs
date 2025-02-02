using UnityEngine;

public class PinManager : MonoBehaviour {


    public static PinManager pm = null;

    [SerializeField]
    private GameObject Map;
    [SerializeField]
    private GameObject AbilityMap;


    private KeyCode mapKey;
    private MapAbility ma;



    private void Awake() {

        mapKey = KeyController.Instance.Settings.MapKey;
        Map.SetActive(false);
        AbilityMap.SetActive(false);
        pm = this;
    }

    void Update() {


        if (Input.GetKeyDown(mapKey))
        {

            Map.SetActive(!Map.activeSelf);
            AbilityMap.SetActive(false);
        }

            RayController.rc.MapOpening = Map.activeSelf;
        
        



    }

   

 

    public void BlueLight() {

        Map.SetActive(true);
        AbilityMap.SetActive(true);
        Map.GetComponent<MapPin>().BlueLight();


    }

    public void Aqua()
    {

        Map.SetActive(true);
        AbilityMap.SetActive(true);
        Map.GetComponent<MapPin>().Aqua();


    }


    public void Hide() {

        ma = Map.GetComponent<MapPin>().ma;
        Map.SetActive(false);
        AbilityMap.SetActive(false);
        Map.GetComponent<MapPin>().CanSmoke = false;
        Map.GetComponent<MapPin>().CanSetAqua = false;
        pm = GetComponentInParent<PinManager>();
        pm.Hide();
        ma.EndAbility();

    }
}
