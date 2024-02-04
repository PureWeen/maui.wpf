#nullable enable
using System;
using System.Windows.Controls;
using Microsoft.Maui.Graphics;
using WRect = global::System.Windows.Rect;
using WSize = global::System.Windows.Size;

namespace Microsoft.Maui.Platform.WPF
{
	public class LayoutPanel : Panel
	{
		internal Func<double, double, Size>? CrossPlatformMeasure { get; set; }
		internal Func<Rect, Size>? CrossPlatformArrange { get; set; }

		public bool ClipsToBounds { get; set; }

		public LayoutPanel()
		{
			Height = 1000;
			Width = 1000;
		}


		// TODO: Possibly reconcile this code with ViewHandlerExtensions.MeasureVirtualView
		// If you make changes here please review if those changes should also
		// apply to ViewHandlerExtensions.MeasureVirtualView
		protected override WSize MeasureOverride(WSize availableSize)
		{
			if (CrossPlatformMeasure == null)
			{
				return base.MeasureOverride(availableSize);
			}

			var width = availableSize.Width;
			var height = availableSize.Height;

			var crossPlatformSize = CrossPlatformMeasure(width, height);

			width = crossPlatformSize.Width;
			height = crossPlatformSize.Height;

			return new WSize(width, height);
		}

		// TODO: Possibly reconcile this code with ViewHandlerExtensions.LayoutVirtualView
		// If you make changes here please review if those changes should also
		// apply to ViewHandlerExtensions.LayoutVirtualView
		protected override WSize ArrangeOverride(WSize finalSize)
		{
			if (CrossPlatformArrange == null)
			{
				return base.ArrangeOverride(finalSize);
			}

			var width = finalSize.Width;
			var height = finalSize.Height;

			CrossPlatformArrange(new Rect(0, 0, width, height));

			return finalSize;
		}
	}
}
