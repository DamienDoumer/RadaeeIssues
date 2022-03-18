﻿using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace RadaeePDFIssue.iOS
{
    public static class Utilities
    {
        public static UIWindow GetKeyWindow(this UIApplication application)
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                return application.KeyWindow; // deprecated in iOS 13

            var window = application
                .ConnectedScenes
                .ToArray()
                .OfType<UIWindowScene>()
                .SelectMany(scene => scene.Windows)
                .FirstOrDefault(w => w.IsKeyWindow);

            return window;
        }

        public static UIViewController GetTopViewController()
        {
            var window = UIApplication.SharedApplication.GetKeyWindow();
            var vc = window?.RootViewController;
            while (vc is { PresentedViewController: { } })
                vc = vc.PresentedViewController;

            if (vc is UINavigationController { ViewControllers: { } } navController)
                vc = navController.ViewControllers.Last();

            return vc;
        }
    }
}