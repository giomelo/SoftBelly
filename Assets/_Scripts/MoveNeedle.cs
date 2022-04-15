using System.Collections;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

public class MoveNeedle : MonoBehaviour
{
    public float needleTime = 0;

    public GameObject check;

    Vector2 needlePos;
    private Animator anim;
    private float maxTime;
    private bool started = true;    //Usado para que a posi��o de needle s� seja atualizada uma vez quando o objeto for criado

    private void Awake()
    {
        needlePos = transform.localPosition;
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    public IEnumerator StartNeedle(BaseMachine machine, float prevTime = 9001, bool endLoop = false) //prevTime é o tempo adquirido ao fim de um loop no IEnum
                                                                                                     //e endLoop é usado para determinar quando o HUD entra em seu estado final do processo
    {
        if (started)
            yield return new WaitForSeconds(0.05f); //delay usado quando a máquina é (re)criada na cena para que tudo fora do IEnum possa ser iniciado antes de entrarmos aqui

        const float step = 0.7f; //Constante indicando o espa�o total percorrido do in�cio ao fim do trajeto de Needle

        if (!LabTimeController.Instance.LabTimer.ContainsKey(machine.MachineId) || prevTime == 0 || endLoop)    //machine n�o existe mais? ent�o o timer acabou.//toque umas anima��es, destrua o parent e termine o IEnum
        {
            if (!endLoop)
            {
                endLoop = true;
                check.SetActive(true);
                anim.Play("default.TH_CheckFades");
            }
            if (machine as Burn)
            {
                Burn burnMachine = machine as Burn;
                if (machine.MachineId != 0 && LabTimeController.Instance.LabTimer[machine.MachineId].Time <= -burnMachine.BurnTime)
                {
                    anim.Play("default.TH_FailFade");
                }
            }
            else
            {
                // A ser feito: tocar animação de fadeout quando interagir com a máquina

                //  fadeout toca aqui por um timer
                //yield return new WaitForSeconds(5);
                //anim.Play("default.TH_FadeOut");
                //yield return new WaitForSeconds(1);
                //Destroy(transform.parent.gameObject);
                //yield break;
            }
            yield return new WaitForFixedUpdate(); //Impedindo um stack overflow
            StartCoroutine(StartNeedle(machine, 0, true));
        }

        if (!endLoop)
        {
            maxTime = machine.machineWorkingTime;
            var time = LabTimeController.Instance.LabTimer[machine.MachineId].Time;

            if (started)    //entramos aqui sempre que o objeto for criado na cena novamente para atualizarmos sua posi��o X
            {
                needlePos.x -= step / maxTime * (maxTime - time);   //a posi��o nova ser� baseada no tempo percorrido * (step/maxTime),
                                                                    //que nos d� a dist�ncia de um "passo" baseado no tempo de trabalho da m�quina
                transform.localPosition = needlePos;
            }

            yield return new WaitForSeconds(1);
            if (needlePos.x >= -0.34f)  //Usamos a posi��o do objeto para determinar se devemos continuar andando, n�o o tempo. Sincronizar isso com TIME pode dar errado.
            {
                needlePos.x -= step / maxTime;    // step/maxTime nos d� a posi��o m�xima do needle (step) dividida em partes pequenas
                                                  // baseadas no tempo de trabalho total da m�quina, ent�o tiramos isso de X
                transform.localPosition = needlePos;
                started = false;
                StartCoroutine(StartNeedle(machine, time));
            }
            if (time == 0)
                StartCoroutine(StartNeedle(machine, time));
        }
    }
}
