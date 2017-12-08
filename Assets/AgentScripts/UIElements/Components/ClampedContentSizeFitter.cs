using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [AddComponentMenu("Layout/Limited Content Size Fitter", 142)]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class ClampedContentSizeFitter : UIBehaviour, ILayoutSelfController
    {
        public enum FitMode
        {
            Unconstrained,
            MinSize,
            PreferredSize
        }

        [SerializeField] protected FitMode _horizontalFit = FitMode.Unconstrained;
        [OptionalFloat]
        [SerializeField] protected float _minHorizontalSize;
        [OptionalFloat]
        [SerializeField] protected float _maxHorizontalSize;

        [NonSerialized] private RectTransform _rect;

        private DrivenRectTransformTracker _tracker;

        [SerializeField] protected FitMode _verticalFit = FitMode.Unconstrained;

        [OptionalFloat]
        [SerializeField] protected float _minVerticalSize;
        [OptionalFloat]
        [SerializeField] protected float _maxVerticalSize;
        
        public FitMode horizontalFit
        {
            get { return _horizontalFit; }
            set
            {
                if (!_horizontalFit.Equals(value))
                {
                    _horizontalFit = value;
                    SetDirty();
                }
            }
        }

        public FitMode verticalFit
        {
            get { return _verticalFit; }
            set
            {
                if (!_verticalFit.Equals(value))
                {
                    _verticalFit = value;
                    SetDirty();
                }
            }
        }

        private RectTransform rectTransform
        {
            get
            {
                if (_rect == null)
                {
                    _rect = GetComponent<RectTransform>();
                }
                return _rect;
            }
        }

        public virtual void SetLayoutHorizontal()
        {
            _tracker.Clear();
            HandleSelfFittingAlongAxis(0);
        }

        public virtual void SetLayoutVertical()
        {
            HandleSelfFittingAlongAxis(1);
        }

        protected override void OnRectTransformDimensionsChange()
        {
            SetDirty();
        }

        private void HandleSelfFittingAlongAxis(int axis)
        {
            FitMode fitting = axis == 0 ? horizontalFit : verticalFit;
            if (fitting == FitMode.Unconstrained)
            {
                return;
            }

            _tracker.Add(this, rectTransform,
                axis == 0 ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);

            // Set size to min or preferred size
            if (fitting == FitMode.MinSize)
            {
                rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis) axis,
                    OptionalClamp(LayoutUtility.GetMinSize(_rect, axis), axis));
            }
            else
            {
                rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis) axis,
                    OptionalClamp(LayoutUtility.GetPreferredSize(_rect, axis), axis));
            }
        }

        private float OptionalClamp(float value, int axis)
        {
            float min = axis == 0 ? _minHorizontalSize : _minVerticalSize;
            float max = axis == 0 ? _maxHorizontalSize : _maxVerticalSize;

            if (min > 0)
            {
                value = Mathf.Max(min, value);
            }
            if (max > 0)
            {
                value = Mathf.Min(max, value);
            }

            return value;
        }

        protected void SetDirty()
        {
            if (!IsActive())
            {
                return;
            }

            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetDirty();
        }

#endif

        #region Unity Lifetime calls

        protected override void OnEnable()
        {
            base.OnEnable();
            SetDirty();
        }

        protected override void OnDisable()
        {
            _tracker.Clear();
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
            base.OnDisable();
        }

        #endregion
    }
}