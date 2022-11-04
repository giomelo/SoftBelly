using _Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.Helpers.DragAndDrop
{
    public class DragAndDropScript : MonoBehaviour , IPointerDownHandler , IBeginDragHandler , IEndDragHandler , IDragHandler , IDropHandler
    {
        private RectTransform rectTransform;
        private SlotBase baseSlot;
        private Vector3 initialTransform;
        private Image thisImage;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            baseSlot = transform.parent.GetComponent<SlotBase>();
            thisImage = GetComponent<Image>();
            initialTransform = rectTransform.anchoredPosition;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
           Debug.Log("OnPointerDow");
           
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("BeginDrag");
            thisImage.maskable = false;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            BackToPos();
        }

        public void BackToPos()
        {
            rectTransform.anchoredPosition = initialTransform;
            thisImage.maskable = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (baseSlot.uiSlot.item == null) return;
            Debug.Log("Dragin");;
            rectTransform.anchoredPosition += eventData.delta / baseSlot.Subject.thisCanvas.scaleFactor;
        }

        public void OnDrop(PointerEventData eventData)
        {
          
        }
    }
}
