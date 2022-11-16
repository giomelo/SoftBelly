using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TextArray : MonoBehaviour
{
    private int txtAtual = 1;
    private Animator anim;

    [SerializeField]
    private string proximaCena;

    [SerializeField]
    private TextMeshProUGUI tmp;

    [SerializeField]
    [TextArea(5, 16)]
    string[] textos;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        if (tmp) {
            if (textos.Length > 0) { tmp.text = textos[0]; }
        } else
            Debug.LogWarning("A área de textos não possui um componente TextMeshPro!");
    }

    public void NovoTexto()
    {
        if (txtAtual < textos.Length)
            anim.Play("Base.TrocaTexto");
        else
            StartCoroutine("IniciaJogo");
    }

    public void ProxEmArray() { 
        tmp.text = textos[txtAtual];
        txtAtual++;
    }

    private IEnumerator IniciaJogo()
    {
        GameObject.Find("Fade").GetComponent<Animator>().Play("Base Layer.FadeIn");
        for (int i = 0; i < 49; i++)
            yield return new WaitForFixedUpdate();
        SceneManager.LoadScene(proximaCena);
    }
}
