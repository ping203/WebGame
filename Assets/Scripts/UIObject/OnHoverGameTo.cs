using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverGameTo : MonoBehaviour {
    [SerializeField]
    GameObject obj_help;
    RectTransform rect1;
    // Use this for initialization
    void Start() {
        rect1 = GetComponent<Image>().rectTransform;

        EventTrigger.Entry eventtypeHover = new EventTrigger.Entry();
        eventtypeHover.eventID = EventTriggerType.PointerEnter;
        eventtypeHover.callback.AddListener((eventData) => { onHover(); });

        EventTrigger.Entry eventtypeExit = new EventTrigger.Entry();
        eventtypeExit.eventID = EventTriggerType.PointerExit;
        eventtypeExit.callback.AddListener((eventData) => { onExitHover(); });

        gameObject.AddComponent<EventTrigger>();
        gameObject.GetComponent<EventTrigger>().triggers.Add(eventtypeHover);
        gameObject.GetComponent<EventTrigger>().triggers.Add(eventtypeExit);

        Vector3 vt = obj_help.transform.position;
        vt.x = transform.position.x - 10;
        obj_help.transform.position = vt;
        transform.SetAsLastSibling();
    }
    void onHover() {
        //    if (RectTransformUtility.RectangleContainsScreenPoint(rect1, (Input.mousePosition - new Vector3(0, 10, 0)))){
        obj_help.SetActive(true);
        //} else {
        //    obj_help.SetActive(false);
        //}
    }
    void onExitHover() {
        StopCoroutine(waitHide());
        StartCoroutine(waitHide());
    }

    IEnumerator waitHide() {
        yield return new WaitForSeconds(0.2f);
        obj_help.SetActive(false);
    }
}
