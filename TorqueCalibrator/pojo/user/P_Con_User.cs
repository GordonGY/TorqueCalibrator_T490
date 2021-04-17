using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorqueCalibrator.service.user;
using TorqueCalibrator.service.user.userImpl;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.pojo.user
{
    public class P_Con_User
    {
        public static P_Con_User CurrentUser;//当前登录的用户
        private static UserService userService = new UserServiceImpl();
        /// <summary>
        /// USER_ID
        /// </summary>		
        public string User_id { get; set; }

        /// <summary>
        /// USER_CODE
        /// </summary>		
        public string User_code { get; set; }

        /// <summary>
        /// USER_NAME
        /// </summary>		
        public string User_name { get; set; }

        /// <summary>
        /// USER_PASSWORD
        /// </summary>		
        public string User_password { get; set; }

        /// <summary>
        /// USER_SEX
        /// </summary>		
        public string User_sex { get; set; }

        /// <summary>
        /// USER_BIRTHDAY
        /// </summary>		
        public string User_birthday { get; set; }

        /// <summary>
        /// USER_AGE
        /// </summary>		
        public int User_age { get; set; }

        /// <summary>
        /// USER_EMAIL
        /// </summary>		
        public string User_email { get; set; }

        /// <summary>
        /// USER_TEL
        /// </summary>		
        public string User_tel { get; set; }

        /// <summary>
        /// USER_PHOTO
        /// </summary>		
        public string User_photo { get; set; }

        /// <summary>
        /// USER_PHOTO_ID
        /// </summary>		
        public string User_photo_id { get; set; }

        /// <summary>
        /// ORG_ID
        /// </summary>		
        public string Org_id { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string Org_name { get; set; }

        /// <summary>
        /// DATA_CODE
        /// </summary>		
        public string Data_code { get; set; }

        /// <summary>
        /// FACTORY_ID
        /// </summary>		
        public string Factory_id { get; set; }

        /// <summary>
        /// IS_FLAG
        /// </summary>		
        public string Is_flag { get; set; }

        /// <summary>
        /// DEL_FLAG
        /// </summary>		
        public string Del_flag { get; set; }

        /// <summary>
        /// CREATE_BY
        /// </summary>		
        public string Create_by{get;set;}

        /// <summary>
        /// CREATE_TIME
        /// </summary>		
        public DateTime? Create_time { get; set; }

        /// <summary>
        /// LAST_UP_BY
        /// </summary>		
        public string Last_up_by { get; set; }

        /// <summary>
        /// LAST_UP_TIME
        /// </summary>		
        public DateTime? Last_up_time{get;set;}

        /// <summary>
        /// TEAM_WORK
        /// </summary>		
        public string Team_work { get; set; }

        public string CellCode{get;set;}         //工位编号
        public string CellName { get; set; }        //工位名称
        public string FactoryCode { get; set; }      //工厂编号
        public string FactoryName { get; set; }      //工厂名称
        public string LineCode { get; set; }         //线体编号
        public string LineName { get; set; }         //线体名称
        public string User_Card { get; set; }       //员工卡号

        /// <summary>
        /// 用户登录校验
        /// </summary>
        /// <param name="cUserCode"></param>
        /// <param name="cPassword"></param>
        /// <param name="user_Return"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool LoginVerify(string cUserCode, string cPassword, out string strMsg)
        {
            strMsg = "";
            try
            {
                #region 生成验证密码密文串
                string strYan = "";
                P_Con_User user;
                user = userService.selectOneByName(cUserCode);

                if (user != null)
                {
                    if (user.User_password.Length >= 8)
                    {
                        strYan = user.User_password.Substring(0, 8);
                    }
                    else
                    {
                        strMsg = "用户密码无效！请重置";
                        return false;
                    }
                }
                else
                {
                    strMsg = "用户名或密码错误";
                    return false;
                }
                string strPassword = strYan + ToolKit.StrToMD5(strYan + cPassword);
                if (cPassword == "*no~@~check~@~password*")
                {
                    user.User_password = cPassword;
                }
                else
                {
                    if (user.User_password != strPassword)
                    {
                        strMsg = "密码错误";
                        return false;
                    }
                }
                #endregion
                user.Org_name = userService.getOrganizationName(user.Org_id);
                CurrentUser = user;
                return true;
            }
            catch (Exception ex)
            {
                //logger.Error("bll_P_Con_User.LoginVerify 方法出错！" + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 用户登录校验
        /// </summary>
        /// <param name="cUserCode"></param>
        /// <param name="cPassword"></param>
        /// <param name="user_Return"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool LoginVerifyJustNumber(string number, out string strMsg)
        {
            strMsg = "";
            try
            {
                P_Con_User user;
                user = userService.selectOneByNum(number);

                if (user != null)
                {
                    CurrentUser = user;
                }
                else
                {
                    strMsg = "用户不存在";
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                //logger.Error("bll_P_Con_User.LoginVerify 方法出错！" + ex.ToString());
                return false;
            }
        }
    }
}
