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
        Invoke("GetCamera", 3f);
    }

    void Update() {


        if (Input.GetKeyDown(mapKey))
        {

            Map.SetActive(!Map.activeSelf);
            AbilityMap.SetActive(false);
            Map.GetComponent<MapPin>().CanSmoke = false;
            Map.GetComponent<MapPin>().CanSetAqua = false;
            Map.GetComponent<MapPin>().CanCat = false;
            ma.EndAbility();
        }

        RayController.rc.MapOpening = Map.activeSelf;

       
        
        


    }

    private void GetCamera()
    {
        Debug.Log(Camera.main.GetComponentInParent<Ability>());
        Camera.main.GetComponentInParent<Ability>().CanAbility = !Map.activeSelf;
       
    }



    public void BlueLight() {

        Map.SetActive(true);
        AbilityMap.SetActive(true);
        Map.GetComponent<MapPin>().BlueLight();


    }

    public void Aqua()
    {

        
        Map.SetActive(!Map.activeSelf);
        AbilityMap.SetActive(!AbilityMap.activeSelf);
        Map.GetComponent<MapPin>().Aqua();


    }
    public void Cat()
    {

        Map.SetActive(true);
        AbilityMap.SetActive(true);
        Map.GetComponent<MapPin>().Cat();


    }

    public void Hide() {

        ma = Map.GetComponent<MapPin>().ma;
        Map.SetActive(false);
        AbilityMap.SetActive(false);
        Map.GetComponent<MapPin>().CanSmoke = false;
        Map.GetComponent<MapPin>().CanSetAqua = false;
        Map.GetComponent<MapPin>().CanCat = false;
        pm = GetComponentInParent<PinManager>();
        ma.EndAbility();

    }
}
