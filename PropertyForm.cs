using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TransparencyControlEx.Properties;

namespace TransparencyControlEx
{
    public partial class PropertyForm: Form
    {
        public PropertyForm()
        {
            InitializeComponent();
            Icon = Resources.appIcon;

            SetStyle( ControlStyles.SupportsTransparentBackColor, true );
            BackColor = Color.Transparent;

            Configurate();
        }

        private void Configurate()
        {
            // 
            // eIncTrasnpHotKey
            // 
            this.eIncTrasnpHotKey.Location = new System.Drawing.Point( 187, 19 );
            this.eIncTrasnpHotKey.Name = "eIncTrasnpHotKey";
            this.eIncTrasnpHotKey.Size = new System.Drawing.Size( 100, 20 );
            this.eIncTrasnpHotKey.TabIndex = 7;
            this.eIncTrasnpHotKey.Validating+= onHotKeyChanged;
            // 
            // eDecTrasnpHotKey
            // 
            this.eDecTrasnpHotKey.Location = new System.Drawing.Point( 187, 45 );
            this.eDecTrasnpHotKey.Name = "eDecTrasnpHotKey";
            this.eDecTrasnpHotKey.Size = new System.Drawing.Size( 100, 20 );
            this.eDecTrasnpHotKey.TabIndex = 7;
            this.eDecTrasnpHotKey.Validating+= onHotKeyChanged;
            // 
            // eSwitchGlassHotKey
            // 
            this.eSwitchGlassHotKey.Location = new System.Drawing.Point( 187, 71 );
            this.eSwitchGlassHotKey.Name = "eSwitchGlassHotKey";
            this.eSwitchGlassHotKey.Size = new System.Drawing.Size( 100, 20 );
            this.eSwitchGlassHotKey.TabIndex = 7;
            this.eSwitchGlassHotKey.Validating+= onHotKeyChanged;

            groupBox1.Controls.Add( eIncTrasnpHotKey );
            groupBox1.Controls.Add( eDecTrasnpHotKey );
            groupBox1.Controls.Add( eSwitchGlassHotKey );

            if( !TransparancyManager.CheckBlurSystemRequirements() )
            {
                namesList.Enabled = false;
                useExtendClientArea.Enabled = false;
                eSwitchGlassHotKey.Enabled = false;
            }
        }

        private void transpStep_Validated( object sender, EventArgs e )
        {
            Properties.Settings.Default.TransparancyStep = (int)transpStep.Value;
            Properties.Settings.Default.Save();
        }

        private void useExtendClientArea_CheckedChanged( object sender, EventArgs e )
        {
            Properties.Settings.Default.UseExtendClientArea = useExtendClientArea.Checked;
            Properties.Settings.Default.Save();
        }


        private void captionsList_Validating( object sender, CancelEventArgs e )
        {
            Properties.Settings.Default.Names = namesList.Text;
            Properties.Settings.Default.Save();
        }

        private void PropertyForm_Load( object sender, EventArgs e )
        {
            transpStep.Value = Properties.Settings.Default.TransparancyStep;
            useExtendClientArea.Checked = Properties.Settings.Default.UseExtendClientArea;
            namesList.Text = Properties.Settings.Default.Names;

            eIncTrasnpHotKey.Text = Properties.Settings.Default.IncTranspKey;
            eDecTrasnpHotKey.Text = Properties.Settings.Default.DecTranspKey;
            eSwitchGlassHotKey.Text = Properties.Settings.Default.SwitchGlassKey;
        }

        private void onHotKeyChanged( object sender, EventArgs e )
        {
            
            var obj = sender as HotKeyPicker;
            string str = obj.Text;
            

            if( sender == eIncTrasnpHotKey )
            {
                if( Properties.Settings.Default.IncTranspKey != str )
                    Properties.Settings.Default.IncTranspKey = str;
            }
            else if( sender == eDecTrasnpHotKey )
            {
                if( Properties.Settings.Default.DecTranspKey != str )
                    Properties.Settings.Default.DecTranspKey = str;
            }
            else
            {
                if( Properties.Settings.Default.SwitchGlassKey != str )
                    Properties.Settings.Default.SwitchGlassKey = str;
            }

            Properties.Settings.Default.Save();
        }

        HotKeyPicker eIncTrasnpHotKey = new HotKeyPicker(),
            eDecTrasnpHotKey = new HotKeyPicker(),
            eSwitchGlassHotKey = new HotKeyPicker();
    }
}
