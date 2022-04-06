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

    private void Start()
    {
        needlePos = transform.localPosition;
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    public IEnumerator StartNeedle(BaseMachine machine)
    {
        const float step = 0.7f;
        
        if (!LabTimeController.Instance.LabTimer.ContainsKey(machine.MachineId)) yield break;
        maxTime = machine.machineWorkingTime;
        var time = LabTimeController.Instance.LabTimer[machine.MachineId].Time;
        needlePos.x = step/maxTime * time;
        
        yield return new WaitForSeconds(0.9f);
        if (needlePos.x >= -0.34f)
        {
            needlePos.x -= step * (maxTime - time);
            transform.localPosition = needlePos;
            StartCoroutine(StartNeedle(machine));
        } else
        {
            check.SetActive(true);
            anim.Play("default.TH_CheckFades");
            yield return new WaitForSeconds(5);
            anim.Play("default.TH_FadeOut");
            yield return new WaitForSeconds(1);
            Destroy(transform.parent.gameObject);
        }
        yield return new WaitForSeconds(1);
        }
}
