﻿using System;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;

namespace MobileAzureDevDaysSampleApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
