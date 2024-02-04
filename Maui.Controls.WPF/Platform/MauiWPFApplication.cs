using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.WPF;
using Application = System.Windows.Application;

namespace Microsoft.Maui.Platform.WPF
{
	public abstract class MauiWPFApplication : Application, IPlatformApplication
	{
		protected abstract MauiApp CreateMauiApp();


		protected override void OnStartup(StartupEventArgs args)
		{
			base.OnStartup(args);

			// Windows running on a different thread will "launch" the app again
			//if (Application != null)
			//{
			//	Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunching>(del => del(this, args));
			//	Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunched>(del => del(this, args));
			//	return;
			//}

			IPlatformApplication.Current = this;
			var mauiApp = CreateMauiApp();

			var rootContext = new WPFMauiContext(mauiApp.Services);

			var applicationContext = rootContext.MakeApplicationScope(this);

			Services = applicationContext.Services;

			//Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunching>(del => del(this, args));

			Application = Services.GetRequiredService<IApplication>();

			this.SetApplicationHandler(Application, applicationContext);

			this.CreatePlatformWindow(Application, args);

			//Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunched>(del => del(this, args));
		}

		public static new MauiWPFApplication Current => (MauiWPFApplication)System.Windows.Application.Current;

		public IServiceProvider Services { get; protected set; } = null!;

		public IApplication Application { get; protected set; } = null!;
	}
}
