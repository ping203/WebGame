using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelMoiChoi : PanelGame {
    public Transform tblContaint;
    List<ItemInvite> list = new List<ItemInvite>();
    // Use this for initialization
    void Start() {
        list = GameControl.instance.list_invite;
        addIcon();
    }

    //public void addIcon(string name, string displayname, long money) {
    public void addIcon() {
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_INVITE_GAME, (objPre) => {
            GameObject obj = objPre;
            obj.transform.SetParent(tblContaint.transform);
            obj.transform.localScale = Vector3.one;

            obj.GetComponent<ItemInvite>().setText(list[0].name, list[0].money);
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                ClickMoi(obj);
            });

            for (int i = 1; i < list.Count; i++) {
                GameObject btnT = obj;
                btnT.transform.SetParent(tblContaint);
                btnT.transform.localScale = Vector3.one;
                btnT.GetComponent<ItemInvite>().setText(list[i].name, list[i].money);
                btnT.GetComponent<Button>().onClick.AddListener(delegate {
                    ClickMoi(btnT);
                });
            }
        });
    }

    public void ClickMoi(GameObject obj) {
        SendData.onInviteFriend(obj.GetComponent<ItemInvite>().full_name);
        Destroy(obj);
        if (tblContaint.transform.childCount == 1) {
            onHide();
        }
    }
}
