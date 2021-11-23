/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-09                    *
* Nowtime:           09:23:03                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    using System.Text;

    public class USDKRoleInfo
    {
        public string uid;//用户ID,游戏必须使用登录时西瓜服务器返回的uid
        public string roleId;//角色ID
        public string roleType;//角色类型
        public string roleLevel;//角色等级
        public string roleVipLevel;//角色Vip等级
        public string serverId;//服Id
        public string zoneId;//区ID
        public string roleName;//角色ID
        public string serverName;//服名称
        public string zoneName;//区名称
        public string partyName;//帮会名称
        public string gender;//性别
        public string balance;//角色账户余额
        public string roleCreateTime;//角色创建时间（Unix时间戳，单位秒），如：1461722392，UC 渠道要求

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(string.Format("uid={0}&", uid));
            buffer.Append(string.Format("roleId={0}&", roleId));
            buffer.Append(string.Format("roleType={0}&", roleType));
            buffer.Append(string.Format("roleLevel={0}&", roleLevel));
            buffer.Append(string.Format("roleVipLevel={0}&", roleVipLevel));
            buffer.Append(string.Format("serverId={0}&", serverId));
            buffer.Append(string.Format("zoneId={0}&", zoneId));
            buffer.Append(string.Format("roleName={0}&", roleName));
            buffer.Append(string.Format("serverName={0}&", serverName));
            buffer.Append(string.Format("zoneName={0}&", zoneName));
            buffer.Append(string.Format("partyName={0}&", partyName));
            buffer.Append(string.Format("gender={0}&", gender));
            buffer.Append(string.Format("balance={0}&", balance));
            buffer.Append(string.Format("roleCreateTime={0}", roleCreateTime));

            return buffer.ToString();
        }
    }
}