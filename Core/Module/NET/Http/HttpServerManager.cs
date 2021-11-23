/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Net.Http 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-12
* Time:             16:05:45
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

using Framework;
using System;

[Serializable]
public struct BaseRet<T>
{
    public int statusCode;
    public bool succeeded;
    public string errors;
    public string extras;
    public long timestamp;
    public T data;
}

[Serializable]
public struct BaseRet
{
    public int statusCode;
    public bool succeeded;
    public string errors;
    public string extras;
    public long timestamp;
    //public object data;
}

/* 1-65535 端口分类
* 1001-2000 配置类 BaseConfig
* 2001-3000 游戏中
* 3001-4000 玩家信息
* 4001-5000 GM工具
* 5001-6000 公告活动运营
* 长连接
* 101 - 200 配置类
* 201 - 300 游戏中
* 301 - 400 玩家信息
* 401 - 500 GM工具
* 501 - 600 活动运营
*/

/// <summary> Http端口类 </summary>
public class HttpServerManager
{
    public static string DevAddr { get; private set; }

    public const string RootName = "/api/";

    public HttpServerManager(string addres)
    {
        DevAddr = addres;
    }

    public static string GetUrlRoot(int cmd)
    {
        return string.Concat(Url.HTTP, DevAddr, RootName, cmd);
    }

    #region 1001 - 1100 配置表

    /// <summary> 枚举表 </summary>
    public const int GETENUMCONFIGSCMD = 1000;

    /// <summary> 枚举配置参数 </summary>
    public const int GETENUMCONFIGSVALUECMD = 1001;

    /// <summary> 配方 </summary>
    public const int GETFORMULACONFIGCMD = 1002;

    /// <summary> 配方类型 </summary>
    public const int GET_FORMULA_TYPE_CONFIG_CMD = 1003;

    /// <summary> 材料 </summary>
    public const int GET_MATERIALS_CONFIG_CMD = 1004;

    /// <summary> 生产随机周期表 </summary>
    public const int GET_PRODUCING_RANDOM_PERIODIC_TABLE_CONFIG_CMD = 1005;

    /// <summary> 商铺 </summary>
    public const int GET_SHOP_CONFIG_CMD = 1006;

    /// <summary> 商品品质属性修正 </summary>
    public const int GET_PRODUCT_QUALITY_CONFIGCMD = 1007;

    /// <summary> 生产里程碑 </summary>
    public const int GET_PRODUCE_ABILITY_CONFIGS_CMD = 1008;

    /// <summary> 货架 </summary>
    public const int GET_STORAGE_CONFIGS_CMD = 1009;

    /// <summary> 货架等级提升 </summary>
    public const int GET_SHELF_LEVEL_CONFIG_CMD = 1011;

    /// <summary> 已作废合并货架等级提升篮子 </summary>
    public const int GET_MERGE_SHELF_LEVEL_CONFIGCMD = 1012;

    /// <summary> 战斗配置 </summary>
    public const int GET_BATTLE_CONFIG_CMD = 1013;

    /// <summary> 角色升级属性表 </summary>
    public const int GET_CHARACTER_LEVEL_CONFIGS_CMD = 1014;

    /// <summary> 英雄战斗技能表 </summary>
    public const int GET_BATTLE_SKILL_CONFIG_CMD = 1015;

    /// <summary> 英雄被动技能表 </summary>
    public const int GET_HERO_ABILITY_CONFIG_CMD = 1016;

    /// <summary> 角色品质修正 </summary>
    public const int GET_CHARACTER_QUALITY_CONFIG_CMD = 1017;

    /// <summary> 角色升级消耗表 </summary>
    public const int GET_CHARACTER_LEVELCOST_CMD = 1018;

    /// <summary> 角色升阶消耗表 </summary>
    public const int GET_CHARACTER_QUALITYCOST_CMD = 1019;

    /// <summary> 店员被动技能表 </summary>
    public const int GET_WORK_ERABILITY_CONFIG_CMD = 1020;

    /// <summary> 全局配置 </summary>
    public const int GET_GLOBAL_CONFIG_CMD = 1021;

    /// <summary> 玩家等级 </summary>
    public const int GET_PLAYER_LEVEL_CONFIG_CMD = 1022;

    /// <summary> 生产序列配置 </summary>
    public const int GET_PRODUCING_LINE_CONFIG_CMD = 1023;

    /// <summary> 职业表 </summary>
    public const int GET_OCCUPATION_CONFIG_CMD = 1024;

