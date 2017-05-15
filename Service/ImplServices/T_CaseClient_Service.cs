using EF_DataModel;
using Microsoft.Practices.Unity;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ImplServices
{
    public partial class T_CaseClient_Service : BaseService<T_CaseClient>, I_T_CaseClient_Service
    {
        [Dependency]
        public I_T_CaseClient_Repository _i_T_CaseClient_Repository { get; set; }
        //自定义接口实现请建立同名partial class文件。防止代码被重新生成覆盖
    }
}
