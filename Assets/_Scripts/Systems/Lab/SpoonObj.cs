using _Scripts.Helpers;

namespace _Scripts.Systems.Lab
{
    public class SpoonObj : DragObject
    {
        public override void StartDrag()
        {
            CanDrag = true;
        }
    }
}