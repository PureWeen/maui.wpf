

namespace Microsoft.Maui.Handlers.WPF
{
	public partial class LabelHandler
	{
		public static IPropertyMapper<ILabel, LabelHandler> Mapper = new PropertyMapper<ILabel, LabelHandler>(ViewHandler.ViewMapper)
		{
			//[nameof(ILabel.Background)] = MapBackground,
			//[nameof(ILabel.Height)] = MapHeight,
			//[nameof(ILabel.Opacity)] = MapOpacity,
			//[nameof(ITextStyle.CharacterSpacing)] = MapCharacterSpacing,
			//[nameof(ITextStyle.Font)] = MapFont,
			//[nameof(ITextAlignment.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
			//[nameof(ITextAlignment.VerticalTextAlignment)] = MapVerticalTextAlignment,
			//[nameof(ILabel.LineHeight)] = MapLineHeight,
			//[nameof(ILabel.Padding)] = MapPadding,
			[nameof(ILabel.Text)] = MapText,
			//[nameof(ITextStyle.TextColor)] = MapTextColor,
			//[nameof(ILabel.TextDecorations)] = MapTextDecorations,
		};

		public static CommandMapper<ILabel, ILabelHandler> CommandMapper = new(ViewCommandMapper)
		{
		};

		public LabelHandler() : base(Mapper)
		{
		}

		public LabelHandler(IPropertyMapper? mapper)
			: base(mapper ?? Mapper, CommandMapper)
		{
		}

		public LabelHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
			: base(mapper ?? Mapper, commandMapper ?? CommandMapper)
		{
		}
	}
}