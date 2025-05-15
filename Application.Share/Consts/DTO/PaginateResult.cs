using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Share.Consts.DTO
{
    public class PaginateResult<TModel> where TModel : class
    {
        public List<TModel> DATA { get; set; }
        public int TOTAL_RECORD {  get; set; }
    }
}
