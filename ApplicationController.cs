using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TransparencyControlEx.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Specialized;

namespace TransparencyControlEx
{
    class ApplicationController: ApplicationContext
    {
        private PropertyForm properties;
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private GlobalKeysHook hook;
        private TransparancyManager manager;
        private AutoGlasser glasser;

        int _decTr = -1,
            _incTr = -1,
            _swGlass = -1;

        public ApplicationController()
        {
            if( IsApplicationRunningAlready() )
            {
                throw new Exception( Settings.Default.AppAlreadyRunning );
            }

            Init();
        }

        private void Init()
        {
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add( Settings.Default.PropertiesAction, OnSettingsClicked );
            trayMenu.MenuItems.Add( "-" );
            trayMenu.MenuItems.Add( Settings.Default.ExitAction, OnExitClicked );

            trayIcon = new NotifyIcon();
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Icon = Resources.appIcon;
            trayIcon.Text = "Transparency Controller";
            trayIcon.Visible = true;

            properties = new PropertyForm();
            properties.FormClosing+= OnPropertiesClosing;

            manager = new TransparancyManager();
            manager.Step = (int)Properties.Settings.Default.TransparancyStep;
            manager.UseExtendClientArea = Properties.Settings.Default.UseExtendClientArea;

            hook = new GlobalKeysHook();

            try
            {
                var keys = HotKeyPicker.parseText( Properties.Settings.Default.DecTranspKey );
                _decTr = hook.RegisterHotKey( keys.modifier, keys.key );

                keys = HotKeyPicker.parseText( Properties.Settings.Default.IncTranspKey );
                _incTr = hook.RegisterHotKey( keys.modifier, keys.key );

                if( TransparancyManager.CheckBlurSystemRequirements() )
                {
                    keys = HotKeyPicker.parseText( Properties.Settings.Default.SwitchGlassKey );
                    _swGlass = hook.RegisterHotKey( keys.modifier, keys.key );
                }
            }
            catch( System.Exception ex)
            {
                MessageBox.Show( ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            if( TransparancyManager.CheckBlurSystemRequirements() )
            {
                StringCollection sc = new StringCollection();
                var strings = Properties.Settings.Default.Names.Split( new char[] { '\n' } );
                foreach( var str in strings )
                {
                    sc.Add( str );
                }

                glasser = new AutoGlasser();

                glasser.Names = sc;
                glasser.ApplyStyleToRunningProcesses();
            }

            hook.KeyPressed+= OnHotKeyPressed;
            Properties.Settings.Default.PropertyChanged+= OnApplicationPropertyChanged;
        }

        private static bool IsApplicationRunningAlready()
        {
            foreach( Process process in Process.GetProcessesByName( Process.GetCurrentProcess().ProcessName ) )
            {
                if( ( process.Id != Process.GetCurrentProcess().Id ) && ( process.ProcessName == Process.GetCurrentProcess().ProcessName ) )
                {
                    return true;
                }
            }
            return false;
        }


        void OnSettingsClicked( object sender, EventArgs e )
        {
            properties.Show();
        }
        void OnExitClicked( object sender, EventArgs e )
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
        void OnPropertiesClosing( Object sender, FormClosingEventArgs e )
        {
            if( e.CloseReason == CloseReason.UserClosing )
            {
                e.Cancel = true;
                properties.Hide();
            }
        }
        void OnHotKeyPressed( object sender, KeyPressedEventArgs e )
        {
            if( e.Id == _decTr )
            {
                manager.DoAction(
                        TransparancyManager.Action.DecTransparancy );
            }
            else if( e.Id == _incTr )
            {
                manager.DoAction(
                        TransparancyManager.Action.IncTransparancy );
            }
            else
            {
                manager.DoAction(
                        TransparancyManager.Action.SwitchGlass );
            }

        }
        void OnApplicationPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            try
            {
                KeyShortCut keys;
                switch( e.PropertyName )
                {
                    case "TransparancyStep":
                        manager.Step = (int)Properties.Settings.Default.TransparancyStep;
                        break;

                    case "UseExtendClientArea":
                        manager.UseExtendClientArea = Properties.Settings.Default.UseExtendClientArea;
                        break;

                    case "Names":
                        StringCollection sc = new StringCollection();
                        var strings = Properties.Settings.Default.Names.Split( new char[] { '\n' } );
                        foreach( var str in strings )
                        {
                            sc.Add( str );
                        }
                        glasser.Names = sc;
                        break;

                    case "DecTranspKey":
                        keys = HotKeyPicker.parseText(Properties.Settings.Default.DecTranspKey);
                        hook.UnregisterHotKey( _decTr );
                        _decTr = hook.RegisterHotKey(keys.modifier, keys.key );
                        break;

                    case "IncTranspKey":
                        keys = HotKeyPicker.parseText(Properties.Settings.Default.IncTranspKey);
                        hook.UnregisterHotKey( _incTr );
                        _incTr = hook.RegisterHotKey(keys.modifier, keys.key );
                        break;

                    case "SwitchGlassKey":
                        keys = HotKeyPicker.parseText(Properties.Settings.Default.SwitchGlassKey);
                        hook.UnregisterHotKey( _swGlass );
                        _swGlass = hook.RegisterHotKey(keys.modifier, keys.key );
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show( ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
        }
    }
}
