using UnityEngine;
using System.Collections;

public class PanelWaiting : PanelGame {
    float timeShow;

    // Update is called once per frame
    void Update() {
        if (isShow) {
            timeShow = timeShow + Time.deltaTime;
            if (timeShow >= 10) {
                onHide();
                timeShow = 0;
            }
        }
    }

    public override void onShow() {
        isShow = true;
        gameObject.SetActive(true);
        timeShow = 0;
    }
    public override void onHide() {
        isShow = false;
        gameObject.SetActive(false);
    }
}
