using UnityEngine;
using UnityEngine.UI;

public class SoundDetectorController : MonoBehaviour
{



    private Transform player;       // プレイヤーのTransform
    private Transform opponent;  // 音源(相手)のTransform


    [SerializeField] private RectTransform compassCircle; // UIの円形の背景
    [SerializeField] private RectTransform opponentIcon;  // 相手の位置を示すアイコン


    private void Start()
    {
    }

    void Update()
    {
        if (opponent == null)
        {
            GameObject players = EnemyTag.enemytag.gameObject;
            if (players != null)
            {
                opponent = players.transform;
            }
        }
        if (player == null)
        {
            GameObject p = MyTag.mytag.gameObject;
            if (p != null)
            {
                player = p.transform;
            }
        }
        if (opponent == null || player == null)
        {
            // 足跡は非表示
            opponentIcon.gameObject.SetActive(false);
            return;
        }


        
        if (opponent.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            // 足跡は表示
            opponentIcon.gameObject.SetActive(true);
            // 相手とプレイヤーの位置から方向ベクトルを計算
            Vector3 direction = opponent.position - player.position;
            direction.y = 0; // 高さを無視して水平面の方向のみ計算
            direction.Normalize();

            // プレイヤーの前方を基準に相手の方向を角度で計算
            float angle = Vector3.SignedAngle(player.forward, direction, Vector3.up);

            // 円周上の座標を計算
            UpdateOpponentIconPosition(angle);

        }
        else
        {
            // 足跡は非表示
            opponentIcon.gameObject.SetActive(false);
            return;
        }
    }

    private void UpdateOpponentIconPosition(float angle)
    {
        // 円の半径を計算
        float radius = compassCircle.rect.width / 2;

        // 角度から円周上の位置を計算
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Sin(radians) * radius;
        float y = Mathf.Cos(radians) * radius;

        // アイコンの位置を更新
        opponentIcon.anchoredPosition = new Vector2(x, y);
    }
}
