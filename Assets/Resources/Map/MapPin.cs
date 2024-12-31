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

    public void OnPointerClick(PointerEventData eventData) {
        player = Camera.main.transform.parent;
        if (CanWarp) {
            SoundManager.sm.PlaySound("stray");
            Vector3 screenPosition = Input.mousePosition;
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out localCursor);


            rb = player.GetComponent<Rigidbody>();

            Vector3 to = new Vector3((screenPosition.x - 920) / 30, 10, (screenPosition.y - 544) / 29);
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

            Vector3 to = new Vector3((screenPosition.x - 920) / 30, 0.5f, (screenPosition.y - 544) / 29);
            PhotonNetwork.Instantiate("BlueLightSmoke", to, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSmoke = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
        }
        if (CanSetAqua)
        {
            Vector3 screenPosition = Input.mousePosition;
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out localCursor);


            rb = player.GetComponent<Rigidbody>();
            Vector3 to = new Vector3((screenPosition.x - 920) / 30, 0.5f, (screenPosition.y - 544) / 29);
            PhotonNetwork.Instantiate("Aquaring", to, Quaternion.identity);
            Debug.Log(screenPosition);
            CanSetAqua = false;
            pm = GetComponentInParent<PinManager>();
            pm.Hide();
            Ability.ability.Spend(2);
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

