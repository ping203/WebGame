﻿using UnityEngine;
using System.Collections;

public class BacayPlayer : LiengPlayer {
    public override void setMaster(bool isMaster) {
        isMasters = isMaster;
        master.gameObject.SetActive(isMaster);
    }

    public override void setRank(int rank) {
        sp_typeCard.StopAllCoroutines();
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
        }
    }

}
