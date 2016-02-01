using ListViewExample.Model;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ListViewExample
{
    public static class Extentions
    {
        public static void ScrollToElement(this ScrollViewer scrollViewer, UIElement element,
    bool isVerticalScrolling = true, bool smoothScrolling = true, float? zoomFactor = null)
        {
            var transform = element.TransformToVisual((UIElement)scrollViewer.Content);
            var position = transform.TransformPoint(new Point(0, 0));

            if (isVerticalScrolling)
            {
                scrollViewer.ChangeView(null, position.Y, zoomFactor, !smoothScrolling);
            }
            else
            {
                scrollViewer.ChangeView(position.X, null, zoomFactor, !smoothScrolling);
            }
        }
        public static int GetItemId(this List<Task> list, int itemid)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == itemid) return i;
            }
            return -1;
        }
    }
}