    /// <summary> 黑市商人配置等级 </summary>
    public const int GET_SHOPER_LEVEL_CONFIG_CMD = 1025;

    /// <summary> 市场解锁栏位配置 </summary>
    public const int GET_MARKETSLOT_CONFIG_CMD = 1026;

    /// <summary> 市场等级 </summary>
    public const int GET_MARKET_LEVELS_CMD = 1027;

    /// <summary> 角色基础属性表 </summary>
    public const int GET_CHARACTERS_CMD = 1028;

    public const int GET_TIME_TO_GEM_CMD = 1030;

    /// <summary> 配方表扩展 </summary>
    public const int GET_FORMULA_EXTEND_CMD = 1100;

    public const int GET_PRODUCE_ABILITY_EXTEND_CMD = 1102;

    #endregion

    #region 2001 - 2050 出征场景

    /// <summary> 获取出征场景 英雄信息  </summary>
    public const int GET_ALL_HERO_DATA_CMD = 2001;

    /// <summary> 获取出征场景 恢复面板  </summary>
    public const int POST_HERO_RECOVER_DATA_CMD = 2002;

    /// <summary> 查询单个英雄装备接口  </summary>
    public const int GET_HERO_INFO_CMD = 2003;

    /// <summary> 获取出征场景 物品信息 </summary>
    public const int GET_ALL_GOODS_DATA_CMD = 2004;

    /// <summary> 商品详情 锁定 </summary>
    public const int POST_LOCK_GOODS_CMD = 2005;

    /// <summary> 商品详情 删除 </summary>
    public const int POST_DEL_GOODS_CMD = 2006;

    /// <summary> 查询单个英雄信息 名字，职业，战力等 入参 HEROID </summary>
    public const int POST_GET_HERO_INFO_CMD = 2007;

    public const int GET_MY_WORKERS_CMD = 2009;

    /// <summary> 查询单个英雄属性 生命，攻击，防御，战斗 入参 HEROID </summary>
    public const int POST_GET_HERO_ATTRIBUTE_CMD = 2010;

    /// <summary> 查询单个技能面板 HEROID </summary>
    public const int POST_GET_HERO_SKILL_CMD = 2014;

    #endregion

    #region 2051 - 2100 客户管理

    /// <summary> 获取客户列表 </summary>
    public const int POST_CUSTOMER_LIST_CMD = 2051;

    /// <summary> 创建客户 </summary>
    public const int POST_CUSTOMER_CREATE_CMD = 2052;

    /// <summary> 清空所有顾客 </summary>
    public const int POST_CUSTOMER_CLEAR_ALL_CMD = 2057;

    /// <summary> 顾客聊天 </summary>
    public const int POST_CUSTOMER_CHATING_CMD = 2053;

    /// <summary> 顾客交易 交互管理接口 </summary>
    public const int POST_CUSTOMER_MAMANGER_BARGAIN_CMD = 2058;

    #endregion

    #region 2101 - 2150 Element

    /// <summary> 查询玩家所有材料 </summary>
    public const int GET_PLAYER_ALL_ELEMENT_CMD = 2101;

    /// <summary> 查询所有材料 </summary>
    public const int GET_ALL_ELEMENT_DATA_CMD = 2102;

    /// <summary> 获取我的某一类材料数量 </summary>
    public const int GET_MYSELF_ELEMENTS_CMD = 2103;

    /// <summary> 更新我的材料,可加可减 </summary>
    public const int POST_CHANGE_ELEMENT_CMD = 2104;

    #endregion

    #region 2201 - 2250 Process

    /// <summary> 读取我的所有进度信息 缺少是否完成 </summary>
    public const int GET_LOAD_MY_PROCESS_CMD = 2201;

    /// <summary> 检查某个进度是否完成 判断依据：存在进度并且服务器时间 </summary>
    public const int GET_CHECK_MY_PROCESS_CMD = 2202;

    /// <summary> 确认我的过程 </summary>
    public const int POST_COMFIRM_MY_PROCESS_CMD = 2203;

    /// <summary> 添加一个商品队列 </summary>
    public const int POST_ADD_MYPROCESS_CMD = 2204;

    /// <summary> 获取所有我的生产数据，isOnStore 是否 是否在生产队列 </summary>
    public const int GET_MY_PRODUCTS_CMD = 2205;
    public const int POST_UNLOCK_PRODUCE_LINE_CMD = 2206;

