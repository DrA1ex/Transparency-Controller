using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TransparencyControlEx.Properties;

namespace TransparencyControlEx
{
    class GlobalKeysHook: IDisposable
    {
        internal class Window : NativeWindow, IDisposable
        {
            public Window()
            {
                this.CreateHandle( new CreateParams() );
            }

            protected override void WndProc( ref Message m )
            {
                base.WndProc( ref m );

                // is a hot key pressed
                if( m.Msg == NativeMethods.WM_HOTKEY )
                {
                    // get the hot key
                    Keys key = (Keys)( ( (int)m.LParam >> 16 ) & 0xFFFF );
                    ModifierKeys modifier = (ModifierKeys)( (int)m.LParam & 0xFFFF );

                    // invoke the event to  inform the parent
                    if( KeyPressed != null )
                        KeyPressed( this, new KeyPressedEventArgs( modifier, key, (int)m.WParam ) );
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                this.DestroyHandle();
            }
        }

        private Window _window = new Window();
        private int _currentId;

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public GlobalKeysHook()
        {
            _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                    KeyPressed(this, args);
            };
        }

        public int RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            _currentId = _currentId + 1;
 
            // register a hot key
            if( !NativeMethods.RegisterHotKey( _window.Handle, _currentId, (uint)modifier, (uint)key ) )
                throw new InvalidOperationException(Settings.Default.UnableToRegisterHotKey 
                    + HotKeyPicker.parseKeys(new KeyShortCut(key, modifier)));
            return _currentId;
        }

        public void UnregisterHotKey( int id )
        {
            NativeMethods.UnregisterHotKey( _window.Handle, id );
        }
 
        public void Dispose()
        {
            // unregister all registered hot keys
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey( i );
            }
 
            // destroy inner native-window
            _window.Dispose();
        }
    }
}

public class KeyPressedEventArgs: EventArgs
{
    private ModifierKeys _modifier;
    private Keys _key;
    private int _id;

    internal KeyPressedEventArgs( ModifierKeys modifier, Keys key, int id )
    {
        _modifier = modifier;
        _key = key;
        _id = id;
    }

    public ModifierKeys Modifier
    {
        get { return _modifier; }
    }

    public Keys Key
    {
        get { return _key; }
    }

    public int Id
    {
        get {return _id; }
    }
}

[Flags]
public enum ModifierKeys: uint
{
    Alt = 1,
    Control = 2,
    Shift = 4,
    Win = 8
}