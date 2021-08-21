using System.ComponentModel;

namespace Freesql.Template.Enum
{
    /// <summary>
    /// 会员类型
    /// </summary>
    public enum EnumMemMemberType
    {
        /// <summary>
        /// 会员
        /// </summary>
        [Description("会员")]
        User = 1,
        /// <summary>
        /// 测试用户
        /// </summary>
        [Description("测试用户")]
        Test = 2,
        /// <summary>
        /// 3
        /// </summary>
        [Description("3")]
        Reseller = 3,
        /// <summary>
        /// 4
        /// </summary>
        [Description("4")]
        Seller = 4,
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum EnumMemSexType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Men = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Women = 2,
    }

    /// <summary>
    /// 收藏类型
    /// </summary>
    public enum EnumMemFavType
    {
        /// <summary>
        /// 商品收藏
        /// </summary>
        [Description("商品收藏")]
        ProductFav = 1,

        /// <summary>
        /// 品牌收藏
        /// </summary>
        [Description("品牌收藏")]
        BrandFav = 2,

        /// <summary>
        /// 店铺收藏
        /// </summary>
        [Description("店铺收藏")]
        ShopFav = 3,
    }

}
