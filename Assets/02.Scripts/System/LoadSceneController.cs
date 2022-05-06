using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image roadingBar;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("MainStage"); //비동기방식(씬 이동 시 다른 작업 가능)
        //씬의 90%까지 업로드 된 상태로 놔두고 true로 변경 시 다시 로드
        op.allowSceneActivation = false;

        float timer = 0;
        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress>0.89f)
            {
                roadingBar.fillAmount = Mathf.Lerp(roadingBar.fillAmount, 1f, timer);
                if (roadingBar.fillAmount>=0.95f)
                {
                    op.allowSceneActivation = true;
                }
            }
            else
            {
                roadingBar.fillAmount = Mathf.Lerp(roadingBar.fillAmount, op.progress, timer);
                if (roadingBar.fillAmount>=op.progress)
                {
                    timer = 0;
                }
            }
            //yield return null;
            //if (op.progress < 0.9f)
            //{
            //    roadingBar.fillAmount = op.progress;
            //}
            //else
            //{//진행도가 90%보다 커지면 Fake 로딩
            //    timer += Time.unscaledDeltaTime;
            //    roadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            //    if (roadingBar.fillAmount >= 1f)
            //    {
            //        op.allowSceneActivation = true;
            //        yield break;
            //    }
            //}
        }
    }
}
