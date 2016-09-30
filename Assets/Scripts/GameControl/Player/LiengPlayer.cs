using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class LiengPlayer : ABSUser {
    public Text txt_diem;
    // Use this for initialization
    void Start() {
        txt_diem.gameObject.SetActive(false);
        for (int i = 0; i < cardHand.getSizeArrayCard(); i++) {
            LiengInput liengIP = new LiengInput(cardHand,
                           cardHand.getCardbyPos(i), this);

            cardHand.getCardbyPos(i).setListenerClick(delegate {
                liengIP.click();
            });
        }
    }

    // Update is called once per frame
    public override void setInfo(string diem) {
        if (diem.Length > 0) {
            lb_name_sansang.text = diem;
        }
    }
    
    public void setDiem(string diem) {
        if (txt_diem != null) {
            txt_diem.text = diem;
            txt_diem.gameObject.SetActive(true);
            txt_diem.transform.DOKill();
            Tween tw = txt_diem.transform.DOScale(0.8f, 0.4f);
            if (!getName().Equals(BaseInfo.gI().mainInfo.nick)) {
                tw.SetLoops(10);
                tw.OnComplete(delegate { setVisibleDiemPlayer(); });
            } else {
                tw.SetLoops(-1);
            }
        }
    }
    public void setVisibleDiemPlayer() {
        txt_diem.gameObject.SetActive(false);
    }

    public new void resetData() {
        base.resetData();
        setVisibleDiemPlayer();
    }

    public void setRank(int rank, long money) {
        sp_typeCard.StopAllCoroutines();
        sp_typeCard.gameObject.transform.position = new Vector3(0, -25, 0);
        if (rank == 1 || rank == 5 || money > 0) {
            chipBay.onMoveto(money, 2);
        }
        switch (rank) {
            case 0:
                if (pos == 0) {
                    GameControl.instance.sound.startLostAudio();
                }
                break;
            case 1:
                sp_xoay.gameObject.SetActive(true);
                if (pos == 0) {
                    GameControl.instance.sound.startWinAudio();
                }
                break;
            case 2:
            case 3:
            case 4:
                if (pos == 0) {
                    GameControl.instance.sound.startLostAudio();
                }
                break;
            case 5:
                sp_xoay.gameObject.SetActive(true);
                if (pos == 0) {
                    GameControl.instance.sound.startWinAudio();
                }
                break;
            default:
                break;

        }
        Invoke("setVisibleXoay", 8f);
    }
}
