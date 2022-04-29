using System;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class PestleObject : DragObject
    {
        public void StartDrag()
        {
            canDrag = true;
        }
    }
}