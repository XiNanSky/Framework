/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-09                    *
* Nowtime:           09:21:46                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    using System.Text;

    public class USDKPayInfo
    {
        public string uid;//用户ID,游戏必须使用登录时西瓜服务器返回的uid
        public string productId;//产品ID
        public string productName;//产品名称
        public string productDesc;//产品描述
        public string productUnit;//产品单位：比如元宝或金币等
        public int productUnitPrice;//产品单价,单位元
        public int productQuantity;//产品数量
        public int totalAmount;//总金额,单位元
        public int payAmount;//付费金额,单位元
        public string currencyName;//货币名称(人民币：CNY)
        public string roleId;//角色ID
        public string roleName;//角色名
        public string roleLevel;//角色等级
        public string roleVipLevel;//角色的VIP等级
        public string serverId;//服ID
                               //public string serverName;//服名称
        public string zoneId;//区ID
        public string partyName;//帮会名称
        public string virtualCurrencyBalance;//虚拟货币余额
        public string customInfo;//扩展字段，订单支付成功后，透传给游戏
        public string gameTradeNo;//游戏订单ID，支付成功后，透传给游戏
        public string gameCallBackURL;//支付回调地址，如果为空，则后台配置的回调地址
        public string additionalParams;// 扩展参数
        public string transaction_id;//交易号目前只有iOS AppStore pay使用
        public string pay_type;// 支付方式

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(string.Format("uid={0}&", uid));
            buffer.Append(string.Format("productId={0}&", productId));
            buffer.Append(string.Format("productName={0}&", productName));
            buffer.Append(string.Format("productDesc={0}&", productDesc));
            buffer.Append(string.Format("productUnit={0}&", productUnit));
            buffer.Append(string.Format("productUnitPrice={0}&", productUnitPrice));
            buffer.Append(string.Format("productQuantity={0}&", productQuantity));
            buffer.Append(string.Format("totalAmount={0}&", totalAmount));
            buffer.Append(string.Format("payAmount={0}&", payAmount));
            buffer.Append(string.Format("currencyName={0}&", currencyName));
            buffer.Append(string.Format("roleId={0}&", roleId));
            buffer.Append(string.Format("roleName={0}&", roleName));
            buffer.Append(string.Format("roleLevel={0}&", roleLevel));
            buffer.Append(string.Format("roleVipLevel={0}&", roleVipLevel));
            buffer.Append(string.Format("serverId={0}&", serverId));
            //buffer.Append(string.Format("serverName={0}&", serverName));
            buffer.Append(string.Format("zoneId={0}&", zoneId));
            buffer.Append(string.Format("partyName={0}&", partyName));
            buffer.Append(string.Format("virtualCurrencyBalance={0}&", virtualCurrencyBalance));
            buffer.Append(string.Format("customInfo={0}&", customInfo));
            buffer.Append(string.Format("gameTradeNo={0}&", gameTradeNo));
            buffer.Append(string.Format("gameCallBackURL={0}&", gameCallBackURL));
            buffer.Append(string.Format("additionalParams={0}&", additionalParams));
            buffer.Append(string.Format("transaction_id={0}&", transaction_id));
            buffer.Append(string.Format("pay_type={0}", pay_type));

            return buffer.ToString();
        }
    }
}