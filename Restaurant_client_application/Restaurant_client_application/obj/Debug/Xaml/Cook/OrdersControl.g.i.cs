﻿#pragma checksum "..\..\..\..\Xaml\Cook\OrdersControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C446B3330487ADCC90FB2BFBB8D907D66922ECC28E0EE4322AEDB8B029F8BA3B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Cook;


namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Cook {
    
    
    /// <summary>
    /// OrdersControl
    /// </summary>
    public partial class OrdersControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Xaml\Cook\OrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxOrders;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Xaml\Cook\OrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxDetails;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Xaml\Cook\OrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonChangeStateReady;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Restaurant_client_application;component/xaml/cook/orderscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Xaml\Cook\OrdersControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ListBoxOrders = ((System.Windows.Controls.ListBox)(target));
            
            #line 28 "..\..\..\..\Xaml\Cook\OrdersControl.xaml"
            this.ListBoxOrders.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBoxOrders_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ListBoxDetails = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.ButtonChangeStateReady = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\Xaml\Cook\OrdersControl.xaml"
            this.ButtonChangeStateReady.Click += new System.Windows.RoutedEventHandler(this.ButtonChangeStateReady_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

