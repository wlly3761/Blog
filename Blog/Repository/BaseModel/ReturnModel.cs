using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BaseModel;

//封装通用返回数据模型，包含数据、状态码、总数
public class ReturnModel<T>: PageModel
{

    /// <summary>
    /// 数据
    /// </summary>
    public List<T> Data { get; set; }
    /// <summary>
    /// Code码
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 提示消息
    /// </summary>
    public string Msg { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMsg { get; set; }
}