using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tools.Components.Universal
{
    /// <summary>
    /// Interface for simplified use and encapsulation compliance
    /// </summary>
    public interface IScrollableComponent
    {
        void SetContent(List<IScrollableContainerContent> content, bool updatePosition);
    }

    public class ScrollableComponent : MonoBehaviour, IScrollableComponent, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler
    {
        #region Inspector variables

#pragma warning disable
        [SerializeField] private BaseScrollableContainer containerPrefab;
        [SerializeField] private Slider slider;
        [SerializeField] private RectTransform scrollAreaTransform;
        [SerializeField] private float dragSensitivity = 1;
        [SerializeField] private float mouseScrollSensitivity;
        [SerializeField] private bool isSlide = false;
        [SerializeField] private AnimationCurve decreasingImpulsePlot;
#pragma warning restore

        #endregion Inspector variables

        private float containerHeight => containerPrefab.RectTransform.rect.height;
        private float scrollAreaHeight => scrollAreaTransform.rect.height;
        private float maxOffset => (content?.Length * containerHeight - scrollAreaHeight >= 0 ? content?.Length * containerHeight - scrollAreaHeight : 0) ?? 0;

        #region Private variables

        private BaseScrollableContainer[] scrollableContainers;
        private IScrollableContainerContent[] content;

        private float itemsOffset;
        private int itemsCount;

        private bool isHover = false;

        #region sliding variables

        private bool isSliding = false;
        private float scrollImpulse;
        private Tools.AverageFloat averageImpulse = new Tools.AverageFloat();
        private float timer;
        private float currentTime => Time.time - timer;
        private float lastTimeUpdateContainers;

        /// <summary>
        /// If the user released the scroll area quickly, then it will slide.
        /// If he held the pointer on it, then it will stop when the pointer is released.
        /// 0.1 is the hold time of the area so that there is no slide
        /// </summary>
        private bool shouldStartSlide => Time.time - lastTimeUpdateContainers < 0.1f;

        #endregion sliding variables

        #region coroutines instances

        private Coroutine updateMouseScrollCoroutineInstance;
        private Coroutine updateSlideCoroutineInstance;

        #endregion coroutines instances

        #endregion Private variables

        #region Unity functions

        private void OnDisable()
        {
            StopMouseScrollUpdateCoroutine();
            StopSlideUpdateCoroutine();
        }

        private void OnEnable()
        {
            StartSlideUpdateCorotine();
            StartMouseScrollUpdateCorotine();
        }

        #endregion Unity functions

        #region Public functions

        public void Init()
        {
            InitContainers();
        }

        /// <summary>
        /// Set all content in scrollable area
        /// </summary>
        /// <param name="content">List of content</param>
        public void SetContent(List<IScrollableContainerContent> content, bool updatePosition = false)
        {
            if (scrollableContainers == null)
            {
                Init();
            }
            if (updatePosition)
            {
                itemsOffset = 0;
            }
            this.content = content.ToArray();
            foreach (BaseScrollableContainer container in scrollableContainers)
            {
                container.gameObject.SetActive(true);
            }
            UpdateContainers();
            StartMouseScrollUpdateCorotine();
            StartSlideUpdateCorotine();
        }

        public void UpdateOffset()
        {
            if (slider != null)
            {
                itemsOffset = Mathf.Lerp(0, maxOffset, slider.normalizedValue);
                UpdateContainers();
            }
        }

        public void UncommonUpdate()
        {
            UpdateContainers();
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
            UpdateSlider();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isSliding = false;
            scrollImpulse = 0;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isSliding = isSlide;
            if (isSliding && shouldStartSlide)
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

        private void InitContainers()
        {
            itemsCount = Mathf.FloorToInt(scrollAreaHeight / containerHeight) + 2;
            scrollableContainers = new BaseScrollableContainer[itemsCount];
            for (int i = 0; i < itemsCount; i++)
            {
                BaseScrollableContainer container = Instantiate(containerPrefab, scrollAreaTransform);
                container.OnFirstInit();
                container.gameObject.SetActive(false);
                scrollableContainers[i] = container;
            }
        }

        private void UpdateContainers()
        {
            ClampOffset();
            int firstIndex = Mathf.FloorToInt(itemsOffset / containerPrefab.RectTransform.rect.height);

            for (int i = 0; i < itemsCount; i++)
            {
                if ((i + firstIndex) <= content.Length - 1 && i + firstIndex >= 0)
                {
                    scrollableContainers[i]
                        .Init(content[Mathf.Clamp(i + firstIndex, 0, content.Length - 1)]);

                    scrollableContainers[i].RectTransform.anchoredPosition =
                        new Vector2(0, -(i * containerHeight) + (itemsOffset % containerHeight));
                    scrollableContainers[i].gameObject.SetActive(true);
                }
                else
                {
                    scrollableContainers[i].gameObject.SetActive(false);
                }
            }
            lastTimeUpdateContainers = Time.time;
        }

        private void ClampOffset()
        {
            itemsOffset = Mathf.Clamp(itemsOffset, 0, maxOffset);
        }

        private void StartMouseScrollUpdateCorotine()
        {
            if (gameObject.activeInHierarchy)
            {
                if (updateMouseScrollCoroutineInstance == null)
                {
                    updateMouseScrollCoroutineInstance = StartCoroutine(UpdateMouseScrollCoroutine());
                }
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
            if (gameObject.activeInHierarchy)
            {
                if (updateSlideCoroutineInstance == null)
                {
                    updateSlideCoroutineInstance = StartCoroutine(UpdateSlidingCoroutine());
                }
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
                UpdateSlider();
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
                    UpdateSlider();
                    UpdateContainers();
                }
            }
        }

        private void UpdateSlider()
        {
            if (slider != null)
            {
                slider.normalizedValue = itemsOffset / maxOffset;
            }
        }

        #endregion Private functions
    }
}