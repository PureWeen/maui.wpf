﻿using System;
using System.Linq;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;

namespace Microsoft.AspNetCore.Components.WebView.Maui.WPF
{
#if ANDROID
	[SupportedOSPlatform("android23.0")]
#endif
	public partial class BlazorWebViewHandler
	{
		/// <summary>
		/// This field is part of MAUI infrastructure and is not intended for use by application code.
		/// </summary>
		public static PropertyMapper<BlazorWebView, BlazorWebViewHandler> BlazorWebViewMapper = new(ViewMapper)
		{
			[nameof(IBlazorWebView.HostPage)] = MapHostPage,
			[nameof(IBlazorWebView.RootComponents)] = MapRootComponents,
		};

		/// <summary>
		/// Initializes a new instance of <see cref="BlazorWebViewHandler"/> with default mappings.
		/// </summary>
		public BlazorWebViewHandler() : this(BlazorWebViewMapper)
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="BlazorWebViewHandler"/> using the specified mappings.
		/// </summary>
		/// <param name="mapper">The property mappings.</param>
		public BlazorWebViewHandler(PropertyMapper? mapper) : base(mapper ?? BlazorWebViewMapper)
		{
		}

		//TODO
		//internal BlazorWebViewDeveloperTools DeveloperTools => MauiContext!.Services.GetRequiredService<BlazorWebViewDeveloperTools>();

		/// <summary>
		/// Maps the <see cref="IBlazorWebView.HostPage"/> property to the specified handler.
		/// </summary>
		/// <param name="handler">The <see cref="BlazorWebViewHandler"/>.</param>
		/// <param name="webView">The <see cref="IBlazorWebView"/>.</param>
		public static void MapHostPage(BlazorWebViewHandler handler, BlazorWebView webView)
		{
#if WINDOWS
			handler.PlatformView.HostPage = webView.HostPage!;
			//handler.StartWebViewCoreIfPossible();
#endif
		}

		/// <summary>
		/// Maps the <see cref="IBlazorWebView.RootComponents"/> property to the specified handler.
		/// </summary>
		/// <param name="handler">The <see cref="BlazorWebViewHandler"/>.</param>
		/// <param name="webView">The <see cref="IBlazorWebView"/>.</param>
		public static void MapRootComponents(BlazorWebViewHandler handler, BlazorWebView webView)
		{
#if WINDOWS
			handler.PlatformView.RootComponents.Clear();


			foreach (var component in webView.RootComponents)
			{
				handler.PlatformView.RootComponents.Add(new Wpf.RootComponent()
				{
					ComponentType = component.ComponentType!,
					Parameters = component.Parameters!,
					Selector = component.Selector!
				});
			}

			//handler.StartWebViewCoreIfPossible();
#endif
		}

#if WINDOWS
		private string? HostPage { get; set; }

		//internal void UrlLoading(UrlLoadingEventArgs args) =>
		//	VirtualView.UrlLoading(args);

		//private RootComponentsCollection? _rootComponents;

		//private RootComponentsCollection? RootComponents
		//{
		//	get => _rootComponents;
		//	set
		//	{
		//		if (_rootComponents != null)
		//		{
		//			// Remove any previously-known root components and unhook events
		//			_rootComponents.Clear();
		//			_rootComponents.CollectionChanged -= OnRootComponentsCollectionChanged;
		//		}

		//		_rootComponents = value;

		//		if (_rootComponents != null)
		//		{
		//			// Add new root components and hook events
		//			if (_rootComponents.Count > 0 && _webviewManager != null)
		//			{
		//				_webviewManager.Dispatcher.AssertAccess();
		//				foreach (var component in _rootComponents)
		//				{
		//					_ = component.AddToWebViewManagerAsync(_webviewManager);
		//				}
		//			}
		//			_rootComponents.CollectionChanged += OnRootComponentsCollectionChanged;
		//		}
		//	}
		//}

		//private void OnRootComponentsCollectionChanged(object? sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs eventArgs)
		//{
		//	// If we haven't initialized yet, this is a no-op
		//	if (_webviewManager != null)
		//	{
		//		// Dispatch because this is going to be async, and we want to catch any errors
		//		_ = _webviewManager.Dispatcher.InvokeAsync(async () =>
		//		{
		//			var newItems = eventArgs.NewItems!.Cast<RootComponent>();
		//			var oldItems = eventArgs.OldItems!.Cast<RootComponent>();

		//			foreach (var item in newItems.Except(oldItems))
		//			{
		//				await item.AddToWebViewManagerAsync(_webviewManager);
		//			}

		//			foreach (var item in oldItems.Except(newItems))
		//			{
		//				await item.RemoveFromWebViewManagerAsync(_webviewManager);
		//			}
		//		});
		//	}
		//}
#endif
	}
}