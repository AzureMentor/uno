﻿using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Uno.UI.Samples.Controls;

namespace UITests.Windows_UI_Xaml_Controls.ThumbTests
{
	[Sample]
	public sealed partial class Thumb_DragEvents : Page
	{
		public Thumb_DragEvents()
		{
			this.InitializeComponent();
		}

		private void OnThumbDragStarted(object sender, DragStartedEventArgs e)
		{
			DragStartedOutput.Text = $"@x={e.HorizontalOffset:F2},@y={e.VerticalOffset:F2}";
		}

		private void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
		{
#if XAMARIN // Total properties are uno only
			DragDeltaOutput.Text = $"Δx={e.HorizontalChange:F2},Δy={e.VerticalChange:F2}|Σx={e.TotalHorizontalChange:F2},Σy={e.TotalVerticalChange:F2}";
#else
			DragDeltaOutput.Text = $"Δx={e.HorizontalChange:F2},Δy={e.VerticalChange:F2}|Σx=0.0,Σy=0.0";
#endif
		}

		private void OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
		{
#if XAMARIN // Total properties are uno only
			DragCompletedOutput.Text = $"Δx={e.HorizontalChange:F2},Δy={e.VerticalChange:F2}|Σx={e.TotalHorizontalChange:F2},Σy={e.TotalVerticalChange:F2}";
#else
			DragCompletedOutput.Text = $"Δx={e.HorizontalChange:F2},Δy={e.VerticalChange:F2}|Σx=0.0,Σy=0.0";
#endif
		}
	}
}
