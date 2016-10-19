using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class MauBinhPlayer : ABSUser {

    public override void setExit() {
        base.setExit();
        cardMauBinh[0].removeAllCard();
        cardMauBinh[1].removeAllCard();
        cardMauBinh[2].removeAllCard();
        setLung(false);
    }
    public override void resetData() {
        base.resetData();
        /*if (pos == 0)
        {
            Vector3 posi = this.transform.localPosition;
            posi.x = 0;
            this.transform.localPosition = posi;
        }*/
        //if (sp_thang != null)
        //{
        //    sp_thang.gameObject.SetActive(false);
        //}

        cardMauBinh[0].removeAllCard();
        cardMauBinh[1].removeAllCard();
        cardMauBinh[2].removeAllCard();

        setLung(false);
    }

    public override void setLoaiBai(int type) {
        base.setLoaiBai(type);
        if (type == -1) {
            //sp_thang.StopAllCoroutines();
            //sp_thang.gameObject.SetActive(true);

            //sp_thang.spriteName = Res.TypeCard_Name[type];
            //sp_thang.MakePixelPerfect();
            //sp_thang.gameObject.transform.localPosition = new Vector3(0, -50, 0);
            //sp_thang.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        if (type < 0 || type > 8) {
            return;
        }


        if (pos != 0) {
            sp_typeCard.StopAllCoroutines();
            sp_typeCard.gameObject.SetActive(true);

            LoadAssetBundle.LoadSprite(sp_typeCard, Res.AS_UI_TYPE_CARD, Res.type_card[type], () => {
                //sp_typeCard.sprite = GameControl.instance.list_typecards[type];
                sp_typeCard.SetNativeSize();
                sp_typeCard.gameObject.transform.localPosition = new Vector3(0, -50, 0);
                sp_typeCard.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                sp_typeCard.transform.DOLocalMoveY(-20, 0.5f);
                StartCoroutine(setVisible(sp_typeCard.gameObject, 2.5f));
            });
        } else {
            if (((MauBinh)casinoStage).listTypeCard != null)
                ((MauBinh)casinoStage).listTypeCard.setTg(type);
        }
    }

    public override void setThangTrang(int type) {
        if (type < 0 || type > 6) {
            return;
        }

        StartCoroutine(delayThangTrang(type));
    }

    IEnumerator delayThangTrang(int type) {
        yield return new WaitForSeconds(2f);
        GameControl.instance.sound.startMaubinhAudio();
        sp_thang.sprite = casinoStage.animationMauBinh[type];
        sp_thang.SetNativeSize();
        //sp_thang.spriteName = animationMB[type];
        //sp_thang.MakePixelPerfect ();
        sp_thang.gameObject.SetActive(true);
        Invoke("setVisibleThang", 2f);
    }

    new void setVisibleThang() {
        sp_thang.gameObject.SetActive(false);
        sp_xoay.gameObject.SetActive(false);
    }
}

