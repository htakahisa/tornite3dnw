using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapPin : MonoBehaviourPun, IPointerClickHandler {
    public Camera minimapCamera;
    private Transform player;

    private bool CanSmoke = false;
    private bool CanSetAqua = false;
    private PinManager pm;
    private Ability ability;
    private SoundManager sm;
    public void OnPointerClick(PointerEventData eventData) {
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        player = Camera.main.transform.parent;
  
        if (CanSmoke) {
            ability = Camera.main.transform.parent.GetComponent<Ability>();
            Vector3 screenPosition = Input.mousePosition;
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out localCursor);


            

            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 10f, (screenPosition.y - 544.275f) / 32f);
            PhotonNetwork.Instantiate("BlueLightSmoke", to, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSmoke = false;
            pm = GetComponentInParent<PinManager>();            
            pm.Hide();
            ability.Spend(2, 1);
        }
        if (CanSetAqua)
        {
            ability = Camera.main.transform.parent.GetComponent<Ability>();
            Vector3 screenPosition = Input.mousePosition;
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out localCursor);


            
            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 10f, (screenPosition.y - 544.275f) / 32f);
            PhotonNetwork.Instantiate("Aquaring", to, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSetAqua = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
            ability.Spend(2, 1);
        }
    }



 

    public void BlueLight() {

        CanSmoke = true;


    }

    public void Aqua()
    {

        CanSetAqua = true;


    }

}

