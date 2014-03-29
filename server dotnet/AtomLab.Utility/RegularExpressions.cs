namespace AtomLab.Utility
{
    public class RegularExpressions
    {
        public static readonly string QQ_NO = "[1-9][0-9]{4,}";//QQ
        public static readonly string EMAIL = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";//MSN 或 EMail
        public static readonly string TELEPHONE = @"\d{2,5}-\d{7,8}(-\d{1,})?";//固定电话或小灵通 (匹配形式如 0511-4405222 或 021-87888822)
        public static readonly string MOBILE = @"^1[3,5]\d{9}$";//手机 
        public static readonly string IDENTITYCARD = @"(^\d{15}$)|(^\d{17}([0-9]|X)$)";//身份证号    (中国的身份证为15位或18位)
        public static readonly string POSTCODE = @"[1-9]\d{5}(?!\d)";//邮政编码     (中国邮政编码为6位数字)
        public static readonly string CHINESE = "[\u4e00-\u9fa5]";//中文字符
        public static readonly string DOUBLE_BYTE = "[^\x00-\xff]";//双字节字符  (包括汉字在内)(一个双字节字符长度计2，ASCII字符计1)
        public static readonly string HTML = @"<(\S*?)[^>]*>.*?</\1>|<.*? />";//HTML标记    (这个也仅仅能匹配部分，对于复杂的嵌套标记依旧无能为力)
        public static readonly string URL = @"[a-zA-z]+://[^\s]*";//网址URL
        public static readonly string IP = @"\d+\.\d+\.\d+\.\d+";//ip地址
        public static readonly string ENGLISH_CHAR = @"^[A-Za-z]+$";　　//匹配由26个英文字母组成的字符串
        public static readonly string UPCHAR = @"^[A-Z]+$";　　//匹配由26个英文字母的大写组成的字符串
        public static readonly string LOWERCHAR = @"^[a-z]+$";　　//匹配由26个英文字母的小写组成的字符串
        public static readonly string CHAR_ANDN_NUMBER = @"^[A-Za-z0-9]+$";　//匹配由数字和26个英文字母组成的字符串
        public static readonly string SPECIAL_STRING = @"^\w+$";　　//匹配由数字、26个英文字母或者下划线组成的字符串
        public static readonly string POSITIVE_INTEGERS = @"^[1-9]\d*$";　 　 //匹配正整数
        public static readonly string NEGATIVE_INTEGERS = @"^-[1-9]\d*$"; 　 //匹配负整数
        public static readonly string INTEGERS = @"^-?[1-9]\d*$";　　 //匹配整数
        public static readonly string NO_NEGATIVE_INTEGERS = @"^[1-9]\d*|0$";　 //匹配非负整数（正整数 + 0）
        public static readonly string NO_POSITIVE_INTEGERS = @"^-[1-9]\d*|0$";　　 //匹配非正整数（负整数 + 0）
        public static readonly string POSITIVE_FLOATS = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$";　　 //匹配正浮点数
        public static readonly string NEGATIVE_FLOATS = @"^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$";　 //匹配负浮点数
        public static readonly string FLOATS = @"^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$";　 //匹配浮点数
        public static readonly string NO_NEGATIVE_FLOATS = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$";　　 //匹配非负浮点数（正浮点数 + 0）
        public static readonly string NO_POSITIVE_FLOATS = @"^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$";　　//匹配非正浮点数（负浮点数 + 0）
    }
}