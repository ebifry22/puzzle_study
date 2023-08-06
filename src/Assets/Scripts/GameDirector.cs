using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject prefabMessage = default!;
    [SerializeField] GameObject gameObjectCanvas = default!;
    [SerializeField] PlayDirector PlayDirector = default!;
    GameObject _message = null;

    void CreateMessage(string message)
    {
        Debug.Assert(_message == null);
        _message = Instantiate(prefabMessage, Vector3.zero, Quaternion.identity,
            gameObjectCanvas.transform);

        _message.transform.localPosition = new Vector3(0, 0, 0);//画面中心に配置

        _message.GetComponent<TextMeshProUGUI>().text = message;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GameFlow");
    }

    private IEnumerator GameFlow()
    {
        CreateMessage("Ready?");

        yield return new WaitForSeconds(1.0f);
        Destroy(_message); _message = null;

        PlayDirector.EnableSpawn(true);//プレイ開始

        while (!PlayDirector.IsGameOver())//終了待ち
        {
            yield return null;
        }

        CreateMessage("Game Over");

        while (!Input.anyKey)//何かしら押すのを待つ
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("TitleScene");
    }
}
