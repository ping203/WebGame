using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelMoiChoi : PanelGame {
    public GameObject tblContaint;
    public GameObject btnIcon;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void addIcon (string name, string displayname, long money) {
        GameObject btnT = Instantiate (btnIcon) as GameObject;
        btnT.transform.SetParent(tblContaint.transform);
        btnT.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        btnT.GetComponent<ItemInvite>().full_name = name;
        if(name.Length > 17) {
            name = name.Substring (0, 14) + "...";
        }
        
        btnT.GetComponent<ItemInvite>().setText(name, money);
        btnT.GetComponent<Button>().onClick.AddListener(delegate {
            ClickMoi(btnT);
        });
    }

    public void ClearParent () {
        foreach(Transform t in tblContaint.transform) {
            Destroy (t.gameObject);
        }
    }

    public void ClickMoi (GameObject obj) {
        SendData.onInviteFriend (obj.GetComponent<ItemInvite>().full_name);
        Destroy (obj);
        if(tblContaint.transform.childCount == 1) {
            onHide ();
        }
    }
}
