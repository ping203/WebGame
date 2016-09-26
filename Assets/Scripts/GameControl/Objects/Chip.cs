using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chip : MonoBehaviour {
    private long soChip;
    public Image sp_chip;
    public Text lb_sochip;
    public void setSoChip(long soChip) {
        if(soChip == 0) {
            gameObject.SetActive(false);
        }
        this.soChip = soChip;
    }

    public void setMoneyChip(long money) {
        soChip = money;

        string name;
        if (money > BaseInfo.gI().moneyTable * 20) {
            name = "chip24";
        } else if (money > BaseInfo.gI().moneyTable * 10) {
            name = "chip23";
        } else if (money > BaseInfo.gI().moneyTable * 5) {
            name = "chip22";
        } else if (money > BaseInfo.gI().moneyTable * 1) {
            name = "chip21";
        } else {
            name = "chip20";
        }
        if (money == 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            lb_sochip.text = "" + BaseInfo.formatMoneyDetailDot(money);
        }
    }

    public void setMoneyChipChu(long money) {
        soChip = money;

        string name;
        if (money > BaseInfo.gI().moneyTable * 20) {
            name = "chip34";
        } else if (money > BaseInfo.gI().moneyTable * 10) {
            name = "chip33";
        } else if (money > BaseInfo.gI().moneyTable * 5) {
            name = "chip32";
        } else if (money > BaseInfo.gI().moneyTable * 1) {
            name = "chip31";
        } else {
            name = "chip30";
        }
        //sp_chip.spriteName = name;
        if (money == 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            lb_sochip.text = "" + BaseInfo.formatMoneyDetailDot(money);
        }
    }

    public void setMoneyChipBay(long money) {
        soChip = money;

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
        //sp_chip.spriteName = name;
        if (money == 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            lb_sochip.text = "" + BaseInfo.formatMoneyDetailDot(money);
        }
    }

    public long getMoneyChip() {
        return soChip;
    }
}
