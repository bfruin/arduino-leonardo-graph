﻿#pragma checksum "..\..\Main.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DFBA821AECF9E5585FFA30F157DCE4F3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using Microsoft.Research.DynamicDataDisplay.Charts.Shapes;
using Microsoft.Research.DynamicDataDisplay.Common.Palettes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.Navigation;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
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


namespace ArduinoGraph {
    
    
    /// <summary>
    /// Main
    /// </summary>
    public partial class Main : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter analogInGraph;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter analogOutGraph;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital01Graph;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital23Graph;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital45Graph;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital67Graph;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital89Graph;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital1011Graph;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter digital1213Graph;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageLogger;
        
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
            System.Uri resourceLocater = new System.Uri("/SimulationSample;component/main.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Main.xaml"
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
            
            #line 5 "..\..\Main.xaml"
            ((ArduinoGraph.Main)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.analogInGraph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 3:
            this.analogOutGraph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 4:
            this.digital01Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 5:
            this.digital23Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 6:
            this.digital45Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 7:
            this.digital67Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 8:
            this.digital89Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 9:
            this.digital1011Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 10:
            this.digital1213Graph = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 11:
            this.MessageLogger = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

