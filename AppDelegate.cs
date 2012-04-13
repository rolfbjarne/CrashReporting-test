using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace CrashReporting
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		DialogViewController dvc;

		[DllImport ("libc")]
		static extern int strlen (IntPtr str);
		public void NativeCrash ()
		{
			strlen (IntPtr.Zero);
		}
		
		public void UnhandledException ()
		{
			throw new Exception ("Unhandled I am!");
		}
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			dvc = new DialogViewController (
				new MonoTouch.Dialog.RootElement ("Root")
				{
					new Section ("Crash reporting tester") {
						new StyledStringElement ("Native crash", NativeCrash),
						new StyledStringElement ("Unhandled exception", UnhandledException),
					}
				}
			);
			
			dvc.Autorotate = true;
			dvc.ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation.Portrait);
			
			window.RootViewController = dvc;
			
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

