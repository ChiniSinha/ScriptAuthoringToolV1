namespace UnityEngine.UI
{
    public class FixedSizeText : Text
    {
        public override float minHeight
        {
            get
            {
                if (horizontalOverflow == HorizontalWrapMode.Wrap)
                {
                    return preferredHeight;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override float minWidth
        {
            get
            {
                if (verticalOverflow == VerticalWrapMode.Truncate)
                {
                    return preferredWidth;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}