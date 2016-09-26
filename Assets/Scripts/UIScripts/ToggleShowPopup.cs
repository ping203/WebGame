using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleShowPopup : MonoBehaviour {
    Toggle tg;

    public NPCController OBJ;
    public bool isStart;

    RectTransform rect1;//, rect_girl;// = new Rect(Screen.width - 75, 0, 75, Screen.height);
    RectTransform rect_player;
    public Align _align = Align.None;
    //public bool isPlayer = true;
    ABSUser user;
    // Use this for initialization
    void Awake() {
        tg = GetComponent<Toggle>();
        if (tg != null) {
            tg.onValueChanged.AddListener(change);
            //rect_girl = tg.GetComponent<Image>().rectTransform;
        }
        if (OBJ != null) {
            OBJ.gameObject.SetActive(isStart);
            rect1 = OBJ.GetComponent<Image>().rectTransform;
            rect_player = OBJ.gourp_player.GetComponent<Image>().rectTransform;
        }
        user = GetComponent<ABSUser>();
    }
    void Update() {
        if (Input.GetButtonDown("Fire1") && rect1 != null /*&& rect_girl != null*/) {
            if (RectTransformUtility.RectangleContainsScreenPoint(rect1, Input.mousePosition)
                || (rect_player != null && RectTransformUtility.RectangleContainsScreenPoint(rect_player, Input.mousePosition))) {
                return;
            } else {
                // if (!RectTransformUtility.RectangleContainsScreenPoint(rect_girl, Input.mousePosition)) {
                tg.isOn = false;
                // }
            }
        }
    }

    void change(bool isCheck) {
        //OBJ.transform.localPosition = transform.localPosition;
        // OBJ.isPlayer = isPlayer;
        Vector3 vt = transform.localPosition;
        switch (_align) {
            case Align.None:
                break;
            case Align.Left:
                vt.x = transform.localPosition.x /*- rect_girl.rect.width / 2*/ - rect1.rect.width / 2 - 10;
                break;
            case Align.Right:
                vt.x = transform.localPosition.x /*+ rect_girl.rect.width / 2*/ + rect1.rect.width / 2 + 10;
                break;
            case Align.Top:
                vt.y = transform.localPosition.y /*+ rect_girl.rect.height / 2*/ + rect1.rect.height / 2 + 10;
                break;
            case Align.Bot:
                vt.y = transform.localPosition.y /*- rect_girl.rect.height / 2*/ - rect1.rect.height / 2 - 10;
                break;
        }
        if (OBJ != null) {
            OBJ.transform.localPosition = vt;
            OBJ.gameObject.SetActive(isCheck);
            if (user != null)
                OBJ.name_player = user.getName();
            OBJ.tg = this.tg;
        }
    }
}
