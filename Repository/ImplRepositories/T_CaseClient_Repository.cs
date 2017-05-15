using EF_DataModel;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImplRepositories
{
    public partial class T_CaseClient_Repository : BaseRepository<T_CaseClient>, I_T_CaseClient_Repository
    {

        public T_CaseClient_Repository(DataModelContext dbContext)
            : base(dbContext)
        { }
    }
}
