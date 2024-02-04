﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Platform.WPF;

namespace Microsoft.Maui.Handlers.WPF
{
	public partial class ContentViewHandler : ViewHandler<IContentView, ContentPanel>
	{
		public override void SetVirtualView(IView view)
		{
			base.SetVirtualView(view);

			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");

			PlatformView.CrossPlatformMeasure = VirtualView.CrossPlatformMeasure;
			PlatformView.CrossPlatformArrange = VirtualView.CrossPlatformArrange;
		}

		static void UpdateContent(ContentViewHandler handler)
		{
			_ = handler.PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = handler.VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			handler.PlatformView.Children.Clear();

			if (handler.VirtualView.PresentedContent is IView view)
				handler.PlatformView.Children.Add((UIElement)view.ToPlatform(handler.MauiContext));
		}

		protected override ContentPanel CreatePlatformView()
		{
			if (VirtualView == null)
			{
				throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a LayoutView");
			}

			var view = new ContentPanel
			{
				CrossPlatformMeasure = VirtualView.CrossPlatformMeasure,
				CrossPlatformArrange = VirtualView.CrossPlatformArrange
			};

			return view;
		}

		public static void MapContent(ContentViewHandler handler, IContentView page)
		{
			UpdateContent(handler);
		}
	}
}
