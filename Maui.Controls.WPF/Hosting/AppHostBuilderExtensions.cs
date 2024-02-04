using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Handlers.WPF;
using Microsoft.Maui.Platform.WPF;
using Microsoft.Extensions.Logging;

namespace Microsoft.Maui.Controls.Hosting.WPF
{
	public static partial class AppHostBuilderExtensions
	{
		public static MauiAppBuilder UseMauiAppWPF<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TApp>(this MauiAppBuilder builder)
			where TApp : class, IApplication
		{
			builder.UseMauiApp<TApp>();
			builder.SetupDefaults();
			return builder;
		}

		public static MauiAppBuilder UseMauiAppWPF<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TApp>(this MauiAppBuilder builder, Func<IServiceProvider, TApp> implementationFactory)
			where TApp : class, IApplication
		{
			builder.UseMauiApp<TApp>();
			builder.SetupDefaults();
			return builder;
		}

		static IMauiHandlersCollection AddMauiControlsHandlers(this IMauiHandlersCollection handlersCollection)
		{
			handlersCollection.AddHandler<Application, ApplicationHandler>();
			handlersCollection.AddHandler<Window, WindowHandler>();
			handlersCollection.AddHandler<Label, LabelHandler>();
			handlersCollection.AddHandler<ContentPage, PageHandler>();
			handlersCollection.AddHandler<Layout, LayoutHandler>();
			handlersCollection.AddHandler<AspNetCore.Components.WebView.Maui.BlazorWebView, AspNetCore.Components.WebView.Maui.WPF.BlazorWebViewHandler>();

			return handlersCollection;
		}

		static MauiAppBuilder SetupDefaults(this MauiAppBuilder builder)
		{


#pragma warning disable CS0612, CA1416 // Type or member is obsolete, 'ResourcesProvider' is unsupported on: 'iOS' 14.0 and later
			DependencyService.Register<Platform.WPF.ResourcesProvider>();


			//DependencyService.Register<PlatformSizeService>();
			//DependencyService.Register<FontNamedSizeService>();


			builder.Services.AddSingleton<IDispatcherProvider>(svc =>
				// the DispatcherProvider might have already been initialized, so ensure that we are grabbing the
				// Current and putting it in the DI container.
				new WPFDispatcherProvider());

			builder.Services.AddScoped(svc =>
			{
				var provider = svc.GetRequiredService<IDispatcherProvider>();
				if (DispatcherProvider.SetCurrent(provider))
					svc.GetService<ILogger<Dispatcher>>()?.LogWarning("Replaced an existing DispatcherProvider with one from the service provider.");

				return Dispatcher.GetForCurrentThread()!;
			});

			builder.ConfigureImageSourceHandlers();
			builder
				.ConfigureMauiHandlers(handlers =>
				{
					handlers.AddMauiControlsHandlers();
				});

			//builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IMauiInitializeService, MauiControlsInitializer>());

			builder.RemapForControls();

			return builder;
		}

		class MauiControlsInitializer : IMauiInitializeService
		{
			public void Initialize(IServiceProvider services)
			{
			}
		}


		internal static MauiAppBuilder ConfigureImageSourceHandlers(this MauiAppBuilder builder)
		{
			//builder.ConfigureImageSources(services =>
			//{
			//	services.AddService<FileImageSource>(svcs => new FileImageSourceService(svcs.CreateLogger<FileImageSourceService>()));
			//	services.AddService<FontImageSource>(svcs => new FontImageSourceService(svcs.GetRequiredService<IFontManager>(), svcs.CreateLogger<FontImageSourceService>()));
			//	services.AddService<StreamImageSource>(svcs => new StreamImageSourceService(svcs.CreateLogger<StreamImageSourceService>()));
			//	services.AddService<UriImageSource>(svcs => new UriImageSourceService(svcs.CreateLogger<UriImageSourceService>()));
			//});

			return builder;
		}

		internal static MauiAppBuilder RemapForControls(this MauiAppBuilder builder)
		{
			// Update the mappings for IView/View to work specifically for Controls


			return builder;
		}
	}
}