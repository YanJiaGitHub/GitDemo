using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DispatchClientDemo
{
    public static class SIPPhoneSDK
    {
        public enum TransportType
        {
            UDP = 1,
            TCP,
        };
        public enum CallState
        {
            STATE_NULL,	    /**< Before INVITE is sent or received  */
            STATE_CALLING,	    /**< After INVITE is sent		    */
            STATE_INCOMING,	    /**< After INVITE is received.	    */
            STATE_EARLY,	    /**< After response with To tag.	    */
            STATE_CONNECTING,	    /**< After 2xx is sent/received.	    */
            STATE_CONFIRMED,	    /**< After ACK is sent/received.	    */
            STATE_DISCONNECTED,   /**< Session is terminated.		    */
        };
        public enum StatusCode
        {
            TRYING = 100,
            RINGING = 180,
            CALL_BEING_FORWARDED = 181,
            QUEUED = 182,
            PROGRESS = 183,

            OK = 200,
            ACCEPTED = 202,

            MULTIPLE_CHOICES = 300,
            MOVED_PERMANENTLY = 301,
            MOVED_TEMPORARILY = 302,
            USE_PROXY = 305,
            ALTERNATIVE_SERVICE = 380,

            BAD_REQUEST = 400,
            UNAUTHORIZED = 401,
            PAYMENT_REQUIRED = 402,
            FORBIDDEN = 403,
            NOT_FOUND = 404,
            METHOD_NOT_ALLOWED = 405,
            NOT_ACCEPTABLE = 406,
            PROXY_AUTHENTICATION_REQUIRED = 407,
            REQUEST_TIMEOUT = 408,
            GONE = 410,
            REQUEST_ENTITY_TOO_LARGE = 413,
            REQUEST_URI_TOO_LONG = 414,
            UNSUPPORTED_MEDIA_TYPE = 415,
            UNSUPPORTED_URI_SCHEME = 416,
            BAD_EXTENSION = 420,
            EXTENSION_REQUIRED = 421,
            SESSION_TIMER_TOO_SMALL = 422,
            INTERVAL_TOO_BRIEF = 423,
            TEMPORARILY_UNAVAILABLE = 480,
            CALL_TSX_DOES_NOT_EXIST = 481,
            LOOP_DETECTED = 482,
            TOO_MANY_HOPS = 483,
            ADDRESS_INCOMPLETE = 484,
            AMBIGUOUS = 485,
            BUSY_HERE = 486,
            REQUEST_TERMINATED = 487,
            NOT_ACCEPTABLE_HERE = 488,
            BAD_EVENT = 489,
            REQUEST_UPDATED = 490,
            REQUEST_PENDING = 491,
            UNDECIPHERABLE = 493,

            INTERNAL_SERVER_ERROR = 500,
            NOT_IMPLEMENTED = 501,
            BAD_GATEWAY = 502,
            SERVICE_UNAVAILABLE = 503,
            SERVER_TIMEOUT = 504,
            VERSION_NOT_SUPPORTED = 505,
            MESSAGE_TOO_LARGE = 513,
            PRECONDITION_FAILURE = 580,

            BUSY_EVERYWHERE = 600,
            DECLINE = 603,
            DOES_NOT_EXIST_ANYWHERE = 604,
            NOT_ACCEPTABLE_ANYWHERE = 606,

            TSX_TIMEOUT = REQUEST_TIMEOUT,
            /*PJSIP_SC_TSX_RESOLVE_ERROR = 702,*/
            TSX_TRANSPORT_ERROR = SERVICE_UNAVAILABLE,

            /* This is not an actual status code, but rather a constant
             * to force GCC to use 32bit to represent this enum, since
             * we have a code in PJSUA-LIB that assigns an integer
             * to this enum (see pjsua_acc_get_info() function).
             */
            force_32bit = 0x7FFFFFFF

        };
        public enum UserType
        {
            UserTypeDispatch,
            UserTypeHandheld,
            UserTypeCommonuser,
            UserTypeOutlineuser,
            UserTypeMonitoruser,
            UserTypeSsu,
            UserType3ghandheld,
            UserTypeMonitordevice,
            UserTypeNone,

            UserTypeTrunkDispatch = 100,
            UserTypeTrunkHandheld,
            UserTypeTrunkCommonuser,
            UserTypeTrunkOutlineuser,
            UserTypeTrunkMonitoruser,
            UserTypeTrunkSsu,
            UserTypeTrunk3ghandheld,
            UserTypeTrunkMonitordevice,
            UserTypeTrunkNone
        };
        public enum UserState
        {
            CallStateNone,
            CallStateInit,
            CallStateNormal,
            CallStateCallout,
            CallStateIncoming,
            CallStateRinging,
            CallStateConnect,
            CallStateHold,
            CallStateBusy,
            CallStateOffhook,
            CallStateRelease,
            CallStateUnspeak,
            CallStateSpeak,
            CallStateQueue,
            CallStateUnhold,
            CallStateZombie
        };

        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
        public struct VideoDeviceInfo
        {
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int id;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
	        public string name;
        };
        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
        public struct AudioDeviceInfo
        {
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int id;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string name;
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int inputCnt;
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int outputCnt;
        };
        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
        public struct PTTGroupInfo
        {
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int id;
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int islocal;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string groupName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string groupNum;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string dnsprefix;
        };

        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
        public struct UserInfo
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string userId;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string userName;
            public UserType typ;
            public UserState state;
            [MarshalAsAttribute(UnmanagedType.I4)]
            public int islocal;
            public bool isLogin;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string dnsprefix;
        };

        public delegate void SDK_onIncomingCall(int callid, CallState state);
        public delegate void SDK_onCallState(int callid, CallState state, string stateText);
        public delegate void SDK_onRegState(byte state);
        public delegate void SDK_onSendMsgState(StatusCode status,string reason);
        public delegate void SDK_onReceiveMsg(string from, string msg);
        public delegate void SDK_onPTTState(string state);

        //来电回调
        [DllImport("SPhone.dll")]
        public extern static void SetCallback_IncomingCall(SDK_onIncomingCall callback);
        //呼叫状态回调
        [DllImport("SPhone.dll")]
        public extern static void SetCallback_CallState(SDK_onCallState callback);
        //注册状态
        [DllImport("SPhone.dll")]
        public extern static void SetCallback_RegState(SDK_onRegState callback);
        //短消息发送状态回调
        [DllImport("SPhone.dll")]
        public extern static void SetCallback_SendMsgState(SDK_onSendMsgState callback);
        //收消息回调
        [DllImport("SPhone.dll")]
        public extern static void SetCallback_ReceiveMsg(SDK_onReceiveMsg callback);
        //对讲状态通知
        [DllImport("SPhone.dll")]
        public extern static void SetCallback_PTTState(SDK_onPTTState callback);

        //初始化
        [DllImport("SPhone.dll")]
        public extern static void SDKInit(string localIp, int localPort, TransportType typ = TransportType.UDP);
        //销毁
        [DllImport("SPhone.dll")]
        public extern static void SDKDestory();
        //注册
        [DllImport("SPhone.dll")]
        public extern static bool Regist(string host, string uname, string pwd, bool mobile = false, bool localMode = false, int regTimeout = 60);
        //注销
        [DllImport("SPhone.dll")]
        public extern static void UnRegist();
        //发起呼叫
        [DllImport("SPhone.dll")]
        public extern static bool MakeCall(string number, bool isVideo);
        //本地模式发起呼叫
        [DllImport("SPhone.dll")]
        public extern static bool MakeCallLocalMode(string uri,int port, bool isVideo);
        //应答
        [DllImport("SPhone.dll")]
        public extern static void Answer(int callid, bool isVideo);
        //挂断呼叫
        [DllImport("SPhone.dll")]
        public extern static void Hangup(int callid);
        //挂断所有
        [DllImport("SPhone.dll")]
        public extern static void HangupAll();
        //切换视频设备
        [DllImport("SPhone.dll")]
        public extern static bool ChangeVideoDevice(int callid, int deviceIndex);
        //切换音频输入设备
        [DllImport("SPhone.dll")]
        public extern static bool ChangeAudCaptureDevice(int callid, int deviceIndex);
        //切换音频输出设备
        [DllImport("SPhone.dll")]
        public extern static bool ChangeAudSpeakerDevice(int callid, int deviceIndex);
        //获取所有视频设备
        [DllImport("SPhone.dll")]
        public extern static void GetVideoDevices([Out]VideoDeviceInfo[] info, out int len);        
        //获取所有音频设备信息
        [DllImport("SPhone.dll")]
        public extern static void GetAudioDevices([Out]AudioDeviceInfo[] info, out int len);
        //设置默认视频设备
        [DllImport("SPhone.dll")]
        public extern static void SetDefaultVideoDevice(int index);
        //设置默认音频设备
        [DllImport("SPhone.dll")]
        public extern static bool SetDefaultAudioDevice(int input, int output);
        //预览-开始
        [DllImport("SPhone.dll")]
        public extern static void Preview_Start(IntPtr p);
        //预览-结束
        [DllImport("SPhone.dll")]
        public extern static void Preview_Stop();
        //设置视频图像句柄
        [DllImport("SPhone.dll")]
        public extern static void SetVideoHandle(IntPtr p);
        //设置视频大小
        [DllImport("SPhone.dll")]
        public extern static void SetVideoSize(int callid,int x,int y,int w, int h);
        //设置视频参数，分辨率、帧率、码率
        [DllImport("SPhone.dll")]
        public extern static bool SetVideoCodecParam(int w, int h, int fps, int bps);
        //发送关键帧
        [DllImport("SPhone.dll")]
        public extern static void SendKeyFrame(int callid);
        //请求关键帧
        [DllImport("SPhone.dll")]
        public extern static void RequestKeyFrame(int callid);
        //保持
        [DllImport("SPhone.dll")]
        public extern static void Hold(int callid);
        //取保持
        [DllImport("SPhone.dll")]
        public extern static void UnHold(int callid);
        //发送DTMF（rfc2833）
        [DllImport("SPhone.dll")]
        public extern static void SendDtmf(int callid, string dtmf);
        //发送短消息
        [DllImport("SPhone.dll")]
        public extern static void SendMsg(string num, string msg);
        //设置接收音频放大倍数
        [DllImport("SPhone.dll")]
        public extern static void SetRxLevel(int callid, float value);
        //设置发送音频放大倍数
        [DllImport("SPhone.dll")]
        public extern static void SetTxLevel(int callid, float value);
        //获取对讲组
        [DllImport("SPhone.dll")]
        public extern static void GetPTTGroup([In, Out]PTTGroupInfo[] pttGrps, out int len);
        //获取组成员
        [DllImport("SPhone.dll")]
        public extern static void GetGroupMember(string grp, [Out]UserInfo[] info, out int len);
        //申请或释放话权
        [DllImport("SPhone.dll")]
        public extern static bool PttReqRight(string grp, bool press);
        //创建会议
        [DllImport("SPhone.dll")]
        public extern static bool CreateConf([In]string[] mems, int len, StringBuilder cid);
        //解散会议
        [DllImport("SPhone.dll")]
        public extern static bool DeleteConf(string cid);        
        //会议邀请成员
        [DllImport("SPhone.dll")]
        public extern static bool ConfAddMember(string cid, string mem);
        //会议踢出成员
        [DllImport("SPhone.dll")]
        public extern static bool ConfRemoveMember(string cid, string mem);
                
        //该量为实时音量幅度，无法作为声音大小量化系数
        [DllImport("SPhone.dll")]
        public extern static void GetRxLevel(int callid, ref float value);
        //[DllImport("SPhone.dll")]
        //public extern static void GetTxLevel(int callid, ref float value); 
    }
}
