using UnityEngine;
using System.Collections;

public class PanelGame : MonoBehaviour {
    public GameObject group;
    public bool isShow = false;

    public virtual void onShow() {
        show();
    }
    void show() {
        isShow = true;
        this.gameObject.SetActive(true);
    }
    public virtual void onHide() {
        hide();
        //GameControl.instance.sound.startClickButtonAudio();
    }
    void hide() {
        Invoke("disApear", 0f);
    }
    void disApear() {
        isShow = false;
        this.gameObject.SetActive(false);
    }
}
