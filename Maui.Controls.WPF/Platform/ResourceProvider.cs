﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Maui.Controls.Internals;

namespace Microsoft.Maui.Controls.Platform.WPF
{
#pragma warning disable CS0612 // Type or member is obsolete
	internal class ResourcesProvider : ISystemResourcesProvider
#pragma warning restore CS0612 // Type or member is obsolete
	{
		ResourceDictionary _dictionary;

		public IResourceDictionary GetSystemResources()
		{
			_dictionary = new ResourceDictionary();

			UpdateStyles();

			return _dictionary;
		}

		Style GetStyle(System.Windows.Style style, TextBlock hackbox)
		{
			hackbox.Style = style;

			var result = new Style(typeof(Label));
			result.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = hackbox.FontFamily });
			result.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = hackbox.FontSize });

			return result;
		}

		void UpdateStyles()
		{
			var textBlock = new TextBlock();
#pragma warning disable CS0612 // Type or member is obsolete
			_dictionary[Device.Styles.TitleStyleKey] = GetStyle((System.Windows.Style)System.Windows.Application.Current.Resources["HeaderTextBlockStyle"], textBlock);
			_dictionary[Device.Styles.SubtitleStyleKey] = GetStyle((System.Windows.Style)System.Windows.Application.Current.Resources["SubheaderTextBlockStyle"], textBlock);
			_dictionary[Device.Styles.BodyStyleKey] = GetStyle((System.Windows.Style)System.Windows.Application.Current.Resources["BodyTextBlockStyle"], textBlock);
			_dictionary[Device.Styles.CaptionStyleKey] = GetStyle((System.Windows.Style)System.Windows.Application.Current.Resources["CaptionTextBlockStyle"], textBlock);
			_dictionary[Device.Styles.ListItemTextStyleKey] = GetStyle((System.Windows.Style)System.Windows.Application.Current.Resources["BaseTextBlockStyle"], textBlock);
			_dictionary[Device.Styles.ListItemDetailTextStyleKey] = GetStyle((System.Windows.Style)System.Windows.Application.Current.Resources["BodyTextBlockStyle"], textBlock);
#pragma warning restore CS0612 // Type or member is obsolete
		}
	}
}