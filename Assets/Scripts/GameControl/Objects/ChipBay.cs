using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ChipBay : MonoBehaviour {
    private long soChip;
    public Image chip1;
    public Image chip2;
    public Image chip3;
    public Transform posPlayer;
    public Transform posChipTong;

    Vector3 vtPosDefaut;

    void Awake() {


    }
    void Start() {
        vtPosDefaut = chip1.transform.localPosition;
    }

    public void setSoChip(long soChip) {
        this.soChip = soChip;
    }
    public void onMoveto(long money, int type) {
        soChip = money;
        if (money <= 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            string name;
            if (money > BaseInfo.gI().moneyTable * 20) {
                name = "chip44";
            } else if (money > BaseInfo.gI().moneyTable * 10) {
                name = "chip43";
            } else if (money > BaseInfo.gI().moneyTable * 5) {
                name = "chip42";
            } else if (money > BaseInfo.gI().moneyTable * 1) {
                name = "chip41";
            } else {
                name = "chip40";
            }
            chip1.sprite = GameControl.instance.getChipByName(name);
            chip2.sprite = GameControl.instance.getChipByName(name);
            chip3.sprite = GameControl.instance.getChipByName(name);

            if (type == 1) {
                StartCoroutine(Moveto());
            } else {
                StartCoroutine(Moveback());
            }
        }
    }
    public long getMoneyChip() {
        return soChip;
    }


    IEnumerator Moveback() {
        chip1.transform.localPosition = posChipTong.localPosition;
        chip2.transform.localPosition = posChipTong.localPosition;
        chip3.transform.localPosition = posChipTong.localPosition;
        chip1.transform.DOLocalMove(posPlayer.localPosition, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip2.transform.DOLocalMove(posPlayer.localPosition, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip3.transform.DOLocalMove(posPlayer.localPosition, 0.6f);
        yield return new WaitForSeconds(0.65f);
        gameObject.SetActive(false);

    }
    IEnumerator Moveto() {
        chip1.transform.localPosition = vtPosDefaut;
        chip2.transform.localPosition = vtPosDefaut;
        chip3.transform.localPosition = vtPosDefaut;
        chip1.transform.DOLocalMove(posChipTong.localPosition, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip2.transform.DOLocalMove(posChipTong.localPosition, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip3.transform.DOLocalMove(posChipTong.localPosition, 0.6f);
        yield return new WaitForSeconds(0.65f);
        gameObject.SetActive(false);
    }


}
