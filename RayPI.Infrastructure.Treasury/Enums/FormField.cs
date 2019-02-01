using System.ComponentModel;

namespace RayPI.Infrastructure.Treasury.Enums
{
    /// <summary>
    /// 用于前端显示的字段类型
    /// </summary>
    public enum FormFieldDisplayTypeCode
    {
        /// <summary>
        ///     文本框
        /// </summary>
        [Description("text")]
        TextBox = 1,

        /// <summary>
        ///     多行文本
        /// </summary>
        [Description("textarea")]
        TextArea = 2,

        /// <summary>
        ///     单选框组
        /// </summary>
        [Description("radioGroup")]
        RadioGroup = 3,

        /// <summary>
        ///     选择框
        /// </summary>
        [Description("checkbox")]
        CheckBox = 4,

        /// <summary>
        ///     选择框组
        /// </summary>
        [Description("checkboxGroup")]
        CheckBoxGroup = 5,

        /// <summary>
        ///     下拉框
        /// </summary>
        [Description("dropdown")]
        DropDown = 6,

        /// <summary>
        ///     日期
        /// </summary>
        [Description("date")]
        Date = 7,

        /// <summary>
        ///     日期时间
        /// </summary>
        [Description("datetime")]
        DateTime = 8,

        /// <summary>
        ///     隐藏域
        /// </summary>
        [Description("hidden")]
        Hidden = 9
    }

    /// <summary>
    /// 字段的值必填的类型
    /// </summary>
    public enum RequiredTypeEnum
    {
        /// <summary>
        ///     非必填
        /// </summary>
        None = 1,

        /// <summary>
        ///     在有条件的必填【住院天数|胸痛患者+有治疗措施】
        /// </summary>
        Rule = 2,

        /// <summary>
        ///     审核通过必填【发病地址】
        /// </summary>
        Submit = 3,

        /// <summary>
        ///     创建时(保存时)必填
        /// </summary>
        Create = 4
    }

    /// <summary>
    /// 验证的类型
    /// </summary>
    public enum ValidateRuleType
    {
        /// <summary>
        ///     基础类型的format，int|datetime
        /// </summary>
        BaseFormat = 1,

        /// <summary>
        /// 自动审核的时间先后验证
        /// </summary>
        AutoAuditor = 2,
        /// <summary>
        ///     业务验证/自定义验证/提交到国家平台审核的的自定义验证/组合必填验证/?时间先后验证?
        /// </summary>
        ChinaCPCBusiness = 3
    }

    /// <summary>
    /// 选项数据的类型
    /// </summary>
    public enum OptionDataTypeEnum
    {
        /// <summary>
        ///     字典
        /// </summary>
        Dictionary = 1,

        /// <summary>
        ///     本地，直接存在数据的OptionData字段
        /// </summary>
        Local = 2
    }

    /// <summary>
    /// 规则技术实现类型
    /// </summary>
    public enum RuleTypeEnum
    {
        /// <summary>
        ///     JS函数
        /// </summary>
        JsFun = 1,

        /// <summary>
        ///     正则表达式
        /// </summary>
        RegularExpression = 2
    }

    /// <summary>
    /// 填报表单的状态
    /// </summary>
    public enum FillStatusEnum
    {
        /// <summary>
        /// 初始状态/填报中
        /// </summary>
        [Description("填报中")]
        Writing = 1,
        /// <summary>
        /// 提交审核/等待存档
        /// </summary>
        [Description("等待存档")]
        WaitForArchive = 2,

        /// <summary>
        /// 审核通过/已归档
        /// </summary>
        [Description("已归档")]
        Archive = 4

    }

    /// <summary>
    /// 角色
    /// Description 用于显示
    /// 数字直接对应到应用基础服务的角色ID
    /// 枚举值直接对应到应用基础服务的角色cpc_[Code]（不完全一致)
    /// </summary>
    public enum ChinaCPCRoleEnum
    {

        /// <summary>
        /// 填报员
        /// </summary>
        [Description("填报员")]
        TianBaoYuan = 30090,
        /// <summary>
        /// 审核员
        /// </summary>
        [Description("审核员")]
        ShenHeYuan = 30091,
        /// <summary>
        /// 归档员
        /// </summary>
        [Description("归档员")]
        GuiDangYuan = 30092,
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        GuanLiYuan = 30093
    }
    /// <summary>
    /// 填报表单数据操作类型
    /// </summary>
    public enum CaseReportFormDataOperationType
    {
        /// <summary>
        /// 创建
        /// </summary>
        [Description("创建")]
        Create = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Modify = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Remove = 4,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Audit = 5,

        /// <summary>
        /// 归档
        /// </summary>
        [Description("归档")]
        Archive = 6,
    }

    /// <summary>
    /// 数据转换类型
    /// </summary>
    public enum ConvertSign
    {
        /// <summary>
        /// 转为急诊
        /// </summary>
        From_ChestPain_To_SIR = 0,

        /// <summary>
        /// 转为中国填报中心
        /// </summary>
        From_ChestPain_To_ChinaCPC = 1,

        /// <summary>
        /// 从CPCV103转为填报表单数据
        /// </summary>
        From_CPCV103_To_ChestPain = 2,

        /// <summary>
        /// 从扁鹊系统类型转为填报表单
        /// </summary>
        From_BianQue_To_ChestPain = 3,

        /// <summary>
        /// 
        /// </summary>
        From_Interface_To_ChestPain = 4,

        /// <summary>
        /// 
        /// </summary>
        From_TimeCollector_To_ChestPain = 5,

        /// <summary>
        /// 
        /// </summary>
        From_POCT_To_ChestPain = 6,

        /// <summary>
        /// 
        /// </summary>
        From_ECG_To_ChestPain = 7,

        /// <summary>
        /// 
        /// </summary>
        From_SIR_To_ChestPain = 8,

        /// <summary>
        /// 
        /// </summary>
        From_PreHospitalCare_To_ChestPain = 9,

        /// <summary>
        /// 
        /// </summary>
        From_AW__To_ChestPain = 10,

        /// <summary>
        /// 从国家填报平台数据转为填报表单数据
        /// </summary>
        From_ChinaCPC_To_ChestPain = 11,

        /// <summary>
        /// 微信院前急救
        /// </summary>
        From_PreHospitalFirstAid_To_ChestPain = 12,
        /// <summary>
        /// 急诊绿色通道到卒中
        /// </summary>
        From_EmergencyGreenChannel_To_Stroke = 13,
        /// <summary>
        /// 时间采集器到卒中
        /// </summary>
        From_TimeCollector_To_Stroke = 14,
        /// <summary>
        /// 卒中到急诊绿色通道到
        /// </summary>
        From_Stroke_To_EmergencyGreenChannel = 15,

        /// <summary>
        /// 接口到卒中
        /// </summary>
        From_Interface_To_Stroke = 16,
    }

    /// <summary>
    /// 填报表单数据转换的实现类型
    /// </summary>
    public enum DataConvertType
    {
        /// <summary>
        /// 字段名直接获取值
        /// </summary>
        Simple = 1,
        /// <summary>
        /// 利用CRFData通过JS函数
        /// </summary>
        JsFun = 2,
    }
}
