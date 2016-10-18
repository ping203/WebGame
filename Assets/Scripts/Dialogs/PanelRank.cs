using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelRank : PanelGame {
    //public GameObject itemRanking;
    public Transform parentItem;

    public static List<ItemRanking> list_top = new List<ItemRanking>();

    public void Start() {
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_ITEMRANK, (prefabAB) => {
            for (int i = 0; i < list_top.Count; i++) {
                GameObject go = Instantiate(prefabAB) as GameObject;
                go.transform.SetParent(parentItem);
                go.transform.localScale = Vector3.one;
                go.GetComponent<ItemRanking>().SetData(list_top[i].rank, list_top[i].id_avata, list_top[i].rank_name, list_top[i].money);
                // list_top.Add(go.GetComponent<ItemRanking>());
                go.GetComponent<ItemRanking>().setUI();
            }
        });
    }

    //public void InstanceItem(int rank, int idAvata, string playername, long money) {
    //    LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, Res.AS_PREFABS_ITEMRANK, (prefabAB) => {
    //        GameObject go = (GameObject)Instantiate(prefabAB);
    //        go.transform.SetParent(parentItem);
    //        go.transform.localScale = Vector3.one;
    //        go.GetComponent<ItemRanking>().SetData(rank, idAvata, playername, money);

    //        list_top.Add(go.GetComponent<ItemRanking>());
    //    });
    //}

    public static void clearList() {
        foreach (ItemRanking it in list_top) {
            Destroy(it.gameObject);
        }
        list_top.Clear();
    }
}
