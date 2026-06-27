using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeeSceneByIndex(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
