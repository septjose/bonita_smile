﻿#pragma checksum "..\..\..\Interfaz\Agregar_Estudios.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "936436D6D151583F9BB3317D85029B7D339A281204E07C2B30B4C5AEBD79B76B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
using bonita_smile_v1;


namespace bonita_smile_v1 {
    
    
    /// <summary>
    /// Pagina_Agregar_Estudios
    /// </summary>
    public partial class Pagina_Agregar_Estudios : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Interfaz\Agregar_Estudios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lista;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Interfaz\Agregar_Estudios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rt_imagen;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Interfaz\Agregar_Estudios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar pb_imagen;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Interfaz\Agregar_Estudios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNombre;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Interfaz\Agregar_Estudios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblcontador;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Interfaz\Agregar_Estudios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCantidad;
        
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/agregar_estudios.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Interfaz\Agregar_Estudios.xaml"
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
            this.lista = ((System.Windows.Controls.ListBox)(target));
            
            #line 15 "..\..\..\Interfaz\Agregar_Estudios.xaml"
            this.lista.DragEnter += new System.Windows.DragEventHandler(this.lista_DragEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Interfaz\Agregar_Estudios.xaml"
            this.lista.Drop += new System.Windows.DragEventHandler(this.lista_Drop);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Interfaz\Agregar_Estudios.xaml"
            this.lista.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lista_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.rt_imagen = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 3:
            
            #line 17 "..\..\..\Interfaz\Agregar_Estudios.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\..\Interfaz\Agregar_Estudios.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.pb_imagen = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 6:
            this.lblNombre = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblcontador = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lblCantidad = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