    public const int POST_EXPAND_PROCESS_CMD = 2208;

    #endregion

    #region 2151 - 2200 交易市场

    /// <summary> 查询市场商品 </summary> 查询 获取商场已寄售商品信息
    public const int POST_CHECK_MARKET_SHOP_CMD = 2151;

    /// <summary> 创建出售订单确定 </summary> 寄售商品 出售订单
    public const int POST_CONSIGNMENT_SALES_CMD = 2152;

    /// <summary> 我的市场 </summary>
    public const int POST_MY_MARKET_CMD = 2153;

    /// <summary> 购买面板 显示两个信息 </summary>
    public const int GET_MY_BUY_PANEL_CMD = 2154;

    /// <summary> 买商品 </summary> 单个购买商品
    public const int POST_BUY_GOODS_CMD = 2155;

    /// <summary> 查询最小价格 </summary> 获取当前商品数量 最小钻石金额 最小金币金额
    public const int GET_CHECK_MIN_PRICE_CMD = 2156;

    /// <summary> 取消订单 </summary>
    public const int POST_CANEL_ORDER_CMD = 2157;

    /// <summary> 出售商品 </summary>
    public const int POST_SELL_QUICK_GOODS_CMD = 2158;

    /// <summary> 创建购买订单确定 </summary>
    public const int POST_CONSIGNMENT_BUY_CMD = 2161;

    /// <summary> 获取快速出售界面的订单信息 </summary>
    public const int POST_GET_QUICK_SELL_GOODS_CMD = 2162;

    /// <summary> 查询最大价格 </summary> 获取当前商品数量 最小钻石金额 最小金币金额
    public const int GET_CHECK_MAX_PRICE_CMD = 2163;

    /// <summary> 查询我的订单 </summary>
    public const int GET_GET_MY_ORDER_CMD = 2164;

    /// <summary> 黑市 我的订单 收款 </summary>
    public const int GET_MY_ORDER_PRICE_CMD = 2165;

    /// <summary> 黑市 我的订单 收货 </summary>
    public const int GET_MY_ORDER_GOODS_CMD = 2166;

    /// <summary> 黑市 解锁交易栏位 </summary>
    public const int GET_MY_MARKET_SOLT_NUM_CMD = 2167;

    ///// <summary> 卖商品 </summary> 单个购买商品
    //public const int POST_BUY_GOODS_CMD = 2155;

    // public const int POST_BUILD_SHOP_CMD = 2251;
    // public const int POST_UPGRADE_SHOP_CMD = 2252;
    // public const int POST_BUILD_FURNITURE_CMD = 2253;
    // public const int POST_UPGRADE_FURNITURE_CMD = 2254;
    public const int POST_BUILD_OR_UPGRADE_SHOP_CMD = 2255;
    public const int POST_BUILD_OR_UPGRADE_FURNITURE_CMD = 2256;
    public const int POST_CONFIRM_FURNITURE_BUILT_OR_UPGRADED_CMD = 2261;
    public const int GET_MY_SHOPS_CMD = 2254;
    public const int GET_MY_FUNITURES_CMD = 2257;

    public const int POST_INSTANT_BUILD_OR_UPGRADE_CMD = 2262;

    public const int GET_MY_FORMULAS_CMD = 2301;
    #endregion

    #region 3001 - 3050 玩家账户

    /// <summary> 获取当前游戏账户信息 </summary>
    public const int GET_OLD_PLAYER_DATA_CMD = 3001;

    /// <summary> 获取玩家数据 </summary>
    public const int GET_PLAYER_DATA_CMD = 3002;

    #endregion

    #region 3051 - 3100 玩家登录，玩家版本，服务器地址

    /// <summary> 玩家设备登录兑换Token </summary>
    public const int POST_LOGIN_CMD = 3051;

    /// <summary> 设置服务器版本号 </summary>
    public const int POST_SERVER_VERSION_CMD = 3502;

    /// <summary> 获取服务器版本号 </summary>
    public const int GET_SERVER_VERSION_CMD = 3503;

    #endregion

    #region 4001 - 5000 GM

    /// <summary> 增加材料 </summary>
    public const int POST_GM_ADD_MATERIALS_CMD = 4001;

    /// <summary> 增加商品 </summary>
    public const int POST_GM_ADD_GOODS_CMD = 4002;

    /// <summary> 修改英雄  </summary>
    public const int POST_GM_CHANGE_HERO_CMD = 4003;

    #endregion

}
