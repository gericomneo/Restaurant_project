﻿#pragma checksum "..\..\..\..\Xaml\Waiter\OrderNew.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8FA601AE4719D4BB2832DD013F92A10F90A9B7B889E45C52E5F8E4D3614D80F9"
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
using pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Waiter;


namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Waiter {
    
    
    /// <summary>
    /// OrderNew
    /// </summary>
    public partial class OrderNew : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxTables;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxMenu;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBlockAmount;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAddItem;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxDetails;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonRemoveItem;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonConfirm;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockPrice;
        
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
            System.Uri resourceLocater = new System.Uri("/Restaurant_client_application;component/xaml/waiter/ordernew.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
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
            this.ComboBoxTables = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
            this.ComboBoxTables.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxTables_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ComboBoxMenu = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
            this.ComboBoxMenu.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxMenu_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TextBlockAmount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ButtonAddItem = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
            this.ButtonAddItem.Click += new System.Windows.RoutedEventHandler(this.ButtonAddItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ListBoxDetails = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.ButtonRemoveItem = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
            this.ButtonRemoveItem.Click += new System.Windows.RoutedEventHandler(this.ButtonRemoveItem_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\Xaml\Waiter\OrderNew.xaml"
            this.ButtonConfirm.Click += new System.Windows.RoutedEventHandler(this.ButtonConfirm_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TextBlockPrice = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

