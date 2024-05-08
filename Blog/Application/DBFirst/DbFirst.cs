using Core.Attribute;
using Repository;
using Repository.Model;
using SqlSugar;

namespace Application.DBFirst;

// [DynamicApi(ServiceLifeCycle = "Scoped")]
public class DbFirst : BaseRepository<CertificateModel>,IDbFirst
{
    // private readonly ISqlSugarClient _sugarClient;
    //
    // public DbFirst(ISqlSugarClient sugarClient)
    // {
    //     _sugarClient = sugarClient;
    // }
    //
    // /// <summary>
    // ///     使用DbFirst创建数据库所有表的Model类
    // /// </summary>
    // public void DbFirstCreateModel()
    // {
    //     _sugarClient.DbFirst.SettingClassTemplate(old => { return old; })
    //         //类构造函数
    //         .SettingConstructorTemplate(old => { return old; /*修改old值替换*/ })
    //         .SettingNamespaceTemplate(old =>
    //         {
    //             return $"{old}\r\nusing SqlSugar;\r\nusing System.ComponentModel.DataAnnotations;"; //追加引用SqlSugar
    //         })
    //         //属性备注
    //         .SettingPropertyDescriptionTemplate(old => { return old; /*修改old值替换*/ })
    //         //属性:新重载 完全自定义用配置
    //         .SettingPropertyTemplate((columns, temp, type) =>
    //         {
    //             var returnStr = string.Empty;
    //             var columnattribute = string.Empty;
    //             var attributes = new List<string>();
    //             if (columns.IsPrimarykey)
    //             {
    //                 columnattribute = "\r\n           [SugarColumn({0})]";
    //                 attributes.Add("IsPrimaryKey=true");
    //                 if (columns.IsIdentity) attributes.Add("IsIdentity=true");
    //                 returnStr = temp.Replace("{PropertyType}", type)
    //                     .Replace("{PropertyName}", columns.DbColumnName)
    //                     .Replace("{SugarColumn}", string.Format(columnattribute, string.Join(",", attributes)));
    //             }
    //             else
    //             {
    //                 if (type == "string") columnattribute = $"\r\n           [MaxLength({columns.Length})]";
    //                 if (columns.IsNullable)
    //                     columnattribute += "\r\n           [Required(AllowEmptyStrings =true)]";
    //                 else
    //                     columnattribute += "\r\n           [Required(AllowEmptyStrings =false)]";
    //                 returnStr = temp.Replace("{PropertyType}", type)
    //                     .Replace("{PropertyName}", columns.DbColumnName)
    //                     .Replace("{SugarColumn}", columnattribute);
    //             }
    //
    //             return returnStr;
    //         })
    //         .CreateClassFile("/Users/wenni/wl/SelfProject/Blog/Blog/Repository/Model");
    // }
    public DbFirst(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
    {
    }
}