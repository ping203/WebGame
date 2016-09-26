using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadControl : MonoBehaviour {
    // AsyncOperation async;
    //public Slider slider;
    //public Transform tf_fade;
    // Use this for initialization
    // IEnumerator Start() {
    //slider.value = 0;
    //tf_fade.Do
    //    yield return new WaitForSeconds(1);
    // async = 
    //SceneManager.LoadSceneAsync("main");
    //}
    // Update is called once per frame
    // void Update() {
    //if (async != null && !async.isDone) {
    //    slider.value = async.progress;
    //}
    // }
    void Start() {
#if !UNITY_WEBGL
        Application.targetFrameRate = 60;
#endif

        StartCoroutine(onLoad());
    }
    IEnumerator onLoad() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("main");
    }
}
