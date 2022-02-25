using UnityEngine;

namespace Systems.Plantation
{
    public class GridSystem : MonoBehaviour
    {
        [SerializeField]
        private int Width = 10;
        [SerializeField]
        private int Height = 10;

        [SerializeField]
        private Transform startPosition;
        
        [SerializeField]
        private float OffsetX = 10f;
        [SerializeField]
        private float OffsetZ = 10f;

        [SerializeField]
        private GameObject plot;

        private void Start()
        {
            CreatGrid();
        }

        public void CreatGrid()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var position = startPosition.position;
                    var pos = new Vector3(position.x + OffsetX * j, position.y, position.z+ OffsetZ * i);
                    var plotInstance = Instantiate(plot, pos, Quaternion.identity);
                    plotInstance.transform.parent = this.transform;
                }
            }
        }
    }
}
