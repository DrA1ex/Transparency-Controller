using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Specialized;
using TransparencyControlEx.Properties;

namespace TransparencyControlEx
{
    class AutoGlasser: NativeWindow, IDisposable
    {
        public AutoGlasser()
        {
            this.CreateHandle( new CreateParams() );

            if( TransparancyManager.CheckBlurSystemRequirements() )
            {
                SetUpHook();
            }
        }

        protected override void WndProc( ref Message m )
        {
            switch( (int)m.WParam )
            {
                case NativeMethods.HSHELL_WINDOWACTIVATED:
                    NativeMethods.EmptyWorkingSet( Process.GetCurrentProcess().Handle );
                    break;

                case NativeMethods.HSHELL_WINDOWCREATED:
                    if( IsNeedBlur( (IntPtr)m.LParam ) )
                    {
                        if( Properties.Settings.Default.UseExtendClientArea )
                            TransparancyManager.ExtendClientArea( (IntPtr)m.LParam, true );
                        else
                            TransparancyManager.BlurWindow( (IntPtr)m.LParam, true );
                    }

                    break;

                default:
                    base.WndProc( ref m );
                    break;
            }
        }

        bool IsNeedBlur( IntPtr hWnd )
        {
            uint num;
            NativeMethods.GetWindowThreadProcessId( hWnd, out num );
            return IsNeedBlur( Process.GetProcessById( (int)num ) );
        }
        bool IsNeedBlur( Process process )
        {
            string str= process.ProcessName.ToLower();
            foreach( var i in names )
            {
                if( i.Trim().ToLower().Equals( str ) )
                    return true;
            }

            return false;
        }

        private void SetUpHook()
        {
            hookId = NativeMethods.RegisterWindowMessage( "SHELLHOOK" );
            if( NativeMethods.RegisterShellHookWindow( Handle ) == 0 )
                throw new InvalidOperationException( Settings.Default.ErrorSetUpHook);
        }

        public void ApplyStyleToRunningProcesses()
        {
            foreach( Process process in Process.GetProcesses() )
            {
                if( IsNeedBlur( process ) )
                {
                    NativeMethods.EnumWindows( new NativeMethods.EnumWindowsCallback( CallbackFunction ), process.Id );
                }
            }

        }

        private bool CallbackFunction( IntPtr hWnd, int lParam )
        {
            if(IsNeedBlur(hWnd))
            if( Properties.Settings.Default.UseExtendClientArea )
                TransparancyManager.ExtendClientArea( hWnd, true );
            else
                TransparancyManager.BlurWindow( hWnd, true );
            return true;
        }



        int hookId;

        public void Dispose()
        {
            NativeMethods.DeregisterShellHookWindow( Handle );

            this.DestroyHandle();
        }

        StringCollection names;

        public StringCollection Names
        {
            get { return names; }
            set { names = value; ApplyStyleToRunningProcesses(); }
        }
    }
}
