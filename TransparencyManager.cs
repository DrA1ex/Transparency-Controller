using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TransparencyControlEx
{
    class TransparancyManager
    {
        internal enum Action { DecTransparancy, IncTransparancy, SwitchGlass };

        private Dictionary<IntPtr, int> transpDict = new Dictionary<IntPtr,int>();
        private Dictionary<IntPtr, bool> glassDict = new Dictionary<IntPtr, bool>();

        private int transpStep = 20;
        private const int minTasp = 50;

        private bool useExtendClientArea;

        public int Step
        {
            get { return transpStep; }
            set { transpStep = value; }
        }
        public bool UseExtendClientArea
        {
            get { return useExtendClientArea; }
            set { useExtendClientArea = value;}
        }

        public void DoAction( Action act )
        {
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            switch( act )
            {
                case Action.DecTransparancy:
                    UpdateTransparancy( hWnd, transpStep );
                    SetTransparancy( hWnd, transpDict[hWnd] );
                    break;

                case Action.IncTransparancy:
                    UpdateTransparancy( hWnd, -transpStep );
                    SetTransparancy( hWnd, transpDict[hWnd] );
                    break;

                case Action.SwitchGlass:
                    UpdateGlassEffect( hWnd );
                    if( UseExtendClientArea )
                        ExtendClientArea( hWnd, glassDict[hWnd] );
                    else
                        BlurWindow( hWnd, glassDict[hWnd] );
                    break;
            }
        }

#region work functions

        private void UpdateTransparancy(IntPtr hWnd , int value )
        {
            if( !transpDict.ContainsKey( hWnd ) )
            {
                transpDict.Add( hWnd, NativeMethods.MAX_TRANSPARANCY );
                // set window layered. This is need to set transparancy
                NativeMethods.SetWindowLong( hWnd, NativeMethods.GWL_EXSTYLE,
                    NativeMethods.GetWindowLong( hWnd, NativeMethods.GWL_EXSTYLE ) 
                    | NativeMethods.WS_EX_LAYERED );
            }

            int tmp = transpDict[hWnd] + value;
            if( tmp > NativeMethods.MAX_TRANSPARANCY )
                tmp = NativeMethods.MAX_TRANSPARANCY;
            else if( tmp < minTasp )
                tmp = minTasp;

            transpDict[hWnd] = tmp;

            ClearOldTranspHWND();
        }
        private void UpdateGlassEffect( IntPtr hWnd )
        {
            if( !glassDict.ContainsKey( hWnd ) )
            {
                glassDict.Add( hWnd, false );
            }

            glassDict[hWnd] = !glassDict[hWnd];

            ClearOldGlassHWND();
        }

        private void ClearOldTranspHWND()
        {
            if( transpDict.Count > 0 )
            {
            repeat:
                foreach( var i in transpDict )
                {
                    if( !NativeMethods.IsWindow( i.Key ) )
                    {
                        transpDict.Remove( i.Key );
                        goto repeat;
                    }
                }
            }
        }
        private void ClearOldGlassHWND()
        {
            if( glassDict.Count > 0 )
            {
            repeat:
                foreach( var i in glassDict )
                {
                    if( !NativeMethods.IsWindow( i.Key ) )
                    {
                        glassDict.Remove( i.Key );
                        goto repeat;
                    }
                }
            }
        }

        public static void SetTransparancy( IntPtr hWnd, int value )
        {
            NativeMethods.SetLayeredWindowAttributes( hWnd, 0,
                (byte)value, NativeMethods.LWA_ALPHA );
        }
        internal static bool CheckBlurSystemRequirements()
        {
            if( Environment.OSVersion.Version.Major >= 6 )
                if( NativeMethods.DwmIsCompositionEnabled() )
                    return true;
            return false;
        }
        public static void BlurWindow( IntPtr hWnd, bool enabled )
        {
            if( NativeMethods.DwmIsCompositionEnabled() )
            {
                NativeMethods.DWM_BLURBEHIND dwm_blurbehind;
                dwm_blurbehind.dwFlags = NativeMethods.DWM_BB.Enable;
                dwm_blurbehind.fEnable = enabled;
                dwm_blurbehind.fTransitionOnMaximized = false;
                dwm_blurbehind.hRgnBlur = IntPtr.Zero;
                NativeMethods.DwmEnableBlurBehindWindow( hWnd, ref dwm_blurbehind );
            }
            else
            {
                Trace.WriteLine( "composition is not enabled in BlurWindow()" );
            }
        }
        public static void ExtendClientArea( IntPtr hWnd, bool enabled )
        {
            if( NativeMethods.DwmIsCompositionEnabled() )
            {
                NativeMethods.MARGINS m = new NativeMethods.MARGINS();
                int value = enabled? -1 : 0;
                m.cxLeftWidth = value;
                m.cxRightWidth = value;
                m.cyBottomHeight = value;
                m.cyTopHeight = value;
                // -1 means that client area will be full extended. 0 means that effect will be turn off

                NativeMethods.DwmExtendFrameIntoClientArea( hWnd, m );
            }
            else
            {
                Trace.WriteLine( "composition is not enabled in ExtendClientArea()" );
            }
        }
#endregion

    }
}
