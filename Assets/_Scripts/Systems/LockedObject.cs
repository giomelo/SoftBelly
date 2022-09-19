using _Scripts.U_Variables;
using UnityEngine;

namespace _Scripts.Systems
{
    public abstract class LockedObject : MonoBehaviour
    {
        public bool IsLocked;
        public int PriceToUnlock = 10;
        [SerializeField]
        private GameObject lockedObj;
        private int idAux;
        private bool plotAux;

        public void UnLock()
        {
            IsLocked = false;
            lockedObj.SetActive(false);
        }
        public void Locked(bool locked, int id, bool plot)
        {
            if (id != idAux) return;
            if(plot != plotAux) return;
            UniversalVariables.Instance.ModifyMoney(PriceToUnlock, false);
            IsLocked = locked;
            lockedObj.SetActive(locked);
        }

        public void Initialized(int id, bool plot)
        {
            idAux = id;
            plot = plot;
        }
    }
}
