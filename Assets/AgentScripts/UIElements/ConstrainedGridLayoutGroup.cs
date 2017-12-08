namespace UnityEngine.UI
{
    [AddComponentMenu("Layout/Modified Grid Layout Group", 152)]
    public class ConstrainedGridLayoutGroup : LayoutGroup
    {
        public enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        public enum Corner
        {
            UpperLeft = 0,
            UpperRight = 1,
            LowerLeft = 2,
            LowerRight = 3
        }

        //Lazlo Added Features
        [SerializeField] protected bool distributeEvenly;

        [SerializeField] protected Vector2 m_CellSize = new Vector2(100, 100);

        [SerializeField] protected int m_ConstraintCount = 2;

        [SerializeField] protected bool m_Limit;

        [SerializeField] protected Vector2 m_Spacing = Vector2.zero;

        [SerializeField] protected Axis m_StartAxis = Axis.Horizontal;

        [SerializeField] protected Corner m_StartCorner = Corner.UpperLeft;

        public Corner startCorner
        {
            get { return m_StartCorner; }
            set { SetProperty(ref m_StartCorner, value); }
        }

        public Axis startAxis
        {
            get { return m_StartAxis; }
            set { SetProperty(ref m_StartAxis, value); }
        }

        public Vector2 cellSize
        {
            get { return m_CellSize; }
            set { SetProperty(ref m_CellSize, value); }
        }

        public Vector2 spacing
        {
            get { return m_Spacing; }
            set { SetProperty(ref m_Spacing, value); }
        }

        public bool Limit
        {
            get { return m_Limit; }
            set { SetProperty(ref m_Limit, value); }
        }

        public int constraintCount
        {
            get { return m_ConstraintCount; }
            set { SetProperty(ref m_ConstraintCount, Mathf.Max(1, value)); }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            constraintCount = constraintCount;
        }

#endif

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (distributeEvenly && rectChildren.Count > 0)
            {
                m_ConstraintCount = Mathf.Max(1, (rectChildren.Count + 1)/2);
            }

            int columnCount = 0;

            if (m_Limit)
            {
                if (m_StartAxis == Axis.Horizontal)
                {
                    columnCount = Mathf.Min(rectChildren.Count, m_ConstraintCount);
                }
                else
                {
                    float rowCount = Mathf.Min(rectChildren.Count, m_ConstraintCount);
                    columnCount = Mathf.CeilToInt(rectChildren.Count/rowCount);
                }
            }
            else
            {
                if (m_StartAxis == Axis.Horizontal)
                {
                    columnCount = rectChildren.Count;
                }
                else
                {
                    columnCount = 1;
                }
            }

            SetLayoutInputForAxis(
                padding.horizontal + (cellSize.x + spacing.x)*columnCount - spacing.x,
                padding.horizontal + (cellSize.x + spacing.x)*columnCount - spacing.x,
                -1, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            if (distributeEvenly && rectChildren.Count > 0)
            {
                m_ConstraintCount = Mathf.Max(1, (rectChildren.Count + 1)/2);
            }

            int minRows = 0;

            if (m_Limit)
            {
                if (m_StartAxis == Axis.Vertical)
                {
                    minRows = Mathf.Min(rectChildren.Count, m_ConstraintCount);
                }
                else
                {
                    float colCount = Mathf.Min(rectChildren.Count, m_ConstraintCount);
                    minRows = Mathf.CeilToInt(rectChildren.Count/colCount);
                }
            }
            else
            {
                if (m_StartAxis == Axis.Vertical)
                {
                    minRows = rectChildren.Count;
                }
                else
                {
                    minRows = 1;
                }
            }

            float minSpace = padding.vertical + (cellSize.y + spacing.y)*minRows - spacing.y;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 1);
        }

        public override void SetLayoutHorizontal()
        {
            SetCellsAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetCellsAlongAxis(1);
        }

        private void SetCellsAlongAxis(int axis)
        {
            // Normally a Layout Controller should only set horizontal values when invoked for the horizontal axis
            // and only vertical values when invoked for the vertical axis.
            // However, in this case we set both the horizontal and vertical position when invoked for the vertical axis.
            // Since we only set the horizontal position and not the size, it shouldn't affect children's layout,
            // and thus shouldn't break the rule that all horizontal layout must be calculated before all vertical layout.

            if (axis == 0)
            {
                // Only set the sizes when invoked for horizontal axis, not the positions.
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    RectTransform rect = rectChildren[i];

                    m_Tracker.Add(this, rect,
                        DrivenTransformProperties.Anchors |
                        DrivenTransformProperties.AnchoredPosition |
                        DrivenTransformProperties.SizeDelta);

                    rect.anchorMin = Vector2.up;
                    rect.anchorMax = Vector2.up;
                    rect.sizeDelta = cellSize;
                }
                return;
            }

            float width = rectTransform.rect.size.x;
            float height = rectTransform.rect.size.y;

            int cellCountX = 1;
            int cellCountY = 1;

            if (m_Limit)
            {
                if (m_StartAxis == Axis.Horizontal)
                {
                    cellCountX = Mathf.Min(rectChildren.Count, m_ConstraintCount);
                    cellCountY = Mathf.CeilToInt(rectChildren.Count/(float) cellCountX);
                }
                else
                {
                    cellCountY = Mathf.Min(rectChildren.Count, m_ConstraintCount);
                    cellCountX = Mathf.CeilToInt(rectChildren.Count/(float) cellCountY);
                }
            }
            else
            {
                if (m_StartAxis == Axis.Horizontal)
                {
                    cellCountX = rectChildren.Count;
                    cellCountY = 1;
                }
                else
                {
                    cellCountX = 1;
                    cellCountY = rectChildren.Count;
                }
            }

            int cornerX = (int) startCorner%2;
            int cornerY = (int) startCorner/2;

            int cellsPerMainAxis, actualCellCountX, actualCellCountY;
            if (startAxis == Axis.Horizontal)
            {
                cellsPerMainAxis = cellCountX;
                actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildren.Count);
                actualCellCountY = Mathf.Clamp(cellCountY, 1,
                    Mathf.CeilToInt(rectChildren.Count/(float) cellsPerMainAxis));
            }
            else
            {
                cellsPerMainAxis = cellCountY;
                actualCellCountY = Mathf.Clamp(cellCountY, 1, rectChildren.Count);
                actualCellCountX = Mathf.Clamp(cellCountX, 1,
                    Mathf.CeilToInt(rectChildren.Count/(float) cellsPerMainAxis));
            }

            Vector2 requiredSpace = new Vector2(
                actualCellCountX*cellSize.x + (actualCellCountX - 1)*spacing.x,
                actualCellCountY*cellSize.y + (actualCellCountY - 1)*spacing.y
                );
            Vector2 startOffset = new Vector2(
                GetStartOffset(0, requiredSpace.x),
                GetStartOffset(1, requiredSpace.y)
                );

            for (int i = 0; i < rectChildren.Count; i++)
            {
                int positionX;
                int positionY;
                if (startAxis == Axis.Horizontal)
                {
                    positionX = i%cellsPerMainAxis;
                    positionY = i/cellsPerMainAxis;
                }
                else
                {
                    positionX = i/cellsPerMainAxis;
                    positionY = i%cellsPerMainAxis;
                }

                if (cornerX == 1)
                {
                    positionX = actualCellCountX - 1 - positionX;
                }
                if (cornerY == 1)
                {
                    positionY = actualCellCountY - 1 - positionY;
                }

                SetChildAlongAxis(rectChildren[i], 0, startOffset.x + (cellSize[0] + spacing[0])*positionX, cellSize[0]);
                SetChildAlongAxis(rectChildren[i], 1, startOffset.y + (cellSize[1] + spacing[1])*positionY, cellSize[1]);
            }
        }
    }
}