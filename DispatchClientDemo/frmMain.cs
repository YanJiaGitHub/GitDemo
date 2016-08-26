using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DispatchClient;
using System.Collections;

namespace DispatchClientDemo
{    
    public partial class frmMain : Form
    {
        private static string strDispatchServerIP = "192.168.1.182";  /*用以初始化调度机IP*/
        private static string strDispatchServerPort = "10001";
        private static string strSipServerIP = "192.168.1.182";
        private static int nSipPort = 6060;
        private static string strCallId_Last = "";                    /*保存准备要操作的会话的CallId*/
        private static string strLocalIP = "192.168.1.30";
        private static string strPassword = "123456";//"820068";
        private static string strUserName = "2001";//"24048";
        private static string strSoftPhoneNum = "1013";//"18013";
        private static string strLeftHandleNum = "";
        private static string strRightHandleNum = "";
        private static string strSoftPhonePwd = "123456";//"661475";        

        public frmMain()
        {
            InitializeComponent();
            InitDispatchClient();    //初始化
        }

        private void InitDispatchClient()
        {
            Base.g_MainForm = this;
            Base.myClient = new DispatchClient.DispatchClient_cs();
            Base.g_GroupNum_IsLoaded = new Hashtable();            
            this.txtDispatchServerIP.Text = strDispatchServerIP + ":" + strDispatchServerPort;
            this.txtSipServerIP.Text = strSipServerIP + ":" + nSipPort.ToString();
            this.txtSoftPhoneNum.Text = strSoftPhoneNum;
            this.txtLocalIP.Text = strLocalIP;
            this.txtPassword.Text = strPassword;
            this.txtUserName.Text = strUserName;
            this.txtLeftHandleNum.Text = strLeftHandleNum;
            this.txtRightHandleNum.Text = strRightHandleNum;
            this.txtSoftPhonePwd.Text = strSoftPhonePwd;
            RegDispatchClientEvent();//注册事件            
        }

        #region 左侧ListView显示Session列表
        private void UpdateListViewItem(string strCallId, string strSessionInfo)
        {
            if (!this.lstvSession.Items.ContainsKey(strCallId))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = 0;
                lvi.Text = strCallId;
                lvi.Name = strCallId;
                lvi.ToolTipText = strSessionInfo;
                lvi.ForeColor = Color.Blue;  //设置行颜色 
                this.lstvSession.Items.Add(lvi);
            }
            else
            {
                string strSessionInfo_Src = this.lstvSession.Items[strCallId].ToolTipText;
                string[] strSessionInfos_Now = strSessionInfo.Split('|');
                if (strSessionInfo.Contains("CMSTATUS") || strSessionInfo_Src.Contains("会议成员"))
                {
                    string[] strSessionInfos_Src = strSessionInfo_Src.Split('|');
                    string strCallMember = "";
                    if (strSessionInfos_Src.Length > 1)
                    {
                        strCallMember = strSessionInfos_Src[1];
                    }
                    string strNewCM = strSessionInfos_Now[1];
                    if (strCallMember != "" && (!strCallMember.Contains(strNewCM)))
                    {
                        strCallMember += ",";
                        strCallMember += strNewCM;
                    }
                    else if (strCallMember == "" && strNewCM != "")
                        strCallMember = strNewCM;
                    this.lstvSession.Items[strCallId].ToolTipText = "会议成员|" + strCallMember;
                }
            }
        }

