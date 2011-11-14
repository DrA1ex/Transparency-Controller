using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TransparencyControlEx.Properties;

namespace TransparencyControlEx
{
    class HotKeyPicker: TextBox
    {
        public HotKeyPicker()
        {
            ReadOnly = true;
            ContextMenu = new ContextMenu();
            BackColor = Color.White;
            Text = "Нет";
        }

        public bool isEmpty()
        {
            return Text == "Нет";
        }

        public static KeyShortCut parseText( string str )
        {
            KeyShortCut keys = new KeyShortCut();

            if( str != Settings.Default.HKPickerNothing )
                foreach( var key in str.Split( new[] { '+' }, StringSplitOptions.RemoveEmptyEntries ) )
                {
                    switch( key.Trim() )
                    {
                        case "Ctrl":
                            keys.modifier|= global::ModifierKeys.Control;
                            break;
                        case "Shift":
                            keys.modifier|= global::ModifierKeys.Shift;
                            break;
                        case "Alt":
                            keys.modifier|= global::ModifierKeys.Alt;
                            break;
                        default:
                            foreach( var val in Enum.GetValues( typeof( Keys ) ) )
                            {
                                if( key == val.ToString() )
                                {
                                    keys.key= (Keys)val;
                                    goto ok;
                                }

                            }
                            throw new InvalidOperationException( Settings.Default.HKPickerKeyIsUnknown + key );
ok:
                            break;
                    }
                }

            return keys;
        }
        public static string parseKeys( KeyEventArgs e )
        {
            string text = null;

            if( e.Control )
            {
                text += "Ctrl+";
            }
            if( e.Shift )
            {
                text += "Shift+";
            }
            if( e.Alt )
            {
                text += "Alt+";
            }

            if( text == null )
            {
                text = "Alt+";
            }

            if( e.KeyCode == Keys.ControlKey 
                || e.KeyCode == Keys.ShiftKey 
                || e.KeyCode == Keys.Menu )
            {
                return Settings.Default.HKPickerNothing;
            }

            text += e.KeyCode.ToString();

            return text;
        }
        public static string parseKeys( KeyShortCut e )
        {
            return parseKeys( new KeyEventArgs( e.key | (Keys)e.modifier ) );
        }

        protected override void OnKeyDown( KeyEventArgs e )
        {
            base.Text = parseKeys( e );
        }
    }

    internal struct KeyShortCut
    {
        public KeyShortCut( Keys k, ModifierKeys m )
        {
            key = k;
            modifier = m;
        }

        public ModifierKeys modifier;
        public Keys key;
    }
}
