using UnityEngine;
using System.Collections;
using DG.Tweening;

public class XitoPlayer : ABSUser {
    // Use this for initialization
    void Start() {
        for (int i = 0; i < cardHand.getSizeArrayCard(); i++) {
            XitoInput xitoIP = new XitoInput((Xito)casinoStage, cardHand,
                           cardHand.getCardbyPos(i));
            cardHand.getCardbyPos(i).setListenerClick(delegate {
                xitoIP.click();
            });
        }
    }

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
        cardHand.StopAllCoroutines();
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

        //sp_typeCard.sprite = GameControl.instance.list_typecards[type];
        LoadAssetBundle.LoadSprite(sp_typeCard, Res.AS_UI_TYPE_CARD, Res.type_card[type], () => {
            sp_typeCard.SetNativeSize();

            sp_typeCard.gameObject.transform.localPosition = new Vector3(0, -50, 0);
            sp_typeCard.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            sp_typeCard.transform.DOLocalMoveY(-20, 0.5f);
            //TweenPosition.Begin (sp_typeCard.gameObject, 0.5f, new Vector3 (0, -20, 0));
            StartCoroutine(setVisible(sp_typeCard.gameObject, 2.5f));
        });
        //} else {
        if (pos == 0) {
            if (((Xito)casinoStage).listTypeCard != null)
                ((Xito)casinoStage).listTypeCard.setTg(type);
        }
    }
}
