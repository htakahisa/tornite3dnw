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

    [SerializeField]
    private LayerMask mapobject;


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


            

            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 12f, (screenPosition.y - 544.275f) / 32f);
            Vector3 position = Position(to);
            PhotonNetwork.Instantiate("BlueLightSmoke", position, Quaternion.identity);
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




            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 12f, (screenPosition.y - 544.275f) / 32f);
            Vector3 position = Position(to);
            PhotonNetwork.Instantiate("Aquaring", position, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSetAqua = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
            ability.Spend(2, 1);
        }
    }


    private Vector3 Position(Vector3 to)
    {
        RaycastHit hit;
        Physics.Raycast(to, Vector3.down, out hit, mapobject);
        return hit.point;
    }


    public void BlueLight() {

        CanSmoke = true;


    }

    public void Aqua()
    {

        CanSetAqua = true;


    }

}

