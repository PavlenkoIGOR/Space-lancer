using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Space_lancer
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _joystickBG;
        [SerializeField] private Image _stick;

        public Vector3 value { get; private set; }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            //установка точки отсчета в 0,0 координат, в данном случае, в точку якоря _joystickBG
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out position);


            position.x = (position.x / _joystickBG.rectTransform.sizeDelta.x);
            position.y = (position.y / _joystickBG.rectTransform.sizeDelta.y);

            //normalize
            value = new Vector3(position.x, position.y, 0);
            if (value.magnitude > 1)
            {
                value = value.normalized;
            }

            float offsetX = _joystickBG.rectTransform.sizeDelta.x / 2 - _stick.rectTransform.sizeDelta.x / 2;
            float offsetY = _joystickBG.rectTransform.sizeDelta.y / 2 - _stick.rectTransform.sizeDelta.y / 2;

            _stick.rectTransform.anchoredPosition = new Vector2(offsetX, offsetY) * value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //reset stick position
            value = Vector2.zero;
            _stick.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
