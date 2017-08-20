using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Abstractions.Configuration
{
    public class EnvironmentConfiguration
    {
        public enum Environment
        {
            LocalUnit,
            LocalMock,
            LocalDev,
            LocalTest,
            Unit,
            UITests,
            Dev,
            Mock,
            Test,
            Prod,
            Store
        }

        public string ServiceUrl { get; set; }
        public bool UsesMockData { get; set; }




#if ENV_LOCAL_UNIT
                const Environment CurrentEnvironment = Environment.LocalUnit;
#endif

#if ENV_LOCAL_MOCK
                const Environment CurrentEnvironment = Environment.LocalMock;
#endif

#if ENV_LOCAL_DEV
                const Environment CurrentEnvironment = Environment.LocalDev;
#endif

#if ENV_LOCAL_TEST
                const Environment CurrentEnvironment = Environment.LocalTest;
#endif

#if ENV_UNIT
        const Environment CurrentEnvironment = Environment.Unit;
#endif

#if ENV_MOCK
                const Environment CurrentEnvironment = Environment.Mock;
#endif

#if ENV_DEV
                const Environment CurrentEnvironment = Environment.Dev;
#endif

#if ENV_TEST
                const Environment CurrentEnvironment = Environment.Test;
#endif

#if ENV_PROD
                const Environment CurrentEnvironment = Environment.Prod;
#endif
#if ENV_STORE
                const Environment CurrentEnvironment = Environment.Store;
#endif



        private static readonly Lazy<EnvironmentConfiguration> lazy =
           new Lazy<EnvironmentConfiguration>(() => new EnvironmentConfiguration());

        public static EnvironmentConfiguration Instance { get { return lazy.Value; } }

        private EnvironmentConfiguration()
        {

#if MOCK

              SetupLocalMock();
#endif
#if ENV_LOCAL_UNIT
                SetupLocalUnit();
#endif

#if ENV_LOCAL_MOCK
                SetupLocalMock();
#endif

#if ENV_LOCAL_DEV
                SetupLocalDev();
#endif

#if ENV_LOCAL_TEST
                SetupLocalTest();
#endif

#if ENV_UNIT
                SetupUnit();
#endif

#if ENV_UITESTS
                SetupUITests();
#endif

#if ENV_MOCK
                SetupMock();
#endif

#if ENV_DEV
                SetupDev();
#endif

#if ENV_TEST
                 SetupTest();
#endif

#if ENV_PROD
                 SetupProd();
#endif
#if ENV_STORE
                SetupStore();
#endif
        }



#if ENV_LOCAL_UNIT
        private void SetupLocalUnit()
        {
            //general
            this.ServiceUrl = String.Empty;
            this.UsesMockData = true;
  			this.IsInScreenUnitTestingMode = true;

        }
#endif

#if ENV_LOCAL_MOCK
        private void SetupLocalMock()
        {
            //general
            this.ServiceUrl = String.Empty;
            this.UsesMockData = true;
        }
#endif

#if ENV_LOCAL_DEV
		private void SetupLocalDev()
        {
            
            //general
            this.ServiceUrl = "https://rigservemobile-azurewebsites-net-jb6l0grkqylx.runscope.net";
            //this.ServiceUrl = "http://localhost:30486/";
            this.UsesMockData = false;
        }
#endif

#if ENV_LOCAL_TEST
        private void SetupLocalTest()
        {
            //general
            this.ServiceUrl = "http://raptorapi--staging-cloudapp-net-3f8z49bkaagd.runscope.net";
            this.UsesMockData = false;
        }
#endif


#if ENV_DEV
        private void SetupDev()
        {
            //general
            this.ServiceUrl = "http://raptorapi--test-cloudapp-net-e0cea29pcnrm.runscope.net";
            this.UsesMockData = false;
        }
#endif

#if ENV_UNIT
        private void SetupUnit()
        {
            //general
            this.ServiceUrl = String.Empty;
            this.UsesMockData = true;
            this.IsInScreenUnitTestingMode = true;		
        }
#endif

#if ENV_UITESTS
        private void SetupUITests()
        {
            //general
            this.ServiceUrl = String.Empty;
            this.UsesMockData = true;
        }
#endif

#if ENV_MOCK
        private void SetupMock()
        {
            //general
            this.ServiceUrl = String.Empty;
            this.UsesMockData = true;
        }
#endif



#if ENV_TEST
        private void SetupTest()
        {
            //general
            this.ServiceUrl = "http://raptorapi--staging-cloudapp-net-3f8z49bkaagd.runscope.net";
            this.UsesMockData = false;
        }
#endif

#if ENV_PROD
        private void SetupProd()
        {
            //general
            this.ServiceUrl = "https://test.raptortech.com:8081";
            this.UsesMockData = false;	
        }
#endif

#if ENV_STORE
        private void SetupStore()
        {
            //general
            this.ServiceUrl = "https://test.raptortech.com:8081";
            this.UsesMockData = false;	
        }
#endif
    }
}
