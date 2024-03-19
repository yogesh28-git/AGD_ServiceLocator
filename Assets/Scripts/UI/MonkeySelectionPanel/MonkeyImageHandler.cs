using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform rectTransform;
        private Vector2 originalAnchoredPosition;
        private Vector2 originalPosition;
        private LayoutElement layoutElement;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;

            rectTransform = GetComponent<RectTransform>( );
            originalAnchoredPosition = rectTransform.anchoredPosition;
            originalPosition = rectTransform.position;

            layoutElement = GetComponent<LayoutElement>();
        }

        public void OnDrag( PointerEventData eventData )
        {
            rectTransform.position = eventData.position;
            owner.MonkeyDraggedAt( eventData.position );
        }

        public void OnDrop( PointerEventData eventData )
        {
            ResetMonkey( );
            owner.MonkeyDroppedAt( eventData.position );
        }

        public void ResetMonkey( )
        {
            monkeyImage.color = new Color( 1, 1, 1, 1 );

            rectTransform.anchoredPosition = originalAnchoredPosition;
            rectTransform.position = originalPosition;

            layoutElement.enabled = false;
            layoutElement.enabled = true;
        }

        public void OnPointerDown( PointerEventData eventData )
        {
            monkeyImage.color = new Color( 1, 1, 1, 0.6f );
        }
    }
}