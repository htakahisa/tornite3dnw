using Photon.Pun;
using UnityEngine;

// IPunObservableインターフェースを実装して、PhotonViewの監視対象コンポーネントにする
public class SampleTransformView : MonoBehaviourPunCallbacks, IPunObservable {
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // Transformの値をストリームに書き込んで送信する
            stream.SendNext(transform.localPosition);
            stream.SendNext(transform.localRotation);
            stream.SendNext(transform.localScale);
        } else {
            // 受信したストリームを読み込んでTransformの値を更新する
            transform.localPosition = (Vector3)stream.ReceiveNext();
            transform.localRotation = (Quaternion)stream.ReceiveNext();
            transform.localScale = (Vector3)stream.ReceiveNext();
        }
    }
}