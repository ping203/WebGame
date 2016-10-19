using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMail : MonoBehaviour {
    [HideInInspector]
    public int id;
    [HideInInspector]
    public string content;
    [HideInInspector]
    public string guiTu, guiLuc;
    [HideInInspector]
    public sbyte isRead;
    public Image icon, check;
    public Text lb_content;
    public Button del;

    //public Sprite[] icon_mail;
    public void setRead() {
        //icon.sprite = icon_mail[1];
        LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_thu_mo");
    }
    public void setCheck(bool isCheck) {
        check.gameObject.SetActive(isCheck);
    }
    public void setIconItemTN(int id, string guiTu, string guiLuc, string noiDung, sbyte isRead) {
        this.id = id;
        content = noiDung;
        this.guiTu = guiTu;
        this.guiLuc = guiLuc;
        this.isRead = isRead;
        del.gameObject.SetActive(true);
        check.gameObject.SetActive(false);
        if (isRead == 0) {
            LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_thu_dong");
            //icon.sprite = icon_mail[0];
        } else {
            //icon.sprite = icon_mail[1];
            LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_thu_mo");
        }
        icon.SetNativeSize();
        if (noiDung.Length > 20) {
            lb_content.text = (noiDung.Substring(0, 20) + "...");
        } else {
            lb_content.text = (noiDung);
        }
    }

    public void setIconItemSK(int id, string tilte, string noiDung) {
        this.id = id;
        this.content = noiDung;
        del.gameObject.SetActive(false);
        check.gameObject.SetActive(false);
        if (isRead == 0) {
            LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_thu_dong");
            //icon.sprite = icon_mail[0];
        } else {
            //icon.sprite = icon_mail[1];
            LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_thu_mo");
        }
        if (content.Length > 20) {
            lb_content.text = (content.Substring(0, 20) + "...");
        } else {
            lb_content.text = (content);
        }
    }

  public  void delMess() {
        GameControl.instance.panelMessageSytem.onShow("Bạn muốn xóa tin nhắn này?", delegate {
            SendData.onDelMessage(id);
            Destroy(this);
        });
    }
}
