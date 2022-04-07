using System.Collections;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines.Base;
using UnityEngine;

public class MoveNeedle : MonoBehaviour
{
    public float needleTime = 0;

    public GameObject check;

    Vector2 needlePos;
    private Animator anim;
    private float maxTime;
    private bool started = true;    //Usado para que a posição de needle só seja atualizada uma vez quando o objeto for criado

    private void Awake()
    {
        needlePos = transform.localPosition;
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    public IEnumerator StartNeedle(BaseMachine machine)
    {
        if (started)
            yield return new WaitForSeconds(0.1f); //SOLUÇÃO TEMPORÁRIA PARA HERB DRYER

        const float step = 0.7f; //Constante indicando o espaço total percorrido do início ao fim do trajeto de Needle

        if (!LabTimeController.Instance.LabTimer.ContainsKey(machine.MachineId))    //machine não existe mais? então o timer acabou.
                                                                                    //toque umas animações, destrua o parent e termine o IEnum
        {
            check.SetActive(true);
            anim.Play("default.TH_CheckFades");
            yield return new WaitForSeconds(5);
            anim.Play("default.TH_FadeOut");
            yield return new WaitForSeconds(1);
            Destroy(transform.parent.gameObject);
            yield break;
        }

        maxTime = machine.machineWorkingTime;
        var time = LabTimeController.Instance.LabTimer[machine.MachineId].Time;

        if (started)    //entramos aqui sempre que o objeto for criado na cena novamente para atualizarmos sua posição X
        {
            needlePos.x -= step / maxTime * (maxTime - time);   //a posição nova será baseada no tempo percorrido * (step/maxTime),
                                                                //que nos dá a distância de um "passo" baseado no tempo de trabalho da máquina
            transform.localPosition = needlePos;
        }

        yield return new WaitForSeconds(1);
        if (needlePos.x >= -0.34f)  //Usamos a posição do objeto para determinar se devemos continuar andando, não o tempo. Sincronizar isso com TIME pode dar errado.
        {
            needlePos.x -= step/maxTime;    // step/maxTime nos dá a posição máxima do needle (step) dividida em partes pequenas
                                            // baseadas no tempo de trabalho total da máquina, então tiramos isso de X
            transform.localPosition = needlePos;
            started = false;
            StartCoroutine(StartNeedle(machine));
        }
    }
}
