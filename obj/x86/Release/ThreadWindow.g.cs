﻿#pragma checksum "C:\Users\bartl_000\Documents\GitHub\STC\ThreadWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "869498F693CC1E0751729AA8992DFA15"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STC
{
    partial class ThreadWindow : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.title = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2:
                {
                    this.pinTile = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 26 "..\..\..\ThreadWindow.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.pinTile).Click += this.button3_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.deletePost = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 27 "..\..\..\ThreadWindow.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.deletePost).Click += this.button3_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.textBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5:
                {
                    this.button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 33 "..\..\..\ThreadWindow.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.button).Click += this.button_Click;
                    #line default
                }
                break;
            case 6:
                {
                    this.button1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 34 "..\..\..\ThreadWindow.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.button1).Click += this.button1_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.button2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 35 "..\..\..\ThreadWindow.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.button2).Click += this.button2_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.content = (global::Windows.UI.Xaml.Controls.RichEditBox)(target);
                }
                break;
            case 9:
                {
                    this.textBlock1_Copy = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10:
                {
                    this.dataList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 38 "..\..\..\ThreadWindow.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.dataList).DoubleTapped += this.dataList_DoubleClick;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

