using Core.Attribute;
using SqlSugar;

namespace Application.DBFirst;
[DynamicApi(ServiceLifeCycle = "Scoped")]
public class DbFirst:IDbFirst
{
    private readonly ISqlSugarClient _sugarClient;
    public DbFirst(ISqlSugarClient sugarClient)
    {
        _sugarClient = sugarClient;
    }
    /// <summary>
    /// 使用DbFirst创建数据库所有表的Model类
    /// </summary>
    public void DbFirstCreateModel()
    {
        _sugarClient.DbFirst.SettingClassTemplate(old => { return old;})
            //类构造函数
            .SettingConstructorTemplate(old =>{return old;/*修改old值替换*/ })
            .SettingNamespaceTemplate(old => {
                return old + "\r\nusing SqlSugar;"; //追加引用SqlSugar
            })
            //属性备注
            .SettingPropertyDescriptionTemplate(old =>{ return old;/*修改old值替换*/})
            //属性:新重载 完全自定义用配置
            .SettingPropertyTemplate((columns,temp,type) => {
              
                var columnattribute = "\r\n           [SugarColumn({0})]";
                List<string> attributes = new List<string>();
                if (columns.IsPrimarykey)
                    attributes.Add("IsPrimaryKey=true");
                if (columns.IsIdentity)
                    attributes.Add("IsIdentity=true");
                if (attributes.Count == 0) 
                {
                    columnattribute = "";
                }
                return temp.Replace("{PropertyType}", type)
                    .Replace("{PropertyName}", columns.DbColumnName)
                    .Replace("{SugarColumn}",string.Format(columnattribute,string.Join(",", attributes)));
            })
               
            .CreateClassFile("/Users/wenni/wl/SelfProject/Blog/Blog/Repository/Model");
    }
}