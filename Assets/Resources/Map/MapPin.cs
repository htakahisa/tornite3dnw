using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapPin : MonoBehaviourPun, IPointerClickHandler {
    public Camera minimapCamera;
    private Transform player;
    private bool CanWarp = false;
    private bool CanSmoke = false;
    private bool CanSetAqua = false;
    private Rigidbody rb;
    private PinManager pm;
    private Ability ability;
    private SoundManager sm;
    public void OnPointerClick(PointerEventData eventData) {
        sm = Camera.main.transform.parent.GetComponent<SoundManager>();
        player = Camera.main.transform.parent;
        if (CanWarp) {
            sm.PlaySound("stray");
            Vector3 screenPosition = Input.mousePosition;
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out localCursor);


            rb = player.GetComponent<Rigidbody>();
            Debug.Log(screenPosition);
            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 10, (screenPosition.y - 544.275f) / 32f);
            rb.MovePosition(to);
            Debug.Log(screenPosition);
            CanWarp = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
        }
        if (CanSmoke) {
            Vector3 screenPosition = Input.mousePosition;
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out localCursor);


            rb = player.GetComponent<Rigidbody>();

            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 0, (screenPosition.y - 544.275f) / 32f);
            PhotonNetwork.Instantiate("BlueLightSmoke", to, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSmoke = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
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


            rb = player.GetComponent<Rigidbody>();
            Vector3 to = new Vector3((screenPosition.x - 917.5f) / 30f, 0, (screenPosition.y - 544.275f) / 32f);
            PhotonNetwork.Instantiate("Aquaring", to, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSetAqua = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
            ability.Spend(2, 1);
        }
    }



    public void Stray() {

        CanWarp = true;

    }

    public void BlueLight() {

        CanSmoke = true;


    }

    public void Aqua()
    {

        CanSetAqua = true;


    }

}

