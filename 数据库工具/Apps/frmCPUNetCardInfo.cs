using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management; 


namespace Upgrade.Apps
{
    public partial class frmCPUNetCardInfo : Form
    {
        public frmCPUNetCardInfo()
        {
            InitializeComponent();
        }

        private void btnCPU_Click(object sender, EventArgs e)
        {
            String strCPUInfo = "";
            System.Collections.Hashtable objHT = new System.Collections.Hashtable();
            System.Management.ManagementObjectCollection objMOC;

            System.Management.ManagementClass objCPU = new ManagementClass("Win32_Processor");
            objMOC = objCPU.GetInstances();

            foreach(System.Management.ManagementObject mo in objMOC)
            {
                strCPUInfo = strCPUInfo + mo.Properties["ProcessorID"].Value.ToString() + "  #########  ";

            }
            this.tbCPUInfo.Text = strCPUInfo;
        }

        private void btnNetCard_Click(object sender, EventArgs e)
        {
            String strNetInfo = "";
            System.Management.ManagementObjectCollection objMOC;

            System.Management.ManagementClass objCPU = new ManagementClass("Win32_NetworkAdapterConfiguration");
            objMOC = objCPU.GetInstances();

            foreach (System.Management.ManagementObject mo in objMOC)
            {
                if ((bool)mo["IPEnabled"] == true)
                     strNetInfo = strNetInfo + mo.Properties["MacAddress"].Value.ToString() + "  #########  ";
            }

            this.tbNetCard.Text = strNetInfo;
        }

        private void btnHDInfo_Click(object sender, EventArgs e)
        {
            String strHDInfo = "";
            System.Management.ManagementObjectCollection objMOC;

            System.Management.ManagementClass objCPU = new ManagementClass("Win32_DiskDrive");
            objMOC = objCPU.GetInstances();

            foreach (System.Management.ManagementObject mo in objMOC)
            {
                strHDInfo = strHDInfo + mo.Properties["Model"].Value.ToString() + "  #########  ";
            }

            this.tbHDInfo.Text = strHDInfo;
        }

        private void btnCPUType_Click(object sender, EventArgs e)
        {
            this.TBCPUType.Text = this.GetCPUName();
        }


        /// <summary>
        /// 获取cpu名称
        /// </summary>
        /// <returns></returns>
        public string GetCPUName()
        {
            string cpuname = "";
            using (ManagementClass mc = new ManagementClass("win32_Processor"))
            {

                ManagementObjectSearcher MySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //MessageBox.Show(mo["Name"].ToString());
                    foreach (PropertyData property in mo.Properties)
                    {
                        cpuname = cpuname + property.Name + "=" + property.Value + "         \r\n";
                    }
                    
                    
                }
            }
            return cpuname;
        }


    }
}
