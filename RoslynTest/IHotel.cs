using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTest
{
    /// <summary>
    /// 酒店详情
    /// </summary>
    public interface IHotel
    {
        /// <summary>
        /// 查询酒店列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string SearchHotelDetail(string context);
    }
}
