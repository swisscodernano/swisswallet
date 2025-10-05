using Avalonia.Media;

namespace WalletWasabi.Fluent.Helpers;

/// <summary>
/// Swiss-themed color palette for SwissWallet branding.
/// Based on official Swiss flag colors and Swiss design principles.
/// </summary>
public static class SwissColors
{
	/// <summary>
	/// Swiss Red - Primary brand color (official Swiss flag red: Pantone 485 C)
	/// RGB: 220, 20, 60 / HEX: #DC143C
	/// </summary>
	public static readonly Color SwissRed = Color.FromRgb(220, 20, 60);

	/// <summary>
	/// Swiss Gold - Accent color for highlights and important UI elements
	/// RGB: 255, 215, 0 / HEX: #FFD700
	/// </summary>
	public static readonly Color SwissGold = Color.FromRgb(255, 215, 0);

	/// <summary>
	/// Pure White - Primary text and Swiss cross color
	/// RGB: 255, 255, 255 / HEX: #FFFFFF
	/// </summary>
	public static readonly Color PureWhite = Color.FromRgb(255, 255, 255);

	/// <summary>
	/// Dark Charcoal - Background and text for light themes
	/// RGB: 44, 44, 44 / HEX: #2C2C2C
	/// </summary>
	public static readonly Color DarkCharcoal = Color.FromRgb(44, 44, 44);

	/// <summary>
	/// Light Gray - Subtle backgrounds and borders
	/// RGB: 240, 240, 240 / HEX: #F0F0F0
	/// </summary>
	public static readonly Color LightGray = Color.FromRgb(240, 240, 240);

	/// <summary>
	/// Medium Gray - Secondary text and disabled states
	/// RGB: 128, 128, 128 / HEX: #808080
	/// </summary>
	public static readonly Color MediumGray = Color.FromRgb(128, 128, 128);

	/// <summary>
	/// Success Green - Positive actions and confirmations (Swiss-compatible)
	/// RGB: 76, 175, 80 / HEX: #4CAF50
	/// </summary>
	public static readonly Color SuccessGreen = Color.FromRgb(76, 175, 80);

	/// <summary>
	/// Warning Orange - Caution and important notices (Swiss-compatible)
	/// RGB: 255, 152, 0 / HEX: #FF9800
	/// </summary>
	public static readonly Color WarningOrange = Color.FromRgb(255, 152, 0);

	/// <summary>
	/// Error Red - Errors and critical warnings (slightly darker than Swiss Red)
	/// RGB: 198, 40, 40 / HEX: #C62828
	/// </summary>
	public static readonly Color ErrorRed = Color.FromRgb(198, 40, 40);

	// Brushes for convenience
	public static readonly SolidColorBrush SwissRedBrush = new(SwissRed);
	public static readonly SolidColorBrush SwissGoldBrush = new(SwissGold);
	public static readonly SolidColorBrush PureWhiteBrush = new(PureWhite);
	public static readonly SolidColorBrush DarkCharcoalBrush = new(DarkCharcoal);
	public static readonly SolidColorBrush LightGrayBrush = new(LightGray);
	public static readonly SolidColorBrush MediumGrayBrush = new(MediumGray);
	public static readonly SolidColorBrush SuccessGreenBrush = new(SuccessGreen);
	public static readonly SolidColorBrush WarningOrangeBrush = new(WarningOrange);
	public static readonly SolidColorBrush ErrorRedBrush = new(ErrorRed);
}
