﻿#pragma checksum "..\..\..\Componentes\ProdutoFormUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "73DD6E0C81DCC5EB5E4FE4AE452E307521F40E8903957B87E8AC9082D3B482FF"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using NovoTayUmDoce.Componentes;
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


namespace NovoTayUmDoce.Componentes {
    
    
    /// <summary>
    /// ProdutoFormUC
    /// </summary>
    public partial class ProdutoFormUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btCancelar;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btSalvar;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNOme;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDescricao;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTipoPro;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPeso;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Componentes\ProdutoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbValorVenda;
        
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
            System.Uri resourceLocater = new System.Uri("/NovoTayUmDoce;component/componentes/produtoformuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Componentes\ProdutoFormUC.xaml"
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
            this.btCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Componentes\ProdutoFormUC.xaml"
            this.btCancelar.Click += new System.Windows.RoutedEventHandler(this.btCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btSalvar = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Componentes\ProdutoFormUC.xaml"
            this.btSalvar.Click += new System.Windows.RoutedEventHandler(this.btSalvar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbNOme = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbDescricao = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbTipoPro = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbPeso = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tbValorVenda = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
