using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scrolling
{
    public class ScrollableComponent : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private ScrollableContainer containerPrefab;
        [SerializeField] private RectTransform scrollAreaTransform;
        [SerializeField] private float dragSensitivity = 1;
        [SerializeField] private float mouseScrollSensitivity;
        [SerializeField] private bool isSlide;
        [SerializeField] private AnimationCurve decreasingImpulsePlot;
#pragma warining restore

        #endregion Inspector variables

        #region Private variables

        private ScrollableContainer[] scrollableContainers;
        private IScrollableContainerContent[] content;
        private float itemsOffset;
        private int itemsCount;

        private float containerHeight => containerPrefab.RectTransform.rect.height;
        private float scrollAreaHeight => scrollAreaTransform.rect.height;

        private Coroutine updateMouseScrollCoroutineInstance;
        private Coroutine updateSlideCoroutineInstance;

        private bool isHover = false;
        private bool isSliding = false;

        private float scrollImpulse;
        private Tools.AverageFloat averageImpulse = new Tools.AverageFloat();
        private float timer;
        private float currentTime => Time.time - timer;

        #endregion Private variables

        #region Unity functions

        private void Awake()
        {
            Init();
        }

        #endregion Unity functions

        #region Public functions

        public void SetContent(List<IScrollableContainerContent> content)
        {
            itemsOffset = 0;
            this.content = content.ToArray();
            foreach (var container in scrollableContainers)
            {
                container.gameObject.SetActive(true);
            }
            UpdateContainers();
            StartMouseScrollUpdateCorotine();
            StartSlideUpdateCorotine();
        }

        #endregion Public functions

        #region Events system functions

        public void OnDrag(PointerEventData eventData)
        {
            itemsOffset += eventData.delta.y * dragSensitivity;
            if (isSlide)
            {
                averageImpulse.AddNext(eventData.delta.y);
            }
            UpdateContainers();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isSliding = false;
            scrollImpulse = 0;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isSliding = isSlide;
            if (isSliding)
            {
                scrollImpulse = averageImpulse.average;
                timer = Time.time;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHover = false;
        }

        #endregion Events system functions

        #region Private functions

        private void Init()
        {
            InitContainers();
        }

        private void InitContainers()
        {
            itemsCount = Mathf.FloorToInt(scrollAreaHeight / containerHeight) + 3;
            scrollableContainers = new ScrollableContainer[itemsCount];
            for (int i = 0; i < itemsCount; i++)
            {
                var container = Instantiate(containerPrefab, scrollAreaTransform);
                container.gameObject.SetActive(false);
                scrollableContainers[i] = container;
            }
        }

        private void UpdateContainers()
        {
            ClampOffset();
            var firstIndex = Mathf.FloorToInt(itemsOffset / containerPrefab.RectTransform.rect.height);
            for (int i = 0; i < itemsCount; i++)
            {
                scrollableContainers[i]
                    .Init(content[Mathf.Clamp(i + firstIndex, 0, content.Length - 1)]);

                scrollableContainers[i].RectTransform.anchoredPosition =
                    new Vector2(0, scrollAreaTransform.rect.yMax - i * containerHeight + (itemsOffset % containerHeight));
            }
        }

        private void ClampOffset()
        {
            var maxOffset = content.Length * containerHeight - scrollAreaHeight;
            itemsOffset = Mathf.Clamp(itemsOffset, 0, maxOffset);
        }

        private void StartMouseScrollUpdateCorotine()
        {
            if (updateMouseScrollCoroutineInstance == null)
            {
                updateMouseScrollCoroutineInstance = StartCoroutine(UpdateMouseScrollCoroutine());
            }
        }

        private void StopMouseScrollUpdateCoroutine()
        {
            if (updateMouseScrollCoroutineInstance != null)
            {
                StopCoroutine(updateMouseScrollCoroutineInstance);
                updateMouseScrollCoroutineInstance = null;
            }
        }

        private IEnumerator UpdateMouseScrollCoroutine()
        {
            while (true)
            {
                UpdateMouseScroll();
                yield return null;
            }
        }

        private void StartSlideUpdateCorotine()
        {
            if (updateSlideCoroutineInstance == null)
            {
                updateSlideCoroutineInstance = StartCoroutine(UpdateSlidingCoroutine());
            }
        }

        private void StopSlideUpdateCoroutine()
        {
            if (updateSlideCoroutineInstance != null)
            {
                StopCoroutine(updateSlideCoroutineInstance);
                updateSlideCoroutineInstance = null;
            }
        }

        private IEnumerator UpdateSlidingCoroutine()
        {
            while (true)
            {
                UpdateSliding();
                yield return new WaitForFixedUpdate();
            }
        }

        private void UpdateSliding()
        {
            if (isSliding)
            {
                itemsOffset += scrollImpulse * dragSensitivity * decreasingImpulsePlot.Evaluate(currentTime);
                if (scrollImpulse * decreasingImpulsePlot.Evaluate(currentTime) == 0)
                {
                    isSliding = false;
                }
                UpdateContainers();
            }
        }

        private void DecreaseImpulse()
        {
            Debug.Log(decreasingImpulsePlot.Evaluate(currentTime));
            scrollImpulse *= decreasingImpulsePlot.Evaluate(currentTime);
        }

        private void UpdateMouseScroll()
        {
            if (isHover)
            {
                if (Input.mouseScrollDelta.y != 0)
                {
                    itemsOffset -= Input.mouseScrollDelta.y * mouseScrollSensitivity * Time.deltaTime;
                    UpdateContainers();
                }
            }
        }

        #endregion Private functions
    }
}