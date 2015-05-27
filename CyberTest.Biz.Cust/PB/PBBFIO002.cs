using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cybersoft.Log;
using Cybersoft.Base;

namespace Cybersoft.Biz.Cust
{
    public class PBBFIO002
    {
        private string JobID;
        public string strJobID
        {
            get { return JobID; }
            set { JobID = value; }
        }
        private string RunCode;
        public string strRunCode
        {
            get { return RunCode; }
            set { RunCode = value; }
        }
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);

        public string RUN()
        {
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAHC001";
            logger.dtRunDate = DateTime.Now;
            CMCSYS001 SYSINF = new CMCSYS001();
            SYSINF.strJobID = JobID;
            SYSINF.strJobName = "PBBAHC001";
            String SYSINF_RC = SYSINF.getSYSINF();
     

            return "";
        }

    }
}
