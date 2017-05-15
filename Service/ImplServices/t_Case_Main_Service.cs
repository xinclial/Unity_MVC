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
    public partial class t_Case_Main_Service : BaseService<t_Case_Main>, I_t_Case_Main_Service
    {
        [Dependency]
        public I_t_Case_Main_Repositoy _i_t_Case_Main_Repositoy { get; set; }
        //自定义接口实现请建立同名partial class文件。防止代码被重新生成覆盖
    }
}
