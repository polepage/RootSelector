﻿using System;

using Android.App;
using Android.Runtime;
using Droid.Utils.Unity;
using Prism.Ioc;
using Prism.Modularity;

namespace RootSelector
{
    [Application]
    public class RootApp : DroidApp
    {
        public RootApp(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
        }
    }
}