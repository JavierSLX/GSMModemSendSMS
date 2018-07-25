using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSMMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort sp = new SerialPort();
                sp.PortName = txtPort.Text;
                sp.Open();
                sp.WriteLine("AT" + Environment.NewLine);

                Thread.Sleep(100);
                sp.WriteLine("AT+CMGF=1" + Environment.NewLine);

                Thread.Sleep(100);
                sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);

                Thread.Sleep(100);
                sp.WriteLine("AT+CMGS=\"" + txtNumber.Text + "\"" + Environment.NewLine);

                Thread.Sleep(100);
                sp.WriteLine(txtMessage.Text.Trim() + Environment.NewLine);

                Thread.Sleep(100);
                sp.Write(new byte[] { 26 }, 0, 1);

                Thread.Sleep(100);
                var response = sp.ReadExisting();
                if (response.Contains("ERROR"))
                    MessageBox.Show("Send failed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("SMS Sent!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                sp.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
