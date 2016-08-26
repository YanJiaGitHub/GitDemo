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
    public partial class frmDialPlate : Form
    {
        private string strDialNum_Src = "";
        public frmDialPlate()
        {
            InitializeComponent();
            if (Base.g_DialNum != null)
            {
                strDialNum_Src = Base.g_DialNum;
                this.txtDialNum.Text = Base.g_DialNum;
                this.txtDialNum.SelectionStart = Base.g_DialNum.Length;
            }            
        }  

        #region 点击拨号盘按钮
        private void btnNum1_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("1");
        }

        private void btnNum2_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("2");
        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("3");
        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("4");
        }

        private void btnNum5_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("5");
        }

        private void btnNum6_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("6");
        }

        private void btnNum7_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("7");
        }

        private void btnNum8_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("8");
        }

        private void btnNum9_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("9");
        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("*");
        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("0");
        }

        private void btnSharp_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("#");
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            SetDialNumByClickBtn("|");
        }

        private void btnAllClear_Click(object sender, EventArgs e)
        {
            Base.g_DialNum = "";
            this.txtDialNum.Text = Base.g_DialNum;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {            
            int nIdx = this.txtDialNum.SelectionStart;  
            if (nIdx >= 1)
            {
                string strFront = (nIdx==1)? "": Base.g_DialNum.Substring(0, nIdx - 1);
                string strBehind = Base.g_DialNum.Substring(nIdx);
                Base.g_DialNum = strFront + strBehind;
                this.txtDialNum.Text = Base.g_DialNum;
                nIdx--; 
            }   
            this.txtDialNum.Focus();
            this.txtDialNum.Select(nIdx, 0);
        }

        private void SetDialNumByClickBtn(string strBtnNum)
        {            
            int nIdx = this.txtDialNum.SelectionStart;
            if (nIdx == 0)
            {
                Base.g_DialNum += strBtnNum;
            }
            else
            {
                string strFront = Base.g_DialNum.Substring(0, nIdx);
                string strBehind = Base.g_DialNum.Substring(nIdx);
                strFront += strBtnNum;
                Base.g_DialNum = strFront + strBehind;                
            }
            nIdx++;
            this.txtDialNum.Text = Base.g_DialNum;
            this.txtDialNum.Focus();
            this.txtDialNum.Select(nIdx,0);
            //this.txtDialNum.SelectionStart = nIdx;
        }
        #endregion   

        private void txtDialNum_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == 13)//回车键
            {
                btnOK.PerformClick();
            }
            else if (e.KeyChar == (char)27)//Esc键
            {
                pbClose_Click(null,null);
            }                    
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Base.g_DialNum = strDialNum_Src;
            this.DialogResult = DialogResult.OK;            
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            Base.g_DialNum = txtDialNum.Text.ToString().Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtDialNum_TextChanged(object sender, EventArgs e)
        {
            Base.g_DialNum = txtDialNum.Text.ToString().Trim();            
        }   
    }
}
