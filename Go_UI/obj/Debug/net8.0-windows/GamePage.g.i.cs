﻿#pragma checksum "..\..\..\GamePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41A567AC436ECFF9502C95DB76CC98B89A4F1086"
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
    /// GamePage
    /// </summary>
    public partial class GamePage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Game;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid top;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock text_block;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Resign;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid middle;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Board;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid PlacingGrid;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid PiecesGrid;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid buttom;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\GamePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button passButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Go_UI;V1.0.0.0;component/gamepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GamePage.xaml"
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
            this.Game = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.top = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.text_block = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Resign = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\GamePage.xaml"
            this.Resign.Click += new System.Windows.RoutedEventHandler(this.Resign_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.middle = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.Board = ((System.Windows.Controls.Grid)(target));
            
            #line 40 "..\..\..\GamePage.xaml"
            this.Board.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Board_MouseDown);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\GamePage.xaml"
            this.Board.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Board_MouseLeave);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\GamePage.xaml"
            this.Board.MouseMove += new System.Windows.Input.MouseEventHandler(this.Image_MouseMove);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PlacingGrid = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 8:
            this.PiecesGrid = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 9:
            this.buttom = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.passButton = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\GamePage.xaml"
            this.passButton.Click += new System.Windows.RoutedEventHandler(this.Pass_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

