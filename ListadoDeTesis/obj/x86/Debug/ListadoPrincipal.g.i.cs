﻿#pragma checksum "..\..\..\ListadoPrincipal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7B24CF05C2F493A695D3EDB5C37B75FC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ListadoDeTesis.Converters;
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
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Legend;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Controls.RibbonView;
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeListView;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Data;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using Telerik.Windows.Input.Touch;
using Telerik.Windows.Shapes;


namespace ListadoDeTesis {
    
    
    /// <summary>
    /// ListadoPrincipal
    /// </summary>
    public partial class ListadoPrincipal : Telerik.Windows.Controls.RadWindow, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 25 "..\..\..\ListadoPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadRibbonButton BtnAddTesis;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\ListadoPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadRibbonButton BtnMisTesis;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\ListadoPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadRibbonButton BtnTodasTesis;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\ListadoPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadRibbonButton BtnVerEnvios;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\ListadoPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadRibbonButton BtnPrint;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\ListadoPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadGridView GTesis;
        
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
            System.Uri resourceLocater = new System.Uri("/ListadoDeTesis;component/listadoprincipal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ListadoPrincipal.xaml"
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
            
            #line 9 "..\..\..\ListadoPrincipal.xaml"
            ((ListadoDeTesis.ListadoPrincipal)(target)).Loaded += new System.Windows.RoutedEventHandler(this.RadWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtnAddTesis = ((Telerik.Windows.Controls.RadRibbonButton)(target));
            
            #line 25 "..\..\..\ListadoPrincipal.xaml"
            this.BtnAddTesis.Click += new System.Windows.RoutedEventHandler(this.RadRibbonButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnMisTesis = ((Telerik.Windows.Controls.RadRibbonButton)(target));
            
            #line 35 "..\..\..\ListadoPrincipal.xaml"
            this.BtnMisTesis.Click += new System.Windows.RoutedEventHandler(this.BtnMisTesis_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnTodasTesis = ((Telerik.Windows.Controls.RadRibbonButton)(target));
            
            #line 43 "..\..\..\ListadoPrincipal.xaml"
            this.BtnTodasTesis.Click += new System.Windows.RoutedEventHandler(this.BtnTodasTesis_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnVerEnvios = ((Telerik.Windows.Controls.RadRibbonButton)(target));
            
            #line 51 "..\..\..\ListadoPrincipal.xaml"
            this.BtnVerEnvios.Click += new System.Windows.RoutedEventHandler(this.VerEnvios_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnPrint = ((Telerik.Windows.Controls.RadRibbonButton)(target));
            
            #line 53 "..\..\..\ListadoPrincipal.xaml"
            this.BtnPrint.Click += new System.Windows.RoutedEventHandler(this.BtnPrint_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GTesis = ((Telerik.Windows.Controls.RadGridView)(target));
            
            #line 71 "..\..\..\ListadoPrincipal.xaml"
            this.GTesis.DataLoaded += new System.EventHandler<System.EventArgs>(this.GTesis_DataLoaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 8:
            
            #line 103 "..\..\..\ListadoPrincipal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnValidaFecha_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 114 "..\..\..\ListadoPrincipal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnAclaraFecha_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

