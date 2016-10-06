using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PokerPlayer : ABSUser {
    public override void setMaster(bool isMaster) {
        base.setMaster(false);
    }
    public override void setRank(int rank) {
        sp_typeCard.StopAllCoroutines();
        sp_typeCard.gameObject.transform.position = new Vector3(0, -25, 0);

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
    }
    public override void addToCardHand(int card, bool p) {
        base.addToCardHand(card, p);
        addToCard(cardHand, card, true, p, false);
    }
    public override void setLoaiBai(int type) {
        base.setLoaiBai(type);
        if (type < 0 || type > 8) {
            return;
        }
        //if (pos != 0) {
        sp_typeCard.StopAllCoroutines();
        sp_typeCard.gameObject.SetActive(true);
        sp_typeCard.sprite = GameControl.instance.list_typecards[type];
        sp_typeCard.SetNativeSize();

        sp_typeCard.gameObject.transform.localPosition = new Vector3(0, -50, 0);
        sp_typeCard.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        sp_typeCard.transform.DOLocalMoveY(-20, 0.5f);
        StartCoroutine(setVisible(sp_typeCard.gameObject, 2.5f));
        //} else {
        if (pos == 0) {
            if (((Poker)casinoStage).listTypeCard != null)
                ((Poker)casinoStage).listTypeCard.setTg(type);
        }
    }
}
