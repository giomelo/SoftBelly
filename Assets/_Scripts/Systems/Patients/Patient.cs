using _Scripts.Systems.Item;
using _Scripts.Systems.Npcs;
using UnityEngine;

namespace _Scripts.Systems.Patients
{
    public class Patient : NpcBase
    {
        [SerializeField]
        private OrderObj _order;
        private void Start()
        {
            SetOrder();
            DisplayOrder();
        }

        private void DisplayOrder()
        {
            _order.Object.SetActive(true);
        }

        public void SetOrder()
        {
            _order.Order = PatientsController.Instance.GenerateRandomOrder();
        }
    }
}