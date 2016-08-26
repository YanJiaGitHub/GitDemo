using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DispatchClientDemo
{
    public partial class FrmTranCall : Form
    {
        private string strEmployee_A = "";
        private string strEmployee_B = "";
        private string strEmployee_C = "";
        private string strEmployee_D = "";
        private string strCallId_1 = "";
        private string strCallId_2 = "";

        public FrmTranCall()
        {
            InitializeComponent();
        }        

        private void btnTranCall_Click(object sender, EventArgs e)
        {
            bool blCanTran = false;
            strEmployee_A = this.txtEmployee_A.Text.ToString().Trim();
            strEmployee_C = this.txtEmployee_C.Text.ToString().Trim();
            string strTemp_1 = Base.myClient.DTGetEmployeeSession(strEmployee_A);            
            if (strTemp_1 != "")
            {
                string[] strCalls_1 = strTemp_1.Split('|');
                strEmployee_B = ((strEmployee_A == strCalls_1[0]) ? strCalls_1[1] : strCalls_1[0]);
                strCallId_1 = strCalls_1[2];                
                if (strCallId_1 != "")
                {
                    blCanTran = true;                                   
                }
            }
            if (blCanTran)
            {
                Base.myClient.DTTranCall(strCallId_1, strEmployee_A, strEmployee_C);
                string strInfo = String.Format("DTC->DTS; KEY=TRANCALL; CallId={0}; Employee_A={1}; Employee_C={2}",
                        strCallId_1, strEmployee_A, strEmployee_C);
                //ShowDebugInfo(strInfo);   
            }
            else
            {
                string strInfo = "Error: Msg=" + strEmployee_A + "当前并未通话，无法转接!";
                MessageBox.Show(strInfo);
                return;
            }            
            strEmployee_A = this.txtEmployee_A.Text.ToString().Trim();
            strEmployee_C = this.txtEmployee_C.Text.ToString().Trim();     
            
        }

        private void btnConfsTran_Click(object sender, EventArgs e)
        {
            bool blCanTran = false;
            strEmployee_A = this.txtEmployee_A.Text.ToString().Trim();
            strEmployee_C = this.txtEmployee_C.Text.ToString().Trim();
            string strTemp_1 = Base.myClient.DTGetEmployeeSession(strEmployee_A);
            string strTemp_2 = Base.myClient.DTGetEmployeeSession(strEmployee_C);
            if (strTemp_1 != "" && strTemp_2 != "")
            {
                string[] strCalls_1 = strTemp_1.Split('|');
                strEmployee_B = ((strEmployee_A == strCalls_1[0])? strCalls_1[1]: strCalls_1[0]);
                strCallId_1 = strCalls_1[2];
                string[] strCalls_2 = strTemp_2.Split('|');
                strEmployee_D = ((strEmployee_C == strCalls_1[0]) ? strCalls_1[1] : strCalls_1[0]);
                strCallId_2 = strCalls_2[2];
                if (strCallId_1 != "" && strCallId_2 != "")
                {
                    blCanTran = true;    
                }
            }
            if (blCanTran)
            {
                Base.myClient.DTConsTransCall(strCallId_1, strEmployee_A, strCallId_2, strEmployee_C);
                string strInfo = String.Format("DTC->DTS; KEY=CONSTRANSCALL; CallId_1={0}; Employee_A={1}; CallId_2={2}; Employee_C={3}",
                        strCallId_1, strEmployee_A, strCallId_2, strEmployee_C);
                //ShowDebugInfo(strInfo);
            }
            else
            {
                string strInfo = "Error: Msg=" + strEmployee_A + "或" + strEmployee_C + "当前并未通话，无法转接!";
                MessageBox.Show(strInfo);
                return;
            }  
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = global::DispatchClientDemo.Properties.Resources.Close_On;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = global::DispatchClientDemo.Properties.Resources.Close;
        }
    }
}
