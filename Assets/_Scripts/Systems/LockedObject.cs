using System;
using _Scripts.SaveSystem;
using _Scripts.U_Variables;
using TMPro;
using UnityEngine;

namespace _Scripts.Systems
{
    public abstract class LockedObject : MonoBehaviour , DataObject
    {
        public bool IsLocked;
        public int PriceToUnlock = 10;
        [SerializeField]
        private GameObject lockedObj;
        public int idAux;
        private bool plotAux;
        [SerializeField]
        private TextMeshProUGUI priceText;

        public void UnLock()
        {
            IsLocked = false;
            lockedObj.SetActive(false);
            priceText.gameObject.SetActive(false);
        }
        public void Locked(bool locked, int id, bool plot, bool buy)
        {
            if (id != idAux) return;
            if(plot != plotAux) return;
            if(!locked && buy)
                UniversalVariables.Instance.ModifyMoney(PriceToUnlock, false);
            IsLocked = locked;
            Debug.Log("Esse obje esta " +  locked);
            priceText.gameObject.SetActive(locked);
            lockedObj.SetActive(locked);
        }

        public void Initialized(int id, bool plot)
        {
            idAux = id;
            plotAux = plot;
            priceText.text = PriceToUnlock.ToString();
        }

        protected void Awake()
        {
            Load();
        }

        public void Load()
        {
            SaveLockedObject d = (SaveLockedObject)Savesystem.Load(GetType().ToString() + idAux);
            // if (IsNewGame)
            // {
            //     //Developer.ClearSaves();
            //     // clear save
            //     Debug.Log("NewGame");
            //     return;
            // }
            if (d != null)
            {
                // /*variavell*/ = /*variavel*/ = data./*variavel*/;

                IsLocked = d.IsLocked;

            }
        }

        public void Save()
        {
            SaveData data = new SaveLockedObject(IsLocked);
            Savesystem.Save(data, GetType().ToString() + idAux);
        }
    }
}