        private void lstvSession_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Selected)
            {
                strCallId_Last = e.Item.Name;
            }
        }
        #endregion

        #region 拨号盘
        private void btnDial_Click(object sender, EventArgs e)
        {
            frmDialPlate frmDial = new frmDialPlate();
            DialogResult ret = frmDial.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.txtDialNum.Text = Base.g_DialNum;
            }
        }

        private void txtDialNum_TextChanged(object sender, EventArgs e)
        {
            Base.g_DialNum = txtDialNum.Text.ToString().Trim();
        }
        #endregion

        #region 公共类方法
        private void btnConnect_Click(object sender, EventArgs e)
        {            
            if (pmAgent.nStatus == e_Status.nDisconnect
                || pmAgent.nStatus == e_Status.nUnavailable)
            {
                bool blReturn = false;
                string[] strTemp = this.txtDispatchServerIP.Text.ToString().Split(':');
                strDispatchServerIP = strTemp[0];
                if (strTemp.Length > 1)
                {                    
                    strDispatchServerPort = strTemp[1];
                    if (strDispatchServerPort == "")
                        return;
                    if (strDispatchServerPort == "")
                        strDispatchServerPort = "10001";
                }
                blReturn = Base.myClient.DTConnect(strDispatchServerIP, strDispatchServerPort);
                string strInfo = String.Format("DTC->DTS; KEY=CONNECT; IP={0}; PORT={1}; RESULT={2};",
                    strDispatchServerIP, strDispatchServerPort, blReturn.ToString());
                ShowDebugInfo(strInfo);
            }
            else
            {
                Base.myClient.DTDisconnect();
                string strInfo = "DTC->DTS; KEY=DISCONNECT;";
                ShowDebugInfo(strInfo);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (pmAgent.nStatus == e_Status.nConn_noLogin)
            {
                bool blReturn = false;
                string strDNSprefix = "";
                string strTemp = DispatchClient.Base.EncodeBase64(this.txtPassword.Text);
                blReturn = Base.myClient.DTLogin(this.txtUserName.Text, this.txtPassword.Text, this.txtLocalIP.Text, out strDNSprefix);
                string strInfo = String.Format("DTC->DTS; KEY=LOGIN; UserName={0}; PassWord={1}; LocalIP={2}; RESULT={3}; DNSPREFIX={4}",
                    this.txtUserName.Text, strTemp, this.txtLocalIP.Text, blReturn.ToString(), strDNSprefix);
                ShowDebugInfo(strInfo);
            }
            else if (pmAgent.nStatus == e_Status.nLogin)
            {
                bool blReturn = false;
                blReturn = Base.myClient.DTLogout();
                string strInfo = String.Format("DTC->DTS; KEY=LOGOUT; RESULT={0}", blReturn);
                ShowDebugInfo(strInfo);
                AllClear();
            }
        }

        private void AllClear()
        {
            this.EmployeeTree.Nodes.Clear();
            this.RadioTree.Nodes.Clear();
            this.VideoDeviceTree.Nodes.Clear();
            this.VideoEmployeeTree.Nodes.Clear();
            if (Base.g_GroupNum_IsLoaded != null)
                Base.g_GroupNum_IsLoaded.Clear();
            Base.g_VideoTreeRootNode = null;

            if (pmPhone.IsRegistered)
            {
                SIPPhoneSDK.UnRegist();
                pmPhone.IsRegistered = false;
            }
        }        
        #endregion

        #region 通话类方法
        private void btnSingleCall_Click(object sender, EventArgs e)
        {
            string strKey = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            int nCallType = 1;
            Base.myClient.DTPlaceCall(strKey, this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            string strInfo = String.Format("DTC->DTS; KEY=PLACEAUDIOCALL; OPID={0}; CALLER={1}; CALLED={2}; CALLTYPE={3};", strKey,
                this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            ShowDebugInfo(strInfo);
        }

        private void btnVideoCall_Click(object sender, EventArgs e)
        {
            string strKey = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            int nCallType = 2;
            Base.myClient.DTPlaceCall(strKey, this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            string strInfo = String.Format("DTC->DTS; KEY=PLACEVIDEOCALL; OPID={0}; CALLER={1}; CALLED={2}; CALLTYPE={3};", strKey,
                this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            ShowDebugInfo(strInfo);
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strCalled = this.txtSoftPhoneNum.Text.ToString().Trim();
            Base.myClient.DTAnswerCallFormQueue(strCallId, strCalled);
            string strInfo = String.Format("DTC->DTS; KEY=ANSWERCALLFORMQUEUE; CallId={0}; Called={1}", strCallId, strCalled);
            ShowDebugInfo(strInfo);
        }

        private void btnHungUp_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            Base.myClient.DTHangupCall(strCallId);
            if (this.lstvSession.Items.ContainsKey(strCallId))
            {
                this.lstvSession.Items[strCallId].Remove();
            }
            string strInfo = String.Format("DTC->DTS; KEY=HANGUPCALL; CallId={0}", strCallId);
            ShowDebugInfo(strInfo);
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strCalled = this.txtSoftPhoneNum.Text.ToString().Trim();
            string strRoadParam = "";
            Base.myClient.DTHoldCall(strCallId, strCalled, strRoadParam);
            string strInfo = String.Format("DTC->DTS; KEY=PUSHCALLTOQUEUE; CallId={0}; Called={1}; RoadParam={2}",
                    strCallId, strCalled, strRoadParam);
            ShowDebugInfo(strInfo);
        }

        private void btnRetrive_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strCalled = this.txtSoftPhoneNum.Text.ToString().Trim();
            Base.myClient.DTRetriveCall(strCallId, strCalled);
            string strInfo = String.Format("DTC->DTS; KEY=RETRIVECALL; CallId={0}; Called={1}",
                    strCallId, strCalled);
            ShowDebugInfo(strInfo);
        }

        private void btnPickCall_Click(object sender, EventArgs e)
        {
            string strCallId = "";//this.txtCallId_1.Text.ToString().Trim();
            string strCalled = this.txtSoftPhoneNum.Text.ToString().Trim();
            string strCalled_Dest = Base.g_DialNum;
            Base.myClient.DTPickCall(strCallId, strCalled, strCalled_Dest);
            string strInfo = String.Format("DTC->DTS; KEY=RETRIVECALL; CallId={0}; Called={1}; Called_Dest={2}",
                    strCallId, strCalled, strCalled_Dest);
            ShowDebugInfo(strInfo);
        }

        private void btnTranCall_Click(object sender, EventArgs e)
        {
            FrmTranCall frmTrans = new FrmTranCall();
            frmTrans.ShowDialog();
        }

        private void btnInsertCall_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strCaller = this.txtSoftPhoneNum.Text.ToString().Trim();
            Base.myClient.DTInsertCall(strCallId, strCaller, Base.g_DialNum, 1);
            string strInfo = String.Format("DTC->DTS; KEY=INSERTCALL; CallId={0}; Caller={1}; Called={2}",
                    strCallId, strCaller, Base.g_DialNum);
            ShowDebugInfo(strInfo);
        }

        private void btnDiscCall_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            Base.myClient.DTForceReleaseCall(strCallId, Base.g_DialNum);
            if (this.lstvSession.Items.ContainsKey(strCallId))
            {
                this.lstvSession.Items[strCallId].Remove();
            }
            string strInfo = String.Format("DTC->DTS; KEY=FORCERELEASECALL; CallId={0}; Called={1}",
                     strCallId, Base.g_DialNum);
            ShowDebugInfo(strInfo);
        }

        private void btnListenCall_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strCaller = this.txtSoftPhoneNum.Text.ToString().Trim();
            Base.myClient.DTInsertCall(strCallId, strCaller, Base.g_DialNum, 2);
            string strInfo = String.Format("DTC->DTS; KEY=MONITORCALL; CallId={0}; Caller={1}; Called={2}",
                    strCallId, strCaller, Base.g_DialNum);
            ShowDebugInfo(strInfo);
        }

        #region GetSessionInfo
        private void btnGetAllSession_Click(object sender, EventArgs e)
        {
            string strEmployee = "";
            int nCallType = 0;
            string strInfo = String.Format("DTC->DTS; KEY=GETALLSESSION; EMPLOYEE={0}; CALLTYPE={1}",
                strEmployee, nCallType);
            ShowDebugInfo(strInfo);
            Base.myClient.DTGetAllSession(strEmployee, nCallType);
        }
        #endregion
        #endregion

        #region 会议类方法
        private void btnRadioCall_Click(object sender, EventArgs e)
        {
            string strKey = DateTime.Now.ToString("yyyyMMddHHmmssffff"); ;
            Base.myClient.DTMakeIntercom(strKey, this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum);
            string strInfo = String.Format("DTC->DTS; KEY=MAKEINTERCOM; OPID={0}; CALLER={1}; CALLED={2};", strKey,
                this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum);
            ShowDebugInfo(strInfo);
        }

        private void btnConfenceCall_Click(object sender, EventArgs e)
        {
            string strKey = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            int nCallType = 1;
            Base.myClient.DTCreatConf(strKey, this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            string strInfo = String.Format("DTC->DTS; KEY=CREATAUDIOCONF; OPID={0}; CALLER={1}; CALLED={2}; CALLTYPE={3};", strKey,
                this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            ShowDebugInfo(strInfo);
        }

        private void btnVideoConf_Click(object sender, EventArgs e)
        {
            string strKey = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            int nCallType = 2;
            Base.myClient.DTCreatConf(strKey, this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            string strInfo = String.Format("DTC->DTS; KEY=CREATVIDEOCONF; OPID={0}; CALLER={1}; CALLED={2}; CALLTYPE={3};", strKey,
                this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum, nCallType);
            ShowDebugInfo(strInfo);
        }

        private void btnMakeBroadcast_Click(object sender, EventArgs e)
        {
            string strKey = DateTime.Now.ToString("yyyyMMddHHmmssffff"); ;
            Base.myClient.DTCreatBroadcast(strKey, this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum);
            string strInfo = String.Format("DTC->DTS; KEY=MAKEBROADCAST; OPID={0}; CALLER={1}; CALLED={2};", strKey,
                this.txtSoftPhoneNum.Text.ToString().Trim(), Base.g_DialNum);
            ShowDebugInfo(strInfo);
        }

        private void btnGetCMStatus_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strSessionId = "";
            Base.myClient.DTGetCMStatus(strCallId, strSessionId);
            string strInfo = String.Format("DTC->DTS; KEY=GETCMSTATUS; CALLID={0}; SID={1}",
                strCallId, strSessionId);
            ShowDebugInfo(strInfo);
        }

        private void btnCanSpeak_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strEmployee = Base.g_DialNum;
            int nSpeakType = 2;
            Base.myClient.DTSetCMSpeak(strCallId, strEmployee, nSpeakType);
            string strInfo = String.Format("DTC->DTS; KEY=SETSPEAK; CallId={0}; Employee={1}; nSpeakType={2}",
                    strCallId, strEmployee, nSpeakType);
            ShowDebugInfo(strInfo);
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strEmployee = Base.g_DialNum;
            int nSpeakType = 1;
            Base.myClient.DTSetCMSpeak(strCallId, strEmployee, nSpeakType);
            string strInfo = String.Format("DTC->DTS; KEY=SETUNSPEAK; CallId={0}; Employee={1}; nSpeakType={2}",
                    strCallId, strEmployee, nSpeakType);
            ShowDebugInfo(strInfo);
        }

        private void btnSetListen_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strEmployee = Base.g_DialNum;
            int nListenType = 2;
            Base.myClient.DTSetCMListen(strCallId, strEmployee, nListenType);
            string strInfo = String.Format("DTC->DTS; KEY=SETLISTEN; CallId={0}; Employee={1}; nListenType={2}",
                    strCallId, strEmployee, nListenType);
            ShowDebugInfo(strInfo);
        }

        private void btnSetUnListen_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strEmployee = Base.g_DialNum;
            int nListenType = 1;
            Base.myClient.DTSetCMListen(strCallId, strEmployee, nListenType);
            string strInfo = String.Format("DTC->DTS; KEY=SETUNLISTEN; CallId={0}; Employee={1}; nListenType={2}",
                    strCallId, strEmployee, nListenType);
            ShowDebugInfo(strInfo);
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strEmployee = Base.g_DialNum;
            int nAnswerType = 1;
            Base.myClient.DTAddCallMember(strCallId, strEmployee, nAnswerType);
            string strInfo = String.Format("DTC->DTS; KEY=ADDCALLMEMBER; CallId={0}; Employee={1}; nAnswerType={2}",
                    strCallId, strEmployee, nAnswerType);
            ShowDebugInfo(strInfo);
        }

        private void btnDelMember_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            string strEmployee = Base.g_DialNum;
            Base.myClient.DTDeleteCallMember(strCallId, strEmployee);
            string strInfo = String.Format("DTC->DTS; KEY=DELETECALLMEMBER; CallId={0}; Employee={1}",
                    strCallId, strEmployee);
            ShowDebugInfo(strInfo);
        }

        private void btnForceEndConf_Click(object sender, EventArgs e)
        {
            string strCallId = strCallId_Last;
            Base.myClient.DTForceEndConf(strCallId);
            if (this.lstvSession.Items.ContainsKey(strCallId))
            {
                this.lstvSession.Items[strCallId].Remove();
            }
            string strInfo = String.Format("DTC->DTS; KEY=FORCEENDCONF; CALLID={0}",
                strCallId);
            ShowDebugInfo(strInfo);
        }
        #endregion

        #region 短信类方法
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            string strMsg = this.txtShortMsg.Text.ToString().Trim();
            if (strMsg == "") return;
            string strEmployee = this.txtUserName.Text.ToString().Trim();
            if (Base.g_DialNum == "") return;
            Base.myClient.DTSendMessage(strEmployee, Base.g_DialNum, strMsg);
            string strInfo = String.Format("DTC->DTS; KEY=SENDMESSAGE; EMPLOYEE_SEND={0}; EMPLOYEE_DEST={1}; MSG={2}",
                strEmployee, Base.g_DialNum, strMsg);
            ShowDebugInfo(strInfo);
        }
        #endregion

        #region 事件或者回调函数
        private void RegDispatchClientEvent()
        {
            this.EmployeeTree.Tag = Dispatcher.GroupType.GroupTypeOther;
            this.RadioTree.Tag = Dispatcher.GroupType.GroupTypeIntercomgroup;
            this.EmployeeTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(EmployeeTree_NodeMouseClick);
            this.RadioTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(EmployeeTree_NodeMouseClick);
            this.VideoEmployeeTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(EmployeeTree_NodeMouseClick);
            this.VideoDeviceTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(EmployeeTree_NodeMouseClick);            
            Base.myClient.onReceiveEmployeeTree_EventChanged += new DispatchClient_cs.onReceiveEmployeeTree_EventHandler(onReceiveEmployeeTree_EventChanged);
            Base.myClient.onReceiveVideoDeviceTree_EventChanged += new DispatchClient_cs.onReceiveVideoDeviceTree_EventHandler(onReceiveVideoDeviceTree_EventChanged);
            Base.myClient.onReceiveVideoEmployeeTree_EventChanged += new DispatchClient_cs.onReceiveVideoEmployeeTree_EventHandler(onReceiveVideoEmployeeTree_EventChanged);
            Base.myClient.onEcho_EventChanged += new DispatchClient_cs.onEcho_EventHandler(onEcho_EventChanged);
            Base.myClient.onEchoTimeOut_EventChanged += new DispatchClient_cs.onEchoTimeOut_EventHandler(onEchoTimeOut_EventChanged);
            Base.myClient.onDisconnected_EventChanged += new DispatchClient_cs.onDisconnected_EventHandler(onDisconnected_EventChanged);
            Base.myClient.onReceiveError_EventChanged += new DispatchClient_cs.onReceiveError_EventHandler(onReceiveError_EventChanged);
            Base.myClient.onReceiveEmployeeStatusChanged += new DispatchClient_cs.onReceiveEmployeeStatus_EventChanged(onReceiveEmployeeStatusChanged);
            Base.myClient.onReceiveAgentStatus_EventChanged += new DispatchClient_cs.onReceiveAgentStatus_EventHandler(onReceiveAgentStatus_EventChanged);
            Base.myClient.onReceiveOPResult_EventChanged += new DispatchClient_cs.onReceiveOPResult_EventHandler(onReceiveOPResult_EventChanged);
            Base.myClient.onReceiveCallStatus_EventChanged += new DispatchClient_cs.onReceiveCallStatus_EventHandler(onReceiveCallStatus_EventChanged);
            Base.myClient.onReceiveCMStatus_EventChanged += new DispatchClient_cs.onReceiveCMStatus_EventHandler(onReceiveCMStatus_EventChanged);
        }
        private void onReceiveEmployeeStatusChanged(int myStatus)
        {
            switch (myStatus)
            {
                #region 根据连接状态，显示对应颜色
                case -1:    //Unvailable
                    this.picBox_Status.BackgroundImage = DispatchClientDemo.Properties.Resources.Unvailable;
                    this.Invoke((EventHandler)(delegate
                    {
                        this.btnLogin.Enabled = false;
                        this.btnConnect.Enabled = true;
                        this.btnConnect.Text = "连接";
                        this.btnLogin.Text = "登录";
                    }));
                    break;
                case 0:     //Disconnect
                    this.picBox_Status.BackgroundImage = DispatchClientDemo.Properties.Resources.Disconnect;
                    this.Invoke((EventHandler)(delegate
                    {
                        this.btnLogin.Enabled = false;
                        this.btnConnect.Enabled = true;
                        this.btnConnect.Text = "连接";
                        this.btnLogin.Text = "登录";
                        //btnInit.PerformClick();
                    }));
                    break;
                case 1:     //Connect，NoLogin
                    this.picBox_Status.BackgroundImage = DispatchClientDemo.Properties.Resources.Logout;
                    this.Invoke((EventHandler)(delegate
                    {
                        this.btnLogin.Enabled = true;
                        this.btnConnect.Enabled = true;
                        this.btnConnect.Text = "断开";
                        this.btnLogin.Text = "登录";                        
                    }));
                    break;
                case 2:     //Connect，Login
                    this.picBox_Status.BackgroundImage = DispatchClientDemo.Properties.Resources.Login;
                    this.Invoke((EventHandler)(delegate
                    {
                        this.btnLogin.Enabled = true;
                        this.btnConnect.Enabled = false;
                        this.btnConnect.Text = "断开";
                        this.btnLogin.Text = "登出";
                        btnInit.PerformClick();
                        Base.myClient.DTGetEmployeeTree();
                    }));
                    break;
                default:
                    this.picBox_Status.BackgroundImage = DispatchClientDemo.Properties.Resources.Disconnect;
                    break;
                #endregion
            }
        }

        private void onEcho_EventChanged()
        {
            string strInfo = "DTC->DTS; KEY=CONN_ECHO;";
            ShowDebugInfo(strInfo);
        }

        private void onEchoTimeOut_EventChanged()
        {
            string strInfo = "DTC->DTC; KEY=ECHO_TIMEOUT;";
            ShowDebugInfo(strInfo);
        }

        private void onDisconnected_EventChanged()
        {
            string strInfo = "DTS->DTC; KEY=DISCONNECTED;";
            ShowDebugInfo(strInfo);
        }

        private void onReceiveError_EventChanged(string strErrCode, string strErrDesc)
        {
            if (strErrCode == "Dispatcher.ECode.ECodeRegisterDisConnect")
            {
                onReceiveEmployeeStatusChanged(0);
            }
            string strInfo = String.Format("DTS->DTC; KEY=ERROR; CODE={0}; MSG={1}", strErrCode, strErrDesc);
            ShowDebugInfo(strInfo);
        }

        private void onReceiveAgentStatus_EventChanged(string strAgentId, string strAgentName, string strIPAddress, string strOtherNumber, int nRegStatus)
        {
            string strInfo = String.Format("DTS->DTC; KEY=AGENTSTATUSCHANGE; AGENTID={0}; AGENTNAME={1}; IPADDRESS={2}; OTHERNUMBER={3}; REGSTATUS={4};",
                strAgentId, strAgentName, strIPAddress, strOtherNumber, nRegStatus);
            ShowDebugInfo(strInfo);
        }

        private void onReceiveOPResult_EventChanged(string strKey, string strCallId)
        {
            string strInfo = String.Format("DTS->DTC; KEY=OPRESULT; OPID={0}; CALLID={1}", strKey, strCallId);
            ShowDebugInfo(strInfo);
            this.Invoke((EventHandler)(delegate
            {
                string strValue = "发起呼叫";
                UpdateListViewItem(strCallId, strValue);
                btnGetAllSession.PerformClick();
            }));
        }

        private void onReceiveCallStatus_EventChanged(string strCaller, string strCalled, string strCallId, int nCallType, int nCallState)
        {
            this.Invoke((EventHandler)(delegate
            {
                string strValue = strCaller + "|" + strCalled + "|CALLSTATUS|" + nCallType + "|" + nCallState;
                if (nCallType == 5)
                {
                    Base.myClient.DTGetCMStatus(strCallId, "");
                    return;
                }
                UpdateListViewItem(strCallId, strValue);

            }));
            string strInfo = String.Format("DTS->DTC; KEY=SESSIONINFO; CALLER={0}; CALLED={1}; CALLID={2}; CALLTYPE={3}; CALLSTATE={4}",
                strCaller, strCalled, strCallId, nCallType, nCallState);
            ShowDebugInfo(strInfo);
        }

        private void onReceiveCMStatus_EventChanged(string strCallId, string strCallMemberNum, string strOtherInfo, string strName, int nCallState, int nType)
        {
            this.Invoke((EventHandler)(delegate
            {
                string strValue = "CMSTATUS|" + strCallMemberNum + "(" + strName + ")";
                UpdateListViewItem(strCallId, strValue);

            }));
            string strInfo = String.Format("DTS->DTC; KEY=CMSTATUSCHANGE; CALLID={0}; CMNUM={1}; CMNAME={2}; OTHERINFO={3}; CMTYPE={4}; CALLSTATE={5}",
                strCallId, strCallMemberNum, strName, strOtherInfo, nCallState, nType);
            ShowDebugInfo(strInfo);
        }
        #endregion

        #region 关闭窗体
        private void pbClose_Click(object sender, EventArgs e)
        {
            btnConnect.PerformClick();
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
        #endregion

        public void ShowDebugInfo(string strLine)
        {
            string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff ");
            this.Invoke((EventHandler)(delegate
            {
                if (this.ListMsg.Items.Count > 200)
                {
                    this.ListMsg.Items.RemoveAt(200);
                }
                this.ListMsg.Items.Insert(0, strNow + strLine);
                Base.write_log(Application.StartupPath, strLine);
            }));
        }    

        private void LoadDevices()
        {            
            //LoadInputAudioDevice and LoadOutputAudioDevice         
            int audLen;
            SIPPhoneSDK.GetAudioDevices(Base.g_AudioDevices, out audLen);
            this.cmbAudioInputDevices.Items.Clear();
            this.cmbAudioOutputDevices.Items.Clear();
            if (audLen > 0)
            {
                foreach (SIPPhoneSDK.AudioDeviceInfo audio in Base.g_AudioDevices)
                {
                    if (audio.name == "Wave mapper")
                        continue;
                    if (audio.inputCnt > 0)
                    {
                        this.cmbAudioInputDevices.Items.Add(audio.id.ToString() + "-" + audio.name);
                    }
                    if (audio.outputCnt > 0)
                    {
                        this.cmbAudioOutputDevices.Items.Add(audio.id.ToString() + "-" + audio.name);
                    }
                }
                if (this.cmbAudioInputDevices.Items.Count >= 1)
                    this.cmbAudioInputDevices.SelectedIndex = 0;
                if (this.cmbAudioOutputDevices.Items.Count >= 1)
                    this.cmbAudioOutputDevices.SelectedIndex = 0;
            }
            //LoadInputVideoDevice            
            int nTemp;
            SIPPhoneSDK.GetVideoDevices(Base.g_VideoDevices, out nTemp);
            this.cmbVideoDevices.Items.Clear();
            if (nTemp > 0)
            {
                foreach (SIPPhoneSDK.VideoDeviceInfo video in Base.g_VideoDevices)
                {
                    if (video.name == "SDL renderer" || video.name == "Colorbar generator"
                        || video.name == "Colorbar-active" || video.name == "")
                        continue;
                    this.cmbVideoDevices.Items.Add(video.id.ToString() + "-" + video.name);
                }
                if (this.cmbVideoDevices.Items.Count >= 1)
                    this.cmbVideoDevices.SelectedIndex = 0;
                else
                {                    
                    this.cmbVideoDevices.Items.Add("无");
                    this.cmbVideoDevices.SelectedIndex = 0;
                }
            }           
        }

        #region 获取树结构
        private void onReceiveEmployeeTree_EventChanged(Dispatcher.TreeRT info)
        {
            foreach (Dispatcher.GroupT group in info.roots)
            {
                if (Base.g_VideoTreeRootNode == null && group.id == 1)
                {
                    TreeNode rtNode = new TreeNode();
                    rtNode.Name = group.dnsprefix + group.groupnum;
                    rtNode.Text = group.groupname;
                    Base.g_VideoTreeRootNode = rtNode;
                    Base.myClient.DTGetVideoDeviceTree();
                    Base.myClient.DTGetVideoEmployeeTree();
                }
                this.Invoke((EventHandler)(delegate
                {
                    Fill_TreeView(ref this.EmployeeTree, group);
                    Fill_TreeView(ref this.RadioTree, group);
                }));
            }
        }

        private void onReceiveVideoDeviceTree_EventChanged(Dispatcher.VideoInfoRT info)
        {
            foreach (Dispatcher.VideoInfoElem video in info.vseq)
            {
                this.Invoke((EventHandler)(delegate
                {
                    Fill_VideoDeviceTree(video);
                }));
            }
        }

        private void onReceiveVideoEmployeeTree_EventChanged(Dispatcher.TreeRT info)
        {
            foreach (Dispatcher.GroupT group in info.roots)
            {
                this.Invoke((EventHandler)(delegate
                {
                    Fill_VideoEmployeeTree(group);
                }));
            }
        }

        private void Fill_TreeView(ref TreeView myTree, Dispatcher.GroupT group)
        {
            if (myTree.Nodes.Find(group.dnsprefix + group.groupnum, true).Length > 0)
            {
                TreeNode thisNode = myTree.Nodes.Find(group.dnsprefix + group.groupnum, true)[0];
                foreach (Dispatcher.UserT childuser in group.user)
                {
                    thisNode.Nodes.Add(SetEmployeeTree(childuser));
                }
            }
            else
            {
                TreeNode thisNode = SetEmployeeTree((Dispatcher.GroupType)myTree.Tag, group);
                if (thisNode != null)
                {
                    myTree.Nodes.Add(thisNode);
                }
            }
        }

        private void Fill_VideoEmployeeTree(Dispatcher.GroupT group)
        {
            if (this.VideoEmployeeTree.Nodes.Find(Base.g_VideoTreeRootNode.Name, true).Length == 0)
            {
                TreeNode rtNode = new TreeNode();
                rtNode.Name = Base.g_VideoTreeRootNode.Name;
                rtNode.Text = Base.g_VideoTreeRootNode.Text;
                this.VideoEmployeeTree.Nodes.Add(rtNode);
            }
            SetVideoEmployeeTree(group);
        }

        private void SetVideoEmployeeTree(Dispatcher.GroupT group)
        {
            foreach (Dispatcher.GroupT childgroup in group.group)
            {
                SetVideoEmployeeTree(childgroup);
            }
            foreach (Dispatcher.UserT user in group.user)
            {
                if (this.VideoEmployeeTree.Nodes.Find(user.dnsprefix + user.userid, true).Length == 0)
                {
                    TreeNode rtNode = this.VideoEmployeeTree.Nodes.Find(Base.g_VideoTreeRootNode.Name, true)[0];
                    TreeNode userNode = new TreeNode();
                    userNode.Name = user.dnsprefix + user.userid;
                    userNode.Text = user.username;
                    userNode.Tag = user;
                    string strInfo = String.Format("DTS->DTC; KEY=VIDEOEMPLOYEE; DNSPREFIX={0}; USERID={1}; USERNAME={2}; USERTYPE={3}; ",
                        user.dnsprefix, user.userid, user.username, user.type.ToString());
                    ShowDebugInfo(strInfo);
                    rtNode.Nodes.Add(userNode);
                }
            }
        }

        private void Fill_VideoDeviceTree(Dispatcher.VideoInfoElem video)
        {
            if (this.VideoDeviceTree.Nodes.Find(Base.g_VideoTreeRootNode.Name, true).Length == 0)
            {
                TreeNode rtNode = new TreeNode();
                rtNode.Name = Base.g_VideoTreeRootNode.Name;
                rtNode.Text = Base.g_VideoTreeRootNode.Text;
                this.VideoDeviceTree.Nodes.Add(rtNode);
            }
            if (this.VideoDeviceTree.Nodes.Find(video.id.ToString(), true).Length == 0)
            {
                TreeNode rtNode = this.VideoDeviceTree.Nodes.Find(Base.g_VideoTreeRootNode.Name, true)[0];
                TreeNode videoNode = new TreeNode();
                videoNode.Name = video.id.ToString();
                videoNode.Text = video.showname;
                videoNode.Tag = video;
                string strTemp = DispatchClient.Base.EncodeBase64(video.password);
                string strInfo = String.Format("DTS->DTC; KEY=VIDEODEVICE; ID={0}; NAME={1}; IP={2}; PORT={3}; USER={4}; PWD={5}; TYPE={6} "
                , video.id, video.showname, video.videoIP, video.videoport, video.user, strTemp, video.type.ToString());
                ShowDebugInfo(strInfo);
                rtNode.Nodes.Add(videoNode);
            }
        }

        private TreeNode SetEmployeeTree(Dispatcher.GroupType groupType, Dispatcher.GroupT group)
        {
            if (group.id != 1 && group.type != groupType)
            {
                return null;
            }
            TreeNode rtNode = new TreeNode();
            rtNode.Name = group.dnsprefix + group.groupnum;
            rtNode.Text = group.groupname;
            rtNode.Tag = group;
            foreach (Dispatcher.GroupT childgroup in group.group)
            {
                if (group.id != 1 && groupType != childgroup.type)
                    continue;
                else
                {
                    TreeNode thisNode = SetEmployeeTree(groupType, childgroup);
                    if (thisNode != null)
                    {
                        rtNode.Nodes.Add(thisNode);
                        GetGroupMember(childgroup.dnsprefix, childgroup.groupnum);
                    }
                }
            }
            foreach (Dispatcher.UserT childuser in group.user)
            {
                TreeNode thisNode = SetEmployeeTree(childuser);
                if (thisNode != null)
                    rtNode.Nodes.Add(thisNode);
            }
            return rtNode;
        }

        private TreeNode SetEmployeeTree(Dispatcher.UserT user)
        {
            TreeNode userNode = new TreeNode();
            userNode.Name = user.dnsprefix + user.userid;
            userNode.Text = user.username;
            userNode.Tag = user;
            string strInfo = String.Format("DTS->DTC; KEY=USER; DNSPREFIX={0}; USERID={1}; USERNAME={2}; USERTYPE={3}; RSTATE={4} ",
                user.dnsprefix, user.userid, user.username, user.type.ToString(), user.rstate.ToString());
            ShowDebugInfo(strInfo);
            return userNode;
        }

        private void GetGroupMember(string strDNSprifix, string strGroupNum)
        {
            if (!Base.g_GroupNum_IsLoaded.ContainsKey(strDNSprifix + strGroupNum))
            {
                Base.g_GroupNum_IsLoaded.Add(strDNSprifix + strGroupNum, true);
                Base.myClient.DTGetEmployeeTree(strDNSprifix, strGroupNum);
                string strInfo = String.Format("DTC->DTS; KEY=GETGROUPMEMBER; DNSPREFIX={0}; GROUPNUM={1};", strDNSprifix, strGroupNum);
                ShowDebugInfo(strInfo);
            }
        }
        #endregion

        #region 点击树
        private void EmployeeTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode thisNode = e.Node;
                if (thisNode.Tag.ToString() == "Dispatcher.GroupT")
                {
                    Dispatcher.GroupT group = (Dispatcher.GroupT)thisNode.Tag;
                    //GetGroupMember(group.dnsprefix, group.groupnum);
                    Base.g_DialNum = group.dnsprefix + group.groupnum;
                    this.txtDialNum.Text = Base.g_DialNum;
                }
                else if (thisNode.Tag.ToString() == "Dispatcher.UserT")
                {
                    Dispatcher.UserT user = (Dispatcher.UserT)thisNode.Tag;
                    Base.g_DialNum = user.dnsprefix + user.userid;
                    this.txtDialNum.Text = Base.g_DialNum;
                }
                else if (thisNode.Tag.ToString() == "Dispatcher.VideoInfoElem")
                {
                    Dispatcher.VideoInfoElem video = (Dispatcher.VideoInfoElem)thisNode.Tag;
                }
            }
            catch 
            {
                //记录错误                
            }
        }
        #endregion   

        private void btnInit_Click(object sender, EventArgs e)
        { 
            try
            {
                pmAgent.strSoftPhoneNum = this.txtSoftPhoneNum.Text;
                pmAgent.strLeftPhoneNum = this.txtLeftHandleNum.Text;
                pmAgent.strRightPhoneNum = this.txtRightHandleNum.Text;
                pmAgent.strSoftPhonePwd = this.txtSoftPhonePwd.Text;
                string[] strTemp = this.txtSipServerIP.Text.ToString().Split(':');
                strSipServerIP = strTemp[0];
                if (strTemp.Length > 1)
                {
                    if (strTemp[1] == "")
                        nSipPort = 5060;
                    else
                        nSipPort = DispatchClient.Base.atoi(strTemp[1]);
                }
                if (strSipServerIP == "")
                    strSipServerIP = strDispatchServerIP;
                pmPhone.strSipServerIP = strSipServerIP;
                pmPhone.nServerPort = nSipPort;
                pmPhone.nLocalPort = 6060;
                pmPhone.strLocalIP = this.txtLocalIP.Text;
                if (!pmPhone.IsRegistered && (pmAgent.nStatus == e_Status.nLogin))//注册
                {
                    SIPPhoneSDK.SDKInit(pmPhone.strLocalIP, pmPhone.nLocalPort);
                    LoadDevices();
                    pmPhone.IsRegistered = SIPPhoneSDK.Regist(pmPhone.strSipServerIP + ":" + pmPhone.nServerPort, pmAgent.strSoftPhoneNum, pmAgent.strSoftPhonePwd);
                    if (pmPhone.IsRegistered)
                    {
                        this.Invoke((EventHandler)(delegate
                        {
                            this.btnInit.Text = "注销";
                        }));
                    }
                    string strInfo = String.Format("DTC->DTS; KEY=REGISTERSOFTPHONE; SOFTPHONENUM={0}; RESULT={1};", pmAgent.strSoftPhoneNum, pmPhone.IsRegistered.ToString());
                    ShowDebugInfo(strInfo);
                }
                else//注销
                {
                    SIPPhoneSDK.UnRegist();
                    SIPPhoneSDK.SDKDestory();
                    this.Invoke((EventHandler)(delegate
                    {
                        this.btnInit.Text = "注册";
                        pmPhone.IsRegistered = false;
                    }));
                }
            }
            catch (System.Exception ex)
            {
                ShowDebugInfo(ex.ToString());
            }            
        }

        private void btnBindHandle_Click(object sender, EventArgs e)
        {           
            Base.myClient.DTBindSipPhone(this.txtLeftHandleNum.Text, this.txtSoftPhoneNum.Text, this.txtRightHandleNum.Text);
            string strInfo = String.Format("DTC->DTS; KEY=BINDPHONE; LEFTPHONENUM={0}; SOFTPHONENUM={1}; RIGHTPHONEMUM={2};", this.txtLeftHandleNum.Text, this.txtSoftPhoneNum.Text, this.txtRightHandleNum.Text);
            ShowDebugInfo(strInfo);                     
        }
    }
}
