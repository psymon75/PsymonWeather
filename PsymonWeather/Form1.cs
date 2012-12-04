using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PsymonWeather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void reloadInfos()
        {
            
            YTemperature temp = YTemperature.FindTemperature("temperature");
            progressTemp.Value = (int)temp.get_currentValue();
            lblTemp.Text = string.Format("{0} °C", temp.get_currentValue());
            YHumidity humid = YHumidity.FindHumidity("humidity");
            lblHumid.Text = string.Format("{0} % d'humidité", humid.get_currentValue());
            YPressure pressure = YPressure.FindPressure("pressure");
            lblPressure.Text = string.Format("{0} Bar", pressure.get_currentValue());
            notifyIcon1.Text = string.Format("PsymonWeather Beta\nTemp. : {0} °C\nHumid. : {1}\nPress. : {2} Bar", temp.get_currentValue(),humid.get_currentValue(), pressure.get_currentValue());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YModule m;
            string errmsg = "";
            if (YAPI.RegisterHub("usb", ref errmsg) == YAPI.SUCCESS)
            {
                notifyIcon1.ShowBalloonTip(5,"PsymonWeather Beta","Connection effectuée",ToolTipIcon.Info);
                timer1.Enabled = true;
                btnConnexion.Enabled = false;
                btnDeco.Enabled = true;
                toolStripStatusLabel1.Text = "Connecté : Oui";
                m = YModule.FindModule("PsymonWeather");
                lblSerial.Text = m.get_firmwareRelease().ToString();
                progressTemp.Minimum = 0;
                progressTemp.Maximum = 40;
                YTemperature temp = YTemperature.FindTemperature("temperature");
                progressTemp.Value = (int)temp.get_currentValue();
                lblTemp.Text = string.Format("{0} °C", temp.get_currentValue());
                YHumidity humid = YHumidity.FindHumidity("humidity");
                lblHumid.Text = string.Format("{0} % d'humidité", humid.get_currentValue());
                YPressure pressure = YPressure.FindPressure("pressure");
                lblPressure.Text = string.Format("{0} Bar", pressure.get_currentValue());
                notifyIcon1.Text = string.Format("PsymonWeather Beta\nTemp. : {0}\nHumid. : {1}\nPress. : {2}", temp.get_currentValue(), humid.get_currentValue(), pressure.get_currentValue());
            }
            else
            {
                toolStripStatusLabel1.Text = "Connecté : Erreur";
                MessageBox.Show("Le module n'est pas branché.");
            }

        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Développé par Simon Menetrey");
        }

        private void btnDeco_Click(object sender, EventArgs e)
        {
            btnConnexion.Enabled = true;
            btnDeco.Enabled = false;
            YAPI.UnregisterHub("usb");
            YAPI.FreeAPI();
            timer1.Enabled = false;
            toolStripStatusLabel1.Text = "Connecté : Non";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            reloadInfos();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveCurrentValues_Click(object sender, EventArgs e)
        {
            
        }

    }
}