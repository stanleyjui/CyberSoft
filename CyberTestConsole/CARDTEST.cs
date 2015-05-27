using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Cybersoft.Biz.Cust;
using System.Diagnostics;

namespace CyberTestConsole
{
    class CARDTEST
    {
        //宣告回傳碼
        static string strRC = "";
        static Cybersoft.Base.CMCJOB001 CMCJOB = new Cybersoft.Base.CMCJOB001();
        static string JCL = "";
        static string ACTION = "";
        static string STEP = "";
        static string PARM1 = "";
        static string PARM2 = "";
        static string METHOD = "";
        static string MODE = "AUTO";
        static string strJOBID = "00001";
        static int PID = 0;
        static decimal CPU_TIME = 0;
        static decimal MEMORY_WORK = 0;
        static Stopwatch sw = new Stopwatch();
        static void Main(string[] args)
        {
            //取得PID 
            using (Process currentProcess = Process.GetCurrentProcess()) { CMCJOB.PID = PID = currentProcess.Id; }
            sw.Start();
            //解析命令列引數
            string _input = checkARGS(args);
            //取得主機名稱
            CMCJOB.strJobEnv = Dns.GetHostName();
            //程式開始訊息
            Console.WriteLine("=== JOB {0} START{1} === ", _input, DateTime.Now.ToShortTimeString());
            //取得{JOB NAME}:{JOB STEP}:{PARM1}:{PARM2}
            string[] strARGS = _input.ToString().Split(':');
            //JCL名稱
            JCL = strARGS[0];
            //CWF程式名稱
            ACTION = strARGS[1];
            //CWF程式方法名稱
            string[] BIZ = ACTION.Split('.');
            ACTION = BIZ[0].ToString();
            METHOD = BIZ.Length == 2 ? BIZ[1].ToString().Replace('(', ' ').Replace(')', ' ').TrimEnd() : "";
            //CWF步驟(STEP)
            STEP = strARGS.Length > 2 ? strARGS[2] : "";
            //參數1
            PARM1 = strARGS.Length > 3 ? strARGS[3] : "";
            //參數2
            PARM2 = strARGS.Length > 4 ? strARGS[4] : "";
            //批次訊息
            CMCJOB.iJOB_LOG_HIST(JCL, ACTION, strJOBID, string.Format("{0}:{1} Start!", CMCJOB.strJobEnv, _input));
            if (!"00001".Equals(strJOBID)) { CMCJOB.strJobID = strJOBID; };

            //確認批次是否可執行
            #region 確認批次是否可執行
            CMCJOB.strJobName = JCL;
            CMCJOB.strJobStep = ACTION;
            if (ACTION == "START")
            {
                //呼叫[相依]程式:確認JOB是否可執行
                string JOB_ACTION = CMCJOB.isJOBRunable();
                switch (JOB_ACTION)
                {
                    case "run":
                        break;

                    case "wait":   //WAITING 
                        System.Environment.ExitCode = 1;
                        return;

                    case "pass":   //已執行過了
                        System.Environment.ExitCode = 2;
                        return;
                }

            }
            #endregion

            //BATCH/JOB/JOB SETUP [RUN]訊息
            #region BATCH/JOB/JOB SETUP [RUN]訊息
            switch (ACTION)
            {
                default:
                    CMCJOB.strJobName = JCL;
                    CMCJOB.strJobStep = ACTION;
                    CMCJOB.Start_Job();
                    break;
            }
            #endregion
            //保留JOB_ID
            strJOBID = CMCJOB.strJobID; 
            switch (ACTION)
            {
                //公共事業
                case "PBBELI001":
                    PBBELI001 PBBELI001 = new PBBELI001();
                    PBBELI001.strJobID = strJOBID;
                    strRC = PBBELI001.RUN("1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBELI002":
                    PBBELI002 PBBELI002 = new PBBELI002();
                    PBBELI002.strJobID = strJOBID;
                    strRC = PBBELI002.RUN("5");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPHI001":
                    PBBPHI001 PBBPHI001 = new PBBPHI001();
                    PBBPHI001.strJobID = strJOBID;
                    strRC = PBBPHI001.RUN("1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBTWI001":
                    PBBTWI001 PBBTWI001 = new PBBTWI001();
                    PBBTWI001.strJobID = strJOBID;
                    strRC = PBBTWI001.RUN("1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPWI001":
                    PBBPWI001 PBBPWI001 = new PBBPWI001();
                    PBBPWI001.strJobID = strJOBID;
                    strRC = PBBPWI001.RUN("1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHI001_A":     //主動行
                    PBBAHI001 PBBAHI001_A = new PBBAHI001();
                    PBBAHI001_A.strJobID = "00001";
                    strRC = PBBAHI001_A.RUN("ACTIVE", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHI001_P":     //被動行
                    PBBAHI001 PBBAHI001_P = new PBBAHI001();
                    PBBAHI001_P.strJobID = "00001";
                    strRC = PBBAHI001_P.RUN("PASSIVE", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHO001_A":     //主動行
                    PBBAHO001 PBBAHO001_A = new PBBAHO001();
                    PBBAHO001_A.strJobID = strJOBID;
                    strRC = PBBAHO001_A.RUN("ACTIVE");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHO001_P":     //被動行
                    PBBAHO001 PBBAHO001_P = new PBBAHO001();
                    PBBAHO001_P.strJobID = strJOBID;
                    strRC = PBBAHO001_P.RUN("PASSIVE");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSI001_YM":    //陽明山瓦斯
                    PBBGSI001 PBBGSI001_YM = new PBBGSI001();
                    PBBGSI001_YM.strJobID = strJOBID;
                    strRC = PBBGSI001_YM.RUN("YMGAS", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSI001_SG":    //欣高瓦斯
                    PBBGSI001 PBBGSI001_SG = new PBBGSI001();
                    PBBGSI001_SG.strJobID = strJOBID;
                    strRC = PBBGSI001_SG.RUN("SGGAS", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSI001_GM":    //竹名瓦斯
                    PBBGSI001 PBBGSI001_GM = new PBBGSI001();
                    PBBGSI001_GM.strJobID = strJOBID;
                    strRC = PBBGSI001_GM.RUN("GMGAS", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSO001_YM":    //陽明山瓦斯
                    PBBGSO001 PBBGSO001_YM = new PBBGSO001();
                    PBBGSO001_YM.strJobID = strJOBID;
                    strRC = PBBGSO001_YM.RUN("YMGAS");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSO001_SG":    //欣高瓦斯
                    PBBGSO001 PBBGSO001_SG = new PBBGSO001();
                    PBBGSO001_SG.strJobID = strJOBID;
                    strRC = PBBGSO001_SG.RUN("SGGAS");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSO001_GM":    //竹名瓦斯
                    PBBGSO001 PBBGSO001_GM = new PBBGSO001();
                    PBBGSO001_GM.strJobID = strJOBID;
                    strRC = PBBGSO001_GM.RUN("GMGAS");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_TPE":    //台北市停車費
                    PBBPKI001 PBBPKI001_TPE = new PBBPKI001();
                    PBBPKI001_TPE.strJobID = strJOBID;
                    strRC = PBBPKI001_TPE.RUN("TPE", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_TPC":    //台北縣停車費
                    PBBPKI001 PBBPKI001_TPC = new PBBPKI001();
                    PBBPKI001_TPC.strJobID = strJOBID;
                    strRC = PBBPKI001_TPC.RUN("TPC", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_PRK":    //台南市停車費
                    PBBPKI001 PBBPKI001_PRK = new PBBPKI001();
                    PBBPKI001_PRK.strJobID = strJOBID;
                    strRC = PBBPKI001_PRK.RUN("PRK", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_KAO":    //高雄市停車費
                    PBBPKI001 PBBPKI001_KAO = new PBBPKI001();
                    PBBPKI001_KAO.strJobID = strJOBID;
                    strRC = PBBPKI001_KAO.RUN("KAO", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO001_TPE":    //台北市停車費
                    PBBPKO001 PBBPKO001_TPE = new PBBPKO001();
                    PBBPKO001_TPE.strJobID = strJOBID;
                    strRC = PBBPKO001_TPE.RUN("TPE");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO001_TPC":    //台北縣停車費
                    PBBPKO001 PBBPKO001_TPC = new PBBPKO001();
                    PBBPKO001_TPC.strJobID = strJOBID;
                    strRC = PBBPKO001_TPC.RUN("TPC");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO001_PRK":    //台南市停車費
                    PBBPKO001 PBBPKO001_PRK = new PBBPKO001();
                    PBBPKO001_PRK.strJobID = strJOBID;
                    strRC = PBBPKO001_PRK.RUN("PRK");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO001_KAO":    //高雄市停車費
                    PBBPKO001 PBBPKO001_KAO = new PBBPKO001();
                    PBBPKO001_KAO.strJobID = strJOBID;
                    strRC = PBBPKO001_KAO.RUN("KAO");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_TPE":    //台北市停車費
                    PBBPKI002 PBBPKI002_TPE = new PBBPKI002();
                    PBBPKI002_TPE.strJobID = strJOBID;
                    strRC = PBBPKI002_TPE.RUN("TPE", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_TPC":    //台北縣停車費
                    PBBPKI002 PBBPKI002_TPC = new PBBPKI002();
                    PBBPKI002_TPC.strJobID = strJOBID;
                    strRC = PBBPKI002_TPC.RUN("TPC", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_PRK":    //台南市停車費
                    PBBPKI002 PBBPKI002_PRK = new PBBPKI002();
                    PBBPKI002_PRK.strJobID = strJOBID;
                    strRC = PBBPKI002_PRK.RUN("PRK", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_KAO":    //高雄市停車費
                    PBBPKI002 PBBPKI002_KAO = new PBBPKI002();
                    PBBPKI002_KAO.strJobID = strJOBID;
                    strRC = PBBPKI002_KAO.RUN("KAO", "1");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO002_TPE":    //台北市停車費
                    PBBPKO002 PBBPKO002_TPE = new PBBPKO002();
                    PBBPKO002_TPE.strJobID = strJOBID;
                    strRC = PBBPKO002_TPE.RUN("TPE");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO002_TPC":    //台北縣停車費
                    PBBPKO002 PBBPKO002_TPC = new PBBPKO002();
                    PBBPKO002_TPC.strJobID = strJOBID;
                    strRC = PBBPKO002_TPC.RUN("TPC");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO002_PRK":    //台南市停車費
                    PBBPKO002 PBBPKO002_PRK = new PBBPKO002();
                    PBBPKO002_PRK.strJobID = strJOBID;
                    strRC = PBBPKO002_PRK.RUN("PRK");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKO002_KAO":    //高雄市停車費
                    PBBPKO002 PBBPKO002_KAO = new PBBPKO002();
                    PBBPKO002_KAO.strJobID = strJOBID;
                    strRC = PBBPKO002_KAO.RUN("KAO");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPHC001":
                    PBBPHC001 PBBPHC001 = new PBBPHC001();
                    PBBPHC001.strJobID = strJOBID;
                    strRC = PBBPHC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHC001":
                    PBBAHC001 PBBAHC001 = new PBBAHC001();
                    PBBAHC001.strJobID = strJOBID;
                    strRC = PBBAHC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBELC001":
                    PBBELC001 PBBELC001 = new PBBELC001();
                    PBBELC001.strJobID = strJOBID;
                    strRC = PBBELC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBTWC001":
                    PBBTWC001 PBBTWC001 = new PBBTWC001();
                    PBBTWC001.strJobID = strJOBID;
                    strRC = PBBTWC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGMC001":
                    PBBGMC001 PBBGMC001 = new PBBGMC001();
                    PBBGMC001.strJobID = strJOBID;
                    strRC = PBBGMC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPWC001":
                    PBBPWC001 PBBPWC001 = new PBBPWC001();
                    PBBPWC001.strJobID = strJOBID;
                    strRC = PBBPWC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBYMC001":
                    PBBYMC001 PBBYMC001 = new PBBYMC001();
                    PBBYMC001.strJobID = strJOBID;
                    strRC = PBBYMC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBSGC001":
                    PBBSGC001 PBBSGC001 = new PBBSGC001();
                    PBBSGC001.strJobID = strJOBID;
                    strRC = PBBSGC001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPHR001":
                    PBBPHR001 PBBPHR001 = new PBBPHR001();
                    PBBPHR001.strJobID = strJOBID;
                    strRC = PBBPHR001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHR001":
                    PBBAHR001 PBBAHR001 = new PBBAHR001();
                    PBBAHR001.strJobID = strJOBID;
                    strRC = PBBAHR001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBAHR002":
                    PBBAHR002 PBBAHR002 = new PBBAHR002();
                    PBBAHR002.strJobID = strJOBID;
                    strRC = PBBAHR002.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPWR001":
                    PBBPWR001 PBBPWR001 = new PBBPWR001();
                    PBBPWR001.strJobID = strJOBID;
                    strRC = PBBPWR001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMBS001":
                    PBBMBS001 PBBMBS001 = new PBBMBS001();
                    PBBMBS001.strJobID = strJOBID;
                    strRC = PBBMBS001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMBS002":
                    PBBMBS002 PBBMBS002 = new PBBMBS002();
                    PBBMBS002.strJobID = strJOBID;
                    strRC = PBBMBS002.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBELR001":
                    PBBELR001 PBBELR001 = new PBBELR001();
                    PBBELR001.strJobID = strJOBID;
                    strRC = PBBELR001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;
                case "PBBELR002":
                    PBBELR002 PBBELR002 = new PBBELR002();
                    PBBELR002.strJobID = strJOBID;
                    strRC = PBBELR002.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBELR003":
                    PBBELR003 PBBELR003 = new PBBELR003();
                    PBBELR003.strJobID = strJOBID;
                    strRC = PBBELR003.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMEG001(ELECT)":
                    PBBMEG001 PBBMEG001_ELECT = new PBBMEG001();
                    PBBMEG001_ELECT.strJobID = strJOBID;
                    strRC = PBBMEG001_ELECT.RUN("ELECT_CHGOUT");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMEG001(TWWATER)":
                    PBBMEG001 PBBMEG001_TWWATER = new PBBMEG001();
                    PBBMEG001_TWWATER.strJobID = strJOBID;
                    strRC = PBBMEG001_TWWATER.RUN("TWWATER_CHGOUT");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMEG001(YMGAS)":
                    PBBMEG001 PBBMEG001_YMGAS = new PBBMEG001();
                    PBBMEG001_YMGAS.strJobID = strJOBID;
                    strRC = PBBMEG001_YMGAS.RUN("YMGAS_CHGOUT");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMEG001(SGGAS)":
                    PBBMEG001 PBBMEG001_SGGAS = new PBBMEG001();
                    PBBMEG001_SGGAS.strJobID = strJOBID;
                    strRC = PBBMEG001_SGGAS.RUN("SGGAS_CHGOUT");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMEG001(GMGAS)":
                    PBBMEG001 PBBMEG001_GMGAS = new PBBMEG001();
                    PBBMEG001_GMGAS.strJobID = strJOBID;
                    strRC = PBBMEG001_GMGAS.RUN("GMGAS_CHGOUT");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_TPE2":    //台北市停車費
                    PBBPKI001 PBBPKI001_TPE2 = new PBBPKI001();
                    PBBPKI001_TPE2.strJobID = strJOBID;
                    strRC = PBBPKI001_TPE2.RUN("TPE", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_TPC2":    //台北縣停車費
                    PBBPKI001 PBBPKI001_TPC2 = new PBBPKI001();
                    PBBPKI001_TPC2.strJobID = strJOBID;
                    strRC = PBBPKI001_TPC2.RUN("TPC", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_PRK2":    //台南市停車費
                    PBBPKI001 PBBPKI001_PRK2 = new PBBPKI001();
                    PBBPKI001_PRK2.strJobID = strJOBID;
                    strRC = PBBPKI001_PRK2.RUN("PRK", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI001_KAO2":    //高雄市停車費
                    PBBPKI001 PBBPKI001_KAO2 = new PBBPKI001();
                    PBBPKI001_KAO2.strJobID = strJOBID;
                    strRC = PBBPKI001_KAO2.RUN("KAO", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_TPE2":    //台北市停車費
                    PBBPKI002 PBBPKI002_TPE2 = new PBBPKI002();
                    PBBPKI002_TPE2.strJobID = strJOBID;
                    strRC = PBBPKI002_TPE2.RUN("TPE", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_TPC2":    //台北縣停車費
                    PBBPKI002 PBBPKI002_TPC2 = new PBBPKI002();
                    PBBPKI002_TPC2.strJobID = strJOBID;
                    strRC = PBBPKI002_TPC2.RUN("TPC", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_PRK2":    //台南市停車費
                    PBBPKI002 PBBPKI002_PRK2 = new PBBPKI002();
                    PBBPKI002_PRK2.strJobID = strJOBID;
                    strRC = PBBPKI002_PRK2.RUN("PRK", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBPKI002_KAO2":    //高雄市停車費
                    PBBPKI002 PBBPKI002_KAO2 = new PBBPKI002();
                    PBBPKI002_KAO2.strJobID = strJOBID;
                    strRC = PBBPKI002_KAO2.RUN("KAO", "2");
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBSUM001":
                    PBBSUM001 PBBSUM001 = new PBBSUM001();
                    PBBSUM001.strJobID = strJOBID;
                    strRC = PBBSUM001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBTWR001":
                    PBBTWR001 PBBTWR001 = new PBBTWR001();
                    PBBTWR001.strJobID = strJOBID;
                    strRC = PBBTWR001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBGSR001":
                    PBBGSR001 PBBGSR001 = new PBBGSR001();
                    PBBGSR001.strJobID = strJOBID;
                    strRC = PBBGSR001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBMNT001":
                    PBBMNT001 PBBMNT001 = new PBBMNT001();
                    PBBMNT001.strJobID = strJOBID;
                    strRC = PBBMNT001.RUN();
                    System.Environment.ExitCode = convRC();
                    break;

                case "PBBFIC002":
                    PBBFIC002 PBBFIC002 = new PBBFIC002();
                    PBBFIC002.strJobID = strJOBID;
                    strRC = PBBFIC002.RUN();
                    System.Environment.ExitCode = convRC();
                    break;


                default:
                    bool IsProcess = false;
                    string[] DLLsPath = System.IO.Directory.GetFiles(System.Windows.Forms.Application.StartupPath, "CyberTest.Biz.Cust.dll", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (string StrPath in DLLsPath)
                    {
                        System.Reflection.Assembly Asb = System.Reflection.Assembly.LoadFrom(StrPath);
                        string NameSpace = System.IO.Path.GetFileNameWithoutExtension(StrPath) + ".";
                        Type Typ = Asb.GetType("Cybersoft.Biz.Cust." + ACTION);
                        if (Typ != null)
                        {
                            List<object> ListParameter = new List<object>();
                            //0:JCL、1:ACTION略過
                            for (int i = 2; i < strARGS.Length; i++)
                            {
                                if (!"".Equals(strARGS[i]))
                                {
                                    ListParameter.Add(strARGS[i]);
                                }
                            }
                            Object Obj = Activator.CreateInstance(Typ);
                            Typ.GetProperty("strJobID").SetValue(Obj, strJOBID, null);
                            System.Reflection.MethodInfo[] MInfo = Typ.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
                            foreach (var Var in MInfo)
                            {
                                if (!"RUN".Equals(Var.Name) && !"Main".Equals(Var.Name))
                                {
                                    continue;
                                }
                                if (Var.GetParameters().Length == ListParameter.Count)
                                {
                                    if (ListParameter.Count == 0)
                                    {
                                        strRC = Convert.ToString(Var.Invoke(Obj, null));
                                        break;
                                    }
                                    else
                                    {
                                        strRC = Convert.ToString(Var.Invoke(Obj, ListParameter.ToArray()));
                                        break;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            IsProcess = true;
                            System.Environment.ExitCode = convRC();
                            break;
                        }
                    }
                    if (!IsProcess)
                    {
                        Console.WriteLine("=== PROGRAM NOT DEFINED ==== ");
                        System.Environment.ExitCode = 23;
                    }
                    break;
            }

            //[結束]訊息
            CMCJOB.strJobName = JCL;
            CMCJOB.strJobStep = ACTION;
            CMCJOB.strJobResult = strRC;
            CMCJOB.End_Job();

            Console.WriteLine(strRC);
            Console.WriteLine("=== JOB END  " + DateTime.Now.ToShortTimeString() + " ==== ");
        }

        /// <summary>
        /// 檢查Console輸入參數
        /// </summary>
        /// <param name="args"></param>
        static string checkARGS(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a argument JOB:PGM (ex:RPT01:ISBRPT001)");
                Console.Write("->");
                args = new String[1];
                args[0] = Console.ReadLine();
                while ("".Equals(args[0])) args[0] = Console.ReadLine();
                MODE = "MANUAL";
            }
            strJOBID = args.Length == 2 ? args[1].ToString() : "00001";
            if (!args[0].Contains(":"))
            {
                Console.Write("Please enter a argument JOB:PGM(Colon:)");
                while (!args[0].Contains(":")) args[0] = Console.ReadLine();
            }
            return args[0];
        }
        static int convRC()
        {
            if (strRC.Length >= 5)
            {
                switch (strRC.Substring(0, 5))
                {
                    case "B0000":
                        return 0;

                    default:
                        CMCJOB.strJobName = JCL;
                        CMCJOB.strJobStep = "";
                        CMCJOB.strJobResult = strRC;
                        //CMCJOB.End_Job();

                        return 12;
                }
            }
            else
            {
                CMCJOB.strJobName = JCL;
                CMCJOB.strJobStep = "";
                CMCJOB.strJobResult = strRC;
                //CMCJOB.End_Job();
                return 98;

            }

        }
    }
}