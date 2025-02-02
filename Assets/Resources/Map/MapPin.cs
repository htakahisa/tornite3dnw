using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapPin : MonoBehaviourPun {
    public Camera minimapCamera;
    private Transform player;

    public bool CanSmoke = false;
    public bool CanSetAqua = false;
    private PinManager pm;
    private Ability ability;
    private SoundManager sm;



    [SerializeField]
    public MapAbility ma;


    private void Update(){
      
            sm = Camera.main.transform.parent.GetComponent<SoundManager>();
            player = Camera.main.transform.parent;

            if (CanSmoke)
            {
                ability = Camera.main.transform.parent.GetComponent<Ability>();



                Vector3 position = ma.Position();
            if (Input.GetMouseButtonDown(0) && ability.number2 >= 1)
            {
                PhotonNetwork.Instantiate("BlueLightSmoke", position, Quaternion.identity);
              
                ability.Spend(2, 1);
            }


            }
            if (CanSetAqua)
            {
                ability = Camera.main.transform.parent.GetComponent<Ability>();





                Vector3 position = ma.Position();
            if (Input.GetMouseButtonDown(0) && ability.number2 >= 1)
            {
                PhotonNetwork.Instantiate("Aquaring", position, Quaternion.identity);

              
               
                ability.Spend(2, 1);
            }
            }

    }






    public void BlueLight() {

        CanSmoke = true;
        ma.transform.position = new Vector3(0, 80000, 0);

    }

    public void Aqua()
    {

        CanSetAqua = true;
        ma.transform.position = new Vector3(0, 80000, 0);

    }

}

