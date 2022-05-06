using UnityEngine;
/// <summary>
/// 맵 이동 시 가져가야할 오브젝트들
/// </summary>
public class DontDestroyObject : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
