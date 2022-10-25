using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    private IEnumerator coroutine;
    public void ChangeWithDelay(string s)       //Muda para a cena S após 1.2 segundos. Eu queria especificar o tempo mas OnClick não permite 2 argumentos.
    {
        coroutine = CSD(s);
        StartCoroutine(coroutine);
    }

    private IEnumerator CSD(string s)          //Pode receber "quit" para fechar o jogo
    {
        yield return new WaitForSeconds(1.2f);
        if (s == "quit")
        {
            Application.Quit();
            yield break;
        }
        SceneManager.LoadScene(s);
    }
}
