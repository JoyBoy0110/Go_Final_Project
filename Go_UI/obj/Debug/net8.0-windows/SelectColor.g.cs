﻿#pragma checksum "..\..\..\SelectColor.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3821563B64C9081F2B5854DFEDCFD976FEB84BC5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Go_UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Go_UI {
    
    
    /// <summary>
    /// SelectColor
    /// </summary>
    public partial class SelectColor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock KomiText;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Numpad;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlusHalf;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlusOne;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinusHalf;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinusOne;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackButton;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\SelectColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Continue;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Go_UI;component/selectcolor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SelectColor.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.KomiText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Numpad = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.PlusHalf = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\SelectColor.xaml"
            this.PlusHalf.Click += new System.Windows.RoutedEventHandler(this.PlusHalf_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PlusOne = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\SelectColor.xaml"
            this.PlusOne.Click += new System.Windows.RoutedEventHandler(this.PlusOne_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MinusHalf = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\SelectColor.xaml"
            this.MinusHalf.Click += new System.Windows.RoutedEventHandler(this.MinusHalf_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.MinusOne = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\SelectColor.xaml"
            this.MinusOne.Click += new System.Windows.RoutedEventHandler(this.MinusOne_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BackButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\SelectColor.xaml"
            this.BackButton.Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Continue = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\SelectColor.xaml"
            this.Continue.Click += new System.Windows.RoutedEventHandler(this.Continue_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

